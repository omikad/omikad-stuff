using System;
using System.Linq;
using ProblemSets.Services;

namespace ProblemSets.Problems.Hr
{
	public class Sudoku
	{
		public void Go()
		{
			Solve(
@"0 0 0 0 0 0 0 0 0
  0 0 8 0 0 0 0 4 0
  0 0 0 0 0 0 0 0 0
  0 0 0 0 0 6 0 0 0
  0 0 0 0 0 0 0 0 0
  0 0 0 0 0 0 0 0 0
  2 0 0 0 0 0 0 0 0
  0 0 0 0 0 0 2 0 0
  0 0 0 0 0 0 0 0 0");

			Solve(
@"0 0 0 0 5 3 9 0 0
  0 0 0 8 0 0 0 4 0
  0 0 0 9 0 0 0 0 5
  0 8 5 0 0 0 0 0 2
  4 0 0 0 0 0 0 0 1
  2 0 0 0 0 0 3 6 0
  7 0 0 0 0 4 0 0 0
  0 5 0 0 0 6 0 0 0
  0 0 9 3 1 0 0 0 0");
		}

		private static void Solve(string input)
		{
			var data = Convert(input);

			data.Print();
			Console.WriteLine();

			var flat = Flat(data);

			Solve2(flat);

			data = FlatBack(flat);

			data.Print();
		}

		private static int[,] Convert(string input)
		{
			return input.SplitToLines().ToTwoDimensional(line => line.SplitBySpaces().Select(int.Parse).ToArray());
		}

		private static int[] Flat(int[,] input)
		{
			var arr = new int[81];
			for (var i = 0; i < 9; i++)
				for (var j = 0; j < 9; j++)
					arr[9 * i + j] = input[i, j];
			return arr;
		}

		private static int[,] FlatBack(int[] input)
		{
			var arr = new int[9,9];
			for (var i = 0; i < 9; i++)
				for (var j = 0; j < 9; j++)
					arr[i, j] = input[9 * i + j];
			return arr;
		}

		private static void Solve(int[,] grid)
		{
			var copy = new int[9,9];
			Array.Copy(grid, 0, copy, 0, 81);

			var checkArray = new int[10];

			Func<Func<int, int>, bool> checkFunc = getNumber =>
				{
					Array.Clear(checkArray, 0, 10);
					for (var i = 0; i < 9; i++)
					{
						var num = getNumber(i);
						var before = checkArray[num]++;
						if (num > 0 && before > 0)
							return false;
					}
					return true;
				};

			Func<int, bool> checkCol = c => checkFunc(i => grid[c, i]);

			Func<int, bool> checkRow = c => checkFunc(i => grid[i, c]);

			Func<int, int, bool> checkSquare = (c, r) => checkFunc(i => grid[3 * c + i / 3, 3 * r + i % 3]);

			var x = 0;
			var y = 0;
			while (true)
			{
				if (copy[x, y] == 0)
				{
					var ok = false;

					if (grid[x, y] < 9)
						for (var i = grid[x, y] + 1; i <= 9; i++)
						{
							grid[x, y] = i;
							if (checkCol(x) && checkRow(y) && checkSquare(x / 3, y / 3))
							{
								ok = true;
								break;
							}
						}

					if (!ok)
					{
						grid[x, y] = 0;

						do
						{
							x--;
							if (x < 0)
							{
								x = 8;
								y--;
							}
						} while (copy[x, y] > 0);

						continue;
					}
				}

				x++;
				if (x == 9)
				{
					x = 0;
					y++;
					if (y == 9)
						return;
				}
			}
		}

		private static void Solve2(int[] grid)
		{
			var copy = grid.ToArray();

			var arr = new int[10];

			Func<Func<int, int>, bool> check = get =>
				{
					Array.Clear(arr, 0, 10);
					for (var i = 0; i < 9; i++)
					{
						var n = get(i);
						var c = arr[n]++;
						if (n > 0 && c > 0)
							return false;
					}
					return true;
				};

			Func<int, int, bool> checks = (c, r) =>
			    check(i => grid[c * 9 + i]) 
				&& check(i => grid[i * 9 + r]) 
				&& check(i => grid[(c / 3 * 3 + i / 3) * 9 + r / 3 * 3 + i % 3]);

			var x = 0;
			while (true)
			{
				if (copy[x] == 0)
				{
					var ok = false;

					var n = grid[x];

					if (n < 9)
						for (var i = n + 1; i <= 9; i++)
						{
							grid[x] = i;
							if (checks(x / 9, x % 9))
							{
								ok = true;
								break;
							}
						}

					if (!ok)
					{
						grid[x] = 0;
						do { x--; } while (copy[x] > 0);
						continue;
					}
				}

				if (++x == 81) return;
			}
		}
	}
}