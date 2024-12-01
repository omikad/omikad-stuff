using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using ProblemSets.ComputerScience.DataTypes;
using ProblemSets.Services;

namespace ProblemSets.ComputerScience
{
	public class MaxSpacingKClustering
	{
		private static readonly int[] masks =
			Enumerable.Range(0, 24).Select(i => 1 << i).ToArray();
	
		public class Node
		{
			public readonly BitVector32 Vector;
			public readonly int Numeric;
			public readonly int Index;

			public readonly int Len;

			public Node(bool[] bits, int index)
			{
				Len = bits.Length;

				Vector = new BitVector32();
				for (var i = 0; i < Len; i++) if (bits[i]) Vector[masks[i]] = true;

				Numeric = Vector.Data;

				Index = index;
			}

			public int HammingDistance(Node other)
			{
				var result = 0;
				for (var i = 0; i < Len; i++)
					if (other.Vector[i] != Vector[i])
						result++;
				return result;
			}

			public override string ToString()
			{
				return " ".Join(Enumerable.Range(0, Len).Select(i => Vector[masks[i]] ? "1" : "0"));
			}
		}
	
		public void Go()
		{
			var data = @"15 8
1 1 0 0 1 0 0 0 
0 0 1 1 1 1 1 1 
1 0 1 0 1 0 0 1 
0 0 1 0 0 1 1 0 
1 0 1 0 1 1 1 0 
1 1 0 1 1 0 1 1 
1 0 1 0 0 1 1 1 
1 1 1 0 0 1 0 0 
0 0 0 0 0 0 0 1 
0 1 0 0 0 1 1 0 
1 1 0 0 0 0 0 0 
1 0 0 1 0 1 1 0 
0 0 1 1 1 1 1 0 
0 0 1 0 1 0 1 1 
0 0 0 1 1 1 1 0 ".SplitToLines();
			Console.WriteLine(new MaxSpacingKClustering().MaxSpacing_ForHamming(data));
		}

		public int MaxSpacing_ForHamming(string[] lines)
		{
			var nodes = lines.Skip(1)
			                 .Select(l => l.SplitBySpaces().Select(x => x == "1").ToArray())
							 .WithIndices()
			                 .Select(wi => new Node(wi.Item, wi.Index))
							 .ToArray();

			var union = new UnionFind(nodes.Length);

			var nodesDict = new Dictionary<int, Node>();
			foreach (var node in nodes)
			{
				Node exists;
				if (nodesDict.TryGetValue(node.Numeric, out exists))
					union.Union(node.Index, exists.Index);
				else
					nodesDict.Add(node.Numeric, node);
			}

			Console.WriteLine("Unique nodes count: " + nodesDict.Count);
			Console.WriteLine("Current clasters count: " + union.ClastersCound);

			var bits = nodes[0].Len;

			var indices = Enumerable.Range(0, bits).ToArray();

			const int maxSpaceLen = 2;

			var combinations = Enumerable.Range(1, maxSpaceLen)
			                             .Select(l => 
											indices
											.Combinations(l)
											.Select(comb =>
												{
													var v = new BitVector32();
													foreach (var i in comb)
														v[masks[i]] = true;
													return v;
												})
											.ToArray())
			                             .ToArray();

			for (var length = 1; length <= maxSpaceLen; length++)
			{
				foreach (var node in nodes)
				{
					foreach (var combination in combinations[length - 1])
					{
						var vector = node.Vector.Data ^ combination.Data;

						Node neighboor;
						if (nodesDict.TryGetValue(vector, out neighboor))
							union.Union(node.Index, neighboor.Index);
					}
				}

				Console.WriteLine(new { length, union.ClastersCound });
			}

			return union.ClastersCound;
		}

		public long GetMaxSpacing(int k, string[] lines)
		{
			var nodes = lines[0].ToInt();

			var graph = lines
				.Skip(1)
				.Select(l => l.SplitBySpaces())
				.Select(a => new Edge { From = a[0].ToInt() - 1, To = a[1].ToInt() - 1, Weight = a[2].ToLong() })
				.ToArray();

			return GetMaxSpacing(k, graph, nodes);
		}

		private long GetMaxSpacing(int k, Edge[] graph, int nodes)
		{
			var union = new UnionFind(nodes);

			var clusters = nodes;
			var i = 0;

			var sorted = graph.OrderBy(e => e.Weight).ToArray();

			for (; i < sorted.Length && clusters > k; i++)
			{
				var edge = sorted[i];

				var fromLabel = union.Find(edge.From);
				var toLabel = union.Find(edge.To);

				if (fromLabel == toLabel)
					continue;

				union.Union(edge.From, edge.To);
				clusters--;
			}

			for (; i < sorted.Length; i++)
			{
				var edge = sorted[i];

				var fromLabel = union.Find(edge.From);
				var toLabel = union.Find(edge.To);

				if (fromLabel != toLabel)
					return edge.Weight;
			}

			throw new InvalidOperationException();
		}
	}
}
