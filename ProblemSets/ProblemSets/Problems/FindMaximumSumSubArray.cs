using System;
using System.ComponentModel.Composition;
using System.Linq;
using ProblemSets.Services;

namespace ProblemSets.Problems
{
	[Export]
	public class FindMaximumSumSubArray
	{
		public void Go()
		{
			const int size = 10;
			var rnd = new Random();
			var arr = Enumerable.Repeat(0, size).Select(i => rnd.Next(-5, 6)).ToArray();

			Console.WriteLine(", ".Join(arr));
			Console.WriteLine(", ".Join(Solve(arr)));
		}

		public int[] Solution_Kadane(int[] arr)
		{
			var max_so_far = arr[0];
			var max_ending_here = arr[0];

			var begin = 0;
			var begin_tmp = 0;
			var end = 0;

			for (var i = 1; i < arr.Length; i++)
			{
				if (max_ending_here < 0)
				{
					max_ending_here = arr[i];
					begin_tmp = i;
				}
				else max_ending_here += arr[i];

				if (max_ending_here >= max_so_far)
				{
					max_so_far = max_ending_here;
					begin = begin_tmp;
					end = i;
				}
			}
			return arr.Skip(begin).Take(end - begin + 1).ToArray();
		}

		public int[] Solve(int[] arr)
		{
			// O(n) time, O(1) memory

			if (arr.Length == 0) return arr;
			if (arr.Length == 1) return arr[0] > 0 ? arr : new int[0];

			// Tuple: startIndex, countElements, sum
			Tuple<int, int, int> msa = null;			// Best MSA so far
			Tuple<int, int, int> candidate = null;		// Growing candidate to MSA prefix: 3,-2,4,-3,...

			var i = 0;
			while (i < arr.Length && arr[i] <= 0) i++;

			while (i < arr.Length)
			{
				var sumStart = i;
				var sumPlus = 0;
				while (i < arr.Length && arr[i] >= 0) { sumPlus += arr[i]; i++; }

				var minusStart = i;
				var sumMinus = 0;
				while (i < arr.Length && arr[i] < 0) { sumMinus += arr[i]; i++; }

				// Check whether the current +++ sequence is the best
				if (sumPlus > 0 && (msa == null || sumPlus > msa.Item3))
				{
					msa = Tuple.Create(sumStart, minusStart - sumStart, sumPlus);
				}

				// Check whether the current +++---+++ sequence is the best
				if (candidate != null && (msa == null || candidate.Item3 + sumPlus >= msa.Item3))
				{
					var start = candidate.Item1;
					var len = minusStart - candidate.Item1;
					var longSum = candidate.Item3 + sumPlus;
					msa = Tuple.Create(start, len, longSum);
				}

				// Check whether the current +-+-+- sequence is the better candidate than previous
				if (candidate != null && candidate.Item3 + sumPlus + sumMinus >= 0)
				{
					var start = candidate.Item1;
					var len = i - candidate.Item1;
					var longSum = candidate.Item3 + sumPlus + sumMinus;
					candidate = Tuple.Create(start, len, longSum);
				}
				// Check whether the current +++--- sequence can be a candidate
				else if (sumPlus >= sumMinus)
				{
					candidate = Tuple.Create(sumStart, i - sumStart, sumPlus + sumMinus);
				}
				else
				{
					candidate = null;
				}
			}

			if (msa == null && candidate == null) return new int[0];

			var best = msa ?? candidate;

			if (msa != null && candidate != null && candidate.Item3 > msa.Item3)
				best = candidate;

			return arr.Skip(best.Item1).Take(best.Item2).ToArray();
		}
	}
}
