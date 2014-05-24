using System;
using System.ComponentModel.Composition;
using ProblemSets.Services;

namespace ProblemSets.ComputerScience
{
	[Export]
	public class QuickSort
	{
		public void Go()
		{
			var arr = new ulong[] {7, 4, 6, 1, 8, 2, 5, 3, 9, 0};
			DoQuickSort(arr);
			Console.WriteLine(", ".Join(arr));
		}

		private void DoQuickSort(ulong[] arr)
		{
			if (arr.Length <= 1) return;
			Console.WriteLine(", ".Join(arr));
			DoQuickSort(arr, 0, arr.Length - 1);
		}

		private void DoQuickSort(ulong[] arr, int start, int end)
		{
			if (end - start <= 0) return;

			var pivotIndex = start + (end - start) / 2;

			Console.WriteLine("Pivot = " + arr[pivotIndex]);

			pivotIndex = Partition(arr, start, end, pivotIndex);

			Console.WriteLine(", ".Join(arr));

			DoQuickSort(arr, start, pivotIndex - 1);
			DoQuickSort(arr, pivotIndex + 1, end);
		}

		public int CalcMedian(ulong[] array, int start, int end)
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

		public int Partition(ulong[] array, int start, int end, int pivotIndex)
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