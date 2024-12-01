using System.Collections.Generic;
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

		public DecisionNode Root { get; private set; }

		public ID3LearnerInt(int[][] input, int[] output, int inputFactorsCount, int outputFactorsCount)
		{
			this.input = input;
			this.output = output;
			this.inputFactorsCount = inputFactorsCount;
			this.outputFactorsCount = outputFactorsCount;

			factorPresentMasks = ArrayHelper.CreateArray(inputFactorsCount, () => new BitArrayX(input.Length));
			for (var i = 0; i < input.Length; i++)
				foreach (var factor in input[i])
					factorPresentMasks[factor][i] = true;
		}

		public void Learn()
		{
			var rootMask = new BitArrayX(input.Length, true);

			Root = new DecisionNode
			{
				SubsetMask = new SubsetMask(rootMask, input.Length, output, outputFactorsCount),
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
			var mask = node.SubsetMask.Mask;
			var size = node.SubsetMask.Size;

			if (node.SubsetMask.EntropyZeroFactor >= 0)
			{
				node.IsLeaf = true;
				node.Factor = node.SubsetMask.EntropyZeroFactor;
				return;
			}

			var bestInformationGain = double.MinValue;
			var bestFactor = -1;
			var bestPresentSplitMask = default(SubsetMask);
			var bestAbsentSplitMask = default(SubsetMask);

			for (var inputFactor = 0; inputFactor < inputFactorsCount; inputFactor++)
			{
				if (node.UsedFactors[inputFactor]) continue;

				var factorPresentMask = new BitArrayX(mask).And(factorPresentMasks[inputFactor]);
				var factorAbsentMask = new BitArrayX(mask).AndNot(factorPresentMask);

				var factorPresentCount = factorPresentMask.CountBitSet();
				var factorAbsentCount = size - factorPresentCount;

				var factorPresentSubset = new SubsetMask(factorPresentMask, factorPresentCount, output, outputFactorsCount);
				var factorAbsentSubset = new SubsetMask(factorAbsentMask, factorAbsentCount, output, outputFactorsCount);

				var informationGain = node.SubsetMask.Entropy -
				                      (factorPresentSubset.Entropy * factorPresentCount / size) -
				                      (factorAbsentSubset.Entropy * factorAbsentCount / size);

				if (informationGain > bestInformationGain)
				{
					bestInformationGain = informationGain;
					bestFactor = inputFactor;
					bestPresentSplitMask = factorPresentSubset;
					bestAbsentSplitMask = factorAbsentSubset;
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
				SubsetMask = bestPresentSplitMask,
				Factor = bestFactor,
				Parent = node,
				UsedFactors = childUsedFactors,
			};

			node.Absent = new DecisionNode
			{
				SubsetMask = bestAbsentSplitMask,
				Factor = bestFactor,
				Parent = node,
				UsedFactors = childUsedFactors,
			};
		}
	}
}