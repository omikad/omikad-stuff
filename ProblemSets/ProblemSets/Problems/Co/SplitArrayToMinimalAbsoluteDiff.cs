using System;
using System.Linq;
using ProblemSets.Services;

namespace ProblemSets.Problems.Co
{
	public class SplitArrayToMinimalAbsoluteDiff
	{
		public void Go()
		{
			Console.WriteLine(SolutionBrute(new[] {3, 1, 2, 4, 3}));

			var rnd = new Random();

			for (var i = 0; i < 10000; i++)
			{
				var arr = new int[rnd.Next(2, 30)];

				for (var j = 0; j < arr.Length; j++)
					arr[j] = rnd.Next(-20, 20);

				var brute = SolutionBrute(arr);
				var good = SolutionGood(arr);

				if (brute != good)
					throw new InvalidOperationException(new { brute, good, arr = ", ".Join(arr) }.ToString());
			}
		}

		private int SolutionGood(int[] arr)
		{
			var sum = arr.Sum();

			var min = int.MaxValue;

			var d = -sum;

			for (var i = 1; i < arr.Length; i++)
			{
				d += 2*arr[i - 1];

				min = Math.Min(min, Math.Abs(d));
			}

			return min;
		}

		private int SolutionBrute(int[] arr)
		{
			var min = int.MaxValue;

			for (var i = 1; i < arr.Length; i++)
			{
				var left = arr.Take(i).Sum();
				var right = arr.Skip(i).Sum();
				var diff = Math.Abs(left - right);

				min = Math.Min(min, diff);
			}

			return min;
		}
	}
}