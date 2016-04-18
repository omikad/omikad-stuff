using System;
using System.Collections.Generic;
using System.Linq;
using ProblemSets.ComputerScience.DataTypes;
using ProblemSets.Services;

namespace ProblemSets.MachineLearning.RandomForests
{
	/*
		In ID3 algorithm, to split node one can use entropy H or information gain IG. In case of H: if there is
		an attribute x, which we see in input data only once, then for such attribute entropy will be minimal, equals to 0. 
		So, ID3 will eagerily split node using x. But, such rare attribute is probably useless, since all attributes 
		with big impact should be presented in input data more often. That's why, it is better to use IG.
	*/

	public class ID3LearnerInt
	{
		private readonly int[][] input;
		private readonly int[] output;
		private readonly int inputFactorsCount;
		private readonly int outputFactorsCount;
		private readonly BitArrayX[] factorPresentMasks;
		private readonly BitArrayX[] outputFactorMasks;

		public DecisionNode Root { get; private set; }

		public ID3LearnerInt(int[][] input, int[] output, int inputFactorsCount, int outputFactorsCount)
		{
			this.input = input;
			this.output = output;
			this.inputFactorsCount = inputFactorsCount;
			this.outputFactorsCount = outputFactorsCount;

			factorPresentMasks = new BitArrayX[inputFactorsCount];
			for (var inputFactor = 0; inputFactor < inputFactorsCount; inputFactor++)
			{
				var presentMask = new BitArrayX(input.Length);
				for (var i = 0; i < input.Length; i++)
					presentMask[i] = input[i].Contains(inputFactor);
				factorPresentMasks[inputFactor] = presentMask;
			}

			outputFactorMasks = new BitArrayX[outputFactorsCount];
			for (var outputFactor = 0; outputFactor < outputFactorsCount; outputFactor++)
			{
				var outputMask = new BitArrayX(output.Length);
				for (var i = 0; i < output.Length; i++)
					if (output[i] == outputFactor)
						outputMask[i] = true;
				outputFactorMasks[outputFactor] = outputMask;
			}
		}

		public void Learn()
		{
			var wholeOutputMask = new BitArrayX(input.Length, true);

			Root = new DecisionNode
			{
				SplitMask = new SplitMask(
					wholeOutputMask, 
					Entropy(wholeOutputMask),
					input.Length),
				UsedFactors = new BitArrayX(inputFactorsCount)
			};

			var stack = new Stack<DecisionNode>();
			stack.Push(Root);

			while (stack.Count > 0)
			{
				var node = stack.Pop();

				SplitNode(node);

				if (node.IsLeaf) continue;

				stack.Push(node.Present);
				stack.Push(node.Absent);
			}
		}

		private void SplitNode(DecisionNode node)
		{
			var mask = node.SplitMask.Mask;
			var size = node.SplitMask.Size;

			for (var outputFactor = 0; outputFactor < outputFactorsCount; outputFactor++)
			{
				var outputMask = new BitArrayX(mask).And(outputFactorMasks[outputFactor]);

				if (BitArrayX.Equals(mask, outputMask))
				{
					node.IsLeaf = true;
					node.Factor = outputFactor;
					return;
				}

				if (!outputMask.AllZero)
					break;
			}

			var bestInformationGain = double.MinValue;
			var bestFactor = -1;
			var bestPresentSplitMask = default(SplitMask);
			var bestAbsentSplitMask = default(SplitMask);

			for (var inputFactor = 0; inputFactor < inputFactorsCount; inputFactor++)
			{
				if (node.UsedFactors[inputFactor]) continue;

				var factorPresentMask = new BitArrayX(mask).And(factorPresentMasks[inputFactor]);
				var factorAbsentMask = new BitArrayX(mask).AndNot(factorPresentMask);	

				var factorPresentEntropy = Entropy(factorPresentMask);
				var factorAbsentEntropy = Entropy(factorAbsentMask);

				var factorPresentCount = factorPresentMask.CountBitSet();
				var factorAbsentCount = size - factorPresentCount;

				var informationGain = node.SplitMask.Entropy -
				                      (factorPresentEntropy * factorPresentCount / size) -
				                      (factorAbsentEntropy * factorAbsentCount / size);

				if (informationGain > bestInformationGain)
				{
					bestInformationGain = informationGain;
					bestFactor = inputFactor;
					bestPresentSplitMask = new SplitMask(factorPresentMask, factorPresentEntropy, factorPresentCount);
					bestAbsentSplitMask = new SplitMask(factorAbsentMask, factorAbsentEntropy, factorAbsentCount);
				}
			}

			if (bestFactor == -1)
			{
				var counts = new int[outputFactorsCount];
				for (var i = 0; i < output.Length; i++)
					if (mask[i])
						counts[output[i]]++;

				node.IsLeaf = true;
				node.Factor = counts.IndexOfMax();
				return;
			}

			var childUsedFactors = new BitArrayX(node.UsedFactors) {[bestFactor] = true};

			node.Factor = bestFactor;

			node.Present = new DecisionNode
			{
				SplitMask = bestPresentSplitMask,
				Factor = bestFactor,
				Parent = node,
				UsedFactors = childUsedFactors,
			};

			node.Absent = new DecisionNode
			{
				SplitMask = bestAbsentSplitMask,
				Factor = bestFactor,
				Parent = node,
				UsedFactors = childUsedFactors,
			};
		}

		private double Entropy(BitArrayX mask)
		{
			var result = 0d;

			for (var factor = 0; factor < outputFactorsCount; factor++)
			{
				var count = 0d;
				for (var i = 0; i < output.Length; i++)
					if (mask[i] && output[i] == factor)
						count++;

				if (count > 0)
				{
					var px = count / output.Length;
					result -= px * Math.Log(px, 2);
				}
			}

			return result;
		}
	}
}