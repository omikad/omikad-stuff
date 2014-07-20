using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProblemSets.ComputerScience;
using ProblemSets.Services;

namespace Tests
{
	[TestClass]
	public class TestMaxSpacingKClustering
	{
		[TestMethod]
		public void Test1()
		{
			var data = @"10
1 2 134365
1 3 847434
1 4 763775
1 5 255070
1 6 495436
1 7 449492
1 8 651593
1 9 788724
1 10 93860
2 3 28348
2 4 835766
2 5 432768
2 6 762281
2 7 2107
2 8 445388
2 9 721541
2 10 228763
3 4 945271
3 5 901428
3 6 30590
3 7 25446
3 8 541413
3 9 939150
3 10 381205
4 5 216600
4 6 422117
4 7 29041
4 8 221692
4 9 437888
4 10 495813
5 6 233085
5 7 230867
5 8 218782
5 9 459604
5 10 289782
6 7 21490
6 8 837578
6 9 556455
6 10 642295
7 8 185907
7 9 992544
7 10 859947
8 9 120890
8 10 332696
9 10 721485".SplitToLines();

			Assert.AreEqual(134365L, new MaxSpacingKClustering().GetMaxSpacing(4, data));
		}

		[TestMethod]
		public void Test2()
		{
			var data = @"6
1 2 1 
1 3 3 
1 4 8 
1 5 12 
1 6 13 
2 3 2 
2 4 14 
2 5 11 
2 6 10 
3 4 15 
3 5 17 
3 6 16 
4 5 7 
4 6 19 
5 6 9 ".SplitToLines();

			Assert.AreEqual(7, new MaxSpacingKClustering().GetMaxSpacing(4, data));
		}

		[TestMethod]
		public void Test3()
		{
			var data = @"6 7
1 1 1 0 0 0 0
1 1 0 0 0 0 0
1 0 1 0 0 0 1
0 0 0 1 1 1 1
0 0 0 1 0 1 1
1 0 1 1 0 1 1".SplitToLines();
			Assert.AreEqual(1, new MaxSpacingKClustering().MaxSpacing_ForHamming(data));
		}

		[TestMethod]
		public void Test4()
		{
			var data = @"6 7
1 0 0 0 1 0 0
1 0 0 1 1 0 0
1 1 0 0 0 1 1
0 0 0 0 0 1 1
0 0 0 0 0 0 1
0 1 0 1 1 0 1".SplitToLines();
			Assert.AreEqual(3, new MaxSpacingKClustering().MaxSpacing_ForHamming(data));
		}

		[TestMethod]
		public void Test5()
		{
			var data = @"15 8
1 1 0 0 1 0 0 0 
0 0 1 1 1 1 1 1 
1 0 1 0 1 0 0 1 
0 0 1 0 0 1 1 0 
1 0 1 0 1 1 1 0 
1 1 0 1 1 0 1 1 
1 0 1 0 0 1 1 1 
1 1 1 0 0 1 0 0 
0 0 0 0 0 0 0 1 
0 1 0 0 0 1 1 0 
1 1 0 0 0 0 0 0 
1 0 0 1 0 1 1 0 
0 0 1 1 1 1 1 0 
0 0 1 0 1 0 1 1 
0 0 0 1 1 1 1 0 ".SplitToLines();
			Assert.AreEqual(4, new MaxSpacingKClustering().MaxSpacing_ForHamming(data));
		}
	}
}
