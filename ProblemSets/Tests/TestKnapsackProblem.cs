using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProblemSets.ComputerScience;
using ProblemSets.Services;

namespace Tests
{
	[TestClass]
	public class TestKnapsackProblem
	{
		[TestMethod]
		public void Test1()
		{
			Assert.AreEqual(60, new KnapsackProblem().Solve(@"100 10
10 70
10 60
10 50
10 40
10 30
10 20
10 10
10 5
10 2
10 1".SplitToLines()));
		}

		[TestMethod]
		public void Test2()
		{
			Assert.AreEqual(60, new KnapsackProblem().Solve(@"100 10
10 1 
10 2
10 5
10 10
10 20
10 30
10 40
10 50
10 60
10 70".SplitToLines()));
		}

		[TestMethod]
		public void Test3()
		{
			Assert.AreEqual(60, new KnapsackProblem().Solve(@"100 10
10 5
10 70
10 50
10 40
10 30
10 60
10 20
10 10
10 2
10 1".SplitToLines()));
		}

		[TestMethod]
		public void Test4()
		{
			Assert.AreEqual(27000, new KnapsackProblem().Solve(@"6 10
7000 1
6000 1
5000 1
4000 1
3000 1
2000 1
1000 1
500 1
250 1
125 1".SplitToLines()));
		}
	}
}