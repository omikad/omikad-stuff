using System;
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
//			const ulong maxprime = 1000000;		// S = 31388381276205144, time = 3 549 377
			const ulong maxprime = 100000;		// S =  3821999778506561, time =    19 188
//			const ulong maxprime = 10000;		// S =   489099997157400, time =       242
//			const ulong maxprime = 1000;		// S =    63199999956190
//			const ulong maxprime = 100;   		// S =     8299999999481  

			var sieve = CreateSieve(maxprime);

//			var squares = new ulong[sieve.Length];
//			for (ulong i = 0; i < (ulong) sieve.Length; i++)
//				squares[i] = i * i;

			Console.WriteLine("Done precalc");

			var S = 0ul;

			S += (1ul + (maxn - 1) / 2) * 2; // For prime = 2 & i = 1

			for (ulong prime = 3; prime < (ulong)sieve.Length; prime++)
			{
				if (!sieve[prime])
				{
					for (ulong i = 2; i < prime; i++)
					{
						var i2p = (i * i) % prime;
						var i4p = (i2p * i2p) % prime;
						var i8p = (i4p * i4p) % prime;
						var i16p = (i8p * i8p) % prime;
//						var i2p = squares[i] % prime;
//						var i4p = squares[i2p] % prime;
//						var i8p = squares[i4p] % prime;
//						var i16p = squares[i8p] % prime;

						if (i16p == prime - i)
						{
							// Для этого i: если (n % prime == i), то (n^15+1 % prime == 0)
							// Нужно найти сколько разных n такие, что (n % prime == i)
							// Это количество равно 1 + (maxn - i) / prime
							var kp = 1ul + (maxn - i) / prime;
							S += kp * prime;

//							Console.WriteLine("{0,5}: {1}", prime, i);
						}
					}
				}
			}

			Console.WriteLine(S);
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
	}


}
