using System;
using System.ComponentModel.Composition;
using ProblemSets.ComputerScience.DataTypes;
using ProblemSets.Services;

namespace ProblemSets.Problems
{
	[Export]
	public class FindWayThroughMaze
	{
		// Find path from Left-Up corner to Right-Down
		// Cell with 'o' must be in the path
		public void Go()
		{
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
		private const char wayToFinish = '1';
		private const char wayFromFinish = '2';

		private const int right = 0;
		private const int down = 1;
		private const int left = 2;
		private const int up = 3;

		private readonly int[][] directions = new[]
			{
				new[] { 1, 0 },  // Right
				new[] { 0, 1 },  // Down
				new[] { -1, 0 }, // Left
				new[] { 0, -1 }, // Up
			};

		private void Solve(char[,] maze)
		{
			Console.WriteLine("Task: ");
			maze.Print();

			var withWaysToFinish = maze.ShallowCopy();

			FindTwoWaysToFinish(withWaysToFinish);
			withWaysToFinish.Print();
		}

		private void FindTwoWaysToFinish(char[,] maze)
		{
			GoLeftHandTouch(maze);
			GoRightHandTouch(maze);
		}

		private void GoLeftHandTouch(char[,] maze)
		{
			var point = new PointStruct<int>(0, 0);
			var dir = maze[0, 1] != wall ? right : down;

			while (point.X < maze.GetLength(1) - 1 || point.Y < maze.GetLength(0) - 1)
			{
				maze[point.Y, point.X] = wayToFinish;

				var nextDir = (dir + 4 - 1) % 4;

				for (var i = 0; i < 4; i++)
					if (CanGo(maze, point, (nextDir + i) % 4))
					{
						dir = (nextDir + i) % 4;
						point = Go(maze, point, dir);
						break;
					}
			}
		}

		private void GoRightHandTouch(char[,] maze)
		{
			var point = new PointStruct<int>(0, 0);
			var dir = maze[1, 0] != wall ? down : right;

			while (point.X < maze.GetLength(1) - 1 || point.Y < maze.GetLength(0) - 1)
			{
				maze[point.Y, point.X] = wayFromFinish;

				var nextDir = (dir + 1) % 4;

				for (var i = 0; i < 4; i++)
					if (CanGo(maze, point, (nextDir + 4 - i) % 4))
					{
						dir = (nextDir + 4 - i) % 4;
						point = Go(maze, point, dir);
						break;
					}
			}
		}

		private PointStruct<int> Go(char[,] maze, PointStruct<int> from, int direction)
		{
			var dirArray = directions[direction];
			var to = new PointStruct<int>(from.X + dirArray[0], from.Y + dirArray[1]);
			if (!maze.AreIndicesAllowed(to.Y, to.X))
				throw new InvalidOperationException(string.Format("Not allowed to go to {0}", to));
			return to;
		}

		private static bool CanGo(char[,] maze, PointStruct<int> from, int directionIndex)
		{
			if (directionIndex == right)
				return from.X < maze.GetLength(1) - 1 && maze[from.Y, from.X + 1] != wall;

			if (directionIndex == down)
				return from.Y < maze.GetLength(0) - 1 && maze[from.Y + 1, from.X] != wall;

			if (directionIndex == left)
				return from.X > 0 && maze[from.Y, from.X - 1] != wall;

			return from.Y > 0 && maze[from.Y - 1, from.X] != wall;
		}
	}
}
