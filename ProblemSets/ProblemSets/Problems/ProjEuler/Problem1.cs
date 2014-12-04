using System;

namespace ProblemSets
{
	public class Problem1
	{
		public void Go()
		{
			for (var n = 2; n < 10000; n++)
			{
				var expected = SolveBrute(n);
				var actual = Solve(n);

				if (expected != actual)
					throw new InvalidOperationException(new { expected, actual, n }.ToString());
			}
		}

		private static int Solve(int n)
		{
			n--;

			var by3 = n / 3;
			var by5 = n / 5;
			var by15 = n / 15;

			var s3 = 3 * by3 * (by3 + 1) / 2;
			var s5 = 5 * by5 * (by5 + 1) / 2;
			var s15 = 15 * by15 * (by15 + 1) / 2;

			return s3 + s5 - s15;
		}

		public static int SolveBrute(int n)
		{
			var sum = 0;
			for (var i = 2; i < n; i++)
				if (i % 3 == 0 || i % 5 == 0)
					sum += i;
			return sum;
		}
	}
}