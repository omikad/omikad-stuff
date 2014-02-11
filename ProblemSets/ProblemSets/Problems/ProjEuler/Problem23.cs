using System;
using System.Collections.Generic;
using ProblemSets.Services;

namespace ProblemSets
{
	public class Problem23
	{
		public static void Solve()
		{
//			const int max = 100;
			const int max = 28124;

			var sieve = MyMath.CreatePrimesSieve(max);
			var primes = MyMath.ConvertSieveToPrimes(sieve);

			var abundands = FindAbundands(max, primes);

			var canBeSumOfTwo = new bool[max];
			for (var i = 0; i < abundands.Count; i++)
				for (var j = i; j < abundands.Count; j++)
				{
					var sum = abundands[i] + abundands[j];
					if (sum < max)
						canBeSumOfTwo[sum] = true;
				}

			var result = 0ul;
			for (var i = 1; i < max; i++)
				if (!canBeSumOfTwo[i])
				{
					result += (ulong) i;
//					Console.WriteLine(i);
				}

			// 4179935 is incorrect

			Console.WriteLine(result);
		}

		private static IList<ulong> FindAbundands(ulong max, ulong[] primes)
		{
			var result = new List<ulong>();

			var primeFactors = new ulong[500];

			for (ulong candidate = 12; candidate < max; candidate++)
			{
				var primeFactorsLen = 0;

				var n = candidate;
				foreach (var prime in primes)
				{
					if (n % prime == 0)
					{
						primeFactors[primeFactorsLen++] = prime;
						n = n / prime;
						while (n % prime == 0)
						{
							n = n / prime;
							primeFactors[primeFactorsLen++] = prime;
						}
						if (n == 1)
							break;
					}
				}

				var factors = new HashSet<ulong>();
				for (ulong mask = 0; mask < (1ul << primeFactorsLen) - 1; mask++)
				{
					var factor = 1ul;
					for (var i = 0; i < primeFactorsLen; i++)
					{
						if ((mask & (1ul << i)) != 0)
							factor *= primeFactors[i];
					}
					factors.Add(factor);
				}

				var sum = 0ul;
				foreach (var factor in factors)
					sum += factor;

				if (sum > candidate)
					result.Add(candidate);
			}

			return result;
		}
	}
}