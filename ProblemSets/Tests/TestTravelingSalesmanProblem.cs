using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProblemSets.ComputerScience;
using ProblemSets.Services;

namespace Tests
{
	[TestClass]
	public class TestTravelingSalesmanProblem
	{
		[TestMethod]
		public void Test1()
		{
			AssertHelper.DoubleIsNear(4, new TravelingSalesmanProblem().CalcTSP_Euclidian(@"0 0
1 0
1 1
0 1".SplitToLines()));
		}

		[TestMethod]
		public void Test2()
		{
			AssertHelper.DoubleIsNear(10.4721, new TravelingSalesmanProblem().CalcTSP_Euclidian(@"0 2
1 0
2 0
2 1
4 2".SplitToLines()), 0.0001);
		}

		[TestMethod]
		public void Test3()
		{
			AssertHelper.DoubleIsNear(6.17986, new TravelingSalesmanProblem().CalcTSP_Euclidian(@"2.25 1.62
3.00 1.00
1.00 0.00
1.00 1.54
2.00 1.00".SplitToLines()), 0.0001);
		}

		[TestMethod]
		public void Test4()
		{
			AssertHelper.DoubleIsNear(124.9658, new TravelingSalesmanProblem().CalcTSP_Euclidian(@"1.32 56.4
24.5 12.
33.3 41.455
12.44 52.4
12.55 46.6
12.5 21.21
19.34 34.2".SplitToLines()), 0.0001);
		}

		[TestMethod]
		public void Test5()
		{
			AssertHelper.DoubleIsNear(16898.13085, new TravelingSalesmanProblem().CalcTSP_Euclidian(@"20833.3333 17100.0000
20900.0000 17066.6667
21300.0000 13016.6667
21600.0000 14150.0000
21600.0000 14966.6667
21600.0000 16500.0000
22183.3333 13133.3333
22583.3333 14300.0000
22683.3333 12716.6667
23616.6667 15866.6667
23700.0000 15933.3333
23883.3333 14533.3333
24166.6667 13250.0000
25149.1667 12365.8333".SplitToLines()), 0.0001);
		}

		[TestMethod]
		public void Test6()
		{
			AssertHelper.DoubleIsNear(26714.873046, new TravelingSalesmanProblem().CalcTSP_Euclidian(@"20833.3333 17100.0000
21600.0000 16500.0000
22183.3333 13133.3333
22583.3333 14300.0000
23883.3333 14533.3333
24166.6667 13250.0000
25149.1667 12365.8333
26133.3333 14500.0000
26683.3333 8766.6667
26283.3333 12766.6667
26433.3333 13433.3333
26733.3333 11683.3333
27096.1111 13415.8333
27166.6667 9833.33331
27233.3333 10450.0000
28166.6667 12833.33331".SplitToLines()), 0.0001);
		}
	}
}