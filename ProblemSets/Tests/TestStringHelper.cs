using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProblemSets;

namespace Tests
{
	[TestClass]
	public class TestStringHelper
	{
		[TestMethod]
		public void TestCommonPrefix()
		{
			Assert.AreEqual("", StringHelper.CommonPrefix(null, null));
			Assert.AreEqual("", StringHelper.CommonPrefix(null, "asdf"));
			Assert.AreEqual("", "asdf".CommonPrefix(null));

			Assert.AreEqual("as", "asdf".CommonPrefix("as"));
			Assert.AreEqual("asdf", "asdf".CommonPrefix("asdffffff"));
			Assert.AreEqual("asdf", "asdfqwer".CommonPrefix("asdfzxcv"));
		}
	}
}