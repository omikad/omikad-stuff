using System.Collections;
using System.Collections.Generic;
using ProblemSets.Services;

namespace ProblemSets.ComputerScience.DataTypes
{
	public class BinaryHeap : IEnumerable<int>
	{
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

				if (parent <= k || pi == i)
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
					if (left < k)
						array.Swap(i, ci);
					return;
				}

				var right = array[ci + 1];

				var swapme =
					(left < right && left < k)
						? ci
						: (right < left && right < k)
							? (ci + 1)
							: (left == right && left < k)
								? ci
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
	}
}