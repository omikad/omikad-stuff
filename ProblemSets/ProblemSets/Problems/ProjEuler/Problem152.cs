using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using ProblemSets.Services;

namespace ProblemSets.Problems.ProjEuler
{
	[Export]
	public class Problem152
	{

// All combinations: 19466745
// Total distinct solutions: 301   <- Same as for max = 75. Possible improvement
// Elapsed: 94468
// Memory: 3 Gb

		private const int max = 80;

		private class Candidate
		{
			public ulong Bits;
			public FractionBigInt Summa;
		}

		public void Go()
		{
			Console.WriteLine("{0}: ", max);

			var primes = MyMath.ConvertSieveToPrimes(MyMath.CreatePrimesSieve(max));

			// Because there are no combinations for them:
			var forbiddenPrimes = new[] { 11,17,19,23,29,31,37,41,43,47,53,59,61,67,71,73,79 };

			var numbers = Enumerable
				.Range(2, max - 1)
				.Where(i => forbiddenPrimes.All(p => i % p != 0))
				.Select(i => (ulong)i)
				.ToArray();

			var map = numbers.Select((n, i) => new {n, i}).ToDictionary(a => a.n, a => 1ul << a.i);

			// Console.WriteLine(CompareToTarget(Sum(numbers.Where(n => n >= 3)))); // -1 => 1/2 must be in the sum

			var powersOf2 = new ulong[] { 2, 4, 8, 16, 32, 64 };

			var primesCombinations = powersOf2
				.Where(n => n <= max)
				.ToArray()
				.Subsets(1)
				.Where(comb => comb.Contains(2))
				.Select(comb => new Candidate { Bits = ConvertToBitArray(map, comb), Summa = Sum(comb) })
				.ToList();

			var threes = numbers.Where(n => n % 3 == 0).ToArray();		// for max = 80, threes.Length = 26

			var threesMask = ConvertToBitArray(map, threes);

			{
				var timer = Stopwatch.StartNew();

				var squares = threes.Select(t => new BigInteger(t * t / 9)).ToArray();

				var result = new List<int[]>();

				for (var i = 0; i < squares.Length; i++)
					FindAllowedThrees(result, new Stack<int>(), 0, 1, i, squares);

				var copy = primesCombinations.ToList();

				foreach (var combIndices in result)
				{
					var combBits = ConvertToBitArray(map, combIndices.Select(i => threes[i]));

					foreach (var prevComb in primesCombinations)
					{
						var newCombBits = combBits | prevComb.Bits;
						copy.Add(new Candidate { Bits = newCombBits, Summa = Sum(numbers, newCombBits) });
					}
				}

				primesCombinations = copy;

				Console.WriteLine("Threes total: {0}; calc: {1}", result.Count, timer.ElapsedMilliseconds);
			}

			{
				var allBits = ConvertToBitArray(map, numbers);
				var allBut23mask = ConvertToBitArray(map, numbers.Where(n => n % 3 != 0 && !powersOf2.Contains(n)));

				var copy = new List<Candidate>();

				foreach (var comb in primesCombinations)
				{
					var sum = Sum(numbers, comb.Bits);

					if (CompareToTarget(sum) > 0)
						continue;

					var remainingBits = (allBits - comb.Bits) & allBut23mask;

					if (CompareToTarget(Sum(numbers, remainingBits) + sum) >= 0)
						copy.Add(comb);
				}

				primesCombinations = copy;
			}

			var solutions = new HashSet<string>(); // Принимаем на веру, что нет решений только из 2 и 3

			foreach (var prime in primes)
				if (prime >= 5)
				{
					var divisiblesByPrime = numbers.Where(n => n % prime == 0).ToArray();

					var combinations = divisiblesByPrime.Subsets(2)
						.Where(s => Sum(s).Denumerator % prime != 0)
						.ToArray();

//					Console.WriteLine(new { prime, combs = combinations.Length });

					if (combinations.Length == 0) continue;

					var copy = primesCombinations.ToList();

					foreach (var comb in combinations)
					{
						var combBits = ConvertToBitArray(map, comb);

						var addedThrees = threesMask & combBits;

						foreach (var prevComb in primesCombinations)
						{
							if (!BitArrayContains(prevComb.Bits, addedThrees))
								continue;

							if (!BitArrayContains(prevComb.Bits, combBits))
							{
								var newCombBits = combBits | prevComb.Bits;

								var addition = newCombBits ^ prevComb.Bits;

								var sum = prevComb.Summa + Sum(numbers, addition);

								var compare = CompareToTarget(sum);

								if (compare > 0)
									continue;

								if (prime < 13)
									copy.Add(new Candidate { Bits = newCombBits, Summa = sum });

								if (compare == 0)
								{
									var sol = BitArrayToString(numbers, newCombBits);
									solutions.Add(sol);
									Console.WriteLine(sol);
								}
							}
						}
					}

					primesCombinations = copy;
				}
		
			Console.WriteLine("All combinations: " + primesCombinations.Count);

			Console.WriteLine("Total distinct solutions: " + solutions.Count);
		}

