using System;
using System.Collections.Generic;
using System.Linq;

namespace ProblemSets.Services
{
	public static class EnumerableHelper
	{
		public static bool Uncertain<T>(this IEnumerable<T> seq)
		{
			return seq.Skip(1).Any();
		}

		public static bool Confident<T>(this IEnumerable<T> seq)
		{
			return seq.Count() == 1;
		}

		public static IEnumerable<T> BreadthFirstSearch<T>(this T root, Func<T, IEnumerable<T>> getChilds)
		{
			var queue = new Queue<T>();
			queue.Enqueue(root);

			while (queue.Count > 0)
			{
				var item = queue.Dequeue();

				yield return item;

				foreach (var child in getChilds(item))
					queue.Enqueue(child);
			}
		}
	}
}
