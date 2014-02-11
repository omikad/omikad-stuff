using System;
using System.Collections.Generic;
using System.Diagnostics;
using ProblemSets.Services;

namespace ProblemSets
{
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

//			const ulong maxprime = 100000000;	// S = 2304215802083466198, time =  2 062 029
//			const ulong maxprime = 10000000;	// S =  265739204055448802, time =     36 430
			const ulong maxprime = 1000000;		// S =   31388381276205144, time =        919
//			const ulong maxprime = 100000;		// S =    3821999778506561, time =         49
//			const ulong maxprime = 10000;		// S =     489099997157400, time =         10
//			const ulong maxprime = 1000;		// S =      63199999956190
//			const ulong maxprime = 300;			// S =      23399999996126
//			const ulong maxprime = 100;   		// S =       8299999999481  

			var sieve = MyMath.CreatePrimesSieve(maxprime);
			var primes = MyMath.ConvertSieveToPrimes(sieve);
			var invertedPrimeFactorsArray = new ulong[(int) Math.Sqrt(maxprime) + 1];

//			for (ulong num = 0; num < (ulong) primeFactorsTable.Length; num++)
//				Console.WriteLine("{0}: {1}", num, string.Join(", ", primeFactorsTable[num]));

			Console.WriteLine("Done precalc");

			var S = 0ul;

			foreach (var prime in primes)
			{
				// cnt будет равно количеству подходящих остатков
				var prime1 = prime - 1;
				var cnt = GCD15(prime1);

				if (cnt == 1)
				{
					var inverse = ModularInverse(15, prime1);
					var i = MyMath.ModularPow(prime1, inverse, prime);

					S += (1ul + (maxn - i) / prime) * prime;

					continue;
				}

				if (((prime1 / cnt) & 1) == 1)
					continue;

				ulong invertedPrimeFactorsLength;
				FindInvertedPrimeFactors(prime1, primes, invertedPrimeFactorsArray, out invertedPrimeFactorsLength);

				var primitiveRoot = FindPrimitiveRootForPrime(prime, invertedPrimeFactorsArray, invertedPrimeFactorsLength);

				var powerdelta = (prime1 / cnt) / 2;
				var power = powerdelta;

				for (ulong k = 0; k < cnt; k++)
				{
					var solution = MyMath.ModularPow(primitiveRoot, power, prime);

					S += (1ul + (maxn - solution) / prime) * prime;

					power += powerdelta * 2;
				}
			}

			Console.WriteLine(S);
		}

		private static ulong ModularInverse(ulong a, ulong b)
		{
			return (MyMath.ExtendedEuclidGcd(a, b) % b);
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

		private static ulong FindPrimitiveRootForPrime(ulong prime, ulong[] invertedFactors, ulong invertedFactorsLength)
		{
			for (ulong gen = 2; gen < prime; gen++)
			{
				var found = true;
				for (ulong i = 0; i < invertedFactorsLength; i++)
				{
					var test = MyMath.ModularPow(gen, invertedFactors[i], prime);
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

		private static void FindInvertedPrimeFactors(ulong x, IEnumerable<ulong> primes, ulong[] array, out ulong length)
		{
			ulong len = 0;
			ulong mult = 1;
			foreach (var prime in primes)
			{
				if (prime >= x / 2 + 1) break;
				if (x % prime == 0)
				{
					array[len] = (x * mult) / prime;
					len++;
					
					x = x / prime;
					mult = mult * prime;

					if (x % prime == 0)
					{
						x = x / prime;
						mult = mult * prime;

						// Почему это не работает ?
//						if (x % prime == 0)
//						{
//							x = x / prime;
//							mult = mult * prime;
//						}
					}
				}
			}
			length = len;
		}
	}
}