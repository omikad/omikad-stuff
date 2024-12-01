using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProblemSets.ComputerScience;

namespace Tests
{
	[TestClass]
	public class TestNearestNeighborSearch
	{
		[TestMethod]
		public void CanFindNearestForUniform()
		{
			const int d = 10;
			const int n = 100;
			const int tests = 100;

			var tester = new NearestNeighborSearchTester();

			var rnd = new Random();

			var pointsSet = tester.CreateRandomPointsSet_Uniform(d, n, rnd);

			var queryPoints = tester.CreateRandomQueryPoints_NotFarFromSet(tests, d, rnd, n, pointsSet);

			var brutes = queryPoints
				.Select(query => tester.FindNearestBrute(query, pointsSet))
				.ToArray();

			var searcher = new NearestNeighborSearch(pointsSet);

			var algos = queryPoints
				.Select(query => searcher.Closest(query, 0.1))
				.ToArray();

			for (var i = 0; i < queryPoints.Length; i++)
				Assert.AreEqual(brutes[i], algos[i]);
		}
	}
}
