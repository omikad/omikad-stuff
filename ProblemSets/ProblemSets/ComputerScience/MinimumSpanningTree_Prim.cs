using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using ProblemSets.ComputerScience.DataTypes;
using ProblemSets.Services;

namespace ProblemSets.ComputerScience
{
	[Export]
	public class MinimumSpanningTree_Prim
	{
		public void Go()
		{
		}

		public long CalcMinimumSpanningTreeWeight(string[] lines)
		{
			var graph = ReadData(lines);

			var brute = Prim_Brute(graph).Sum(e => e.Weight);

			var result = Prim(graph).Sum(e => e.Weight);

			Console.WriteLine(new { brute, result });

			return result;
		}

		private static List<Edge>[] ReadData(string[] lines)
		{
			var data = lines
				.Select(
					s => s.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))
				.ToArray();

			var graph = new List<Edge>[data[0][0].ToInt() + 1];

			foreach (var edgeData in data.Skip(1))
			{
				var start = edgeData[0].ToInt();
				var to = edgeData[1].ToInt();
				var weight = edgeData[2].ToLong();

				AddEdgeToGraph(graph, start, to, weight);
				AddEdgeToGraph(graph, to, start, weight);
			}

			return graph;
		}

		private static void AddEdgeToGraph(List<Edge>[] graph, int start, int to, long weight)
		{
			var list = (graph[start] ?? (graph[start] = new List<Edge>()));
			list.Add(new Edge { From = start, To = to, Weight = weight });
		}

		private class VertexInfo
		{
			public int IndexInHeap;
			public long Cost;
			public Edge KeyEdge;
		}

		public List<Edge> Prim(List<Edge>[] graph)
		{
			// O(m * log n), m edges, n vertices

			var mst = new List<Edge>();

			var start = new Random().Next(graph.Length - 1) + 1;

			var visited = new bool[graph.Length];
			visited[start] = true;

			var heap = new GenericBinaryHeap<VertexInfo>(
				(x, y) => x.Cost.CompareTo(y.Cost),
				(info, index) => info.IndexInHeap = index);

			var infos = new VertexInfo[graph.Length];
			for (var i = 0; i < infos.Length; i++)
				if (graph[i] != null && i != start)
					infos[i] = new VertexInfo { Cost = long.MaxValue };

			heap.FillRaw(infos.Where(i => i != null));

			foreach (var edge in graph[start])
			{
				var vertexInfo = infos[edge.To];
				vertexInfo.KeyEdge = edge;
				vertexInfo.Cost = edge.Weight;
				heap.BubbleUp(vertexInfo.IndexInHeap);
			}

			while (heap.Count > 0)
			{
				var uv = heap.Min.KeyEdge;

				mst.Add(uv);

				heap.DeleteMin();

				visited[uv.To] = true;

				foreach (var edge in graph[uv.To])
					if (!visited[edge.To])
					{
						var info = infos[edge.To];
						if (edge.Weight < info.Cost)
						{
							info.Cost = edge.Weight;
							info.KeyEdge = edge;
							heap.BubbleUp(info.IndexInHeap);
						}
					}
			}

			return mst;
		}

		public List<Edge> Prim_Brute(List<Edge>[] graph)
		{
			// O(m*n)

			var mst = new List<Edge>();

			var start = new Random().Next(graph.Length - 1) + 1;

			var visited = new bool[graph.Length];
			visited[start] = true;

			var frontier = new List<Edge>(graph[start]);

			while (frontier.Count > 0)
			{
				var edge = frontier.OrderBy(e => e.Weight).First();

				mst.Add(edge);

				visited[edge.To] = true;

				frontier.AddRange(graph[edge.To]);

				frontier.RemoveAll(e => visited[e.To]);
			}

			return mst;
		}
	}
}
