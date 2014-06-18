using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProblemSets.ComputerScience.DataTypes;

namespace Tests
{
	[TestClass]
	public class TestHashTable
	{
		[TestMethod]
		public void HashTableOpenAddressingCanHashCollisionsCorrectly()
		{
			var hashset = new HashTable_OpenAddressing(10);

			var hash = hashset.GetHash(1);
			var collisions = new List<long> { 1 };

			for (var i = 2; i < 10000 && collisions.Count < 5; i++)
				if (hashset.GetHash(i) == hash)
					collisions.Add(i);

			for (var i = 0; i < collisions.Count; i++)
			{
				hashset.Add(collisions[i]);

				Assert.IsTrue(hashset.Contains(collisions[i]));

				for (var j = i + 1; j < collisions.Count; j++)
					Assert.IsFalse(hashset.Contains(collisions[j]), new { i, j }.ToString());
			}

			for (var i = 0; i < 100; i++)
				Assert.AreEqual(collisions.Contains(i), hashset.Contains(i));
		}
	}
}
