using System.Collections.Generic;
using System.Linq;

namespace ProblemSets
{
	public static class MyMath
	{
		public static bool[] CreatePrimesSieve(ulong maxprime)
		{
			var sieve = new bool[maxprime];

			sieve[0] = true;
			sieve[1] = true;

			for (var i = 4; i < sieve.Length; i += 2)
				sieve[i] = true;

			for (var i = 3; i < sieve.Length / 3 + 1; i++)
			{
				if (sieve[i]) continue;

				for (var j = 2 * i; j < sieve.Length; j += i)
					sieve[j] = true;
			}
			return sieve;
		}

		public static ulong[] ConvertSieveToPrimes(bool[] sieve)
		{
			var cnt = sieve.Count(p => !p);
			var result = new ulong[cnt];
			ulong i = 0;
			for (ulong j = 0; j < (ulong)sieve.Length; j++)
				if (!sieve[j])
				{
					result[i] = j;
					i++;
				}
			return result;
		}

		public static ulong Mul(this IEnumerable<int> ints)
		{
			var result = 1ul;
			foreach (var i in ints)
				result *= (ulong) i;
			return result;
		}
	}
}
