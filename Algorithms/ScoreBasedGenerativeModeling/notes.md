Dall-E explaining: https://www.assemblyai.com/blog/how-dall-e-2-actually-works/

Nice explanatory article: https://yang-song.github.io/blog/2021/score/

Colab notebook (Pytorch) https://colab.research.google.com/drive/120kYYBOVa1i0TD85RjlEkFjaWDxSFUx3?usp=sharing#scrollTo=XCR6m0HjWGVV

https://github.com/yang-song/score_sde_pytorch

Some math about score and autoencoders: http://www.iro.umontreal.ca/~vincentp/Publications/smdae_techreport.pdf



Original MNIST U-Net:

```
====================================================================================================
Layer (type:depth-idx)                             Output Shape              Param #
====================================================================================================
DataParallel                                       [11, 1, 28, 28]           --
├─ScoreNet: 1-1                                    [11, 1, 28, 28]           --
│    └─Sequential: 2-1                             [11, 256]                 --
│    │    └─GaussianFourierProjection: 3-1         [11, 256]                 (128)
│    │    └─Linear: 3-2                            [11, 256]                 65,792
│    └─Conv2d: 2-2                                 [11, 32, 26, 26]          288
│    └─Dense: 2-3                                  [11, 32, 1, 1]            --
│    │    └─Linear: 3-3                            [11, 32]                  8,224
│    └─GroupNorm: 2-4                              [11, 32, 26, 26]          64
│    └─Conv2d: 2-5                                 [11, 64, 12, 12]          18,432
│    └─Dense: 2-6                                  [11, 64, 1, 1]            --
│    │    └─Linear: 3-4                            [11, 64]                  16,448
│    └─GroupNorm: 2-7                              [11, 64, 12, 12]          128
│    └─Conv2d: 2-8                                 [11, 128, 5, 5]           73,728
│    └─Dense: 2-9                                  [11, 128, 1, 1]           --
│    │    └─Linear: 3-5                            [11, 128]                 32,896
│    └─GroupNorm: 2-10                             [11, 128, 5, 5]           256
│    └─Conv2d: 2-11                                [11, 256, 2, 2]           294,912
│    └─Dense: 2-12                                 [11, 256, 1, 1]           --
│    │    └─Linear: 3-6                            [11, 256]                 65,792
│    └─GroupNorm: 2-13                             [11, 256, 2, 2]           512
│    └─ConvTranspose2d: 2-14                       [11, 128, 5, 5]           294,912
│    └─Dense: 2-15                                 [11, 128, 1, 1]           --
│    │    └─Linear: 3-7                            [11, 128]                 32,896
│    └─GroupNorm: 2-16                             [11, 128, 5, 5]           256
│    └─ConvTranspose2d: 2-17                       [11, 64, 12, 12]          147,456
│    └─Dense: 2-18                                 [11, 64, 1, 1]            --
│    │    └─Linear: 3-8                            [11, 64]                  16,448
│    └─GroupNorm: 2-19                             [11, 64, 12, 12]          128
│    └─ConvTranspose2d: 2-20                       [11, 32, 26, 26]          36,864
│    └─Dense: 2-21                                 [11, 32, 1, 1]            --
│    │    └─Linear: 3-9                            [11, 32]                  8,224
│    └─GroupNorm: 2-22                             [11, 32, 26, 26]          64
│    └─ConvTranspose2d: 2-23                       [11, 1, 28, 28]           577
====================================================================================================
Total params: 1,115,425
Trainable params: 1,115,297
Non-trainable params: 128
Total mult-adds (M): 661.09
====================================================================================================
Input size (MB): 0.03
Forward/backward pass size (MB): 12.34
Params size (MB): 4.46
Estimated Total Size (MB): 16.84
====================================================================================================
```

# model B loss: Epoch 199 / 199 Average Loss: 652.3914737025859

