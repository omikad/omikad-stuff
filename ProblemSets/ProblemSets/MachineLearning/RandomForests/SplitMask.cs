using ProblemSets.ComputerScience.DataTypes;

namespace ProblemSets.MachineLearning.RandomForests
{
	public struct SplitMask
	{
		public readonly BitArrayX Mask;
		public readonly double Entropy;
		public readonly int Size;

		public SplitMask(BitArrayX mask, double entropy, int size)
		{
			Mask = mask;
			Entropy = entropy;
			Size = size;
		}
	}
}