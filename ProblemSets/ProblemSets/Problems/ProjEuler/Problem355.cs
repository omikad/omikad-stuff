using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using ProblemSets.Services;

namespace ProblemSets.Problems.ProjEuler
{
	[Export]
	public class Problem355
	{
//		private const int max = 10;		// 30
//		private const int max = 30;		// 193
//		private const int max = 100;	// 1356
		private const int max = 1000;	// 
//		private const int max = 10000;	// 
//		private const int max = 30000;	// 
//		private const int max = 100000;	// 
//		private const int max = 200000;	// 

		public class DataItem
		{
			public DataItem(int number)
			{
				Number = number;
				PrimeFactorization = new List<int>();
			}

			public readonly int Number;
			public readonly List<int> PrimeFactorization;
			public string PrimeFactorizationStr;

			public override string ToString()
			{
				return Number.ToString();
			}
		}

		public void Go()
		{
			var dataArr = new DataItem[max + 1];
			for (var i = 0; i <= max; i++)
				dataArr[i] = new DataItem(i);

			for (var i = 2; i <= max; i++)
				if (dataArr[i].PrimeFactorization.Count == 0)
					for (var j = i; j <= max; j += i)
						dataArr[j].PrimeFactorization.Add(i);

			foreach (var item in dataArr)
				item.PrimeFactorizationStr = ",".Join(item.PrimeFactorization);

//			var data = dataArr.GroupBy(i => i.PrimeFactorizationStr).Select(gr => gr.OrderByDescending(d => d.Number).First()).ToArray();
//
//			{
//				// for max = 200000, total 121581 distinct prime factorizations
//				Console.WriteLine("Distinct prime factorizations: " + data.Length);
//
//				var max1 = data.Max(d => d.PrimeFactorization.Count);
//				var maxd = data.First(d => d.PrimeFactorization.Count == max1);
//				Console.WriteLine("Max prime factorizations length: " + max1 + " = " + maxd.PrimeFactorizationStr);
//			}

			var leaders = dataArr.GroupBy(i => i.PrimeFactorizationStr).Select(gr => gr.OrderByDescending(d => d.Number).ToArray()).ToArray();

			var bests = new long[max + 1];
			foreach (var samePrimeFactorization in leaders)
				foreach (var item in samePrimeFactorization)
					bests[item.Number] = samePrimeFactorization[0].Number;

			Console.WriteLine("Stage 1: " + leaders.Length);

			var maxFactors = dataArr.Max(d => d.PrimeFactorization.Count);

			for (var factorsCount = 2; factorsCount <= maxFactors; factorsCount++)
			{
				leaders = leaders.Where(item =>
				{
					var i = item[0];

					if (i.PrimeFactorization.Count != factorsCount) return true;

					foreach (var prime in i.PrimeFactorization)
					{
						long dp = prime;
						while (i.Number % (dp * prime) == 0) dp *= prime;

						var a = bests[dp];
						var b = bests[i.Number / dp];

						if (a + b >= i.Number)
						{
//							Console.WriteLine("Remove: " + i);

							foreach (var sub in item)
								bests[sub.Number] = a + b;

							return false;
						}
					}

					return true;
				}).ToArray();
				Console.WriteLine("Stage {0}: {1}", factorsCount, leaders.Length);
			}

			var candidates = leaders.Select(l => l[0]).ToList();

//			Console.WriteLine(
//				Environment.NewLine.Join(candidates.OrderBy(l => l.PrimeFactorization.Count).Select(l => new
//				{
//					n = l.Number,
//					fact = ", ".Join(l.PrimeFactorization)
//				})));

			var solution = Solve(new Stack<DataItem>(candidates), new List<DataItem>());

			Console.WriteLine(", ".Join(solution));
			Console.WriteLine(solution.Sum(s => s.Number));
		}

		private static List<DataItem> Solve(Stack<DataItem> remaining, List<DataItem> path)
		{
			if (remaining.Count == 0)
				return path;

			var item = remaining.Pop();

			var withItemRemaining = new Stack<DataItem>(remaining.Where(r => !r.PrimeFactorization.Intersect(item.PrimeFactorization).Any()));
			if (withItemRemaining.Count == remaining.Count)
			{
				// Can freely append item to the solution

				var newPath = new List<DataItem>(path) {item};
				var solution = Solve(remaining, newPath);

				remaining.Push(item);

				return solution;
			}

			// Now, remaining does not contain item

			var without = Solve(remaining, path);

			var withPath = new List<DataItem>(path) {item};
			var with = Solve(withItemRemaining, withPath);

			remaining.Push(item);

			return with.Sum(i => i.Number) > without.Sum(i => i.Number) ? with : without;
		}
	}

