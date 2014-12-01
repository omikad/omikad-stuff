using System;
using System.Linq;
using ProblemSets.Services;

namespace ProblemSets.Problems.Co
{
	public class IntersectingDiscs
	{
		public void Go()
		{
			Console.WriteLine(SolveBrute(new[] {1, 5, 2, 1, 4, 0}));
			Console.WriteLine(SolveGood(new[] {1, 5, 2, 1, 4, 0}));

			var rnd = new Random();

			for (var i = 0; i < 10000; i++)
			{
				var arr = new int[rnd.Next(2, 30)];

				for (var j = 0; j < arr.Length; j++)
					arr[j] = rnd.Next(0, 10);

				var brute = SolveBrute(arr);
				var good = SolveGood(arr);

				if (brute != good)
					throw new InvalidOperationException(new { brute, good, arr = ", ".Join(arr) }.ToString());
			}
		}

		private int SolveGood(int[] arr)
		{
			var data =
				arr.SelectMany((r, c) => new[] {new {p = c - r, isleft = true}, new {p = c + r, isleft = false}})
					.OrderBy(a => a.p)
					.ThenBy(a => a.isleft ? 0 : 1)
					.ToArray();

			var cnt = 0;
			var left = 0;

			foreach (var a in data)
			{
				if (a.isleft)
				{
					cnt += left;
					left++;
				}
				else
				{
					left--;
				}
			}

			return cnt;
		}

		private int SolveBrute(int[] arr)
		{
			var cnt = 0;

			for (var i = 0; i < arr.Length; i++)
				for (var j = i + 1; j < arr.Length; j++)
					if (i + arr[i] >= j - arr[j])
						cnt++;

			return cnt;
		}
	}
}