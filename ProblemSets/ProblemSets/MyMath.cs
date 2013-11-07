using System;
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

		public static bool IsPrime(ulong x)
		{
			for (ulong i = 2; i < Math.Sqrt(x) + 1; i++)
			{
				if (x % i == 0)
					return false;
			}
			return true;
		}

		public static ulong Mul(this IEnumerable<int> ints)
		{
			var result = 1ul;
			foreach (var i in ints)
				result *= (ulong) i;
			return result;
		}

		public static ulong ExtendedEuclidGcd(ulong a, ulong b)
		{
			ulong s = 0;
			ulong t = 1;
			ulong r = b;
			ulong olds = 1;
			ulong oldt = 0;
			ulong oldr = a;
			while (r != 0)
			{
				var quotient = oldr / r;

				var temp1 = oldr;
				oldr = r;
				r = temp1 - quotient * r;

				var temp2 = olds;
				olds = s;
				s = temp2 - quotient * s;

				var temp3 = oldt;
				oldt = t;
				t = temp3 - quotient * t;
			}

			// Bézout coefficients: (olds, oldt)
			// greatest common divisor: oldr
			// quotients by the gcd: (t, s)

			return oldr;
		}

		public static ulong ModularPow(ulong a, ulong pow, ulong mod)
		{
			ulong y = 1;

			while (true)
			{
				if ((pow & 1) != 0) y = (a * y) % mod;
				pow = pow >> 1;
				if (pow == 0) return y;
				a = (a * a) % mod;
			}
		}

		public static ulong Pow(ulong a, ulong pow)
		{
			ulong y = 1;

			while (true)
			{
				if ((pow & 1) != 0) y = (a * y);
				pow = pow >> 1;
				if (pow == 0) return y;
				a = (a * a);
			}
		}
	}
}
