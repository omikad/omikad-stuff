using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProblemSets.ComputerScience.DataTypes;

namespace Tests
{
	[TestClass]
	public class BitArrayXTest
	{
		[TestMethod]
		public void CanCreateAllTrues()
		{
			for (var length = 0; length < 200; length++)
			{
				var expected = new BitArrayX(length);
				for (var i = 0; i < length; i++)
					expected[i] = true;

				var actual = new BitArrayX(length, true);
				var isCorrect = BitArrayX.Equals(expected, actual);

				Assert.IsTrue(isCorrect);
			}
		}
	}
}