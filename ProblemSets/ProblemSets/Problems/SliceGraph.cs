using System;
using System.Collections.Generic;

namespace ProblemSets.Problems
{
	public class SliceGraph
	{
		public void Go()
		{
			var graph = new Dictionary<int, int[]>
			{
				{1, new[] {2, 3}},
				{2, new[] {4}},
				{3, new[] {6, 7}},
			};

			foreach (var tuple in GetNeighboors(graph, 1))
				Console.WriteLine(new {node = tuple.Item1, neighboor = tuple.Item2});
		}

		private static IEnumerable<Tuple<int, int?>> GetNeighboors(Dictionary<int, int[]> graph, int root)
		{
			var stack = new Stack<List<int>>();
			stack.Push(new List<int> {root});

			while (stack.Count > 0)
			{
				var items = stack.Pop();

				for (var i = 0; i < items.Count; i++)
				{
					var item = items[i];
					var neighboor = i == items.Count - 1 ? (int?) null : items[i + 1];
					yield return Tuple.Create(item, neighboor);
				}

				var nextLevel = new List<int>();

				foreach (var item in items)
					if (graph.ContainsKey(item))
						nextLevel.AddRange(graph[item]);

				if (nextLevel.Count > 0)
					stack.Push(nextLevel);
			}
		} 
	}
}