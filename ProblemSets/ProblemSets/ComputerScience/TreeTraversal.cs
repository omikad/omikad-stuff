using System;
using System.Collections.Generic;

namespace ProblemSets.ComputerScience
{
	public class TreeTraversal
	{
		public static void Solve()
		{
			var root = new NodeString("root")
				{
					Children = new[]
						{
							new NodeString("5")
								{
									Children = new[]
										{
											new NodeString("3")
												{
													Children = new[]
														{
															new NodeString("1"),
															new NodeString("2"),
														}
												},
											new NodeString("4"),
										}
								},
							new NodeString("8")
								{
									Children = new[]
										{
											new NodeString("6"),
											new NodeString("7"),
										}
								},
						}
				};

			Console.WriteLine("DFS");
			Console.WriteLine(string.Join(", ", DFS(root)));

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
				{
					stack.Push(child);
				}
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
				{
					queue.Enqueue(child);
				}
			}
		}
	}
}
