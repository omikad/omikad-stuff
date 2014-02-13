﻿using System;
using System.ComponentModel.Composition;
using ProblemSets.Services;

namespace ProblemSets.ComputerScience
{
	[Export]
	public class QuickSort
	{
		public void Go()
		{
			var arr = new[] {7, 4, 6, 1, 8, 2, 5, 3, 9, 0};
			DoQuickSort(arr);
			Console.WriteLine(", ".Join(arr));
		}

		private static void DoQuickSort(int[] arr)
		{
			if (arr.Length <= 1) return;
			Console.WriteLine(", ".Join(arr));
			DoQuickSort(arr, 0, arr.Length - 1);
		}

		private static void DoQuickSort(int[] arr, int start, int end)
		{
			if (end - start <= 0) return;

			var pivotIndex = start + (end - start) / 2;

			Console.WriteLine("Pivot = " + arr[pivotIndex]);

			pivotIndex = Partition(arr, start, end, pivotIndex);

			Console.WriteLine(", ".Join(arr));

			DoQuickSort(arr, start, pivotIndex - 1);
			DoQuickSort(arr, pivotIndex + 1, end);
		}

		private static int Partition(int[] arr, int left, int right, int pivotIndex)
		{
			var pivot = arr[pivotIndex];
			Swap(arr, pivotIndex, right);
			var storeIndex = left;
			for (var i = left; i < right; i++)
				if (arr[i] <= pivot)
				{
					Swap(arr, i, storeIndex);
					storeIndex++;
				}
			Swap(arr, storeIndex, right);
			return storeIndex;
		}

		private static void Swap(int[] arr, int i, int j)
		{
			var tmp = arr[i];
			arr[i] = arr[j];
			arr[j] = tmp;
		}
	}
}