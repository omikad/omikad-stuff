using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using ProblemSets.ComputerScience.DataTypes;
using ProblemSets.Services;

namespace ProblemSets.ComputerScience
{
	[Export]
	public class FloodFill
	{
		public void Go()
		{
			FloodFill_Queue_EastWestOptimization(new[]
			{
				".........",
				".   .   .",
				".   .   .",
				".  .    .",
				"... * ...",
				".    .  .",
				".   .   .",
				".   .   .",
				"........."
			}.ToTwoDimensional(s => s));
		}

		private static void FloodFill_Queue_EastWestOptimization(char[,] board)
		{
			var start = FindStart(board);
			board[start.Y, start.X] = ' ';

			var queue = new Queue<PointStruct<int>>();
			queue.Enqueue(start);

			while (queue.Count > 0)
			{
				var v = queue.Dequeue();

				if (board[v.Y, v.X] != ' ') continue;

				var w = v.X;
				while (w > 0 && board[v.Y, w - 1] == ' ') w--;

				var e = v.X;
				while (e < board.GetLength(1) - 1 && board[v.Y, e + 1] == ' ') e++;

				for (var i = w; i <= e; i++)
				{
					board[v.Y, i] = '*';

					Console.ReadKey();
					Console.WriteLine();
					board.Print();
					Console.WriteLine("Queue count = " + queue.Count);

					if (v.Y > 0 && board[v.Y - 1, i] == ' ') queue.Enqueue(new PointStruct<int>(i, v.Y - 1));
					if (v.Y < board.GetLength(0) - 1 && board[v.Y + 1, i] == ' ') queue.Enqueue(new PointStruct<int>(i, v.Y + 1));
				}
			}
		}

		private static PointStruct<int> FindStart(char[,] board)
		{
			for (var i = 0; i < board.GetLength(0); i++)
			for (var j = 0; j < board.GetLength(1); j++)
				if (board[i, j] == '*')
					return new PointStruct<int>(j, i);
			throw new InvalidOperationException("Start point '*' not found");
		}
	}
}