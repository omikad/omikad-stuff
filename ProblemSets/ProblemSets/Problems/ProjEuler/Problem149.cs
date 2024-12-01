using System;
using System.ComponentModel.Composition;
using ProblemSets.Services;

namespace ProblemSets.Problems.ProjEuler
{
	[Export]
	public class Problem149
	{
		private const long len = 2000;

		public void Go()
		{
			var arr = LaggedFibonacciGenerator();
			arr.SubArray(0, 5, 0, 5).Print();
			Console.WriteLine(new { s10 = Get(arr, 10) });
			Console.WriteLine(new { s100 = Get(arr, 100) });

			var max = long.MinValue;

			for (var i = 0; i < len; i++) max = Math.Max(max, Solution_Kadane(arr, i, 0, 0, 1, len));
			for (var i = 0; i < len; i++) max = Math.Max(max, Solution_Kadane(arr, 0, i, 1, 0, len));
			for (var i = 0; i < len; i++) max = Math.Max(max, Solution_Kadane(arr, i, 0, 1, 1, len - i));
			for (var i = 0; i < len; i++) max = Math.Max(max, Solution_Kadane(arr, 0, i, 1, -1, i));

			Console.WriteLine(max);
		}

		private static long Solution_Kadane(long[,] arr, long x, long y, long dx, long dy, long length)
		{
			var max_so_far = arr[x, y];
			var max_ending_here = arr[x, y];

			for (var i = 1; i < length; i++)
			{
				x += dx;
				y += dy;

				if (max_ending_here < 0)
					max_ending_here = arr[x, y];
				else
					max_ending_here += arr[x, y];

				if (max_ending_here >= max_so_far)
					max_so_far = max_ending_here;
			}

			return max_so_far;
		}

		private static long[,] LaggedFibonacciGenerator()
		{
			var result = new long[len, len];

			for (var k = 1L; k <= 55; k++)
				result[0, k - 1] = (100003 - 200003 * k + 300007 * k * k * k) % 1000000 - 500000;

			for (var k = 56L; k <= len * len; k++)
			{
				var sk24 = Get(result, k - 24);
				var sk55 = Get(result, k - 55);
				var sk = (sk24 + sk55 + 1000000) % 1000000 - 500000;
				Set(result, k, sk);
			}

			return result;
		}

		private static long Get(long[,] arr, long k)
		{
			k--;
			return arr[k / len, k % len];
		}

		private static void Set(long[,] arr, long k, long val)
		{
			k--;
			arr[k / len, k % len] = val;
		}
	}
}