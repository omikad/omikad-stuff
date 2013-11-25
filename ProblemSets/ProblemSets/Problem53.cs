using System;
using System.Numerics;

namespace ProblemSets
{
	public class Problem53
	{
		public static void Solve()
		{
			const int max = 101;
			var factorials = new BigInteger[max];
			factorials[0] = 1;
			for (ulong i = 1; i < max; i++)
				factorials[i] = i * factorials[i - 1];

			var cnt = 0;

			for (var n = 1; n < max; n++)
				for (var r = 1; r <= n; r++)
				{
					var c = factorials[n] / (factorials[r] * factorials[n - r]);

					if (c > 1000000)
						cnt++;
				}

			Console.WriteLine(cnt);
		}
	}
}