using System;
using System.Collections.Generic;
using System.Linq;
using ProblemSets.ComputerScience.DataTypes;

namespace ProblemSets.ComputerScience
{
	public class DijkstraShortestPath
	{
		public struct Edge
		{
			public int To;
			public int Weight;

			public override string ToString()
			{
				return string.Format("{{ to = {0}, weight = {1} }}", To, Weight);
			}
		}

		public int[] CalcShortestPaths(string[] graph, int v)
		{
			var inputDict = graph
				.Select(line => line.Split(new[] {' ', '\t'}, StringSplitOptions.RemoveEmptyEntries))
				.ToDictionary(
					a => int.Parse(a[0]),
					a => a.Skip(1).Select(s => s.Split(','))
						.Select(aa => new Edge {To = int.Parse(aa[0]), Weight = int.Parse(aa[1])})
						.ToArray());

			var input = new Edge[inputDict.Max(kvp => kvp.Key) + 1][];
			input[0] = new Edge[0];
			foreach (var edges in inputDict)
				input[edges.Key] = edges.Value;

			var paths = CalcShortestPathsByHeap(input, 1);

			return paths;
		}

		public int[] CalcShortestPathsByHeap(Edge[][] adjacencyList, int v)
		{
			var paths = adjacencyList.Select(kvp => 1000000).ToArray();

			paths[v] = 0;
			var front = new BinaryHeap((x, y) => paths[x].CompareTo(paths[y]));
			front.Insert(v);

			var visited = new bool[adjacencyList.Length];

			while (front.Count > 0)
			{
				var u = front.Min;

				front.DeleteMin();

				if (visited[u])
					continue;

				visited[u] = true;

				foreach (var edge in adjacencyList[u])
				{
					var to = edge.To;

					if (visited[to])
						continue;

					var w = paths[u] + edge.Weight;

					if (w < paths[to])
						// TODO: Should remove previous value of the 'to' from heap
						paths[to] = w;

					front.Insert(to);
				}
			}

			return paths;
		}

		private int[] CalcShortestPaths_Trivial(Edge[][] adjacencyList, int v)
		{
			// O(m*n)

			var paths = adjacencyList.Select(kvp => 1000000).ToArray();

			paths[v] = 0;
			var front = new HashSet<int> {v};

			var visited = new bool[adjacencyList.Length];
			visited[v] = true;

			while (front.Count > 0)
			{
				var u = front.OrderBy(i => paths[i]).First();

				front.Remove(u);

				visited[u] = true;

				foreach (var edge in adjacencyList[u])
				{
					var to = edge.To;

					if (visited[to])
						continue;

					var w = paths[u] + edge.Weight;

					if (w < paths[to])
						paths[to] = w;

					front.Add(to);
				}
			}

			return paths;
		}
	}
}