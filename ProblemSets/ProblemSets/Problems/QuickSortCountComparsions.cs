using System;
using System.ComponentModel.Composition;
using System.Linq;
using ProblemSets.ComputerScience;
using ProblemSets.Services;

namespace ProblemSets.Problems
{
	[Export]
	public class QuickSortCountComparsions
	{
		[Import] private QuickSort quickSort;

		public enum PivotStrategy
		{
			First,
			Last,
			Median
		}

		public void Go()
		{
			var arr = ArrayHelper.CreateRandomArr(100, new Random()).Select(i => (ulong)i).ToArray();

			foreach (PivotStrategy strategy in Enum.GetValues(typeof(PivotStrategy)))
			{
				var comparsionsCount = 0;

				QuickSort(arr, strategy, ref comparsionsCount);

				Console.WriteLine(new { comparsionsCount, strategy });
			}
		}

		private void QuickSort(ulong[] array, PivotStrategy strategy, ref int comparsionsCount)
		{
			var copy = array.ToArray();

			QuickSort(copy, 0, copy.Length - 1, strategy, ref comparsionsCount);
		}

		private void QuickSort(ulong[] array, int start, int end, PivotStrategy strategy, ref int comparsionsCount)
		{
			if (start >= end) return;

			var pivotIndex =
				strategy == PivotStrategy.First ? start
				: strategy == PivotStrategy.Last ? end
				: quickSort.CalcMedian(array, start, end);

			pivotIndex = quickSort.Partition(array, start, end, pivotIndex);

			comparsionsCount += end - start;

			QuickSort(array, start, pivotIndex - 1, strategy, ref comparsionsCount);

			QuickSort(array, pivotIndex + 1, end, strategy, ref comparsionsCount);
		}
	}
}