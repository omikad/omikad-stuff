using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using ProblemSets.Services;

namespace ProblemSets.ComputerScience
{
	[Export]
	public class KosarajuStronglyConnectedComponents
	{
		public void Go()
		{
			var input =
				new[]
				{
					"1 4",
					"2 8",
					"3 6",
					"4 7",
					"5 2",
					"6 9",
					"7 1",
					"8 5",
					"8 6",
					"9 7",
					"9 3",
				}
					.Select(str => str.Split(new[] {' ', '\t'}, StringSplitOptions.RemoveEmptyEntries))
					.Select(a => a.Select(str => int.Parse(str) - 1).ToArray())
					.ToArray();

			var sizes = CalcStronglyConnectedComponentSizes(input).OrderBy(c => c).Take(5);

			Console.WriteLine(",".Join(sizes));
		}

		private Stack<int> s;
		private Dictionary<int, List<int>> adjacencyList;
		private int[][] edges;
		private bool[] visited;

		public IEnumerable<int> CalcStronglyConnectedComponentSizes(int[][] input)
		{
			var result = new List<int>();

			edges = input;
			s = new Stack<int>();

			FillAdjacencyMatrix();

			var n = adjacencyList.Max(kvp => kvp.Key);
			visited = new bool[n + 1];

			Console.WriteLine("Done preparing, memory (Kb) = " + GC.GetTotalMemory(false) / 1024);

			while (s.Count < adjacencyList.Count)
			{
				for (var vertex = 0 ; vertex < visited.Length; vertex++)
					if (!visited[vertex] && adjacencyList.ContainsKey(vertex))
						DFS_Forward(vertex);
			}

			Array.Clear(visited, 0, visited.Length);

			FillAdjacencyListTranspose();

			while (s.Count > 0)
			{
				var v = s.Pop();

				if (visited[v])
					continue;

				var sccSize = DFS_Backward(v);

				result.Add(sccSize);
			}

			return result.OrderByDescending(size => size);
		}

		private void FillAdjacencyMatrix()
		{
			adjacencyList = new Dictionary<int, List<int>>();

			foreach (var edge in edges)
			{
				if (!adjacencyList.ContainsKey(edge[0])) 
					adjacencyList.Add(edge[0], new List<int>());

				if (!adjacencyList.ContainsKey(edge[1])) 
					adjacencyList.Add(edge[1], new List<int>());
			}


			foreach (var edge in edges)
				adjacencyList[edge[0]].Add(edge[1]);
		}

		private void FillAdjacencyListTranspose()
		{
			foreach (var kvp in adjacencyList)
				kvp.Value.Clear();

			foreach (var edge in edges)
				adjacencyList[edge[1]].Add(edge[0]);
		}

		private void DFS_Forward(int vertex)
		{
			var callstack = new Stack<int>();
			callstack.Push(vertex);

			visited[vertex] = true;

			while (callstack.Count > 0)
			{
				var v = callstack.Peek();

				var goDeep = false;

				foreach (var child in adjacencyList[v])
				{
					if (!visited[child])
					{
						visited[child] = true;
						callstack.Push(child);
						goDeep = true;
						break;
					}
				}

				if (!goDeep)
				{
					s.Push(v);
					callstack.Pop();
				}
			}
		}

		private int DFS_Backward(int vertex)
		{
			var result = 0;

			var stack = new Stack<int>();

			visited[vertex] = true;
			stack.Push(vertex);

			while (stack.Count > 0)
			{
				var v = stack.Pop();
				result++;

				foreach (var child in adjacencyList[v])
				{
					if (!visited[child])
					{
						visited[child] = true;
						stack.Push(child);
					}
				}
			}

			return result;
		}
	}
}