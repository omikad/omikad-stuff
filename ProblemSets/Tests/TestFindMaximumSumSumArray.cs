using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProblemSets.ComputerScience;

namespace Tests
{
	[TestClass]
	public class TestFindMaximumSumSumArray
	{
		[TestMethod]
		public void Test_FindMaximumSumSumArray_Mine()
		{
			var runme = new FindMaximumSumSubArray();

			AssertHelper.Seq(new int[0], runme.Solve(new [] { -1 }));
			AssertHelper.Seq(new int[0], runme.Solve(new [] { -1, -2 }));
			AssertHelper.Seq(new[] { 5 }, runme.Solve(new [] { -1, -2, 5 }));
			AssertHelper.Seq(new[] { 5 }, runme.Solve(new [] { 5, -1, -2 }));
			AssertHelper.Seq(new[] { 7 }, runme.Solve(new [] { 7, -1, -2 }));

			AssertHelper.Seq(new[] { 5, -1, -2, 3 }, runme.Solve(new[] { 5, -1, -2, 3 }));
			AssertHelper.Seq(new[] { 5, -1, -2, 3 }, runme.Solve(new[] { 5, -1, -2, 3, -6, 4 }));
			AssertHelper.Seq(new[] { 4, 100 }, runme.Solve(new[] { 5, -1, -2, 2, -6, 4, 100 }));
			AssertHelper.Seq(new[] { 10, -1, -2, 2, -6, 4, 100 }, runme.Solve(new[] { 10, -1, -2, 2, -6, 4, 100 }));
		}

		[TestMethod]
		public void Test_FindMaximumSumSumArray_Kadane()
		{
			var runme = new FindMaximumSumSubArray();

			AssertHelper.Seq(new[] { -1 }, runme.Solution_Kadane(new[] { -1 }));
			AssertHelper.Seq(new[] { -1 }, runme.Solution_Kadane(new[] { -1, -2 }));
			AssertHelper.Seq(new[] { 5 }, runme.Solution_Kadane(new[] { -1, -2, 5 }));
			AssertHelper.Seq(new[] { 5 }, runme.Solution_Kadane(new[] { 5, -1, -2 }));
			AssertHelper.Seq(new[] { 7 }, runme.Solution_Kadane(new[] { 7, -1, -2 }));

			AssertHelper.Seq(new[] { 5, -1, -2, 3 }, runme.Solution_Kadane(new[] { 5, -1, -2, 3 }));
			AssertHelper.Seq(new[] { 5, -1, -2, 3 }, runme.Solution_Kadane(new[] { 5, -1, -2, 3, -6, 4 }));
			AssertHelper.Seq(new[] { 4, 100 }, runme.Solution_Kadane(new[] { 5, -1, -2, 2, -6, 4, 100 }));
			AssertHelper.Seq(new[] { 10, -1, -2, 2, -6, 4, 100 }, runme.Solution_Kadane(new[] { 10, -1, -2, 2, -6, 4, 100 }));
		}
	}
}