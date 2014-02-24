using System;
using System.ComponentModel.Composition;
using ProblemSets.Services;

namespace ProblemSets.Problems
{
	[Export]
	public class FindSubArrayToSort
	{
		public void Go()
		{
			// Find the minimum sub-array which:
			// If this sub-array will be sorted
			// Then whole array will become sorted

			Solve(new[] { 1, 2, 4, 7, 10, 11, 7, 12, 6, 7, 16, 18, 19 });
			Solve(new[] { 1, 2, 4, 7, 10, 11, 16, 18, 19 });
			Solve(new[] { 8, 7, 6, 5, 4, 3, 2, 1 });
		}

		private static void Solve(int[] arr)
		{
			// O(n)

			Console.WriteLine(", ".Join(arr));

			var leftI = 0;
			while (leftI < arr.Length - 1 && arr[leftI] <= arr[leftI + 1]) leftI++;

			var min = arr[leftI]; 
			for (var i = leftI + 1; i < arr.Length; i++)
				if (arr[i] < min)
					min = arr[i];

			
			var rightI = arr.Length - 1;
			while (rightI > 0 && arr[rightI] >= arr[rightI - 1]) rightI--;

			var max = arr[rightI];
			for (var i = rightI - 1; i >= 0; i--)
				if (arr[i] > max)
					max = arr[i];
			
			Console.WriteLine(new { firstpeak = arr[leftI], rightmin = min });
			Console.WriteLine(new { lastbottom = arr[rightI], leftmax = max });

			var answerLeft = 0;
			while (arr[answerLeft] < min && answerLeft < arr.Length - 1) answerLeft++;

			var answerRight = arr.Length - 1;
			while (arr[answerRight] > max && answerRight > answerLeft) answerRight--;

			Console.WriteLine(new { answerLeft, answerRight });
			Console.WriteLine();
		}
	}
}
