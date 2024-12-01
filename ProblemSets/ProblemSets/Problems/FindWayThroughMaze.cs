using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using ProblemSets.ComputerScience;
using ProblemSets.ComputerScience.DataTypes;
using ProblemSets.Services;

namespace ProblemSets.Problems
{
	[Export]
	public class FindWayThroughMaze
	{
		[Import] private Combinations combinations;

		// Find path from Left-Up corner to Right-Down
		// Cell with 'o' must be in the path
		public void Go()
		{
			Solve(new[]
				{
					"              ",
					"   #          ",
					"   #  o       ",
					"   ######     ",
					"              ",
					"   #          ",
					"   #          ",
				}.ToTwoDimensional(s => s));

			Solve(new[]
				{
					"               ",
					"   ########### ",
					"#      o     # ",
					"   #### #### # ",
					"   ##   #### # ",
					"   ## ###### # ",
					"## ## #      # ",
					"   ## # ###### ",
					"#   # # #      ",
					"#   #          ",
					"#   #####      ",
					"#              ",
				}.ToTwoDimensional(s => s));

			Solve(new[]
				{
					"           ",
					"           ",
					"    #####  ",
					"    #      ",
					"    ### ## ",
					"     #   # ",
					"     #   # ",
					"     # # # ",
					" # ### o # ",
					" # # #   # ",
					" #   ### # ",
					"           ",
					"           ",
				}.ToTwoDimensional(s => s));

			Solve(new[]
				{
					"           ",
					" ##########",
					"           ",
					" ######### ",
					" #   #   # ",
					" # # # o # ",
					" # # #   # ",
					" # # # # # ",
					"   #   #   ",
				}.ToTwoDimensional(s => s));
		}

		private const char free = ' ';
		private const char wall = '#';
		private const char coin = 'o';
		private const char bigRound = '+';
		private const char coinWay = '@';

		private readonly char[] allowedCells = { free, bigRound, coin };

		private const int right = 0;
		private const int down = 1;
		private const int left = 2;
		private const int up = 3;

		private readonly int[][] directions =
		{
			new[] { 1, 0 },  // Right
			new[] { 0, 1 },  // Down
			new[] { -1, 0 }, // Left
			new[] { 0, -1 }, // Up
		};

		private void Solve(char[,] maze)
		{
			// O(m*n)

			Console.WriteLine("Task:");
			maze.Print();

			var copyMaze = maze.ShallowCopy();
			GoMarkBigRound(copyMaze);
			Console.WriteLine("With big round:");
			copyMaze.Print();

			var coinPos = FindCoin(maze);
			Console.WriteLine("With way to coin:");
			var mazeSolution = FindWaysOnCoin(copyMaze, coinPos);
			mazeSolution.Print();

			// TODO: Join ways
		}

		private IEnumerable<PointStruct<int>> GetSolution(char[,] maze, char[,] mazeSolution)
		{
			yield break;
		}

		private char[,] FindWaysOnCoin(
			char[,] maze,
			PointStruct<int> coinPos)
		{
			var allowedDirections = Enumerable.Range(0, directions.Length)
				.Where(dir => CanGo(maze, coinPos, dir))
				.ToArray();

			foreach (var dirPair in combinations.Recursive(allowedDirections, 2))
			{
				var dirPairArray = dirPair.ToArray();

				var copyMaze = maze.ShallowCopy();

				var isOk = true;
				
				foreach (var mainDir in dirPairArray)
				{
					var curDir = mainDir;

					var point = coinPos;

					while (isOk && copyMaze[point.Y, point.X] != bigRound)
					{
						copyMaze[point.Y, point.X] = coinWay;

						point = GoFromCoin(copyMaze, point, mainDir, ref curDir, out isOk);
					}


					if (!isOk) break;
					
					copyMaze[point.Y, point.X] = coinWay;
				}

				if (isOk)
					return copyMaze;
			}

			throw new InvalidOperationException("Can't find path :(");
		}

		private static PointStruct<int> FindCoin(char[,] maze)
		{
			for (var i = 0; i < maze.GetLength(0); i++)
			for (var j = 0; j < maze.GetLength(1); j++)
				if (maze[i, j] == coin)
					return new PointStruct<int>(j, i);
			throw new InvalidOperationException("Can't find coin");
		}

		private void GoMarkBigRound(char[,] maze)
		{
			var point = new PointStruct<int>(0, 0);
			var dir = maze[0, 1] != wall ? right : down;

			var finishFound = false;

			while (point.X > 0 || point.Y > 0 || !finishFound)
			{
				if (point.X == maze.GetLength(1) - 1 && point.Y == maze.GetLength(0) - 1)
					finishFound = true;

				maze[point.Y, point.X] = bigRound;

				point = GoLeftHandTouch(maze, point, ref dir);
			}
		}

		private PointStruct<int> GoLeftHandTouch(char[,] maze, PointStruct<int> point, ref int dir)
		{
			var nextDir = (dir + 4 - 1) % 4;

			for (var i = 0; i < 4; i++)
			{
				var candidateDir = (nextDir + i) % 4;
				if (CanGo(maze, point, candidateDir))
				{
					dir = candidateDir;
					return Go(maze, point, dir);
				}
			}

			throw new InvalidOperationException();
		}

		private PointStruct<int> GoFromCoin(char[,] maze, PointStruct<int> point, int mainDir, ref int curDir, out bool isOk)
		{
			if (CanGo(maze, point, mainDir))
			{
				isOk = true;
				return Go(maze, point, mainDir);
			}
			// Else - wall
			// Implement right hand touch

			curDir = (mainDir + 4 - 1) % 4; // Turn unit to make right hand on the wall

			for (var i = 0; i < 4; i++)
			{
				var candidateDir = (mainDir + 4 - i) % 4;
				if (CanGo(maze, point, candidateDir))
				{
					curDir = candidateDir;
					isOk = true;
					return Go(maze, point, curDir);
				}
			}

			isOk = false;
			return point;
		}

		private PointStruct<int> Go(char[,] maze, PointStruct<int> from, int direction)
		{
			var dirArray = directions[direction];
			var to = new PointStruct<int>(from.X + dirArray[0], from.Y + dirArray[1]);
			if (!maze.AreIndicesAllowed(to.Y, to.X))
				throw new InvalidOperationException(string.Format("Not allowed to go to {0}", to));
			return to;
		}

		private bool CanGo(
			char[,] maze, 
			PointStruct<int> from, 
			int directionIndex)
		{
			if (directionIndex == right)
				return from.X < maze.GetLength(1) - 1 && allowedCells.Contains(maze[from.Y, from.X + 1]);

			if (directionIndex == down)
				return from.Y < maze.GetLength(0) - 1 && allowedCells.Contains(maze[from.Y + 1, from.X]);

			if (directionIndex == left)
				return from.X > 0 && allowedCells.Contains(maze[from.Y, from.X - 1]);

			return from.Y > 0 && allowedCells.Contains(maze[from.Y - 1, from.X]);
		}
	}
}
