using System;
using System.ComponentModel.Composition;
using System.Linq;

namespace ProblemSets.ComputerScience
{
	[Export]
	public class KargerMinCut
	{
		public void Go()
		{
			var input = new[]
			{							   	
				new [] {1, 2, 3, 4			   		},
				new [] {2, 1, 3, 4			   		},
				new [] {3, 1, 2, 4			   		},
				new [] {4, 1, 2, 3, 5		   		},
				new [] {5, 4, 6, 7, 8		   		},
				new [] {6, 5, 7, 8			   		},
				new [] {7, 5, 6, 8			   		},
				new [] {8, 5, 6, 7			   		},

			}
			.Select(a => a.Select(i => i - 1).Skip(1).ToArray()).ToArray();

			Console.WriteLine(CalcMinCut(input));
		}

		public int CalcMinCut(int[][] verticesAdjacencyList)
		{
			var random = new Random();

			var min = int.MaxValue;

			var n = verticesAdjacencyList.Length;
			var cnt = n * n * Math.Log(n);	// Probability of success >= 1 / n
			if (n <= 20) cnt *= 50;

			var matrix = new int[n][];
			for (var i = 0; i < n; i++)
			{
				matrix[i] = new int[n];
				foreach (var vertex in verticesAdjacencyList[i])
					matrix[i][vertex]++;
			}

			var m = matrix.Sum(s => s.Sum());

			var edgesCount = new int[n];
			for (var i = 0; i < n; i++)
				edgesCount[i] = matrix[i].Sum();
			
			for (var i = 0; i < cnt; i++)
			{
				if (i % 1000 == 999)
					Console.WriteLine(new { i, min });

				var matrCopy = new int[n][];
				for (var j = 0; j < matrCopy.Length; j++)
					matrCopy[j] = (int[]) matrix[j].Clone();

				var ecCopy = (int[])edgesCount.Clone();

				var mincut = CalcMinCutOneTime(random, matrCopy, m, ecCopy);

				if (mincut < min)
					min = mincut;
			}

			return min;
		}

		private static int CalcMinCutOneTime(Random random, int[][] matrix, int m, int[] edgesCount)
		{
			var n = matrix.Length;

			for (var i = n; i > 2; i--)
			{
				int u;
				int v;

				GetRandomEdgeIndex(random, matrix, edgesCount, m, out u, out v);

				Merge(matrix, edgesCount, u, v);

				var removedEdges = RemoveCycles(matrix, edgesCount, u);

				m -= removedEdges;
			}

			return m / 2;
		}

		private static void GetRandomEdgeIndex(Random random, int[][] matrix, int[] edgesCount, int m, out int u, out int v)
		{
			var c = random.Next(m);
			u = 0;
			while (true)
			{
				var ec = edgesCount[u];

				if (ec > 0 && ec >= c)
				{
					v = 0;
					while (true)
					{
						var e = matrix[u][v];

						if (e > 0)
						{
							c -= e;

							if (c <= 0)
								return;
						}

						v++;
					}
				}

				c -= ec;
				u++;
			}
		}

		private static void Merge(int[][] matrix, int[] edgesCount, int u, int v)
		{
			var n = matrix.Length;

			for (var i = 0; i < n; i++)
			{
				var add = matrix[v][i];

				if (add > 0)
				{
					matrix[u][i] += add;
					edgesCount[u] += add;
				}
			}

			for (var neighboorV = 0; neighboorV < n; neighboorV++)
				if (matrix[v][neighboorV] != 0)
				{
					var add = matrix[neighboorV][v];

					if (add > 0)
					{
						matrix[neighboorV][u] += add;
						edgesCount[neighboorV] += add;
					}

					var remove = matrix[neighboorV][v];

					if (remove > 0)
					{
						matrix[neighboorV][v] = 0;
						edgesCount[neighboorV] -= remove;
					}
				}

			Array.Clear(matrix[v], 0, n);
			edgesCount[v] = 0;
		}

		private static int RemoveCycles(int[][] matrix, int[] edgesCount, int u)
		{
			var removedEdges = matrix[u][u];
			matrix[u][u] = 0;
			edgesCount[u] -= removedEdges;
			return removedEdges;
		}
	}
}
