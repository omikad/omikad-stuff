using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProblemSets.ComputerScience;

namespace Tests
{
	[TestClass]
	public class TestAllPairsShortestPath
	{
		readonly AllPairsShortestPath allPairsShortestPath = new AllPairsShortestPath();

		[TestMethod]
		public void Test_FloydWarshall_1()
		{
			Assert.AreEqual(-2, allPairsShortestPath.FloydWarshall(allPairsShortestPath.Read(@"4 5
1 2 1
1 3 2
2 4 2
3 4 1
4 1 -2")));
		}

		[TestMethod]
		public void Test_FloydWarshall_2()
		{
			Assert.AreEqual(-4, allPairsShortestPath.FloydWarshall(allPairsShortestPath.Read(@"8 11
1 2 10
1 8 8
2 6 2
3 2 1
3 4 1
4 5 3
5 6 -1
6 3 -2
7 6 -1
7 2 -4
8 7 1")));
		}

		[TestMethod]
		public void Test_FloydWarshall_3()
		{
			Assert.AreEqual(null, allPairsShortestPath.FloydWarshall(allPairsShortestPath.Read(@"8 11
1 2 10
1 8 8
2 6 2
3 2 1
3 4 1
4 5 3
5 6 -5
6 3 -2
7 6 -1
7 2 -4
8 7 1")));
		}

		[TestMethod]
		public void Test_FloydWarshall_4()
		{
			Assert.AreEqual(-6, allPairsShortestPath.FloydWarshall(allPairsShortestPath.Read(@"6 7
1 2 -2
2 3 -1
3 1 4
3 4 -3
3 5 2
6 5 1
6 4 -4")));
		}

		[TestMethod]
		public void Test_FloydWarshall_5()
		{
			Assert.AreEqual(1, allPairsShortestPath.FloydWarshall(allPairsShortestPath.Read(@"7 14
1 2 5
1 3 2
1 5 6
1 7 2
2 5 1
2 6 4
2 3 5
3 1 5
3 7 2
4 6 1
4 5 5
5 3 7
6 7 2
7 4 1")));
		}

		[TestMethod]
		public void Test_FloydWarshall_6()
		{
			Assert.AreEqual(null, allPairsShortestPath.FloydWarshall(allPairsShortestPath.Read(@"7 14
1 2 -5
1 3 2
1 5 -2
1 7 2
2 5 1
2 6 4
2 3 5
3 1 5
3 7 2
4 6 -1
4 5 5
5 3 7
6 7 -1
7 4 -1")));
		}

		[TestMethod]
		public void Test_FloydWarshall_7()
		{
			Assert.AreEqual(-3, allPairsShortestPath.FloydWarshall(allPairsShortestPath.Read(@"7 13
1 2 -2
1 3 2
1 5 -2
1 7 2
2 5 1
2 6 4
2 3 -1
3 1 5
3 7 2
4 5 5
5 3 7
6 7 1
7 4 -1")));
		}

		[TestMethod]
		public void Test_FloydWarshall_8()
		{
			Assert.AreEqual(-5, allPairsShortestPath.FloydWarshall(allPairsShortestPath.Read(@"10 32
1 2 2
1 3 4
1 4 1
1 8 5
1 10 -1
2 5 3
2 4 5
2 7 2
2 10 -1
3 6 3
3 7 4
4 1 1
4 5 2
4 2 -2
5 3 1
5 9 4
5 10 1
6 1 4
6 3 1
6 8 4
6 9 1
7 5 3
7 10 5
8 2 5
8 3 -1
8 5 4
9 5 -2
9 6 2
9 10 -1
10 1 3
10 2 4
10 8 -1")));
		}

		[TestMethod]
		public void Test_FloydWarshall_9()
		{
			Assert.AreEqual(-9, allPairsShortestPath.FloydWarshall(allPairsShortestPath.Read(@"14 35
1 2 2
1 3 4
1 4 1
1 8 5
1 10 -1
2 5 3
2 4 5
2 7 2
2 10 -1
3 6 3
3 7 4
4 1 1
4 5 2
4 2 -2
5 3 1
5 9 4
5 10 1
6 1 4
6 3 1
6 8 4
6 9 1
7 5 3
7 10 5
8 2 5
8 3 -1
8 5 4
9 5 -2
9 6 2
9 10 -1
10 1 3
10 2 4
10 8 -1
11 12 -1
12 13 -4
13 14 -4")));
		}

		[TestMethod]
		public void Test_FloydWarshall_10()
		{
			Assert.AreEqual(-8, allPairsShortestPath.FloydWarshall(allPairsShortestPath.Read(@"9 8 
1 2 -1
2 3 -1
3 4 -1
4 5 -1
5 6 -1
6 7 -1
7 8 -1
8 9 -1")));
		}
	}
}