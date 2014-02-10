using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using ProblemSets.ComputerScience.DataTypes;

namespace ProblemSets.ComputerScience
{
	[Export]
	public class CartesianTree
	{
		public void Go()
		{
			var arr = new[] { 9, 3, 7, 1, 8, 12, 10, 20, 15, 18, 5 };
			Console.WriteLine(", ".Join(arr));

			// Both algs works O(n). See AllNearestSmallerValues 

			Console.WriteLine();
			Console.WriteLine(BuildCartesianTree_Stack(arr));

			Console.WriteLine();
			Console.WriteLine(BuildCartesianTree_NoStack(arr));
		}

		private BinaryNode BuildCartesianTree_NoStack(int[] arr)
		{
			var nearestSmallerIndices = new int[arr.Length];
			var nodes = new BinaryNode[arr.Length];
			nodes[0] = new BinaryNode(arr[0]);

			var root = nodes[0];

			for (var i = 1; i < arr.Length; i++) 
			{
				var x = arr[i];

				var j = i - 1;
				while (arr[j] > x && j > 0)
					j = nearestSmallerIndices[j];

				nearestSmallerIndices[i] = j;

				var node = new BinaryNode(x);
				nodes[i] = node;

				if (arr[j] > x)
				{
					var oldRoot = root;
					root = node;
					root.Left = oldRoot;
				}
				else
				{
					var nearestSmaller = nodes[j];
					var oldRight = nearestSmaller.Right;
					nearestSmaller.Right = node;
					node.Left = oldRight;
				}
			}

			return root;
		}

		public BinaryNode BuildCartesianTree_Stack(int[] arr)
		{
			BinaryNode root = null;

			var stack = new Stack<BinaryNode>();

			foreach (var x in arr)
			{
				while (stack.Count > 0 && stack.Peek().Value >= x)
					stack.Pop();

				var node = new BinaryNode(x);

				if (stack.Count == 0)
				{
					if (root == null)
						root = node;
					else
					{
						var oldRoot = root;
						root = node;
						root.Left = oldRoot;
					}
				}
				else
				{
					var nearestSmaller = stack.Peek();
					var oldRight = nearestSmaller.Right;
					nearestSmaller.Right = node;
					node.Left = oldRight;
				}

				stack.Push(node);
			}

			return root;
		}
	}
}
