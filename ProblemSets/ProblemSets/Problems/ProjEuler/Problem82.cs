using System;
using System.IO;
using System.Linq;

namespace ProblemSets
{
	public class Problem82
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
			//262161
			//266288
			//260324

			SolveInternal();
		}

		public static void SolveInternal()
		{
//			var matr = lines.Select(l => l.Split(',').Select(int.Parse).ToArray()).ToArray();

			var matr = File.ReadAllLines("Content\\matrix82.txt").Select(l => l.Split(',').Select(int.Parse).ToArray()).ToArray();

			var rows = matr.Length;
			var cols = matr[0].Length;

			var bests = new int[rows, cols];

			for (var r = 0; r < rows; r++) bests[r, 0] = matr[r][0];

			for (var r = 0; r < rows; r++) for (var c = 1; c < cols; c++) bests[r, c] = int.MaxValue;

			for (var i = 0; i < 16; i++)
			{
				for (var r = 0; r < rows; r++)
					for (var c = 1; c < cols; c++)
					{
						bests[r, c] = matr[r][c] + Math.Min(

							bests[r, c - 1],

							(r == 0) ? bests[r + 1, c]
							: (r == rows - 1) ? bests[r - 1, c]
							: Math.Min(bests[r - 1, c], bests[r + 1, c]));
					}
			}

			Console.WriteLine(Enumerable.Range(0, rows).Select(r => bests[r, cols - 1]).Min());
		}
	}
}