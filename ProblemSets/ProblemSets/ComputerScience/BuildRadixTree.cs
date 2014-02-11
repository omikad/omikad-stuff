using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using ProblemSets.ComputerScience.DataTypes;

namespace ProblemSets.ComputerScience
{
	[Export]
	public class BuildRadixTree
	{
		// End of word marked by .

		public void Go()
		{
			var strings = new[]
			{
				"romane",
				"romanus",
				"romulus",
				"rubens",
				"ruber",
				"rubic",
				"rubicon",
				"rubicundus",
			};

			Console.WriteLine(new Radix(strings));
			Console.WriteLine();
			Console.WriteLine(BuildRadixByInserting(strings));
		}

		private static Radix BuildRadixByInserting(IEnumerable<string> strings)
		{
			var radix = new Radix(Enumerable.Empty<string>());

			foreach (var s in strings)
				radix.Insert(s);

			return radix;
		}
	}
}
