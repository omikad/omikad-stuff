using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using ProblemSets.Services;

namespace ProblemSets.ComputerScience
{
	[Export]
	public class Combinations
	{
		public void Go()
		{
			foreach (var arr in Recursive(new[] {1, 2, 3, 4, 5}, 3))
				Console.WriteLine(", ".Join(arr));

			Console.WriteLine();

			foreach (var arr in Recursive(new[] {1, 2}, 2))
				Console.WriteLine(", ".Join(arr));
		}

		public IEnumerable<IEnumerable<T>> Recursive<T>(T[] source, int k, int sourceStartIndex = 0)
		{
			if (k == 0)
			{
				yield return Enumerable.Empty<T>();
				yield break;
			}

			for (var i = sourceStartIndex; i < source.Length - k + 1; i++)
			{
				var elem = source[i];

				foreach (var recCombs in Recursive(source, k - 1, i + 1))
					yield return new[] {elem}.Concat(recCombs);
			}
		}
	}
}