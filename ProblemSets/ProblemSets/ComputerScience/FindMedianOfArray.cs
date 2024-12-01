using System;
using System.ComponentModel.Composition;
using System.Linq;
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
				var arr = ArrayHelper.CreateRandomArr(size, rnd).Select(e => (ulong)e).ToArray();
				Console.WriteLine(", ".Join(arr));
				Console.WriteLine(ByQuickSort(arr));
				Console.WriteLine();
			}
		}

		private ulong ByQuickSort(ulong[] arr)
		{
			var desire = arr.Length / 2;
			
			DoQuickSort(arr, 0, arr.Length - 1, desire);

			return arr[desire];
		}

		private void DoQuickSort(ulong[] arr, int start, int end, int desire)
		{
			if (end - start <= 0) return;

			var pivotIndex = start + (end - start) / 2;

			pivotIndex = quickSort.Partition(arr, start, end, pivotIndex);

			if (pivotIndex < desire)
			{
				DoQuickSort(arr, pivotIndex + 1, end, arr.Length / 2);
			}
			else if (pivotIndex > desire)
			{
				DoQuickSort(arr, start, pivotIndex - 1, arr.Length / 2);
			}
		}
	}
}
