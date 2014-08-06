using System;
using System.Collections;
using System.Collections.Generic;
using ProblemSets.Services;

namespace ProblemSets.ComputerScience.DataTypes
{
	public class GenericBinaryHeap<T> : IEnumerable<T>
	{
		private readonly Comparison<T> comparisonDelegate;

		private readonly bool needNotifyIndexChange;
		private readonly Action<T, int> notifyIndexChange;

		public GenericBinaryHeap(Comparison<T> comparisonDelegate)
		{
			this.comparisonDelegate = comparisonDelegate;
		}

		public GenericBinaryHeap(Comparison<T> comparsionDelegate, Action<T, int> notifyIndexChange)
			: this(comparsionDelegate)
		{
			this.notifyIndexChange = notifyIndexChange;
			needNotifyIndexChange = true;
		}

		private readonly List<T> array = new List<T>();

		public T Min { get { return array[0]; } }

		public T this[int indexInHeap] { get { return array[indexInHeap]; } }

		public int Count { get { return array.Count; } }

		public void FillRaw(IEnumerable<T> elements)
		{
			array.Clear();
			array.AddRange(elements);

			if (!needNotifyIndexChange) return;
			for (var i = 0; i < array.Count; i++)
				notifyIndexChange(array[i], i);
		}

		public void BubbleUp(int i)
		{
			var k = array[i];

			while (true)
			{
				var pi = (i - 1) / 2;

				if (pi == i || comparisonDelegate(array[pi], k) <= 0)
					break;

				Swap(i, pi);

				i = pi;
			}
		}

		public void Insert(T k)
		{
			var i = array.Count;
			array.Add(k);

			if (needNotifyIndexChange)
				notifyIndexChange(k, i);

			BubbleUp(i);
		}

		public void DeleteMin()
		{
			Remove(0);
		}

		public void Remove(int indexInHeap)
		{
			var i = indexInHeap;

			if (needNotifyIndexChange)
				notifyIndexChange(array[i], -1);

			var k = array[array.Count - 1];
			array[i] = k;
			array.RemoveAt(array.Count - 1);

			if (needNotifyIndexChange)
				notifyIndexChange(k, i);

			while (true)
			{
				var ci = 2 * i + 1;

				if (ci >= array.Count)
					return;

				var left = array[ci];

				if (ci == array.Count - 1)
				{
					if (comparisonDelegate(left, k) < 0)
						Swap(i, ci);
					return;
				}

				var right = array[ci + 1];

				var lr = comparisonDelegate(left, right);

				var swapme =
					(lr <= 0 && comparisonDelegate(left, k) < 0)
						? ci
						: (lr > 0 && comparisonDelegate(right, k) < 0)
							  ? (ci + 1)
							  : -1;

				if (swapme == -1)
					return;

				Swap(i, swapme);
				i = swapme;
			}
		}

		private void Swap(int i, int j)
		{
			array.Swap(i, j);

			if (!needNotifyIndexChange) return;

			notifyIndexChange(array[i], i);
			notifyIndexChange(array[j], j);
		}

		public IEnumerator<T> GetEnumerator()
		{
			return array.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
