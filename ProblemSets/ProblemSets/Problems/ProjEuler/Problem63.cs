using System;
using System.Numerics;

namespace ProblemSets
{
	public class Problem63
	{
		public static void Solve()
		{
			var cnt = 0;

			const int max = 500;

			for (var n = 1; n <= max; n++)
				for (BigInteger i = 1; i <= max; i++)
				{
					var str = BigInteger.Pow(i, n).ToString();
					if (str.Length == n)
					{
						Console.WriteLine("{0}^{1} = {2}", i, n, str);
						cnt++;
					}
				}

			Console.WriteLine(cnt);
		}
	}
}