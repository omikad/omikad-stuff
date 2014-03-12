using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Reflection;
using ProblemSets.Services;

namespace ProblemSets.Problems.ProjEuler
{
	[Export]
	public class Problem152
	{
		private const int max = 75;

		private class Candidate
		{
			public BitArray Bits;
			public FractionBigInt Summa;
		}

		public void Go()
		{
			Console.WriteLine("{0}: ", max);

			var fiBitArrayMArray = typeof(BitArray).GetField("m_array", BindingFlags.NonPublic | BindingFlags.Instance);

			var primes = MyMath.ConvertSieveToPrimes(MyMath.CreatePrimesSieve(max));

			// Because there is no combinations for them:
			var forbiddenPrimes = new[] { 11,17,19,23,29,31,37,41,43,47,53,59,61,67,71,73,79 };

			var numbers = Enumerable
				.Range(2, max - 1)
				.Where(i => forbiddenPrimes.All(p => i % p != 0))
				.Select(i => (ulong)i)
				.ToArray();

			var map = numbers.Select((n, i) => new {n, i}).ToDictionary(a => a.n, a => a.i);

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
						var newCombBits = new BitArray(combBits).Or(prevComb.Bits);
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

					var remainingBits = new BitArray(allBits).Xor(comb.Bits).And(allBut23mask);

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

						var addedThrees = new BitArray(threesMask).And(combBits);

						foreach (var prevComb in primesCombinations)
						{
							if (!BitArrayContains(fiBitArrayMArray, prevComb.Bits, addedThrees))
								continue;

							if (!BitArrayContains(fiBitArrayMArray, prevComb.Bits, combBits))
							{
								var newCombBits = new BitArray(combBits).Or(prevComb.Bits);

								var addition = new BitArray(newCombBits).Xor(prevComb.Bits);

								var sum = prevComb.Summa + Sum(numbers, addition);

								var compare = CompareToTarget(sum);

								if (compare > 0)
									continue;

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

		private static string BitArrayToString(ulong[] numbers, BitArray arr)
		{
			return
				", ".Join(
					arr.Cast<bool>().Select((f, i) => new { f, i }).Where(a => a.f).Select(a => numbers[a.i]).OrderBy(n => n));
		}

		private static int CompareToTarget(FractionBigInt fraction)
		{
			return (fraction.Numerator << 1).CompareTo(fraction.Denumerator);
		}

		private static bool BitArrayContains(FieldInfo fiBitArrayMArray, BitArray big, BitArray small)
		{
			var a = (int[])fiBitArrayMArray.GetValue(big);
			var b = (int[])fiBitArrayMArray.GetValue(small);

			for (var i = 0; i < a.Length; i++)
			{
				if (b[i] != (a[i] & b[i]))
					return false;
			}
			return true;
		}

		private static BitArray ConvertToBitArray(Dictionary<ulong, int> map, IEnumerable<ulong> combination)
		{
			var mask = new BitArray(map.Count);
			foreach (var n in combination) mask.Set(map[n], true);
			return mask;
		}

		private static FractionBigInt Sum(IEnumerable<ulong> integers)
		{
			var sum = new FractionBigInt(0, 1);
			foreach (var i in integers)
				sum = AddInverseSquare(sum, i);
			return sum.Simplify();
		}

		private static FractionBigInt Sum(ulong[] numbers, BitArray mask)
		{
			var sum = new FractionBigInt(0, 1);
			var i = 0;
			foreach (bool flag in mask)
			{
				if (flag)
					sum = AddInverseSquare(sum, numbers[i]);
				i++;
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