	[Export]
	public class Problem355_1
	{
//		private const int max = 10;		// 30
//		private const int max = 30;		// 193
//		private const int max = 100;	// 1348
//		private const int max = 1000;	// 84731
//		private const int max = 10000;	// 5956123		341 ms
		private const int max = 30000;	// 46779053		4672 ms
//		private const int max = 100000;	// 460440478	128526 ms
//		private const int max = 200000;	// 

		const int bytesLen = (max + 1) / 8 + 1;

		public void Go()
		{
			var sums = new ulong[bytesLen, 256];

			var start = 0ul;
			for (var bi = 0; bi < bytesLen; bi++)
			{
				for (var ii = 0; ii < 256; ii++)
					sums[bi, ii] = Sum(start, (byte)ii);
				start += 8;
			}
			Console.WriteLine("Sums calculated, bytes length: " + bytesLen);

			var primeFactors = new List<int>[max + 1];
			for (var i = 2; i <= max; i++)
				primeFactors[i] = new List<int>();

			for (var i = 2; i <= max; i++)
				if (primeFactors[i].Count == 0)
					for (var j = i; j <= max; j += i)
						primeFactors[j].Add(i);

			Console.WriteLine("Prime factors calculated");
			// primeFactors[i] = prime divisors of i


			var coprimeMasks = new BitArray[max + 1];
			for (var i = 2; i <= max; i++)
				if (primeFactors[i].Count == 1)
				{
					var mask = new BitArray(max + 1);
					for (var j = i; j <= max; j += i)
						mask.Set(j, true);
					coprimeMasks[i] = mask.Not();
				}

			Console.WriteLine("Coprime masks calculated");
			// coprimeMasks[i] = mask of numbers, that are coprime with i (i is prime)


			var coprimeFactorMasks = new BitArray[max + 1];
			for (var i = 2; i <= max; i++)
			{
				var ifactors = primeFactors[i];

				if (ifactors.Count == 1) continue;

				var coprimes = new BitArray(max + 1).Not();
				foreach (var prime in ifactors)
				{
					var coprime = coprimeMasks[prime];
					coprimes = coprimes.And(coprime);
				}

				coprimeFactorMasks[i] = coprimes;
			}
			Console.WriteLine("Coprime factor masks calculated");


			var set = new BitArray(max + 1);
			set.Set(1, true);
			var sum = 1ul;

			for (var i = 2; i <= max; i++)
			{
				var ifactors = primeFactors[i];
				if (ifactors.Count == 1)
				{
					if (ifactors[0] == i)
					{
						// i is prime
						var best = (int)MyMath.Pow((ulong)i, (ulong)(Math.Truncate(Math.Log(max, i))));
						set.Set(best, true);
						sum += (ulong) best;
					}
				}
				else
				{
					var newSet = new BitArray(set).And(coprimeFactorMasks[i]);
					newSet.Set(i, true);
					var newSum = Sum(sums, newSet);

					if (newSum > sum)
					{
						set = newSet;
						sum = newSum;
					}
				}
//				Console.WriteLine(i + ": " + ToString(set));
			}

			Console.WriteLine(sum);
		}

		private static string ToString(BitArray mask)
		{
			var sum = "";
			var i = 0ul;
			var isFirst = true;
			foreach (bool bit in mask)
			{
				if (bit) sum += isFirst ? i.ToString() : (", " + i);
				if (isFirst && bit) isFirst = false;
				i++;
			}
			return sum;
		}

		private static ulong Sum(ulong[,] sums, BitArray mask)
		{
			var arr = new byte[bytesLen];
			mask.CopyTo(arr, 0);

			var sum = 0ul;

			for (var bi = 0; bi < bytesLen; bi++)
				sum += sums[bi, arr[bi]];

			return sum;
		}

		private static ulong Sum(ulong i, byte b)
		{
			var sum = 0ul;
			for (var ii = 0; ii < 8; ii++)
			{
				if ((b & 1) == 1) sum += i;
				b >>= 1;
				i++;
			}
			return sum;
		}
	}
}