```
class GaussianFourierProjection(nn.Module):
    def __init__(self, embed_dim, scale=30.):
        super().__init__()
        self.W = nn.Parameter(torch.randn(embed_dim // 2) * scale, requires_grad=False)
    def forward(self, x):
        x_proj = x[:, None] * self.W[None, :] * 2 * np.pi
        return torch.cat([torch.sin(x_proj), torch.cos(x_proj)], dim=-1)


class Dense(nn.Module):
    def __init__(self, input_dim, output_dim):
        super().__init__()
        self.dense = nn.Linear(input_dim, output_dim)
    def forward(self, x):
        return self.dense(x)[..., None, None]


# Model1: channels=[32, 64, 128, 256], embed_dim=256, trainable params 1,790,819
class ScoreNet(nn.Module):
    def __init__(self, marginal_prob_std, channels=[96, 192, 256, 384], embed_dim=256):
        super().__init__()

        IMAGE_CHANNELS = 3

        # Gaussian random feature embedding layer for time
        self.embed = nn.Sequential(GaussianFourierProjection(embed_dim=embed_dim),
              nn.Linear(embed_dim, embed_dim))

        # Encoding layers where the resolution decreases
        self.conv1 = nn.Conv2d(IMAGE_CHANNELS, channels[0], 3, stride=1, bias=False)
        self.dense1 = Dense(embed_dim, channels[0])
        self.gnorm1 = nn.GroupNorm(4, num_channels=channels[0])
        self.conv2 = nn.Conv2d(channels[0], channels[1], 4, stride=2, bias=False)
        self.dense2 = Dense(embed_dim, channels[1])
        self.gnorm2 = nn.GroupNorm(32, num_channels=channels[1])
        self.conv3 = nn.Conv2d(channels[1], channels[2], 4, stride=2, bias=False)
        self.dense3 = Dense(embed_dim, channels[2])
        self.gnorm3 = nn.GroupNorm(32, num_channels=channels[2])
        self.conv4 = nn.Conv2d(channels[2], channels[3], 4, stride=2, bias=False)
        self.dense4 = Dense(embed_dim, channels[3])
        self.gnorm4 = nn.GroupNorm(32, num_channels=channels[3])

        # Decoding layers where the resolution increases
        self.tconv4 = nn.ConvTranspose2d(channels[3], channels[2], 4, stride=2, bias=False)
        self.dense5 = Dense(embed_dim, channels[2])
        self.tgnorm4 = nn.GroupNorm(32, num_channels=channels[2])
        self.tconv3 = nn.ConvTranspose2d(channels[2] + channels[2], channels[1], 4, stride=2, bias=False, output_padding=0)
        self.dense6 = Dense(embed_dim, channels[1])
        self.tgnorm3 = nn.GroupNorm(32, num_channels=channels[1])
        self.tconv2 = nn.ConvTranspose2d(channels[1] + channels[1], channels[0], 4, stride=2, bias=False, output_padding=0)
        self.dense7 = Dense(embed_dim, channels[0])
        self.tgnorm2 = nn.GroupNorm(32, num_channels=channels[0])
        self.tconv1 = nn.ConvTranspose2d(channels[0] + channels[0], IMAGE_CHANNELS, 3, stride=1)

        # The swish activation function
        self.act = lambda x: x * torch.sigmoid(x)
        self.marginal_prob_std = marginal_prob_std

    def forward(self, x, t):
        # Obtain the Gaussian random feature embedding for t
        embed = self.act(self.embed(t))
        # Encoding path
        h1 = self.conv1(x)
        ## Incorporate information from t
        h1 += self.dense1(embed)
        ## Group normalization
        h1 = self.gnorm1(h1)
        h1 = self.act(h1)
        h2 = self.conv2(h1)
        h2 += self.dense2(embed)
        h2 = self.gnorm2(h2)
        h2 = self.act(h2)
        h3 = self.conv3(h2)
        h3 += self.dense3(embed)
        h3 = self.gnorm3(h3)
        h3 = self.act(h3)
        h4 = self.conv4(h3)
        h4 += self.dense4(embed)
        h4 = self.gnorm4(h4)
        h4 = self.act(h4)

        # Decoding path
        h = self.tconv4(h4)
        ## Skip connection from the encoding path
        h += self.dense5(embed)
        h = self.tgnorm4(h)
        h = self.act(h)
        h = self.tconv3(torch.cat([h, h3], dim=1))
        h += self.dense6(embed)
        h = self.tgnorm3(h)
        h = self.act(h)
        h = self.tconv2(torch.cat([h, h2], dim=1))
        h += self.dense7(embed)
        h = self.tgnorm2(h)
        h = self.act(h)
        h = self.tconv1(torch.cat([h, h1], dim=1))

        # Normalize output
        h = h / self.marginal_prob_std(t)[:, None, None, None]
        return h
```