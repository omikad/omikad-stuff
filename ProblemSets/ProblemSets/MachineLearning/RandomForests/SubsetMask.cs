using System;
using ProblemSets.ComputerScience.DataTypes;

namespace ProblemSets.MachineLearning.RandomForests
{
	public struct SubsetMask
	{
		public readonly BitArrayX Mask;
		public readonly double Entropy;
		public readonly int Size;
		public readonly int EntropyZeroFactor;

		public SubsetMask(BitArrayX mask, int size, int[] output, int outputFactorsCount)
		{
			Mask = mask;
			Size = size;
			Entropy = 0d;
			EntropyZeroFactor = -1;

			for (var factor = 0; factor < outputFactorsCount; factor++)
			{
				var count = 0;
				for (var i = 0; i < output.Length; i++)
					if (mask[i] && output[i] == factor)
						count++;

				if (count == Size)
				{
					Entropy = 0d;
					EntropyZeroFactor = factor;
					return;
				}

				if (count > 0)
				{
					var px = (double)count / Size;
					Entropy -= px * Math.Log(px, 2);
				}
			}
		}
	}
}