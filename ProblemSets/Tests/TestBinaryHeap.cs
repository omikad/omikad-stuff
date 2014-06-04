using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProblemSets.ComputerScience.DataTypes;
using ProblemSets.Services;

namespace Tests
{
	[TestClass]
	public class TestBinaryHeap
	{
		[TestMethod]
		public void BinaryHeapCanGetMin()
		{
			TestHeap(1, 1);
			TestHeap(1, 1, 2);
			TestHeap(3, 5, 3);
			TestHeap(1, 4, 3, 2, 1);
			TestHeap(1, 9, 8, 7, 6, 5, 4, 3, 2, 1);
			TestHeap(1, 5, 6, 7, 8, 9, 1, 2, 3, 4);
			TestHeap(1, 9, 5, 8, 6, 10, 11, 4, 3, 2, 1, 7, 12);
		}

		[TestMethod]
		public void BinaryHeapCanDeleteMin()
		{
			TestHeap(2, 1, -1, 2);
			TestHeap(2, 1, 2, -1);
			TestHeap(3, 1, 2, 3, -1, -1);
			TestHeap(3, 1, -1, 2, -1, 3);
			TestHeap(3, 9, 8, 7, 6, 5, 4, 3, 2, 1, -1, -1);
			TestHeap(14, 14, 22, 14, 4, -1, 27);
		}

		[TestMethod]
		public void BinaryHeapRandomTest()
		{
			var random = new Random();

			for (var ops = 3; ops < 20; ops++)
			{
				for (var cnt = 0; cnt < 1000; cnt++)
				{
					var heap = new BinaryHeap();

					var operations = new List<int>();

					for (var i = 0; i < ops; i++)
					{
						if (heap.Count > 0 && random.NextDouble() < 0.2)
						{
							heap.DeleteMin();
							operations.Add(-1);
						}
						else
						{
							var number = random.Next(30);
							operations.Add(number);
							heap.Insert(number);
						}
					}

					{
						var number = random.Next(30);
						operations.Add(number);
						heap.Insert(number);
					}

					AssertHelper.Equal(heap.Min(), heap.Min, () => new { items = ", ".Join(operations), actualMin = heap.Min }.ToString());
				}
			}
		}

		private static void TestHeap(int expectedMin, params int[] values)
		{
			var heap = new BinaryHeap();

			foreach (var value in values)
				if (value > 0)
					heap.Insert(value);
				else
					heap.DeleteMin();

			Assert.AreEqual(expectedMin, heap.Min);
		}
	}
}