using System;
using System.Collections.Generic;
using System.Linq;
using Combinatorics.Collections;

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

		public static void Shuffle<T>(this IList<T> list, Random generator = null)
		{
			generator = generator ?? new Random();
			for (var i = 0; i < list.Count; i++)
			{
				var position = generator.Next(i, list.Count);

				var tmp = list[i];
				list[i] = list[position];
				list[position] = tmp;
			}
		}

		public struct ItemWithIndex<T>
		{
			public T Item;
			public int Index;

			public override string ToString()
			{
				return new {item = Item, index = Index}.ToString();
			}
		}

		public static IEnumerable<ItemWithIndex<T>> WithIndices<T>(this IEnumerable<T> source)
		{
			var i = 0;
			foreach (var item in source)
			{
				yield return new ItemWithIndex<T>
				{
					Item = item,
					Index = i
				};
				i++;
			}
		}

		public static int? BinarySearch(int count, Func<int, int> getDirection)
		{
			var left = 0;
			var right = count - 1;
			var mid = 0;

			while (left < right)
			{
				mid = (left + right) / 2;
				
				var direction = getDirection(mid);

				if (direction > 0)
					left = mid + 1;
				else if (direction == 0)
					return mid;
				else
					right = mid;
			}

			if (mid == left) return null;

			return getDirection(left) == 0 ? left : (int?)null;
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

		public static void Sort4(ref int x0, ref int x1, ref int x2, ref int x3)
		{
			SwapSort(ref x0, ref x1); SwapSort(ref x2, ref x3);
			SwapSort(ref x0, ref x2); SwapSort(ref x1, ref x3);
			SwapSort(ref x1, ref x2);
		}

		public static void Sort3(ref int a, ref int b, ref int c)
		{
			SwapSort(ref a, ref b);
			SwapSort(ref a, ref c);
			SwapSort(ref b, ref c);
		}

		public static void SwapSort(ref int a, ref int b)
		{
			if (a < b) return;

			var tmp = a;
			a = b;
			b = tmp;
		}

		public static IEnumerable<IList<T>> Subsets<T>(this IList<T> list, int startLen)
		{
			if (startLen == 0)
			{
				yield return new T[0];
				startLen = 1;
			}

			for (var i = startLen; i <= list.Count; i++)
				foreach (var variation in new Combinations<T>(list, i, GenerateOption.WithoutRepetition))
					yield return variation;
		}

		public static IEnumerable<IList<T>> Combinations<T>(this IList<T> list, int combLen)
		{
			return new Combinations<T>(list, combLen, GenerateOption.WithoutRepetition);
		}

		public static IEnumerable<T> ConcatIf<T>(this IEnumerable<T> seq, bool predicate, Func<IEnumerable<T>> concatWith)
		{
			return !predicate ? seq : seq.Concat(concatWith());
		}

		public static IEnumerable<int> IndicesWhere<T>(this IEnumerable<T> seq, Func<T, bool> condition)
		{
			var i = 0;
			foreach (var item in seq)
			{
				if (condition(item))
					yield return i;
				i++;
			}
		} 

		public static IEnumerable<int> IndicesWhere<T>(this IEnumerable<T> seq, Func<T, int, bool> condition)
		{
			var i = 0;
			foreach (var item in seq)
			{
				if (condition(item, i))
					yield return i;
				i++;
			}
		}

		public static bool AllSame<T>(this IEnumerable<T> seq) where T : IEquatable<T>
		{
			var elem = default(T);
			var hasAny = false;

			foreach (var item in seq)
			{
				if (!hasAny)
				{
					hasAny = true;
					elem = item;
				}
				else
				{
					var areEqual = elem.Equals(item);
					if (!areEqual)
						return false;
				}
			}

			if (!hasAny)
				throw new InvalidOperationException("Sequence is empty");

			return true;
		}
	}
}
