using System;
using System.ComponentModel.Composition;
using System.Linq;

namespace ProblemSets.Problems
{
	[Export]
	public class CountNumberOf2s
	{
		public void Go()
		{
			Console.WriteLine(new { n = 125, result = Solve(125), brute = Solve_Brute(125) });

			for (var i = 0; i < 2000; i++)
			{
				var result = Solve(i);
				var brute = Solve_Brute(i);

				if (brute != result)
					throw new InvalidOperationException();
			}

			Console.WriteLine("Passed!");
		}

		private static int Solve(int n)
		{
			var cnt = 0;
			var mul = 1;
			var nn = n;

			while (nn > 0)
			{
				var decs = nn / 10;

				if (nn % 10 > 2)
					decs++;

				cnt += decs * mul;

				if (nn % 10 == 2)
					cnt += n - nn * mul + 1;

				nn /= 10;
				mul *= 10;
			}

			return cnt;
		}

		private static int Solve_Brute(int n)
		{
			var cnt = 0;
			for (var i = 2; i <= n; i++)
				cnt += i.ToString().Count(c => c == '2');
			return cnt;
		}
	}
}