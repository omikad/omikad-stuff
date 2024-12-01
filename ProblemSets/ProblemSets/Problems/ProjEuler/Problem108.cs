using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Combinatorics.Collections;
using ProblemSets.Services;

namespace ProblemSets
{
	public class Problem108
	{
		public static void Solve()
		{
//			const ulong max = 1000000;
			const ulong maxn = 100;
			const int maxpower = 5;
			const int maxlen = 10;
			const ulong task = 4000000;

			var sieve = MyMath.CreatePrimesSieve(maxn);
			var primes = MyMath.ConvertSieveToPrimes(sieve);

//			var divisors = new ulong[max];

//			GoSolution(maxn, primes, divisors);
//			CheckS(maxn, primes, divisors);

			var sdict = GetSolutions5(maxpower, maxlen, primes);

//			Check5(100, sdict);

			Console.WriteLine(new { maxpower, maxlen });
			foreach (var kvp in sdict
									 .Where(kvp => kvp.Value >= task)
									 .OrderBy(kvp => kvp.Key)
									 .Take(5))
				Console.WriteLine(new {n = kvp.Key, s = kvp.Value});
		}

		private static Dictionary<BigInteger, BigInteger> GetSolutions5(int maxpower, int maxlen, ulong[] primes)
		{
			var sdict = new Dictionary<BigInteger, BigInteger>();

			var primeInPowers = new BigInteger[maxpower + 1, primes.Length];
			for (var power = 1; power <= maxpower; power++)
				for (var primeI = 0; primeI < primes.Length; primeI++)
					primeInPowers[power, primeI] = BigInteger.Pow(primes[primeI], power);

			for (var power = 1; power <= maxpower; power++)
				for (var primeI = 0; primeI < primes.Length; primeI++)
				{
					var n = primeInPowers[power, primeI];
					sdict[n] = power + 1;
				}

			var powers = Enumerable.Range(1, maxpower).ToArray();

			for (var len = 2; len < maxlen; len++)
			{
				var lastIndex = len - 1;

				foreach (var variation in new Variations<int>(powers, len, GenerateOption.WithRepetition))
				{
					var nprev = BigInteger.One;
					for (var i = 0; i < lastIndex; i++)
						nprev *= primeInPowers[variation[i], i];

					var snprev = sdict[nprev];
					var k = variation[lastIndex];
					var n = nprev * primeInPowers[k, lastIndex];

					sdict[n] = snprev * (2 * k + 1) - k;
				}
			}

			return sdict;
		}

		private static void Check5(ulong max, Dictionary<BigInteger, BigInteger> sdict)
		{
			var divisors = new ulong[max];
			foreach (var kvp in sdict)
			{
				var sbrute = (ulong) GetSolutionsBrute((ulong) kvp.Key, divisors).Count();

				Console.WriteLine(new {n = kvp.Key, s = kvp.Value, sbrute});

				if (kvp.Value != sbrute)
					throw new InvalidOperationException();
			}
		}

		private static void GoSolution(ulong maxn, ulong[] primes, ulong[] divisors)
		{
			var maxcount = 0ul;
			for (var n = 2ul; n < maxn; n++)
			{
				var count = GetSolutions2(n, primes, divisors);
				if (count > maxcount)
				{
					maxcount = count;
					Console.WriteLine(new {n, count});
				}
			}
		}

		private static void CheckS(ulong maxn, ulong[] primes, ulong[] divisors)
		{
			for (var n = 2ul; n <= maxn; n++)
			{
				var sn = (ulong) GetSolutionsBrute(n, divisors).Count();
				var s2 = GetSolutions2(n, primes, divisors);
				var s3 = GetSolutions3(n, primes, divisors);

				Console.WriteLine(new { n, sn, s2, s3 });

				if (sn != s2 || sn != s3)
					throw new InvalidOperationException();
			}
		}

		private static ulong GetSolutions2(ulong n, ulong[] primes, ulong[] divisors)
		{
			var cnt = 1ul;

			var divs = MyMath.FillDivisors(n, divisors);
			for (var j = 0; j < divs; j++)
				cnt += GetT2(divisors[j], primes);

			return cnt;
		}

		private static ulong GetSolutions3(ulong n, ulong[] primes, ulong[] divisors)
		{
			if (n == 1) return 1;

			var mainDivisor = 1ul;
			foreach (var prime in primes)
			{
				if (n % prime == 0)
				{
					mainDivisor = prime;
					break;
				}
				if (prime > n / 2 + 1)
					break;
			}

			if (mainDivisor == 1) return 2; // n is prime

			var nn = n / mainDivisor;

			if (nn % mainDivisor != 0)
			{
				var snn = GetSolutions2(nn, primes, divisors);
				return 3 * snn - 1;
			}
			else
			{
				var k = 1ul;

				do { nn = nn / mainDivisor; k++; } while (nn % mainDivisor == 0);

				var snn = GetSolutions2(nn, primes, divisors);

				return snn * (2 * k + 1) - k;
			}
		}

