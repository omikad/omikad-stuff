using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Numerics;
using ProblemSets.Services;

namespace ProblemSets.Problems.ProjEuler
{
	[Export]
	public class Problem467
	{
		public void Go()
		{
			const int maxP = 110000;
			const int maxC = 12000;

			const int n = 10000;
//			const int n = 100;
			const ulong mod = 1000000007;

			var primesSieve = MyMath.CreatePrimesSieve(maxP);

			var p = MyMath.ConvertSieveToPrimes(primesSieve);

			var c = new List<ulong>(maxC);
			for (var i = 2ul; i < maxC; i++)
				if (primesSieve[i])
					c.Add(i);

			Console.WriteLine("p = " + ", ".Join(p.Take(20)));
			Console.WriteLine("c = " + ", ".Join(c.Take(20)));

			var pd = p.Select(DigitalRoot).ToArray();
			var cd = c.Select(DigitalRoot).ToArray();

			Console.WriteLine("pd = " + "".Join(pd.Take(20)));
			Console.WriteLine("cd = " + "".Join(cd.Take(20)));

			Console.WriteLine("n = " + n);

			var L = new int[n + 1, n + 1];

			for (var i = n; i >= 0; i--)
				for (var j = n; j >= 0; j--)
				{
					if (i == n || j == n) L[i, j] = 0;
					else if (pd[i] == cd[j]) L[i, j] = 1 + L[i + 1, j + 1];
					else L[i, j] = Math.Max(L[i + 1, j], L[i, j + 1]);
				}

			var result = new BigInteger();

			{
				var i = 0;
				var j = 0;
				while (i < n && j < n)
				{
					if (pd[i] == cd[j])
					{
						result = result * 10 + pd[i];
						i++;
						j++;
					}
					else if (L[i + 1, j] == L[i, j + 1])
					{
						if (pd[i] <= cd[j])
						{
							result = result * 10 + pd[i];
							i++;
						}
						else
						{
							result = result * 10 + cd[j];
							j++;
						}
					}
					else if (L[i + 1, j] > L[i, j + 1])
					{
						result = result * 10 + pd[i];
						i++;
					}
					else
					{
						result = result * 10 + cd[j];
						j++;
					}
				}

				if (i < n)
					while (i < n)
					{
						result = result * 10 + pd[i];
						i++;
					}

				else 
					while (j < n)
					{
						result = result * 10 + cd[j];
						j++;
					}
			}

			Console.WriteLine();
			Console.WriteLine(result % mod);
		}

		private static byte DigitalRoot(ulong n)
		{
			return (byte)(n - 9 * ((n - 1) / 9));
		}
	}
}