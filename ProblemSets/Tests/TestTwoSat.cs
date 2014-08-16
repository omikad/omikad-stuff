using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProblemSets.ComputerScience;
using ProblemSets.Services;

namespace Tests
{
	[TestClass]
	public class TestTwoSat
	{
		[TestMethod]
		public void Test1()
		{
			Assert.IsNotNull(new TwoSat().Solve_Papadimitrou(2, @"1 2
-1 -2".SplitToLines()));
		}

		[TestMethod]
		public void Test2()
		{
			Assert.IsNotNull(new TwoSat().Solve_Papadimitrou(2, @"1 2
-1 2".SplitToLines()));
		}

		[TestMethod]
		public void Test3()
		{
			Assert.IsNull(new TwoSat().Solve_Papadimitrou(4, @"1 2
-1 -2
1 -2
-1 2".SplitToLines()));
		}

		[TestMethod]
		public void Test4()
		{
			Assert.IsNotNull(new TwoSat().Solve_Papadimitrou(5, @"1 -2
-1 -3
1 2
-3 4
-1 4".SplitToLines()));
		}

		[TestMethod]
		public void Test5()
		{
			Assert.IsNotNull(new TwoSat().Solve_Papadimitrou(6, @"1 2
1 -4
2 4
-2 -4
-1 4
2 -4".SplitToLines()));
		}

		[TestMethod]
		public void Test6()
		{
			Assert.IsNotNull(new TwoSat().Solve_Papadimitrou(6, @"1 2
1 -3
2 4
3 -4
-1 4
2 -4".SplitToLines()));
		}

		[TestMethod]
		public void Test7()
		{
			Assert.IsNotNull(new TwoSat().Solve_Papadimitrou(9, @"1 -2
3 -4
5 6
1 -6
2 -4
-2 8
8 9
-8 9
1 -9".SplitToLines()));
		}

		[TestMethod]
		public void Test8()
		{
			Assert.IsNotNull(new TwoSat().Solve_Papadimitrou(10, @"1 2
-1 -2
-3 -4
5 6
1 -6
-2 -4
-2 8
8 -9
-8 9
1 -9".SplitToLines()));
		}

		[TestMethod]
		public void Test9()
		{
			Assert.IsNull(new TwoSat().Solve_Papadimitrou(13, @"1 -2
-2 6
8 9
1 2
-6 8
6 7
-8 9
9 2
-9 -8
9 -3
-10 -9
-1 -10
-1 2".SplitToLines()));
		}

		[TestMethod]
		public void Test10()
		{
			Assert.IsNotNull(new TwoSat().Solve_Papadimitrou(13, @"1 2
-3 6
8 9
-10 9
-6 8
6 7
-8 9
9 2
9 -8
9 -3
10 -9
-1 -10
-2 10".SplitToLines()));
		}

		[TestMethod]
		public void Test11()
		{
			Assert.IsNull(new TwoSat().Solve_Papadimitrou(25, @"1 -2
-2 6
8 9
1 2
12 3
-6 8
6 7
8 -13
-8 9
9 2
-9 -8
9 -3
13 4
-10 -9
-1 -10
-1 2
-4 5
-12 8
13 -4
13 4
13 -6
2 4
2 -7
4 -13
-13 -12".SplitToLines()));
		}

		[TestMethod]
		public void Test12()
		{
			Assert.IsNotNull(new TwoSat().Solve_Papadimitrou(25, @"1 -2
-2 6
8 9
1 2
12 3
-6 8
6 7
8 -13
-8 9
9 2
-9 5
9 -3
13 6
-10 -9
-1 -10
-1 2
-4 5
12 8
13 -4
13 5
13 -6
2 4
2 -7
4 -13
13 5".SplitToLines()));
		}
	}
}