		private static void CheckT(ulong maxn, ulong[] primes, ulong[] divisors)
		{
			for (var n = 2ul; n <= maxn; n++)
			{
				var tn = GetT(n, divisors);
				var t2 = GetT2(n, primes);
				Console.WriteLine(new {n, tn, t2});

				if (tn != t2)
					throw new InvalidOperationException();
			}
		}

		private static ulong GetT2(ulong n, IEnumerable<ulong> primes)
		{
			var cnt = 0ul;
			foreach (var p in primes)
			{
				if (n % p == 0)
					cnt++;
				if (p >= n)
					break;
			}

			return MyMath.Pow(2, cnt - 1);
		}

		private static ulong GetT(ulong n, ulong[] divisors)
		{
			var cnt = 0ul;
			var divs = MyMath.FillDivisors(n, divisors);
			
			for (var j = 0; j < divs; j++)
			{
				var xbar = divisors[j];
				if (n % xbar == 0)
				{
					var ybar = n / xbar;
					if (ybar <= xbar && MyMath.ExtendedEuclidGcd(xbar, ybar) == 1)
						cnt++;
				}
			}
			return cnt;
		}

		// Verified
		private static IEnumerable<Tuple<ulong, ulong>> GetSolutionsBrute(ulong n, ulong[] divisors)
		{
			var divs = MyMath.FillDivisors(n, divisors);
			for (var i = 0; i < divs; i++)
			{
				var b = divisors[i];
				var nn = n / b;

				for (var j = 0; j < divs; j++)
				{
					var xbar = divisors[j];
					if (nn % xbar == 0)
					{
						var ybar = nn / xbar;
						if (ybar <= xbar && MyMath.ExtendedEuclidGcd(xbar, ybar) == 1)
						{
							var a = (xbar + ybar) * b;
							var x = a * xbar;
							var y = a * ybar;
							yield return Tuple.Create(x, y);
						}
					}
				}
			}
		}
	}
}



/*
 
{ maxpower = 2, maxlen = 20 }
{ n = 63892555340714100, s = 6150938 }
{ n = 78496567990020180, s = 6643013 }
{ n = 100402586963979300, s = 6150938 }
{ n = 118657602775611900, s = 6150938 }
{ n = 130827613316700300, s = 6643013 }
Elapsed: 8054

{ maxpower = 2, maxlen = 22 }
{ n = 63892555340714100, s = 6150938 }
{ n = 78496567990020180, s = 6643013 }
{ n = 100402586963979300, s = 6150938 }
{ n = 118657602775611900, s = 6150938 }
{ n = 130827613316700300, s = 6643013 }
Elapsed: 36716

{ maxpower = 3, maxlen = 10 }
{ n = 64982868356715309000, s = 4411838 }
{ n = 78663472221286953000, s = 4411838 }
{ n = 84977597081858481000, s = 4411838 }
{ n = 87917998364967771000, s = 4411838 }
{ n = 94974961444430067000, s = 4411838 }
Elapsed: 127

{ maxpower = 3, maxlen = 13 }
{ n = 9350130049860600, s = 4018613 }
{ n = 14693061506923800, s = 4018613 }
{ n = 15583550083101000, s = 4018613 }
{ n = 17364527235455400, s = 4018613 }
{ n = 20570286109693320, s = 4018613 }
Elapsed: 4370

{ maxpower = 3, maxlen = 14 }
{ n = 9350130049860600, s = 4018613 }
{ n = 10953009486979560, s = 4340102 }
{ n = 14693061506923800, s = 4018613 }
{ n = 15583550083101000, s = 4018613 }
{ n = 17364527235455400, s = 4018613 }
Elapsed: 15149

{ maxpower = 3, maxlen = 15 }
{ n = 9350130049860600, s = 4018613 }
{ n = 10953009486979560, s = 4340102 }
{ n = 14693061506923800, s = 4018613 }
{ n = 15583550083101000, s = 4018613 }
{ n = 17364527235455400, s = 4018613 }
Elapsed: 52967

{ maxpower = 4, maxlen = 11 }
{ n = 244798958498094000, s = 4018613 }
{ n = 320121714959046000, s = 4018613 }
{ n = 357783093189522000, s = 4018613 }
{ n = 378325663133418000, s = 4018613 }
{ n = 384684077639862000, s = 4018613 }
Elapsed: 7128

{ maxpower = 4, maxlen = 12 }
{ n = 27797683932018000, s = 4018613 }
{ n = 32851808283294000, s = 4018613 }
{ n = 38916757504825200, s = 4018613 }
{ n = 41696525898027000, s = 4018613 }
{ n = 42960056985846000, s = 4018613 }
Elapsed: 36289

{ maxpower = 5, maxlen = 10 }
{ n = 3895077024378540000, s = 4176563 }
{ n = 4305085132207860000, s = 4287938 }
{ n = 4715093240037180000, s = 4176563 }
{ n = 4811565735997020000, s = 4287938 }
{ n = 5269810091806260000, s = 4176563 }
Elapsed: 11432 
 
 */