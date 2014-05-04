using System;
using System.ComponentModel.Composition;
using System.Linq;

namespace ProblemSets.ComputerScience
{
	[Export]
	public class FindArrayInversionsCount
	{
		// Task: given array of distinct integers, find a number of inversions
		// Inversion - a pair of (i, j) : i < j & array[i] > array[j]

		public void Go()
		{
			var arr = new[] {1, 2, 20, 8, 9, 10};

			Console.WriteLine(arr.Length);

			var brute = CalcInversionsCountBrute(arr);
			var inversions = CalcInversionsCount(arr);
			Console.WriteLine(new { inversions, brute });
		}

		public ulong CalcInversionsCountBrute(int[] array)
		{
			var inversions = 0ul;

			for (var i = 0; i < array.Length - 1; i++)
				for (var j = i + 1; j < array.Length; j++)
					if (array[j] < array[i])
					{
						inversions++;
//						Console.WriteLine("({0}, {1})", array[i], array[j]);
					}

			return inversions;
		}

		public ulong CalcInversionsCount(int[] array)
		{
			var sortedArray = array.ToArray();
			var calculatedInversions = new ulong[array.Length];

			for (var size = 1; size < array.Length; size *= 2)
			{
				SortAndCountInversions(array, sortedArray, size, calculatedInversions);

				var tmp = sortedArray;
				sortedArray = array;
				array = tmp;
			}

			return calculatedInversions[0];
		}

		private void SortAndCountInversions(int[] array, int[] sortedArray, int size, ulong[] calculatedInversions)
		{
			for (var i = 0; i < array.Length; i += 2 * size)
			{
				if (i + size >= array.Length)
				{
					for (var ii = i; ii < array.Length; ii++)
						sortedArray[ii] = array[ii];

					continue;
				}

				var leftInversions = calculatedInversions[i];
				
				var rightInversions = calculatedInversions[i + size];

				var splitInversions = MergeAndCountInversions(
					array,
					i, size,
					i + size, Math.Min(size, array.Length - i - size),
					sortedArray,
					i);

				calculatedInversions[i] = leftInversions + rightInversions + splitInversions;
			}
		}

		public ulong MergeAndCountInversions(
			int[] source, 
			int start1, int len1, 
			int start2, int len2,
			int[] sortedArray,
			int destStart)
		{
			var inversions = 0ul;

			var i = 0;
			var j = 0;

			for (var dest = 0; dest < len1 + len2 && i < len1 && j < len2; dest++)
			{
				var e1 = source[start1 + i];
				var e2 = source[start2 + j];

				if (e1 <= e2)
				{
					sortedArray[destStart + dest] = e1;
					i++;
				}
				else
				{
					sortedArray[destStart + dest] = e2;
					j++;

					inversions += (ulong) (len1 - i);

					// Found inversions:
					// Index i = [start1 + i, ..., start1 + i + len1 - i - 1 = start1 + len1 - 1]
					// Index j = start2 + j

//					for (var k = 0; k < len1 - i; k++)
//						Console.WriteLine("({0}, {1})", source[start1 + i + k], e2);
				}
			}

			if (i == len1)
			{
				for (; j < len2; j++)
					sortedArray[destStart + i + j] = source[start2 + j];
			}
			else if (j == len2)
			{
				for (; i < len1; i++)
					sortedArray[destStart + i + j] = source[start1 + i];
			}
			
			return inversions;
		}
	}
}