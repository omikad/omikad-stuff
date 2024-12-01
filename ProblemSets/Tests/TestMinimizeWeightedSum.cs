using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProblemSets.ComputerScience;
using ProblemSets.Services;

namespace Tests
{
	[TestClass]
	public class TestMinimizeWeightedSum
	{
		private MinimizeWeightedSum mws;

		[TestInitialize]
		public void Init()
		{
			mws = new MinimizeWeightedSum();
		}

		[TestMethod]
		public void CanCalcCorrectly()
		{
			var actual = mws.CalcForSubstractionDivision(@"5
48	14
4	90
64	22
54	66
46	6".SplitToLines());

			Assert.AreEqual(11336, actual.Item1);
			Assert.AreEqual(10548, actual.Item2);
		}

		[TestMethod]
		public void CanCalcCorrectly2()
		{
			var actual = mws.CalcForSubstractionDivision(@"18
50	18
10	44
94	8
30	26
98	68
78	6
2	56
54	20
30	40
56	62
60	22
92	10
98	52
4	52
80	36
12	88
32	86
8	88".SplitToLines());

			Assert.AreEqual(145924, actual.Item1);
			Assert.AreEqual(138232, actual.Item2);
		}
	}
}
