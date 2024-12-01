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
		public void CanGrowComplexTree()
		{
			var input = new[]
			{
				new[] {"b", "a", "c", "f"},
				new[] {"b", "a", "d"},
				new[] {"d", "a", "e"},
				new[] {"d", "b"},
				new[] {"d", "c"},
				new[] {"d", "a", "c", "f"},
			};

			var output = new[] { "has ab", "has ab", "has ad", "no a", "no a", "has ad" };

			var learner = new ID3Learner<string>(input, output);

			Console.WriteLine(learner);

			Assert.AreEqual("has ab", learner.Predict(new [] { "a", "b" }));
			Assert.AreEqual("has ab", learner.Predict(new [] { "a", "b", "c", "f" }));
			Assert.AreEqual("has ad", learner.Predict(new [] { "a", "d", "f" }));
			Assert.AreEqual("no a", learner.Predict(new [] { "b", "d", "f" }));
		}


		[TestMethod]
		public void CanGrowTree()
		{
			var input = new[]
			{
				new[] {"b", "a", "c", "f"},
				new[] {"b", "a", "d"},
				new[] {"d", "a", "e"},
				new[] {"d", "b"},
				new[] {"d", "c"},
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
			var subsetEntropyZero = new SubsetMask(new BitArrayX(1, true), 1, new[] { 0 }, 1);
			AssertHelper.DoubleIsNear(0, subsetEntropyZero.Entropy);
			Assert.AreEqual(0, subsetEntropyZero.EntropyZeroFactor);

			var subsetEntropyOne = new SubsetMask(new BitArrayX(4, true), 4, new[] { 0, 0, 1, 1 }, 2);
			AssertHelper.DoubleIsNear(1, subsetEntropyOne.Entropy);
			Assert.AreEqual(-1, subsetEntropyOne.EntropyZeroFactor);

			var subsetEntropyThree = new SubsetMask(new BitArrayX(8, true), 8, new[] { 0, 1, 2, 3, 4, 5, 6, 7 }, 8);
			AssertHelper.DoubleIsNear(3, subsetEntropyThree.Entropy);
		}
	}
}