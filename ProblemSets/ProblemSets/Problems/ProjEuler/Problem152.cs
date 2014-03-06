using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using ProblemSets.Services;

namespace ProblemSets.Problems.ProjEuler
{
	[Export]
	public class Problem152
	{
		private const int max = 35;

		private Dictionary<ulong, FractionBigInt> Suffixes;

		public void Go()
		{
			Check();

			var numbers = Enumerable.Range(2, max - 1).Select(i => (ulong) i).ToArray();

			Suffixes = numbers
				.ToDictionary(
					i => i,
					i => Sum(numbers.Skip((int) i - 2).ToArray())
				);

			Console.WriteLine("Total sum:\r\n" + Sum(numbers).Simplify());
			Console.WriteLine();
			foreach (var kvp in Suffixes.Take(2).Concat(Suffixes.Skip(numbers.Length - 2)))
			{
				Console.WriteLine("{0}: ", kvp.Key);
				Console.WriteLine(kvp.Value);
				Console.WriteLine();
			}

			Solve(2, new Stack<ulong>(max), new FractionBigInt(0, 1));
		}

		private void Solve(ulong integer, Stack<ulong> path, FractionBigInt pathSum)
		{
			if (integer > max) return;

			if (!CanProceed(path, pathSum, integer))
				return;

			path.Push(integer);
			var sum = AddInverseSquare(pathSum, integer);

			Solve(integer + 1, path, sum);

			path.Pop();
			Solve(integer + 1, path, pathSum);
		}

		private bool CanProceed(Stack<ulong> path, FractionBigInt sum, ulong integer)
		{
			var compare = CompareToTarget(sum);

			if (compare > 0) return false;
			if (compare == 0)
			{
				Console.WriteLine(", ".Join(path.Reverse()));
				Console.WriteLine();
				return false;
			}

			if (path.Count >= 6)
			{
				var maxPossible = sum + Suffixes[integer];
				if (CompareToTarget(maxPossible) < 0)
					return false;
			}

			return true;
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

		private static FractionBigInt Sum(ulong[] integers)
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