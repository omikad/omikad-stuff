using System;
using System.ComponentModel.Composition;
using System.Linq;
using ProblemSets.Services;

namespace ProblemSets.Problems
{
	[Export]
	public class QuickSortCountComparsions
	{
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

		private static void QuickSort(ulong[] array, PivotStrategy strategy, ref int comparsionsCount)
		{
			var copy = array.ToArray();

			QuickSort(copy, 0, copy.Length - 1, strategy, ref comparsionsCount);
		}

		private static void QuickSort(ulong[] array, int start, int end, PivotStrategy strategy, ref int comparsionsCount)
		{
			if (start >= end) return;

			var pivotIndex =
				strategy == PivotStrategy.First ? start
				: strategy == PivotStrategy.Last ? end
				: CalcMedian(array, start, end);

			pivotIndex = Partition(array, start, end, pivotIndex);

			comparsionsCount += end - start;

			QuickSort(array, start, pivotIndex - 1, strategy, ref comparsionsCount);

			QuickSort(array, pivotIndex + 1, end, strategy, ref comparsionsCount);
		}

		private static int CalcMedian(ulong[] array, int start, int end)
		{
			var median = (start + end) / 2;

			var s = array[start];
			var m = array[median];
			var e = array[end];

			return 
				  (s < m && s < e) ? (m < e ? median : end)
				: (m < s && m < e) ? (s < e ? start : end)
				: (m < s ? median : start);
		}

		private static int Partition(ulong[] array, int start, int end, int pivotIndex)
		{
			var pivot = array[pivotIndex];

			array.Swap(start, pivotIndex);

			int i;
			int j;

			for (i = j = start + 1; j <= end; j++)
			{	
				var element = array[j];

				if (element < pivot)
				{
					array.Swap(i, j);
					i++;
				}
			}

			array.Swap(start, i - 1);

			return i - 1;
		}
	}
}