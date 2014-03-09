using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Linq;
using ProblemSets.Services;

namespace ProblemSets.Problems.ProjEuler
{
	[Export]
	public class Problem152
	{
		private const int max = 50;

		private ulong[] numbers;
		private HashSet<ulong> forbidden;
		private Dictionary<ulong, IList<ulong>[]> primesCombinations;

		private HashSet<string> solutions;

		public void Go()
		{
			Console.WriteLine("{0}: ", max);
			Check();

			var primes = MyMath.ConvertSieveToPrimes(MyMath.CreatePrimesSieve(max));

			forbidden = new HashSet<ulong>(primes.Where(p => p > max / 2));
			Console.WriteLine("Forbidden: " + ", ".Join(forbidden));

			numbers = Enumerable.Range(2, max - 1)
				.Select(i => (ulong) i)
				.Where(i => !forbidden.Contains(i))
				.ToArray();

			var timer = Stopwatch.StartNew();
			CalcPrimesCombinations(primes);
			Console.WriteLine("CalcPrimesCombinations elapsed: " + timer.ElapsedMilliseconds);

			Console.WriteLine("Numbers length: " + numbers.Length);
			Console.WriteLine("Numbers: " + ", ".Join(numbers));

			Console.WriteLine();
			solutions = new HashSet<string>();
			Solve(new Stack<ulong>(primesCombinations.Keys), new HashSet<ulong>());
			Console.WriteLine("Total distinct solutions: " + solutions.Count);
			
//			CalcSuffixes();

//			ShowSum(numbers.Skip(1).ToArray());

//			Solve(2, new Stack<ulong>(max), new FractionBigInt(0, 1));
		}

		private void Solve(Stack<ulong> remainingPrimes, HashSet<ulong> checkme)
		{
			if (remainingPrimes.Count == 0)
			{
				if (CompareToTarget(Sum(checkme)) == 0)
				{
					var sol = ", ".Join(checkme.OrderBy(i => i));
					solutions.Add(sol);
					Console.WriteLine(sol);
				}
			}
			else
			{
				var prime = remainingPrimes.Pop();

				// solve without prime:
				Solve(remainingPrimes, checkme);

				// solve with prime
				foreach (var combination in primesCombinations[prime])
				{
					var godeep = new HashSet<ulong>(checkme);
					foreach (var i in combination) godeep.Add(i);
					Solve(remainingPrimes, godeep);
				}

				remainingPrimes.Push(prime);
			}
		}

		private void CalcPrimesCombinations(ulong[] primes)
		{
			primesCombinations = new Dictionary<ulong, IList<ulong>[]>();

			primesCombinations[2] = new ulong[] { 2, 4, 8, 16, 32 }.Subsets(1).ToArray();
			Console.WriteLine(new { prime = 2, divisiblesByPrimeCnt = 5, variationsCnt = primesCombinations[2].Length });

//			{
//				var divisiblesBy3 = numbers.Where(n => n % 3 == 0).ToArray();
//
//				var sums = divisiblesBy3.Select
//
//				Console.WriteLine(new { prime = 3, divisiblesByPrimeCnt = divisiblesBy3.Length, variationsCnt = primesCombinationKvp.Value.Length });
//			}

			foreach (var prime in primes)
				if (prime >= 3)
				{
					var divisiblesByPrime = numbers.Where(n => n % prime == 0).ToArray();

					var combinations = divisiblesByPrime.Subsets(2)
						.Where(s => Sum(s).Simplify().Denumerator % prime != 0)
						.ToArray();

					if (combinations.Length > 0)
					{
						primesCombinations[prime] = combinations;

						Console.WriteLine(new { prime, divisiblesByPrimeCnt = divisiblesByPrime.Length, variationsCnt = combinations.Length });
					}
				}

			var forbiddenPrimes = primes.Where(p => p >= 3 && !primesCombinations.ContainsKey(p)).ToArray();
			numbers = numbers.Where(i => !forbiddenPrimes.Contains(i)).ToArray();

//			foreach (var primesCombinationKvp in primesCombinations)
//			{
//				var prime = primesCombinationKvp.Key;
//				Console.WriteLine(new { prime, variationsCnt = primesCombinationKvp.Value.Length });
//				foreach (var comb in primesCombinationKvp.Value)
//					Console.WriteLine(", ".Join(comb));
//			}
		}

		private static int CompareToTarget(FractionBigInt fraction)
		{
			return (fraction.Numerator << 1).CompareTo(fraction.Denumerator);
		}

		private static void Check()
		{
			var a = new ulong[] {2, 3, 4, 5, 7, 12, 15, 20, 28, 35};
			var r = Sum(a).Simplify();
			if (r.Numerator != 1 || r.Denumerator != 2)
				throw new InvalidOperationException();
		}

		private static void ShowSum(ulong[] a)
		{
			var r = Sum(a).Simplify();
			Console.WriteLine("Sum for: " + ", ".Join(a));
			Console.WriteLine(r);
		}

		private static FractionBigInt Sum(IEnumerable<ulong> integers)
		{
			var sum = new FractionBigInt(0, 1);
			foreach (var i in integers)
				sum = AddInverseSquare(sum, i);
			return sum;
		}

		private static FractionBigInt AddInverseSquare(FractionBigInt cur, ulong integer)
		{
			var isqr = integer * integer;
			var den = cur.Denumerator * isqr;
			var num = cur.Numerator * isqr + cur.Denumerator;

			return new FractionBigInt(num, den).Simplify();
		}
	}
}