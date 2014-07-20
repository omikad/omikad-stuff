using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProblemSets.ComputerScience.DataTypes;

namespace Tests
{
	[TestClass]
	public class TestUnionFind
	{
		[TestMethod]
		public void UnionFindSimpleTest()
		{
			var union = new UnionFind(4);
			AssertNotUnion(union, 0, 1, 2, 3);

			union.Union(0, 1);
			AssertUnion(union, 0, 1);
			AssertNotUnion(union, 0, 2, 3);

			union.Union(2, 3);
			AssertUnion(union, 0, 1);
			AssertUnion(union, 2, 3);
			AssertNotUnion(union, 0, 2);

			union.Union(0, 2);
			AssertUnion(union, 0, 1, 2, 3);
		}

		private static void AssertUnion(UnionFind union, params int[] indices)
		{
			Assert.AreEqual(1, indices.Select(union.Find).Distinct().Count());
		}

		private static void AssertNotUnion(UnionFind union, params int[] indices)
		{
			Assert.AreEqual(indices.Length, indices.Select(union.Find).Distinct().Count());
		}
	}
}
