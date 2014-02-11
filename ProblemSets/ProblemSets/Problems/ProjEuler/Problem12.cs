using System;
using ProblemSets.Services;

namespace ProblemSets
{
	public class Problem12
	{
		public static void Solve()
		{
			var sieve = MyMath.CreatePrimesSieve(1000000);
			var primes = MyMath.ConvertSieveToPrimes(sieve);

			for (ulong n = 2;; n++)
			{
				var sum = n * (n + 1) / 2;

				var cnt = 1ul;
				var s = sum;

				foreach (var prime in primes)
				{
					if (s % prime == 0)
					{
						var power = 1;
						s = s / prime;
						while (s % prime == 0)
						{
							s = s / prime;
							power++;
						}

						cnt *= (ulong) (power + 1);

						if (s == 1)
							break;
					}
				}

				if (cnt > 500)
				{
					Console.WriteLine("{0,3}: {1,6}  {2}", n, sum, cnt);
					break;
				}
			}
		}
	}
}