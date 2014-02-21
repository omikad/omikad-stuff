using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using ProblemSets.ComputerScience.DataTypes;

namespace ProblemSets.ComputerScience
{
	[Export]
	public class TreeTraversal
	{
		public void Go()
		{
			var root =
				new NodeString("root",
					new NodeString("5",
						new NodeString("3",
							"1",
							"2"),
						"4"),
					new NodeString("8",
						"6",
						"7"));

			Console.WriteLine(root);
			Console.WriteLine();

			Console.WriteLine("DFS");
			Console.WriteLine(string.Join(", ", DFS(root)));

			Console.WriteLine();
			
			Console.WriteLine("BFS");
			Console.WriteLine(string.Join(", ", BFS(root)));
		}

		private static IEnumerable<string> DFS(NodeString root)
		{
			var stack = new Stack<NodeString>();

			stack.Push(root);

			while (stack.Count > 0)
			{
				var v = stack.Pop();

				yield return v.Value;

				foreach (var child in v.Children)
					stack.Push(child);
			}
		}

		private static IEnumerable<string> BFS(NodeString root)
		{
			var queue = new Queue<NodeString>();

			queue.Enqueue(root);

			while (queue.Count > 0)
			{
				var v = queue.Dequeue();

				yield return v.Value;

				foreach (var child in v.Children)
					queue.Enqueue(child);
			}
		}
	}
}
