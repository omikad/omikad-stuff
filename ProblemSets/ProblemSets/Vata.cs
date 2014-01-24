using System;
using System.Collections.Generic;

namespace ProblemSets
{
	public class Node
	{
		public Node(string value)
		{
			Value = value;
			Children = new Node[0];
		}

		public string Value;
		public Node[] Children;
	}
 
	public class Vata
	{
		public static void Solve()
		{
			var root = new Node("root")
				{
					Children = new[]
						{
							new Node("5")
								{
									Children = new[]
										{
											new Node("3")
												{
													Children = new[]
														{
															new Node("1"),
															new Node("2"),
														}
												},
											new Node("4"),
										}
								},
							new Node("8")
								{
									Children = new[]
										{
											new Node("6"),
											new Node("7"),
										}
								},
						}
				};

			Console.WriteLine("DFS");
			Console.WriteLine(string.Join(", ", DFS(root)));

			Console.WriteLine("BFS");
			Console.WriteLine(string.Join(", ", BFS(root)));
		}

		private static IEnumerable<string> DFS(Node root)
		{
			var stack = new Stack<Node>();

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

		private static IEnumerable<string> BFS(Node root)
		{
			var queue = new Queue<Node>();

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
