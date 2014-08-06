using System;
using System.Collections.Generic;
using ProblemSets.ComputerScience.DataTypes;
using ProblemSets.Services;

namespace ProblemSets.ComputerScience
{
	public class AllPairsShortestPath
	{
		// Assume graph contains vertices indexed from 1 to N

		public void Go()
		{
		}

		public long? JohnsonAlgorithm(GraphEdgesList graph)
		{
			var incomingEdges = new Dictionary<int, List<Edge>>();
			foreach (var edge in graph.Edges)
			{
				List<Edge> list;
				if (!incomingEdges.TryGetValue(edge.To, out list))
					incomingEdges[edge.To] = list = new List<Edge>();
				list.Add(edge);
			}

			foreach (var kvp in incomingEdges)
			{
				kvp.Value.Add(new Edge
				{
					From = 0,
					To = kvp.Key,
					Weight = 0,
				});
			}

			var bellmanFord = BellmanFord(graph, incomingEdges, 0);

			if (bellmanFord == null)
				return null;

			// TODO: Reweighting
			// TODO: run Dijkstra

			return 42;
		}

		public long[] BellmanFord(GraphEdgesList graph, Dictionary<int, List<Edge>> incomingEdges, int start)
		{
			// TODO: Early stopping

			var n = graph.Vertices;
			var curr = new long[n + 1];
			var prev = new long[n + 1];

			for (var v = 0; v <= n; v++)
				prev[v] = long.MaxValue;
			prev[start] = 0;

			for (var i = 1; i <= n; i++)
			{
				foreach (var incomingEdgeKvp in incomingEdges)
				{
					var v = incomingEdgeKvp.Key;
					var min = prev[v];

					foreach (var incomingEdge in incomingEdgeKvp.Value)
					{
						var prevWeight = prev[incomingEdge.From];
						if (prevWeight != long.MaxValue)
							min = Math.Min(min, prevWeight + incomingEdge.Weight);
					}

					curr[v] = min;
				}

				var temp = curr;
				curr = prev;
				prev = temp;
			}

			// last = prev
			// one before last = curr

			// Detect negative cycles
			for (var v = 0; v <= n; v++)
				if (curr[v] != prev[v])
					return null;

			return curr;
		}

		public long? FloydWarshall(GraphEdgesList graph)
		{
			var n = graph.Vertices;
			var curr = new long[n + 1, n + 1];
			var prev = new long[n + 1, n + 1];

			for (var i = 1; i <= n; i++)
				for (var j = 1; j <= n; j++)
					prev[i, j] = i == j ? 0 : long.MaxValue;

			foreach (var edge in graph.Edges)
				prev[edge.From, edge.To] = edge.Weight;

			for (var k = 1; k <= n; k++)
			{
				for (var i = 1; i <= n; i++)
					for (var j = 1; j <= n; j++)
					{
						var case1 = prev[i, j];

						var ik = prev[i, k];
						var kj = prev[k, j];
						var case2 = (ik == long.MaxValue || kj == long.MaxValue) ? long.MaxValue : (ik + kj);

						curr[i, j] = Math.Min(case1, case2);
					}

				var temp = curr;
				curr = prev;
				prev = temp;
			}

			// Check is there any negative weight cycles
			for (var i = 1; i <= n; i++)
				if (prev[i, i] < 0)
					return null;

			var min = long.MaxValue;
			for (var i = 1; i <= n; i++)
				for (var j = 1; j <= n; j++)
					if (i != j && prev[i, j] < min)
						min = prev[i, j];

			return min;
		}

		public GraphEdgesList Read(string s)
		{
			var lines = s.SplitToLines();

			var n = lines[0].SplitBySpaces()[0].ToInt();

			var edges = new Edge[lines.Length - 1];
			for (var i = 1; i < lines.Length; i++)
			{
				var line = lines[i].SplitBySpaces();
				edges[i - 1] = new Edge
				{
					From = line[0].ToInt(),
					To = line[1].ToInt(),
					Weight = line[2].ToLong()
				};
			}

			return new GraphEdgesList
			{
				Edges = edges,
				Vertices = n,
			};
		}
	}
}