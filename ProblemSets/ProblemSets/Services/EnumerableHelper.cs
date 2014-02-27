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

		public static IEnumerable<T> AsRandom<T>(this IList<T> list, Random generator = null)
		{
			var indices = Enumerable.Range(0, list.Count).ToArray();
			generator = generator ?? new Random();

			for (var i = 0; i < list.Count; i++)
			{
				var position = generator.Next(i, list.Count);
				yield return list[indices[position]];
				indices[position] = indices[i];
			}
		}

		public struct ItemWithIndex<T>
		{
			public T Item;
			public int Index;
		}

		public static ItemWithIndex<T>? BinarySearch<T>(this IList<T> collection, Func<ItemWithIndex<T>, int> getDirection)
		{
			var left = 0;
			var right = collection.Count - 1;
			var mid = 0;

			while (left < right)
			{
				mid = (left + right) / 2;
				var item = collection[mid];

				var point = new ItemWithIndex<T>
				{
					Item = item,
					Index = mid,
				};

				var direction = getDirection(point);

				if (direction > 0)
					left = mid + 1;
				else if (direction == 0)
					return point;
				else
					right = mid;
			}

			if (mid == left) return null;

			var lastPoint = new ItemWithIndex<T>
			{
				Item = collection[left],
				Index = left,
			};

			return getDirection(lastPoint) == 0 ? lastPoint : (ItemWithIndex<T>?) null;
		}
	}
}
