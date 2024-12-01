using System;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Linq;

namespace ProblemSets.ComputerScience
{
	[Export]
	public class NearestNeighborSearchTester
	{
		public void Go()
		{
			const int d = 100;
			const int n = 200000;
			const int tests = 1000;

			var rnd = new Random();

			var pointsSet = CreateRandomPointsSet_Uniform(d, n, rnd);

			var queryPoints = CreateRandomQueryPoints_NotFarFromSet(tests, d, rnd, n, pointsSet);

			Console.WriteLine("Memory: " + GC.GetTotalMemory(false) / 1024 / 1024);

			var timer = Stopwatch.StartNew();

			var searcher = new NearestNeighborSearch(pointsSet);

			Console.WriteLine("Algo init : " + (1d / queryPoints.Length * timer.ElapsedMilliseconds));

			timer.Restart();

			foreach (var query in queryPoints)
				searcher.Closest(query, 0.1);

			Console.WriteLine("By algo   : " + (1d / queryPoints.Length * timer.ElapsedMilliseconds));

			Console.WriteLine("Memory: " + GC.GetTotalMemory(false) / 1024 / 1024);
		}

		public double[][] CreateRandomQueryPoints_NotFarFromSet(int tests, int d, Random rnd, int n,
		                                                                double[][] pointsSet)
		{
			var queryPoints =
				Enumerable.Repeat(1, tests).Select(qi =>
					{
						var query = new double[d];

						var j = rnd.Next(n);

						for (var i = 0; i < d; i++)
							query[i] = pointsSet[i][j] + ((rnd.NextDouble() - 0.5) / 100);

						return query;
					})
				          .ToArray();
			return queryPoints;
		}

		public double[][] CreateRandomPointsSet_Uniform(int d, int n, Random rnd)
		{
			var pointsSet = Enumerable.Repeat(1, d)
			                          .Select(_ => Enumerable.Repeat(1, n).Select(__ => rnd.NextDouble()).ToArray())
			                          .ToArray();
			return pointsSet;
		}

		public int FindNearestBrute(double[] p, double[][] pointsSet)
		{
			var d = pointsSet.Length;
			var n = pointsSet[0].Length;

			var max = double.MaxValue;
			var pos = -1;

			for (var i = 0; i < n; i++)
			{
				var t = 0d;
				for (var j = 0; j < d; j++)
					t += Math.Pow(pointsSet[j][i] - p[j], 2);

				if (t < max)
				{
					max = t;
					pos = i;
				}
			}

			return pos;
		}
	}
}