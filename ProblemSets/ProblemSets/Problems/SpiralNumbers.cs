using System;
using System.ComponentModel.Composition;
using System.Linq;

namespace ProblemSets.Problems
{
	[Export]
	public class SpiralNumbers
	{
		public void Go()
		{
			SolveNoArray(7);
			Console.WriteLine();
			SolveNoArray(10);
		}

		private static void SolveNoArray(int n)
		{
			for (var y = 0; y < n; y++)
			{
				for (var x = 0; x < n; x++)
				{
					int d;

					if (x == 0 && y == 0) d = 1;

					else if (x >= y && (n - x) > y)
					{
						d = CalcDiagonalUp(y, n) + (x - y);
					}

					else if (x < y && (n - x) > y)
					{
						d = CalcDiagonalUp(x + 1, n) + (x - y);
					}

					else if (x >= y)
					{
						d = CalcDiagonalDown(x, n) + (y - x);
					}

					else
					{
						d = CalcDiagonalDown(y, n) + (y - x);
					}

					if (x > 0) Console.Write(", ");
					Console.Write("{0,3}", d);
				}
				Console.WriteLine();
			}
		}

		private static void ShowArray(int n, int[,] arr)
		{
			for (var y = 0; y < n; y++)
			{
				Console.WriteLine(
					string.Join(
						", ",
						Enumerable.Range(0, n).Select(x => string.Format("{0,3}", arr[x, y]))));
			}
		}

		private static int CalcDiagonalUp(int level, int n)
		{
			return 4 * level * (n - level) + 1;
		}

		private static int CalcDiagonalDown(int level, int n)
		{
			var diag = CalcDiagonalUp(level + 1, n) + 4 * (level - n / 2);
			if (n % 2 == 0)
				diag += 2;
			return diag;
		}

		private enum Direction
		{
			Right = 0,
			Down = 1,
			Left = 2,
			Up = 3,
		}

		private static void SolveArray(int n)
		{
			var arr = new int[n, n];

			var directions = new[,]
			{
				{1, 0},			  // Right
				{0, 1},			  // Down
				{-1, 0},		  // Left
				{0, -1},		  // Up
			};

			var dir = Direction.Right;
			var x = 0;
			var y = 0;

			for (var i = 1; i <= n * n; i++)
			{
				arr[x, y] = i;

				if      (dir == Direction.Right && x == n - y - 1) dir = Direction.Down;
				else if (dir == Direction.Down  && y == x)         dir = Direction.Left;
				else if (dir == Direction.Left  && x == n - y - 1) dir = Direction.Up;
				else if (dir == Direction.Up    && y == x + 1)     dir = Direction.Right;

				x += directions[(int) dir, 0];
				y += directions[(int) dir, 1];
			}

			ShowArray(n, arr);
		}
	}
}
