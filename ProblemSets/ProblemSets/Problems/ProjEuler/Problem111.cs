using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using ProblemSets.Services;

namespace ProblemSets.Problems.ProjEuler
{
	[Export]
	public class Problem111
	{
		private const ulong len = 4;
		private const ulong max = 10000000000;
//		private const ulong max = 10000;

		public void Go()
		{
			var sum = 0ul;
			for (var d = 0ul; d <= 9; d++)
			{
				var primes = FilterPrimesByPrimalityTest(GetCandidates(d)).ToArray();

				foreach (var p in primes)
					sum += p;

				Console.WriteLine(new { d, N = primes.Length });
			}

			Console.WriteLine(sum);
		}

		private static IEnumerable<ulong> FilterPrimesByPrimalityTest(IEnumerable<ulong> candidates)
		{
			return candidates.Where(candidate => MyMath.IsPrime(candidate));
		}

		private static IEnumerable<ulong> GetCandidates(ulong digit)
		{
			if (digit == 0)
			{
				for (ulong i = 1; i <= 9; i++)
					for (ulong j = 1; j <= 9; j++)
					{
						var x = i * (max / 10) + j;
							yield return x;
					}
			}
			else if (digit == 2 || digit == 8)
			{
				const ulong rowDigits = (max - 1) / 9;

				for (var pow1 = 1ul; pow1 < rowDigits; pow1 *= 10)
					for (var pow2 = 1ul; pow2 < pow1; pow2 *= 10)
					{
						var mask = (rowDigits - pow1 - pow2) * digit;
						for (ulong i = 0; i <= 9; i++)
							for (ulong j = 0; j <= 9; j++)
							{
								if (i == 0 && pow1 == (max / 10)) continue;

								var x = mask + i * pow1 + j * pow2;
								yield return x;
							}
					}
			}
			else
			{
				const ulong rowDigits = (max - 1) / 9;

				for (var pow = 1ul; pow < rowDigits; pow *= 10)
				{
					var mask = (rowDigits - pow) * digit;
					for (ulong j = 1; j <= 9; j++)
					{
						var x = mask + j * pow;
							yield return x;
					}
				}
			}
		}
	}
}