		private static ulong GetPowThree(BigInteger n)
		{
			var num = n;
			var i = 0ul;
			var three = new BigInteger(3);

			while (true)
			{
				BigInteger remainder;
				num = BigInteger.DivRem(num, three, out remainder);

				if (remainder != 0 || num == 0)
					return i;

				i++;
			}
		}

		private static void FindAllowedThrees(List<int[]> result, Stack<int> path, BigInteger num, BigInteger den, int i, BigInteger[] squares)
		{
			path.Push(i);

			var square = squares[i];

			var newNum = num * square + den;
			var newDen = den * square;

			var num3 = GetPowThree(newNum);
			var den3 = GetPowThree(newDen);

			if (num3 >= den3 + 2)
				result.Add(path.ToArray());

			for (var j = i + 1; j < squares.Length; j++)
				FindAllowedThrees(result, path, newNum, newDen, j, squares);

			path.Pop();
		}

		private static string BitArrayToString(ulong[] numbers, ulong mask)
		{
			return ", ".Join(GetIndices(mask).Select(i => numbers[i]).OrderBy(n => n));
		}

		private static IEnumerable<int> GetIndices(ulong mask)
		{
			var i = 0;
			while (mask > 0)
			{
				if (mask % 2 != 0)
					yield return i;
				i++;
				mask >>= 1;
			}
		}

		private static int CompareToTarget(FractionBigInt fraction)
		{
			return (fraction.Numerator << 1).CompareTo(fraction.Denumerator);
		}

		private static bool BitArrayContains(ulong big, ulong small)
		{
			return small == (big & small);
		}

		private static ulong ConvertToBitArray(Dictionary<ulong, ulong> map, IEnumerable<ulong> combination)
		{
			var mask = 0ul;
			foreach (var n in combination) mask |= map[n];
			return mask;
		}

		private static FractionBigInt Sum(IEnumerable<ulong> integers)
		{
			var sum = new FractionBigInt(0, 1);
			foreach (var i in integers)
				sum = AddInverseSquare(sum, i);
			return sum.Simplify();
		}

		private static FractionBigInt Sum(ulong[] numbers, ulong mask)
		{
			var sum = new FractionBigInt(0, 1);
			var i = 0;
			while (mask > 0)
			{
				if (mask % 2 != 0)
					sum = AddInverseSquare(sum, numbers[i]);
				i++;
				mask >>= 1;
			}
			return sum.Simplify();
		}

		private static FractionBigInt AddInverseSquare(FractionBigInt cur, ulong integer)
		{
			var isqr = integer * integer;
			var den = cur.Denumerator * isqr;
			var num = cur.Numerator * isqr + cur.Denumerator;

			return new FractionBigInt(num, den);
		}
	}
}