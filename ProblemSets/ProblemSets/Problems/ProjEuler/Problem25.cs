using System;
using System.ComponentModel.Composition;
using System.Numerics;
using ProblemSets.Services;

namespace ProblemSets.Problems.ProjEuler
{
	[Export]
	public class Problem25
	{
		public void Go()
		{
			var gen = new BigInteger[,]
			{
				{ 0, 1 },
				{ 1, 1 },
			};

			var f1 = new BigInteger[,]
			{
				{ 0, 0 },
				{ 0, 1 },
			};

			const int desiredLen = 1000;
			const int maxN = 100000;

			var result = 0;

			EnumerableHelper.BinarySearch(maxN, n =>
			{
				var fibn = GetFib(gen, f1, n);
				var len = fibn.ToString().Length;

				result = n;

				if (len < desiredLen) return 1; // go right
				return -1; // go left
			});

			Console.WriteLine(result);

			Console.WriteLine(new { n = 11, len = GetFib(gen, f1, 11).ToString().Length });
			Console.WriteLine(new { n = 12, len = GetFib(gen, f1, 12).ToString().Length });
			Console.WriteLine(new { n = result - 1, len = GetFib(gen, f1, result - 1).ToString().Length });
			Console.WriteLine(new { n = result, len = GetFib(gen, f1, result).ToString().Length });
		}

		private static BigInteger GetFib(BigInteger[,] gen, BigInteger[,] f1, int n)
		{
			return Mul(Pow(gen, n - 1), f1)[1, 1];
		}

		private static void Print(BigInteger[,] matr)
		{
			Console.WriteLine("{");
			Console.WriteLine("  " + matr[0, 0] + ",");
			Console.WriteLine("  " + matr[0, 1] + ",");
			Console.WriteLine("  " + matr[1, 0] + ",");
			Console.WriteLine("  " + matr[1, 1] + ",");
			Console.WriteLine("}");
		}

		private static BigInteger[,] Pow(BigInteger[,] a, int pow)
		{
			var result = a;
			var multiplier = a;
			pow--;

			while (true)
			{
				if ((pow & 1) != 0)
					result = Mul(multiplier, result);

				pow = pow >> 1;
				
				if (pow == 0)
					return result;

				multiplier = Mul(multiplier, multiplier);
			}
		}

		private static BigInteger[,] Mul(BigInteger[,] left, BigInteger[,] right)
		{
			return new[,]
			{
				{
					left[0, 0] * right[0, 0] + left[0, 1] * right[1, 0],
					left[0, 0] * right[0, 1] + left[0, 1] * right[1, 1]
				},
				{
					left[1, 0] * right[0, 0] + left[1, 1] * right[1, 0],
					left[1, 0] * right[0, 1] + left[1, 1] * right[1, 1]
				},
			};
		}
	}
}