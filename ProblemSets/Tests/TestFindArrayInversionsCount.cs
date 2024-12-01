using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProblemSets.ComputerScience;
using ProblemSets.Services;

namespace Tests
{
	[TestClass]
	public class TestFindArrayInversionsCount
	{
		[TestMethod]
		public void MergeAndCountInversionsWorksWell6()
		{
			var solver = new FindArrayInversionsCount();

			var sortedArray = new int[6];

			Assert.AreEqual(0ul, solver.MergeAndCountInversions(
				new[] { 1, 2, 3, 4, 5, 6 },
				0, 3,
				3, 3,
				sortedArray,
				0));
			AssertHelper.Seq(new[] { 1, 2, 3, 4, 5, 6 }, sortedArray);

			Assert.AreEqual(3ul, solver.MergeAndCountInversions(
				new[] { 1, 2, 20, 8, 9, 10 },
				0, 3,
				3, 3,
				sortedArray,
				0));
			AssertHelper.Seq(new[] { 1, 2, 8, 9, 10, 20}, sortedArray);

			Assert.AreEqual(6ul, solver.MergeAndCountInversions(
				new[] { 1, 19, 20, 8, 9, 10 },
				0, 3,
				3, 3,
				sortedArray,
				0));
			AssertHelper.Seq(new[] { 1, 8, 9, 10, 19, 20 }, sortedArray);

			Assert.AreEqual(5ul, solver.MergeAndCountInversions(
				new[] { 1, 15, 20, 8, 9, 17 },
				0, 3,
				3, 3,
				sortedArray,
				0));
			AssertHelper.Seq(new[] { 1, 8, 9, 15, 17, 20 }, sortedArray);
		}

		[TestMethod]
		public void MergeAndCountInversionsWorksWellOnPartialRight()
		{
			var solver = new FindArrayInversionsCount();

			var sortedArray = new int[4];

			Assert.AreEqual(0ul, solver.MergeAndCountInversions(
				new[] { 1, 2, 3, 4 },
				0, 3,
				3, 1,
				sortedArray,
				0));
			AssertHelper.Seq(new[] { 1, 2, 3, 4 }, sortedArray);

			Assert.AreEqual(1ul, solver.MergeAndCountInversions(
				new[] { 1, 2, 20, 8 },
				0, 3,
				3, 1,
				sortedArray,
				0));
			AssertHelper.Seq(new[] { 1, 2, 8, 20}, sortedArray);

			sortedArray = new int[5];
			Assert.AreEqual(4ul, solver.MergeAndCountInversions(
				new[] { 1, 19, 20, 8, 9 },
				0, 3,
				3, 2,
				sortedArray,
				0));
			AssertHelper.Seq(new[] { 1, 8, 9, 19, 20 }, sortedArray);
		}

		[TestMethod]
		public void MergeAndCountInversionsWorksWellTrivial()
		{
			var solver = new FindArrayInversionsCount();

			var sortedArray = new int[2];

			Assert.AreEqual(0ul, solver.MergeAndCountInversions(
				new[] {1, 2},
				0, 1,
				1, 1,
				sortedArray,
				0));
			AssertHelper.Seq(new[] {1, 2}, sortedArray);

			Assert.AreEqual(1ul, solver.MergeAndCountInversions(
				new[] {2, 1},
				0, 1,
				1, 1,
				sortedArray,
				0));
			AssertHelper.Seq(new[] {1, 2}, sortedArray);
		}

		[TestMethod]
		public void CanFindInversionsCount()
		{
			var solver = new FindArrayInversionsCount();

			Assert.AreEqual(1ul, solver.CalcInversionsCount(new[] { 6, 5 }));
			Assert.AreEqual(3ul, solver.CalcInversionsCount(new[] { 6, 5, 4 }));
			Assert.AreEqual(6ul, solver.CalcInversionsCount(new[] { 6, 5, 4, 3 }));
			Assert.AreEqual(15ul, solver.CalcInversionsCount(new[] { 6, 5, 4, 3, 2, 1 }));

			Assert.AreEqual(6ul, solver.CalcInversionsCount(new[] { 1, 2, 6, 5, 4, 3 }));
			Assert.AreEqual(6ul, solver.CalcInversionsCount(new[] { 1, 2, 6, 0, 5, 4, 18 }));

			Assert.AreEqual(66ul, solver.CalcInversionsCount(new[] { 12, 11, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1 }));
		}

		[TestMethod]
		public void CanFindInversionsForRandom()
		{
			var solver = new FindArrayInversionsCount();

			for (var size = 2; size < 100; size++)
			{
				var array = Enumerable.Range(1, size).ToArray();
				array.Shuffle();

				Console.WriteLine(size);

				var expected = solver.CalcInversionsCountBrute(array);
				var actual = solver.CalcInversionsCount(array.ToArray());

				if (expected != actual)
					Console.WriteLine(", ".Join(array));

				Assert.AreEqual(expected, actual);
			}
		}

		[TestMethod]
		public void CanFindInversionsForLargeRandom()
		{
			var solver = new FindArrayInversionsCount();

			var array = Enumerable.Range(0, 1000).ToArray();
			array.Shuffle();

			Assert.AreEqual(solver.CalcInversionsCountBrute(array), solver.CalcInversionsCount(array));
		}
	}
}