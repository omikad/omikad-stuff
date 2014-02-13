using System;
using System.ComponentModel.Composition;
using System.Linq;
using ProblemSets.Services;

namespace ProblemSets.Problems
{
	[Export]
	public class FindLargestRectangleIn2D
	{
		public void Go()
		{
			var rect = new[]
			{
				"100010101011111111111",
				"100011111010000001111",
				"101011111010000001111",
				"100011111010000001111",
				"100010101010000101110",
				"100010101010000001111",
			};
			Solve(rect.ToTwoDimensional(s => s));
		}

		private static void Solve(char[,] rect)
		{
			rect.Print();

			var solutionX = new int[rect.GetLength(0), rect.GetLength(1)];
			var solutionY = new int[rect.GetLength(0), rect.GetLength(1)];

			var maxY = rect.GetLength(0);
			var maxX = rect.GetLength(1);

			for (var i = maxY - 1; i >= 0; i--)
			{
				solutionX[i, maxX - 1] = maxX - 1;
				solutionY[i, maxX - 1] =
					i == maxY - 1 || rect[i + 1, maxX - 1] != rect[i, maxX - 1]
						? i
						: solutionY[i + 1, maxX - 1];
			}

			for (var j = maxX - 1; j >= 0; j--)
			{
				solutionY[maxY - 1, j] = maxY - 1;
				solutionX[maxY - 1, j] =
					j == maxX - 1 || rect[maxY - 1, j + 1] != rect[maxY - 1, j]
						? j
						: solutionX[maxY - 1, j + 1];
			}

			for (var i = maxY - 1; i >= 0; i--)
			{
				for (var j = maxX - 1; j >= 0; j--)
				{
					
				}
			}


			Console.WriteLine("solutionX:");
			Print(solutionX);
			Console.WriteLine("solutionY:");
			Print(solutionY);
		}

		private static void Print(int[,] arr)
		{
			foreach (var row in arr.EnumerateRows())
				Console.WriteLine(" ".Join(row.Select(i => string.Format("{0,2}", i))));
			Console.WriteLine();
		}
	}
}