using System;
using System.Collections;
using System.Collections.Generic;
using ProblemSets.Services;

namespace ProblemSets.ComputerScience.DataTypes
{
	public class BinaryHeap : IEnumerable<int>
	{
		private readonly Comparison<int> comparisonDelegate;

		/// <summary>
		/// Construct <see cref="BinaryHeap"/> as min-heap, with default int comparison
		/// </summary>
		public BinaryHeap()
			: this(DefaultComparison)
		{
		}

		public BinaryHeap(Comparison<int> comparisonDelegate)
		{
			this.comparisonDelegate = comparisonDelegate;
		}

		private readonly List<int> array = new List<int>();

		public int Min { get { return array[0]; } }

		public int Count { get { return array.Count; } }

		public void Insert(int k)
		{
			var i = array.Count;
			array.Add(k);

			while (true)
			{
				var pi = (i - 1) / 2;
				var parent = array[pi];

				if (comparisonDelegate(parent, k) <= 0 || pi == i)
					break;

				array.Swap(i, pi);

				i = pi;
			}
		}

		public void DeleteMin()
		{
			var k = array[array.Count - 1];
			array[0] = k;
			array.RemoveAt(array.Count - 1);

			var i = 0;

			while (true)
			{
				var ci = 2 * i + 1;

				if (ci >= array.Count)
					return;					

				var left = array[ci];

				if (ci == array.Count - 1)
				{
					if (comparisonDelegate(left, k) < 0)
						array.Swap(i, ci);
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

				array.Swap(i, swapme);
				i = swapme;
			}
		}

		public IEnumerator<int> GetEnumerator()
		{
			return array.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		private static int DefaultComparison(int x, int y)
		{
			return x.CompareTo(y);
		}
	}
}