using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace ProblemSets.Services
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

		public static bool IsPrime(BigInteger x, BigInteger checkLimit)
		{
			if (x % 2 == 0) return false;
			
			for (var i = new BigInteger(3); i <= checkLimit; i += 2)
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

		public static ulong ModularInverse(ulong a, ulong b)
		{
			return (ulong)ModularInverse(new BigInteger(a), new BigInteger(b));
		}

		public static BigInteger ModularInverse(BigInteger a, BigInteger b)
		{
			var dividend = a % b;
			var divisor = b;

			var last_x = BigInteger.One;
			var curr_x = BigInteger.Zero;

			while (divisor.Sign > 0)
			{
				var quotient = dividend / divisor;
				var remainder = dividend % divisor;
				if (remainder.Sign <= 0)
					break;

				var next_x = last_x - curr_x * quotient;
				last_x = curr_x;
				curr_x = next_x;

				dividend = divisor;
				divisor = remainder;
			}

//			if (divisor != BigInteger.One)
//				throw new Exception("Numbers a and b are not relatively primes");

			return (curr_x.Sign < 0 ? curr_x + b : curr_x);
		}

		public static int FillDivisors(ulong n, ulong[] divisors)
		{
			divisors[0] = 1;
			divisors[1] = n;
			var cnt = 2;
			ulong i = 2;
			while (i < Math.Sqrt(n))
			{
				if (n % i == 0)
				{
					divisors[cnt] = n / i;
					divisors[cnt + 1] = i;
					cnt += 2;
				}
				i++;
			}
			if (i * i == n)
			{
				divisors[cnt] = i;
				cnt++;
			}
			return cnt;
		}
	}
}
