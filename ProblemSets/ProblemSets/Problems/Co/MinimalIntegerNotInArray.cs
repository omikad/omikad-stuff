using System;
using System.Collections.Generic;

namespace ProblemSets.Problems.Co
{
	public class MinimalIntegerNotInArray
	{
		public void Go()
		{
			Console.WriteLine(Solve(new[] {1, 3, 6, 4, 1, 2}));
		}

		private int Solve(int[] arr)
		{
			var hashset = new HashSet<int>();

			foreach (var i in arr)
				if (i > 0)
					hashset.Add(i);

			if (hashset.Count == 0)
				return 1;

			for (var i = 1; i <= hashset.Count; i++)
				if (!hashset.Contains(i))
					return i;

			throw new InvalidOperationException();
		}
	}
}