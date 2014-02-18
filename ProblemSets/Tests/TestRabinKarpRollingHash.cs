using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProblemSets.ComputerScience;
using ProblemSets.Problems;

namespace Tests
{
	[TestClass]
	public class TestRabinKarpRollingHash
	{
		[TestMethod]
		public void Test_RabinKarpRollingHash_SmallA()
		{
			var hash = new RabinKarpRollingHash(4, 7);
			Assert.AreEqual(343u, hash.Eat((char)1));
			Assert.AreEqual(343u + 2 * 49, hash.Eat((char)2));
			Assert.AreEqual(343u + 2 * 49 + 3 * 7, hash.Eat((char)3));

			Assert.AreEqual(343u + 2 * 49 + 3 * 7 + 4, hash.Eat((char)4));

			Assert.AreEqual(2u * 343 + 3 * 49 + 4 * 7 + 5, hash.Shift((char)1, (char)5));
		}

		[TestMethod]
		public void Test_RabinKarpRollingHash_StringContains()
		{
			var searcher = new SubstringContains();
			Assert.AreEqual(2, searcher.RabinKarpRollingHash("asdfxcv", "dfxc"));
			Assert.AreEqual(0, searcher.RabinKarpRollingHash("asdfxcv", "asdfx"));
			Assert.AreEqual(5, searcher.RabinKarpRollingHash("01234asdfxcv", "a"));
			Assert.AreEqual(5, searcher.RabinKarpRollingHash("01234asdf", "asdf"));
		}
	}
}