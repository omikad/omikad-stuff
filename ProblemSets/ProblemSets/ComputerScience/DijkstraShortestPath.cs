using System;
using System.Collections.Generic;
using System.Linq;
using ProblemSets.ComputerScience.DataTypes;

namespace ProblemSets.ComputerScience
{
	public class DijkstraShortestPath
	{
		public void Go()
		{
		}

		public long[] CalcShortestPaths(string[] graph, int v)
		{
			var inputDict = graph
				.Select(line => line.Split(new[] {' ', '\t'}, StringSplitOptions.RemoveEmptyEntries))
				.ToDictionary(
					a => int.Parse(a[0]),
					a => a.Skip(1).Select(s => s.Split(','))
						.Select(aa => new EdgeEndPoint {To = int.Parse(aa[0]), Weight = int.Parse(aa[1])})
						.ToArray());

			var input = new EdgeEndPoint[inputDict.Max(kvp => kvp.Key) + 1][];
			input[0] = new EdgeEndPoint[0];
			foreach (var edges in inputDict)
				input[edges.Key] = edges.Value;

			var paths = CalcShortestPathsByHeap(input, v);

			return paths;
		}

		private class VertexInfo
		{
			public int IndexInHeap;
			public long Path;
			public bool Visited;
			public EdgeEndPoint[] Adjacents;
		}

		public long[] CalcShortestPathsByHeap(EdgeEndPoint[][] adjacencyList, int v)
		{
			var vertices = adjacencyList
				.Select(p => new VertexInfo { Path = long.MaxValue, Adjacents = p })
				.ToArray();

			vertices[v].Path = 0;

			var front = new GenericBinaryHeap<VertexInfo>(
				(x, y) => x.Path.CompareTo(y.Path),
				(vi, i) => vi.IndexInHeap = i);

			front.Insert(vertices[v]);

			while (front.Count > 0)
			{
				var u = front.Min;

				front.DeleteMin();

				if (u.Visited)
					continue;

				u.Visited = true;

				foreach (var edge in u.Adjacents)
				{
					var to = vertices[edge.To];

					if (to.Visited)
						continue;

					var w = u.Path + edge.Weight;

					if (w < to.Path)
					{
						if (front.Count > 0 && front[to.IndexInHeap] == to)
							front.Remove(to.IndexInHeap);
						to.Path = w;
					}

					front.Insert(to);
				}
			}

			return vertices.Select(vi => vi.Path).ToArray();
		}

		private long[] CalcShortestPaths_Trivial(EdgeEndPoint[][] adjacencyList, int v)
		{
			// O(m*n)

			var paths = adjacencyList.Select(kvp => long.MaxValue).ToArray();

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