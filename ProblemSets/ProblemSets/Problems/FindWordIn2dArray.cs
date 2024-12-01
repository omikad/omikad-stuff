using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using ProblemSets.ComputerScience.DataTypes;
using ProblemSets.Services;

namespace ProblemSets.Problems
{
	[Export]
	public class FindWordIn2dArray
	{
		public void Go()
		{
			Solve("microsoft", new[]
			{
				"abcrd",
				"emogh",
				"istkc",
				"wofmi",
				"zrcit",
			}.ToTwoDimensional(s => s));
		}

		private readonly int[][] directions =
		{
			new[] {-1, 0},
			new[] {0, 1},
			new[] {1, 0},
			new[] {0, -1},
		};

		private void Solve(string pattern, char[,] board)
		{
			// Time O(boardheight * boardwidth * patterlen * countofsolutions)

			board.Print();

			var dp = new DpArray(board);

			for (var pathLen = 0; pathLen < pattern.Length; pathLen++)
			{
				var searchme = pattern[pathLen];

				for (var i = 0; i < board.GetLength(0); i++)
					for (var j = 0; j < board.GetLength(1); j++)
					{
						if (board[i, j] != searchme) continue;

						if (pathLen == 0)
						{
							dp.AddFirstCandidate(i, j);
						}
						else
						{
							foreach (var dir in directions)
							{
								var ii = i + dir[0];
								var jj = j + dir[1];
								if (board.AreIndicesAllowed(ii, jj) && board[ii, jj] == pattern[pathLen - 1])
								{
									dp.AddCandidate(i, j, ii, jj, pathLen);
								}
							}
						}
					}

				dp.Print();
			}

			foreach (var solution in dp.GetSolutions(pattern.Length))
			{
				Console.WriteLine(Environment.NewLine.Join(solution.Path.Select(p => new { row = p.Y, col = p.X, ch = board[p.X, p.Y] })));
				Console.WriteLine();
			}
		}

		private class DpArray
		{
			private readonly char[,] board;
			private readonly List<Candidate>[,] dpArr;

			public DpArray(char[,] board)
			{
				this.board = board;
				dpArr = new List<Candidate>[board.GetLength(0), board.GetLength(1)];
			}

			public IEnumerable<Candidate> GetSolutions(int pathLen)
			{
				foreach (var row in dpArr.EnumerateRows())
					foreach (var list in row)
					{
						if (list == null || list.Count == 0) continue;

						foreach (var solution in list.Where(c => c.Path.Count == pathLen))
							yield return solution;
					}
			}

			public void Print()
			{
				foreach (var row in dpArr.EnumerateRows())
				{
					foreach (var list in row)
					{
						if (list == null || list.Count == 0) Console.Write('.');
						else
						{
							var top = list[0].Path.Last();
							Console.Write(board[top.X, top.Y]);
						}
					}
					Console.WriteLine();
				}
				Console.WriteLine();
			}

			public void AddCandidate(int i, int j, int ipred, int jpred, int pathLen)
			{
				var predCandidates = dpArr[ipred, jpred];
				if (predCandidates == null) return;
				predCandidates.RemoveAll(c => c.Path.Count < pathLen);

				if (predCandidates.Count == 0) return;
				var curCandidates = dpArr[i, j] ?? (dpArr[i, j] = new List<Candidate>());
				curCandidates.RemoveAll(c => c.Path.Count < pathLen);

				curCandidates.AddRange(predCandidates.Select(predCandidate => new Candidate(predCandidate, i, j)));
			}

			public void AddFirstCandidate(int i, int j)
			{
				dpArr[i, j] = new List<Candidate> { new Candidate(i, j) };
			}
		}

		private class Candidate
		{
			public Candidate(Candidate predCandidate, int i, int j)
			{
				Path = new List<PointStruct<int>>();
				Path.AddRange(predCandidate.Path);
				Path.Add(new PointStruct<int>(i, j));
			}

			public Candidate(int i, int j)
			{
				Path = new List<PointStruct<int>> {new PointStruct<int>(i, j)};
			}

			public readonly List<PointStruct<int>> Path;
		}
	}
}