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
			Console.WriteLine(input);
			Console.WriteLine();

			var data = Convert(input);

			Solve(data);

			foreach (var k in digits)
				Console.WriteLine(string.Join(" ", digits.Select(j => data[k * 9 + j])));

			Console.WriteLine();
			Console.WriteLine();
			Console.WriteLine();
			Console.WriteLine();
		}

		private static int[] Convert(string input)
		{
			return input.SplitToLines().SelectMany(line => line.SplitBySpaces().Select(int.Parse)).ToArray();
		}
		
		private static readonly int[] digits = { 0, 1, 2, 3, 4, 5, 6, 7, 8 };

		private static void Solve(int[] grid)
		{
			var copy = grid.ToArray();

			var checkArray = new int[10];

			Func<Func<int, int>, bool> check = get =>
			{
				Array.Clear(checkArray, 0, 10);
				return !digits.Any(i =>
				{
					var n = get(i);
					var c = checkArray[n]++;
					return n > 0 && c > 0;
				});
			};

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

							var c = x / 9;
							var r = x % 9;

							if (check(j => grid[c * 9 + j])
								&& check(j => grid[j * 9 + r])
								&& check(j => grid[(c / 3 * 3 + j / 3) * 9 + r / 3 * 3 + j % 3]))
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
				if (++x == 81) break;
			}
		}
	}
}