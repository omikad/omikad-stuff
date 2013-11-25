using System;
using System.IO;
using System.Linq;

namespace ProblemSets
{
	public class Problem81
	{
		private static readonly string[] lines =
		{
			"131,673,234,103,18",
			"201,96,342,965,150",
			"630,803,746,422,111",
			"537,699,497,121,956",
			"805,732,524,37,331",
		};

		public static void Solve()
		{
			var matr = lines.Select(l => l.Split(',').Select(int.Parse).ToArray()).ToArray();
			SolveInternal(matr);

			SolveInternal(File.ReadAllLines("matrix81.txt").Select(l => l.Split(',').Select(int.Parse).ToArray()).ToArray());
		}

		public static void SolveInternal(int[][] matr)
		{
			var rows = matr.Length;
			var cols = matr[0].Length;

			var bests = new int[rows, cols];
			for (var r = 0; r < rows; r++) for (var c = 0; c < cols; c++) bests[r, c] = int.MaxValue;

			bests[0, 0] = matr[0][0];

			for (var c = 1; c < rows; c++)
				bests[0, c] = bests[0, c - 1] + matr[0][c];

			for (var r = 1; r < rows; r++)
			{
				bests[r, 0] = bests[r - 1, 0] + matr[r][0];

				for (var c = 1; c < cols; c++)
					bests[r, c] = Math.Min(bests[r - 1, c], bests[r, c - 1]) + matr[r][c];
			}

			Console.WriteLine(bests[rows - 1, cols - 1]);
		}
	}
}