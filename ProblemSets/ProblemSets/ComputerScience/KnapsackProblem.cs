using System;
using System.Linq;
using ProblemSets.Services;

namespace ProblemSets.ComputerScience
{
	public class KnapsackProblem
	{
		public void Go()
		{
		}

		public long Solve(string[] data)
		{
			var knapsackSize = data[0].SplitBySpaces()[0].ToLong();

			var items = data.Skip(1).Select(
				i =>
				{
					var parsed = i.SplitBySpaces();
					return new Item
					{
						Value = parsed[0].ToLong(),
						Weight = parsed[1].ToLong(),
					};
				})
				.ToArray();

			var n = items.Length;

			var array = new long[knapsackSize + 1];
			var previous = new long[knapsackSize + 1];
			
			var first = items[0];
			for (var x = 0; x <= knapsackSize; x++)
			{
				previous[x] = x >= first.Weight ? first.Value : 0;
			}

			for (var i = 1; i < n; i++)
			{
				if (i % 100 == 0) Console.WriteLine(i);

				for (var x = 0; x <= knapsackSize; x++)
				{
					var item = items[i];

					var without = previous[x];
					var with = x >= item.Weight ? (previous[x - item.Weight] + item.Value) : 0;

					array[x] = Math.Max(without, with);
				}

				var temp = array;
				array = previous;
				previous = temp;
			}

			return previous[knapsackSize];
		}

		private struct Item
		{
			public long Value;
			public long Weight;
		}
	}
}