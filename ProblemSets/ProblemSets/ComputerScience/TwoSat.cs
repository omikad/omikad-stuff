using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ProblemSets.Services;

namespace ProblemSets.ComputerScience
{
	public class TwoSat
	{
		public void Go()
		{
		}

		/// <summary>
		/// Solve 2-SAT and returns indices of elements which are true
		/// </summary>
		public int[] Solve_Papadimitrou(int n, string[] input)
		{
			var randInts = new int[n / 32 + 1];
			
			var clauses = input
				.Select(s => s.SplitBySpaces().Select(int.Parse).ToArray())
				.ToList();
			var originalClausesCount = clauses.Count;

			var knownTrue = new BitArray(n + 1);
			var knownFalse = new BitArray(n + 1);
			knownFalse.SetAll(true);

			SimplifyClauses(clauses, knownTrue, knownFalse);

			var variables = Enumerable.Range(1, n).Count(i => !knownTrue[i] && knownFalse[i]);

			Console.WriteLine("Clauses length original: {0}, optimized: {1}, variables: {2}", 
				originalClausesCount, clauses.Count, variables);

			var random = new Random();

			var innerLoopCount = 2 * variables * variables;

			for (var i = 0; i < Math.Log(n, 2) + 1; i++)
			{
				var x = CreateRandomSolution(random, randInts, n);
				x.Or(knownTrue);
				x.And(knownFalse);

				for (var j = 0; j < innerLoopCount; j++)
				{
					var unsatisfieds = clauses.Where(c => !IsSatisfied(x, c)).ToArray();

					if (unsatisfieds.Length == 0)
						return CreateResult(x, n);
					
					var arbitrary = Math.Abs(unsatisfieds.Random(random).Random(random));

					var flipme = x[arbitrary];

					x.Set(arbitrary, !flipme);
				}
			}

			return null;
		}

		private static void SimplifyClauses(List<int[]> clauses, BitArray knownTrue, BitArray knownFalse)
		{
			bool found;
			do
			{
				found = false;

				bool foundOneTime;
				do
				{
					foundOneTime = FindAlwaysTrue(clauses, knownTrue);
					found |= foundOneTime;
				} while (foundOneTime);

				do
				{
					foundOneTime = FindAlwaysFalse(clauses, knownFalse);
					found |= foundOneTime;
				} while (foundOneTime);
			} while (found);
		}

		private static bool FindAlwaysTrue(List<int[]> clauses, BitArray knownTrue)
		{
			var canBeFalse = new BitArray(knownTrue.Length);
			foreach (var clause in clauses)
				foreach (var x in clause)
					if (x < 0)
						canBeFalse.Set(-x, true);

			for (var i = 1; i < knownTrue.Length; i++)
				if (!canBeFalse[i])
					knownTrue.Set(i, true);

			var cnt = clauses.Count;
			clauses.RemoveAll(c => knownTrue[Math.Abs(c[0])] || knownTrue[Math.Abs(c[1])]);
			return cnt != clauses.Count;
		}

		private static bool FindAlwaysFalse(List<int[]> clauses, BitArray knownFalse)
		{
			var canBeTrue = new BitArray(knownFalse.Length);
			foreach (var clause in clauses)
				foreach (var x in clause)
					if (x > 0)
						canBeTrue.Set(x, true);

			for (var i = 1; i < knownFalse.Length; i++)
				if (!canBeTrue[i])
					knownFalse.Set(i, false);

			var cnt = clauses.Count;
			clauses.RemoveAll(c => !knownFalse[Math.Abs(c[0])] || !knownFalse[Math.Abs(c[1])]);
			return cnt != clauses.Count;
		}

		private static int[] CreateResult(BitArray bitArray, int n)
		{
			var result = new List<int>();
			for (var i = 1; i <= n; i++)
				if (bitArray[i])
					result.Add(i);
			return result.ToArray();
		}

		private static bool IsSatisfied(BitArray x, int[] clause)
		{
			var c1 = clause[0];
			var c2 = clause[1];

			var a = x[Math.Abs(c1)];
			var b = x[Math.Abs(c2)];

			if (c1 < 0) a = !a;
			if (c2 < 0) b = !b;

			return a || b;
		}

		private static BitArray CreateRandomSolution(Random random, int[] randInts, int n)
		{
			for (var i = 0; i < randInts.Length; i++)
				randInts[i] = random.Next();

			var randomSolution = new BitArray(randInts) { Length = n + 1 };

			return randomSolution;
		}
	}
}
