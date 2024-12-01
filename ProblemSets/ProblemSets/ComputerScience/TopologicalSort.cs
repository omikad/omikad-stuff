using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using ProblemSets.ComputerScience.DataTypes;
using ProblemSets.Services;

namespace ProblemSets.ComputerScience
{
	[Export]
	public class TopologicalSort
	{
		public void Go()
		{
			var vertices = new[]
			{
				new DirectedGraphNode<char>('a'),
				new DirectedGraphNode<char>('b'),
				new DirectedGraphNode<char>('c'),
				new DirectedGraphNode<char>('d'),
				new DirectedGraphNode<char>('e'),
			};

			vertices[0].Children.Add(vertices[1]);
			vertices[0].Children.Add(vertices[2]);
			vertices[0].Children.Add(vertices[3]);
			vertices[0].Children.Add(vertices[4]);

			vertices[1].Children.Add(vertices[3]);

			vertices[2].Children.Add(vertices[3]);
			vertices[2].Children.Add(vertices[4]);

			vertices[3].Children.Add(vertices[4]);

			foreach (var vertex in vertices)
				Console.WriteLine(vertex);

			Console.WriteLine(", ".Join(Tarjan(vertices)));
		}

		private static void DFS_Old(
			HashSet<DirectedGraphNode<char>> used,
			DirectedGraphNode<char> node,
			Stack<char> result)
		{
			used.Add(node);

			foreach (var child in node.Children)
				if (!used.Contains(child))
					DFS_Old(used, child, result);

			result.Push(node.Value);
		}

		private static IEnumerable<DirectedGraphNode<char>> DFS_Rec(
			HashSet<DirectedGraphNode<char>> used,
			DirectedGraphNode<char> node)
		{
			used.Add(node);

			foreach (var child in node.Children)
				if (!used.Contains(child))
					foreach (var res in DFS_Rec(used, child))
						yield return res;

			yield return node;
		}

		// I'm not sure this work correctly
		// TODO: Test & Correct
//		private static IEnumerable<DirectedGraphNode<char>> DFS_NotRec(
//			HashSet<DirectedGraphNode<char>> used,
//			DirectedGraphNode<char> node)
//		{
//			var stack = new Stack<DirectedGraphNode<char>>();
//			stack.Push(node);

//			while (stack.Count > 0)
//			{
//				var v = stack.Peek();

//				if (used.Contains(v))
//					break;
				
//				used.Add(node);

//				foreach (var child in v.Children)
//					if (!used.Contains(child))
//					{
//						stack.Push(child);
//						used.Add(child);
//					}
//			}

//			return stack;
//		}

		private static IEnumerable<char> Tarjan(IEnumerable<DirectedGraphNode<char>> vertices)
		{
			var used = new HashSet<DirectedGraphNode<char>>();

			var result = new Stack<char>();

			foreach (var outerV in vertices)
				if (!used.Contains(outerV))
					foreach (var v in DFS_Rec(used, outerV))
						result.Push(v.Value);

			return result;
		}
	}
}