using System;
using System.ComponentModel.Composition;
using ProblemSets.Services;

namespace ProblemSets.Problems
{
	[Export]
	public class PoolVolume
	{
		public void Go()
		{
			//				  15+14+ 5+12+13     +12        +6= 77
			Solve_1(new[] { 15, 0, 1, 10, 3, 2, 20, 8, 20, 20, 4, 10, 2, 1 });

			//                 4  3  2  1				= 10
			Solve_1(new[] { 2, 5, 1, 2, 3, 4, 7, 7, 6 });
		}

		private static void Solve_1(int[] arr)
		{
			var left = 0;
			var right = arr.Length - 1;
			var leftMax = arr[left];
			var rightMax = arr[right];
			var volume = 0;

			while (left <= right)
			{
				if (leftMax < rightMax)
				{
					volume += Math.Max(0, leftMax - arr[left]);
					leftMax = Math.Max(leftMax, arr[left]);
					left++;
				}
				else
				{
					volume += Math.Max(0, rightMax - arr[right]);
					rightMax = Math.Max(rightMax, arr[right]);
					right--;
				}
			}
			Console.WriteLine(volume);
		}

		private static void Solve_2(int[] arr)
		{
			Console.WriteLine("arr:  " + ", ".Join(arr));

			var maxIndex = 0;
			
			var max = arr[0];
			var result = 0;
			var current = 0;

			var end = arr.Length;

			for (var j = 0; j < 2; j++)
			{
				for (var i = 0; i < end; i++)
				{
					var x = j == 0 ? arr[i] : arr[arr.Length - i - 1];

					if (max > x)
					{
						current += max - x;
					}
					else if (max < x)
					{
						result += current;
						current = 0;
						max = x;
						maxIndex = i;
					}

					Console.WriteLine(new { result, currentLeft = current });
				}

				Console.WriteLine("Go rtl");

				max = arr[arr.Length - 1];
				end = arr.Length - maxIndex;
				if (j == 0) current = 0;
			}

			result += current;

			Console.WriteLine("Result: " + result);
		}
	}
}
