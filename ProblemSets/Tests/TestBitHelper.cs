using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProblemSets.Services;

namespace Tests
{
	[TestClass]
	public class TestBitHelper
	{
		[TestMethod]
		public void IsCountBitSetCorrect()
		{
			Assert.AreEqual(0, 0.CountBitSet());
			Assert.AreEqual(1, 1.CountBitSet());
			Assert.AreEqual(1, 2.CountBitSet());
			Assert.AreEqual(2, 3.CountBitSet());
			Assert.AreEqual(32, (-1).CountBitSet());
			Assert.AreEqual(8, (0xFF).CountBitSet());
			Assert.AreEqual(9, (0x10FF).CountBitSet());
		}
	}
}