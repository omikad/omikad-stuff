using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ProblemSets
{
	public class Program
	{
		public static void Main()
		{
			try
			{
				Problem421.Solve();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}
		}
	}

	public class Problem421
	{
		public static void Solve()
		{
			var timer = Stopwatch.StartNew();
			SolveInternal();
			Console.WriteLine("Elapsed: " + timer.ElapsedMilliseconds);
		}

		public static void SolveInternal()
		{
			// Number of primes less than 1e8 is 5 761 455

			const ulong maxn = 100000000000;

//			const ulong maxprime = 100000000;	// S = ???
//			const ulong maxprime = 10000000;	// S = ???
//			const ulong maxprime = 1000000;		// S = 31388381276205144, time =    81 390
			const ulong maxprime = 100000;		// S =  3821999778506561, time =     1 100
//			const ulong maxprime = 10000;		// S =   489099997157400, time =        21
//			const ulong maxprime = 1000;		// S =    63199999956190
//			const ulong maxprime = 300;			// S =    23399999996126
//			const ulong maxprime = 100;   		// S =     8299999999481  

			var sieve = CreateSieve(maxprime);

//			for (ulong num = 0; num < (ulong) primeFactorsTable.Length; num++)
//				Console.WriteLine("{0}: {1}", num, string.Join(", ", primeFactorsTable[num]));

			Console.WriteLine("Done precalc");

			var S = 0ul;

			S += (1ul + (maxn - 1) / 2) * 2; // For prime = 2 & i = 1

			for (ulong prime = 3; prime < (ulong)sieve.Length; prime++)
			{
				if (!sieve[prime])
				{
					// cnt будет равно количеству подходящих остатков
					var prime1 = prime - 1;
					var cnt = GCD15(prime1);

					if (cnt == 1)
					{
						var inverse = ModularInverse(15, prime1);
						var i = ModularPow(prime1, inverse, prime);

						S += (1ul + (maxn - i) / prime) * prime;

						continue;
					}

					if (((prime1 / cnt) & 1) == 1)
						continue;

					var invertedFactors = InvertedPrimeFactors(prime1, sieve);

					var primitiveRoot = FindPrimitiveRootForPrime(prime, invertedFactors);

					var powerdelta = (prime1 / cnt) / 2;
					var power = powerdelta;

					for (ulong k = 0; k < cnt; k++)
					{
						var solution = ModularPow(primitiveRoot, power, prime);

						S += (1ul + (maxn - solution) / prime) * prime;

						power += powerdelta * 2;
					}
				}
			}

			Console.WriteLine(S);
		}

		private static ulong ModularInverse(ulong a, ulong b)
		{
			return (ExtendedEuclid(a, b) % b);
		}

		private static ulong ExtendedEuclid(ulong a, ulong b)
		{
			ulong x = 1;
			ulong lastx = 0;
			ulong y = 0;
			ulong lasty = 1;

			while (b != 0)
			{
				var quotient = a / b;
				var temp1 = x - quotient * y;
				var temp2 = lastx - quotient * lasty;
				var temp3 = a - quotient * b;

				x = y;
				lastx = lasty;
				a = b;
				y = temp1;
				lasty = temp2;
				b = temp3;
			}

			return x;
		}
		
		private static ulong GCD15(ulong x)
		{
			if (x % 15 == 0) return 15;
			if (x % 5 == 0) return 5;
			if (x % 3 == 0) return 3;
			return 1;
		}

		private static ulong ModularPow15(ulong x, ulong prime)
		{
			var x2 = (x * x) % prime;
			var x4 = (x2 * x2) % prime;
			var x8 = (x4 * x4) % prime;
			var x12 = (x8 * x4) % prime;
			var x14 = (x12 * x2) % prime;
			var x15 = (x14 * x) % prime;
			return x15;
		}

		private static ulong ModularPow(ulong a, ulong pow, ulong mod)
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

		private static ulong FindPrimitiveRootForPrime(ulong prime, List<ulong> invertedFactors)
		{
			for (ulong gen = 2; gen < prime; gen++)
			{
				var found = true;
				foreach (var power in invertedFactors)
				{
					var test = ModularPow(gen, power, prime);
					if (test == 1)
					{
						found = false;
						break;
					}
				}
				if (found)
					return gen;
			}

			throw new InvalidOperationException("No generator found for prime = " + prime);
		}

		private static bool[] CreateSieve(ulong maxprime)
		{
			var sieve = new bool[maxprime];

			sieve[0] = true;
			sieve[1] = true;

			for (var i = 2; i < sieve.Length; i++)
			{
				if (sieve[i]) continue;

				for (var j = 2 * i; j < sieve.Length; j += i)
					sieve[j] = true;
			}
			return sieve;
		}

		private static List<ulong> InvertedPrimeFactors(ulong x, bool[] sieve)
		{
			var result = new List<ulong>();

			for (ulong i = 2; i < x / 2 + 1; i++)
				if (!sieve[i])
					if (x % i == 0)
						result.Add(x / i);

			return result;
		}
	}
}
