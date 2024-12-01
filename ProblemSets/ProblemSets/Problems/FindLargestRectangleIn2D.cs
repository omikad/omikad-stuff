using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using ProblemSets.ComputerScience.DataTypes;
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
				"110010101001111111111",
				"100011111010110001111",
				"100011111010000001111",
				"100011111010000001101",
				"000010101010000101111",
				"110010101010000000111",
				"100010101010000000111",
				"100010101010000000111",
			};
			Solve(rect.ToTwoDimensional(s => s));
		}

		private static void Solve(char[,] rect)
		{
			// Let the size of the rectangle is N width, M height
			// Overall solution: time O(N*M), memory O(N) 

			// Search largest rectangle in the first top line - O(N)
			// Hold heights of the same chars above the line - memory O(N)
			// Outer cycle by lines - M iterations
			//		Update heights array - N iterations
			//		Search rectangles of '0' and '1' above the line
			//			Use stack of the increasing heights - memory O(N)
			//			When the height is decreased (or line ended) - calculate largest rectangle
			//			Every cell can be pushed into the stack once, and popped once => total time complexity is linear O(N)

			rect.Print();

			var width = rect.GetLength(1);

			var best = FindBestFirstLine(rect);
			Console.WriteLine(best);

			var heights = Enumerable.Repeat(1, width).ToArray();

			for (var line = 1; line < rect.GetLength(0); line++)
			{
				for (var i = 0; i < width; i++)
				{
					if (rect[line - 1, i] == rect[line, i])
						heights[i]++;
					else
						heights[i] = 1;
				}

				FindAboveLine(rect, heights, line, '0', best);
				FindAboveLine(rect, heights, line, '1', best);

				Console.WriteLine(best);
			}

			Console.WriteLine();
			Console.WriteLine(best);
		}

		private static RectangleClass<int> FindBestFirstLine(char[,] rect)
		{
			// O(N)

			var bestLu = new PointStruct<int>(0, 0);
			var bestRd = new PointStruct<int>(0, 0);

			var curWidth = 1;
			for (var i = 1; i < rect.GetLength(1); i++)
			{
				if (rect[0, i] == rect[0, i - 1])
				{
					curWidth++;
					if (curWidth > bestRd.X - bestLu.X + 1)
					{
						bestLu = new PointStruct<int>(i - curWidth + 1, 0);
						bestRd = new PointStruct<int>(i, 0);
					}
				}
				else curWidth = 1;
			}

			return new RectangleClass<int>(bestLu, bestRd);
		}

		private static void FindAboveLine(char[,] rect, int[] heights, int line, char target, RectangleClass<int> best)
		{
			var stack = new Stack<PointStruct<int>>();

			var bestArea = (best.RightDown.X - best.LeftUp.X + 1) * (best.RightDown.Y - best.LeftUp.Y + 1);
			Console.WriteLine(new { line, target, bestArea });

			for (var i = 0; i < rect.GetLength(1); i++)
			{
				var curHeight = rect[line, i] == target ? heights[i] : 0;

				if (stack.Count == 0)
				{
					if (curHeight > 0)
						stack.Push(new PointStruct<int>(i, curHeight));
				}
				else
				{
					FinishSlope(stack, curHeight, line, i, ref bestArea, best);

					if (stack.Count == 0 || curHeight > stack.Peek().Y)
						stack.Push(new PointStruct<int>(i, curHeight));
				}
			}

			FinishSlope(stack, 0, line, rect.GetLength(1), ref bestArea, best);
		}

		private static void FinishSlope(Stack<PointStruct<int>> stack,
		                                int curHeight,
										int line,
										int i,
		                                ref int bestArea,
		                                RectangleClass<int> best)
		{
			while (stack.Count > 0 && stack.Peek().Y > curHeight)
			{
				var endedRectLu = stack.Pop();
				var endedArea = (i - endedRectLu.X) * endedRectLu.Y;
				if (endedArea > bestArea)
				{
					best.LeftUp = new PointStruct<int>(endedRectLu.X, line - endedRectLu.Y + 1);
					best.RightDown = new PointStruct<int>(i - 1, line);
					bestArea = endedArea;
					Console.WriteLine(new { bestArea });
				}
			}
		}
	}
}