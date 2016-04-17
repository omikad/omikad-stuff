using System;
using System.Collections.Generic;
using System.Linq;
using ProblemSets.ComputerScience.DataTypes;
using ProblemSets.Services;

namespace ProblemSets.MachineLearning.RandomForests
{
	public class ID3LearnerInt
	{
		private readonly int[][] input;
		private readonly int[] output;
		private readonly int inputFactorsCount;
		private readonly int outputFactorsCount;
		private readonly BitArrayX usedAttributes;
		private readonly BitArrayX[] factorPresentMasks;
		private readonly BitArrayX[] factorAbsentMasks;
		private readonly BitArrayX[] outputFactorMasks;

		public DecisionNode Root { get; private set; }

		private static readonly bool[] isPresentDecisions = {true, false};

		public ID3LearnerInt(int[][] input, int[] output, int inputFactorsCount, int outputFactorsCount)
		{
			this.input = input;
			this.output = output;
			this.inputFactorsCount = inputFactorsCount;
			this.outputFactorsCount = outputFactorsCount;

			usedAttributes = new BitArrayX(inputFactorsCount);

			factorPresentMasks = new BitArrayX[inputFactorsCount];
			factorAbsentMasks = new BitArrayX[inputFactorsCount];
			for (var inputFactor = 0; inputFactor < inputFactorsCount; inputFactor++)
			{
				var presentMask = new BitArrayX(input.Length);
				for (var i = 0; i < input.Length; i++)
					presentMask[i] = input[i].Contains(inputFactor);
				var absentMask = new BitArrayX(presentMask).Not();

				factorPresentMasks[inputFactor] = presentMask;
				factorAbsentMasks[inputFactor] = absentMask;
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
			Root = new DecisionNode
			{
				Mask = new BitArrayX(input.Length, true),
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
			var mask = node.Mask;

			var bestEntropy = double.MaxValue;
			var bestFactor = -1;
			var bestFactorPresent = true;
			BitArrayX bestFactorMask = null;

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

			for (var inputFactor = 0; inputFactor < inputFactorsCount; inputFactor++)
			{
				if (usedAttributes[inputFactor]) continue;

				foreach (var isPresent in isPresentDecisions)
				{
					var factorMask = new BitArrayX(mask).And((isPresent ? factorPresentMasks : factorAbsentMasks)[inputFactor]);

					if (factorMask.AllZero)
						continue;

					var entropy = Entropy(output, outputFactorsCount, factorMask);

					if (entropy < bestEntropy)
					{
						bestEntropy = entropy;
						bestFactor = inputFactor;
						bestFactorPresent = isPresent;
						bestFactorMask = factorMask;
					}
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

			usedAttributes[bestFactor] = true;

			node.Factor = bestFactor;

			var factorNode = new DecisionNode
			{
				Mask = bestFactorMask,
				Factor = bestFactor,
				Parent = node,
			};

			var oppositeFactorNode = new DecisionNode
			{
				Mask = new BitArrayX(mask).And((bestFactorPresent ? factorAbsentMasks : factorPresentMasks)[bestFactor]),
				Factor = bestFactor,
				Parent = node,
			};

			node.Present = bestFactorPresent ? factorNode : oppositeFactorNode;
			node.Absent = bestFactorPresent ? oppositeFactorNode : factorNode;
		}

		public static double Entropy(int[] values, int factorsCount, BitArrayX mask)
		{
			var result = 0d;

			for (var factor = 0; factor < factorsCount; factor++)
			{
				var count = 0d;
				for (var i = 0; i < values.Length; i++)
					if (mask[i] && values[i] == factor)
						count++;

				if (count > 0)
				{
					var px = count / values.Length;
					result -= px * Math.Log(px, 2);
				}
			}

			return result;
		}
	}
}