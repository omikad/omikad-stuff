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
		public void CanGrowTreeForMovement()
		{
			var input = new[]
			{
				new[] {"left wall", "right wall", "move right"},
				new[] {"left wall", "right empty", "move right"},
				new[] {"left empty", "right wall", "move right"},
				new[] {"left empty", "right empty", "move right"},

				new[] {"left wall", "right wall", "move left"},
				new[] {"left wall", "right empty", "move left"},
				new[] {"left empty", "right wall", "move left"},
				new[] {"left empty", "right empty", "move left"},

				new[] {"left wall", "right wall", "no move"},
				new[] {"left wall", "right empty", "no move"},
				new[] {"left empty", "right wall", "no move"},
				new[] {"left empty", "right empty", "no move"},
			};

			var output = new[]
			{
				"stop", "move", "stop", "move",
				"stop", "stop", "move", "move",
				"stop", "stop", "stop", "stop",
			};

			var learner = new ID3Learner<string>(input, output);

			Console.WriteLine(learner);
		}

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
				new[] {"b", "a", "c"},
				new[] {"b", "a", "d"},
				new[] {"d", "a", "e"},
				new[] {"d", "b", "e"},
				new[] {"d", "c", "e"},
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
		public void CanGrowTreeNoLogic()
		{
			var input = new[]
			{
				new[] {"a", "a"},
				new[] {"a", "b"},
				new[] {"b", "a"},
				new[] {"b", "b"},

				new[] {"a", "a"},
				new[] {"a", "b"},
				new[] {"b", "a"},
				new[] {"b", "b"},
			};

			var output = new[] { "x", "x", "x", "x",  "y", "y", "y", "y" };

			var learner = new ID3Learner<string>(input, output);

			Console.WriteLine(learner);
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