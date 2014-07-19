using System;

namespace ProblemSets.ComputerScience.DataTypes
{
	public class BinaryHeap : GenericBinaryHeap<int>
	{
		/// <summary>
		/// Construct <see cref="BinaryHeap"/> as min-heap, with default int comparison
		/// </summary>
		public BinaryHeap()
			: base(DefaultComparison)
		{
		}

		public BinaryHeap(Comparison<int> comparisonDelegate)
			: base(comparisonDelegate)
		{
		}

		private static int DefaultComparison(int x, int y)
		{
			return x.CompareTo(y);
		}
	}
}