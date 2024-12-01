using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProblemSets.ComputerScience;

namespace Tests
{
	[TestClass]
	public class TestDijkstraShortestPath
	{
		[TestMethod]
		public void CanCalcShortestPath1()
		{
			var graph = new[]
			{
				"1		2,7		6,14		3,9",
				"2		1,7		3,10		4,15",
				"3		1,9		2,10		6,2		4,11",
				"4		2,15	3,11		5,6",
				"5		4,6		6,9",
				"6		1,14	3,2			5,9"
			};

			var paths = new DijkstraShortestPath().CalcShortestPaths(graph, 1);

			AssertHelper.Seq(new long[] {0, 7, 9, 20, 20, 11}, paths.Skip(1));
		}

		[TestMethod]
		public void CanCalcShortestPath2()
		{
			var graph = new[]
			{
				"1 2,3 3,3 ",
				"2 3,1 4,2 ",
				"3 4,50	   ",
				"4 		   ",
			};

			var paths = new DijkstraShortestPath().CalcShortestPaths(graph, 1);

			AssertHelper.Seq(new long[] { 0, 3, 3, 5 }, paths.Skip(1));
		}

		[TestMethod]
		public void CanCalcShortestPath3()
		{
			var graph = new[]
			{
				"1 2,3 3,5",
				"2 3,1 4,2",
				"3 4,50	  ",
				"4 		  ",
			};

			var paths = new DijkstraShortestPath().CalcShortestPaths(graph, 1);

			AssertHelper.Seq(new long[] { 0, 3, 4, 5 }, paths.Skip(1));
		}

		[TestMethod]
		public void CanCalcShortestPath4()
		{
			var graph = new[]
			{
				"1 2,8 3,15	  ",
				"2 1,7 3,4 4,5",
				"3 1,12		  ",
				"4 3,5		  ",
			};

			var paths = new DijkstraShortestPath().CalcShortestPaths(graph, 1);

			AssertHelper.Seq(new long[] { 0, 8, 12, 13 }, paths.Skip(1));
		}

		[TestMethod]
		public void CanCalcShortestPath5()
		{
			var graph = new[]
			{
				"1 2,8 3,17",
				"2 1,7 3,10 4,5",
				"3 1,12",
				"4 3,3"
			};

			var paths = new DijkstraShortestPath().CalcShortestPaths(graph, 1);

			AssertHelper.Seq(new long[] { 0, 8, 16, 13 }, paths.Skip(1));
		}
	}
}