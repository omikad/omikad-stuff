using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Numerics;
using ProblemSets.Services;

namespace ProblemSets.Problems.ProjEuler
{
	[Export]
	public class Problem200
	{
		[Import] private MillerRabinPrimality millerRabinPrimality;

		private readonly ulong[] primes = MyMath.ConvertSieveToPrimes(MyMath.CreatePrimesSieve(1000000));

		private ulong maxPrime;

		// not 3100688320075
		// not 200071658143491715
		// not 1200916536125
		// not 200119215368
		// not 236674752008
		// not 472388112008
		// not 237832009928
		// not 239429232008

		public void Go()
		{
			maxPrime = primes.Max();
			Console.WriteLine("Primes sieve calculated, max prime = " + maxPrime);

			var scubes = GetScubes();
			Console.WriteLine("Scubes calculated");

			Console.WriteLine(scubes
				.Where(PrimeProofEasyCheck)
				.Where(i => i.ToString().Contains("200"))
				.Where(PrimeProofHardCheck)
				.Take(200)
				.Last());

			Console.WriteLine(new { maxP, maxQ });
		}

		private class TupleComparer : IComparer<Tuple<int, int, ulong>>
		{
			public int Compare(Tuple<int, int, ulong> x, Tuple<int, int, ulong> y)
			{
				return x.Item3.CompareTo(y.Item3);
			}
		}

		private ulong maxP;
		private ulong maxQ;

		private IEnumerable<ulong> GetScubes()
		{
			var list = new SortedSet<Tuple<int, int, ulong>>(new TupleComparer())
			{
				Tuple.Create(1, 0, 3ul * 3 * 2 * 2 * 2),
				Tuple.Create(0, 1, 3ul * 3 * 3 * 2 * 2)
			};

			while (list.Count > 0)
			{
				var min = list.Min;

				if (min.Item1 != min.Item2)
				{
					if (primes[min.Item1] > maxP) maxP = primes[min.Item1];
					if (primes[min.Item2] > maxQ) maxQ = primes[min.Item2];

//					Console.WriteLine(new { p = primes[min.Item1], q = primes[min.Item2] });
					yield return min.Item3;
				}

				list.Remove(min);

				var p = primes[min.Item1];
				var q = primes[min.Item2];

				if (min.Item1 < primes.Length - 1)
				{
					var p1 = primes[min.Item1 + 1];
					list.Add(Tuple.Create(min.Item1 + 1, min.Item2, p1 * p1 * q * q * q));
				}

				if (min.Item2 < primes.Length - 1)
				{
					var q1 = primes[min.Item2 + 1];
					list.Add(Tuple.Create(min.Item1, min.Item2 + 1, p * p * q1 * q1 * q1));
				}
			}
		}

		private bool PrimeProofEasyCheck(ulong m)
		{
			var mz = m - m % 10;

			if (millerRabinPrimality.IsPrime(new BigInteger(mz + 1))) return false;
			if (millerRabinPrimality.IsPrime(new BigInteger(mz + 3))) return false;
			if (millerRabinPrimality.IsPrime(new BigInteger(mz + 7))) return false;
			if (millerRabinPrimality.IsPrime(new BigInteger(mz + 9))) return false;

			return true;
		}

		private bool PrimeProofHardCheck(ulong m)
		{
			if (m % 2 == 0 || m % 5 == 0)
				return true; // Easy check was enough

			var pow = 1ul;
			for (var j = 0; j < Math.Log10(m) + 1; j++)
			{
				pow *= 10;

				var digit = (m / pow) % 10;

				var z = m - digit * pow;

				for (var i = 0ul; i <= 9; i++)
				{
					var value = z + i * pow;
					if (i != digit && millerRabinPrimality.IsPrime(new BigInteger(value)))
					{
						return false;
					}
				}
			}

			return true;
		}
	}
}