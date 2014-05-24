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
			const int d = 10;
			const int n = 10000;
			const int tests = 1000;

			var rnd = new Random();

			var pointsSet = Enumerable.Repeat(1, d)
			                          .Select(_ => Enumerable.Repeat(1, n).Select(__ => rnd.NextDouble()).ToArray())
			                          .ToArray();

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

			var timer = Stopwatch.StartNew();

			var brutes = queryPoints
				.Select(query => FindNearestBrute(query, pointsSet))
				.ToArray();

			Console.WriteLine("Exhaustive: " + (1d / queryPoints.Length * timer.ElapsedMilliseconds));

			timer.Restart();

			var searcher = new NearestNeighborSearch(pointsSet);

			Console.WriteLine("Algo init : " + (1d / queryPoints.Length * timer.ElapsedMilliseconds));

			timer.Restart();

			var algos = queryPoints
				.Select(query => searcher.Closest(query, 0.1))
				.ToArray();

			Console.WriteLine("By algo   : " + (1d / queryPoints.Length * timer.ElapsedMilliseconds));

			for (var i = 0; i < queryPoints.Length; i++)
				if (brutes[i] != algos[i])
					throw new InvalidOperationException(new { expected = brutes[i], actual = algos[i], i, tests }.ToString());
		}

		private static int FindNearestBrute(double[] p, double[][] pointsSet)
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