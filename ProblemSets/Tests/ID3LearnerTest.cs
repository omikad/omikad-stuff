using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProblemSets.ComputerScience.DataTypes;
using ProblemSets.MachineLearning.RandomForests;

namespace Tests
{
	[TestClass]
	public class TestID3Learner
	{
		[TestMethod]
		public void CanGrowTrivialTreePresent()
		{
			var input = new[]
			{
				new[] {"a", "a", "a"},
				new[] {"a", "b", "b"},
				new[] {"a", "c", "c"},
			};

			var output = new[] { "e", "e", "e" };

			var learner = new ID3Learner<string>(input, output);

			Console.WriteLine(learner);

			Assert.IsTrue(learner.Root.IsLeaf);
			Assert.AreEqual("e", learner.OutputFactors[learner.Root.Factor]);
		}

		[TestMethod]
		public void CanGrowTrivialTreeNotPresent()
		{
			var input = new[]
			{
				new[] {"a", "a", "a"},
				new[] {"a", "b", "b"},
				new[] {"d", "c", "c"},
			};

			var output = new[] { "e", "e", "e" };

			var learner = new ID3Learner<string>(input, output);

			Console.WriteLine(learner);

			Assert.IsTrue(learner.Root.IsLeaf);
			Assert.AreEqual("e", learner.OutputFactors[learner.Root.Factor]);
		}

		[TestMethod]
		public void CanGrowTree()
		{
			var input = new[]
			{
				new[] {"a", "b", "c"},
				new[] {"a", "b", "d"},
				new[] {"a", "d", "e"},
				new[] {"b", "d", "e"},
				new[] {"c", "d", "e"},
			};

			var output = new[] {"has a", "has a", "has a", "no a", "no a" };

			var learner = new ID3Learner<string>(input, output);

			Console.WriteLine(learner);

			Assert.IsFalse(learner.Root.IsLeaf);
			Assert.AreEqual("a", learner.InputFactors[learner.Root.Factor]);

			var aPresent = learner.Root.Present;
			Assert.IsTrue(aPresent.IsLeaf);
			Assert.AreEqual("has a", learner.OutputFactors[aPresent.Factor]);

			var aAbsent = learner.Root.Absent;
			Assert.IsTrue(aAbsent.IsLeaf);
			Assert.AreEqual("no a", learner.OutputFactors[aAbsent.Factor]);
		}

		[TestMethod]
		public void EntropyIsCorrect()
		{
			AssertHelper.DoubleIsNear(0, ID3LearnerInt.Entropy(new[] {0}, 1, new BitArrayX(1, true)));
			AssertHelper.DoubleIsNear(1, ID3LearnerInt.Entropy(new[] {0, 0, 1, 1}, 2, new BitArrayX(4, true)));
			AssertHelper.DoubleIsNear(3, ID3LearnerInt.Entropy(new[] {0, 1, 2, 3, 4, 5, 6, 7}, 8, new BitArrayX(8, true)));
		}
	}
}