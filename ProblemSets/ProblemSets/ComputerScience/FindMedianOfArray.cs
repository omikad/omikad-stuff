using System;
using System.ComponentModel.Composition;
using ProblemSets.Services;

namespace ProblemSets.ComputerScience
{
	[Export]
	public class FindMedianOfArray
	{
		[Import] private QuickSort quickSort;

		public void Go()
		{
			var rnd = new Random();

			for (var i = 0; i < 10; i++)
			{
				var size = rnd.Next(3, 12);
				var arr = ArrayHelper.CreateRandomArr(size, rnd);
				Console.WriteLine(", ".Join(arr));
				Console.WriteLine(ByQuickSort(arr));
				Console.WriteLine();
			}
		}

		private int ByQuickSort(int[] arr)
		{
			DoQuickSort(arr, 0, arr.Length - 1);
			return arr[arr.Length / 2];
		}

		private void DoQuickSort(int[] arr, int start, int end)
		{
			if (end - start <= 0) return;

			var pivotIndex = start + (end - start) / 2;

			pivotIndex = quickSort.Partition(arr, start, end, pivotIndex);

			if (pivotIndex < arr.Length / 2)
			{
				DoQuickSort(arr, pivotIndex + 1, end);
			}
			else if (pivotIndex > arr.Length / 2)
			{
				DoQuickSort(arr, start, pivotIndex - 1);
			}
		}
	}
}
