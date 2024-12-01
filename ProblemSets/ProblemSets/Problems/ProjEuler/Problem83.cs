using System;
using System.IO;
using System.Linq;

namespace ProblemSets
{
	public class Problem83
	{
		// 425185

		private static readonly string[] lines =
		{
			"131,673,234,103,18",
			"201,96,342,965,150",
			"630,803,746,422,111",
			"537,699,497,121,956",
			"805,732,524,37,331",
		};

		private static int rows;
		private static int cols;

		private static int[][] matr;
		private static int[,] bests;
		private static bool[,] worked;

		private static readonly int[] dirC = {-1, 0, 1, 0};
		private static readonly int[] dirR = {0, -1, 0, 1};

		public static void Solve()
		{
//			matr = lines.Select(l => l.Split(',').Select(int.Parse).ToArray()).ToArray();
			matr = File.ReadAllLines("Content\\matrix83.txt").Select(l => l.Split(',').Select(int.Parse).ToArray()).ToArray();

			SolveInternal();
		}
		
		public static void SolveInternal()
		{
			rows = matr.Length;
			cols = matr[0].Length;

			bests = new int[rows, cols];
			for (var r = 0; r < rows; r++) for (var c = 0; c < cols; c++) bests[r, c] = int.MaxValue;

			worked = new bool[rows, cols];

			bests[0, 0] = matr[0][0];
			worked[0, 0] = true;

			// O(n^4)
			for (var i = 1; i < rows * cols; i++)
			{
				int r, c, w;
				FindMinNotWorked(out r, out c, out w);

				bests[r, c] = w;
				worked[r, c] = true;
			}

			Console.WriteLine(bests[rows - 1, cols - 1]);
		}

		private static void FindMinNotWorked(out int rmin, out int cmin, out int weight)
		{
			// O(n^2)

			weight = int.MaxValue;
			rmin = int.MaxValue;
			cmin = int.MaxValue;

			for (var r = 0; r < rows; r++)
				for (var c = 0; c < cols; c++)
				{
					if (worked[r, c]) continue;

					for (var d = 0; d < 4; d++)
					{
						var newR = dirR[d] + r;
						var newC = dirC[d] + c;

						if (newC >= 0 && newC < cols && newR >= 0 && newR < rows && worked[newR, newC])
						{
							var candidate = bests[newR, newC] + matr[r][c];
							if (candidate < weight)
							{
								weight = candidate;
								rmin = r;
								cmin = c;
							}
						}
					}
				}
		}
	}
}