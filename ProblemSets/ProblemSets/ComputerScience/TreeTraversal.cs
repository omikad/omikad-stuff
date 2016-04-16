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
				new NodeString("7",
					new NodeString("1",
						"0",
						new NodeString("3",
							"2",
							new NodeString("5",
								"4",
								"6"))),
					new NodeString("9",
						"8",
						"10"));

			Console.WriteLine(root);
			Console.WriteLine();

			Console.WriteLine("DFS");
			Console.WriteLine(string.Join(", ", DFS(root)));
			Console.WriteLine();
			
			Console.WriteLine("BFS");
			Console.WriteLine(string.Join(", ", BFS(root)));
			Console.WriteLine();
			
			Console.WriteLine("Binary tree, preorder");
			Console.WriteLine(string.Join(", ", BinaryTree_Preorder(root)));
			Console.WriteLine("7, 1, 0, 3, 2, 5, 4, 6, 9, 8, 10    <- Expected");
			Console.WriteLine();
			
			Console.WriteLine("Binary tree, inorder");
			Console.WriteLine(string.Join(", ", BinaryTree_Inorder(root)));
			Console.WriteLine("0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10    <- Expected");
			Console.WriteLine();
			
			Console.WriteLine("Binary tree, postorder");
			Console.WriteLine(string.Join(", ", BinaryTree_Postorder(root)));
			Console.WriteLine("0, 2, 4, 6, 5, 3, 1, 8, 10, 9, 7    <- Expected");
			Console.WriteLine();
		}

		// TODO fix and tests

		private static IEnumerable<string> BinaryTree_Preorder(NodeString root)
		{
			var hasChild = root.Children.Count == 2;

			yield return root.Value;

			if (hasChild)
				foreach (var node in BinaryTree_Preorder(root.Children[0]))
					yield return node;

			if (hasChild)
				foreach (var node in BinaryTree_Preorder(root.Children[1]))
					yield return node;
		}

		private static IEnumerable<string> BinaryTree_Inorder(NodeString root)
		{
			var hasChild = root.Children.Count == 2;

			if (hasChild)
				foreach (var node in BinaryTree_Inorder(root.Children[0]))
					yield return node;

			yield return root.Value;

			if (hasChild)
				foreach (var node in BinaryTree_Inorder(root.Children[1]))
					yield return node;
		}

		private static IEnumerable<string> BinaryTree_Postorder(NodeString root)
		{
			var hasChild = root.Children.Count == 2;

			if (hasChild)
				foreach (var node in BinaryTree_Postorder(root.Children[0]))
					yield return node;

			if (hasChild)
				foreach (var node in BinaryTree_Postorder(root.Children[1]))
					yield return node;

			yield return root.Value;
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

		public static IEnumerable<T> DFS<T>(T root, Func<T, IEnumerable<T>> getChildren)
		{
			var stack = new Stack<T>();
			stack.Push(root);

			while (stack.Count > 0)
			{
				var node = stack.Pop();
				yield return node;

				foreach (var child in getChildren(node))
					stack.Push(child);
			}
		} 
	}
}
