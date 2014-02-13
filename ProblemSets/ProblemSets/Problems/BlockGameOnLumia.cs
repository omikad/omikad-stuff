using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using ProblemSets.Services;

namespace ProblemSets.Problems
{
	[Export]
	public class BlockGameOnLumia
	{
		private readonly int[][] turnSearchdirections =
		{
			new[] {-1, 0},
			new[] {0, 1},
			new[] {1, 0},
			new[] {0, -1},
		};

		private readonly int[][] lineSearchDirections =
		{
			new[] {0, 1},
			new[] {1, 0},
		};

		public void Go()
		{
			Solve(
				"....r....",
				"...rg....",
				"...grg...",
				"...rgr...",
				"...grg...",
				"...rgr...",
				"...grg...");
		}

		private void Solve(params string[] levelStrings)
		{
			var board = levelStrings.ToTwoDimensional(s => s.ToCharArray());
			Console.WriteLine("Task: ");
			board.Print();

			foreach (var turn1 in GetNextTurns(board))
			{
				var afterTurn1 = AdvancePhysics(turn1.ShallowCopy()).Last();

				foreach (var turn2 in GetNextTurns(afterTurn1))
				{
					var afterTurn2 = AdvancePhysics(turn2.ShallowCopy()).Last();

					if (CalcBlocksCount(afterTurn2) == 0)
					{
						Console.WriteLine("************ Solution ************");

						Console.WriteLine("Turn: ");
						turn1.Print();

						foreach (var physics in AdvancePhysics(turn1.ShallowCopy()))
							physics.Print();

						Console.WriteLine("Turn: ");
						turn2.Print();

						foreach (var physics in AdvancePhysics(turn2.ShallowCopy()))
							physics.Print();
					}
				}
			}
		}

		private IEnumerable<char[,]> AdvancePhysics(char[,] board) // Board array is reused
		{
			yield return board;
			while (AdvanceGravity(board) || AdvanceDestruction(board))
				yield return board;
		}

		private bool AdvanceDestruction(char[,] board)
		{
			var destroyMe = new List<Tuple<int, int, int, int>>();  // rowStart, colStart, length, directionIndex

			for (var directionIndex = 0; directionIndex < lineSearchDirections.Length; directionIndex++)
			{
				var dir = lineSearchDirections[directionIndex];

				for (var i = 0; i < board.GetLength(0); i++)
					for (var j = 0; j < board.GetLength(1); j++)
					{
						var item = board[i, j];

						if (item == '.') continue;

						var sameCnt = 1;
						while (true)
						{
							var r = i + sameCnt * dir[0];
							var c = j + sameCnt * dir[1];
							if (!board.AreIndicesAllowed(r, c) || board[r, c] != item)
								break;
							sameCnt++;
						}

						if (sameCnt >= 3)
							destroyMe.Add(Tuple.Create(i, j, sameCnt, directionIndex));
					}
			}

			foreach (var line in destroyMe)
			{
				var rowStart = line.Item1;
				var colStart = line.Item2;
				var length = line.Item3;
				var directionIndex = line.Item4;

				var dir = lineSearchDirections[directionIndex];

				for (var i = 0; i < length; i++)
					board[rowStart + i * dir[0], colStart + i * dir[1]] = '.';
			}

			return destroyMe.Count > 0;
		}

		private static bool AdvanceGravity(char[,] board)
		{
			for (var i = 0; i < board.GetLength(0) - 1; i++)
				for (var j = 0; j < board.GetLength(1); j++)
					if (board[i, j] != '.' && board[i + 1, j] == '.')
					{
						var destRow = i + 1;
						while (destRow < board.GetLength(0) - 1 && board[destRow + 1, j] == '.')
							destRow++;

						board[destRow, j] = board[i, j];
						board[i, j] = '.';

						return true;
					}
			return false;
		}

		private IEnumerable<char[,]> GetNextTurns(char[,] board)
		{
			for (var i = 0; i < board.GetLength(0); i++)
				for (var j = 0; j < board.GetLength(1); j++)
					if (board[i, j] != '.')
						foreach (var direction in turnSearchdirections)
						{
							var nexti = i + direction[0];
							var nextj = j + direction[1];
							if (board.AreIndicesAllowed(nexti, nextj) && board[i, j] != board[nexti, nextj])
								yield return AdvanceBoard(board, i, j, nexti, nextj);
						}
		}

		private static char[,] AdvanceBoard(char[,] board, int i0, int j0, int i1, int j1)
		{
			var copy = board.ShallowCopy();

			copy[i0, j0] = board[i1, j1];
			copy[i1, j1] = board[i0, j0];

			return copy;
		}

		private static int CalcBlocksCount(char[,] board)
		{
			return board.EnumerateRows().SelectMany(r => r).Count(c => c != '.');
		}
	}
}