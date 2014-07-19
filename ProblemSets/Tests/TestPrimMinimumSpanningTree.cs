using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProblemSets.ComputerScience;
using ProblemSets.Services;

namespace Tests
{
	[TestClass]
	public class TestPrimMinimumSpanningTree
	{
		private MinimumSpanningTree_Prim mst;

		[TestInitialize]
		public void SetUp()
		{
			mst = new MinimumSpanningTree_Prim();
		}

		[TestMethod]
		public void CanCalcCorrectly()
		{
			Assert.AreEqual(2624, mst.CalcMinimumSpanningTreeWeight(@"6 7
1 2 2474
2 4 -246
4 3 640
4 5 2088
3 6 4586
6 5 3966
5 1 -3824".SplitToLines()));
		}

		[TestMethod]
		public void CanCalcCorrectly2()
		{
			Assert.AreEqual(-684, mst.CalcMinimumSpanningTreeWeight(@"11 19
3 10 7419
8 3 2973
4 3 -5114
9 7 500
4 2 -3774
10 3 -4104
5 10 6338
5 9 1778
3 2 -9623
7 9 8100
3 2 9938
5 7 9877
4 8 8465
11 6 5133
4 9 4888
5 9 5178
8 7 5973
11 3 -3854
3 1 6739".SplitToLines()));
		}
	}
}
