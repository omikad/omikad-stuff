using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using ProblemSets.ComputerScience.DataTypes;
using ProblemSets.Services;

namespace ProblemSets.ComputerScience
{
	[Export]
	public class MedianMaintenance
	{
		public void Go()
		{
			var input = ArrayHelper.CreateRandomArr(100, new Random());

			Check(input);
		}

		private void Check(int[] input)
		{
			var medians = Medians(input).ToArray();

			for (var i = 0; i < 100; i++)
			{
				var x = input[i];
				var m = medians[i];
				var brute = MedianBrute(input, i + 1);

				if (brute != m)
					throw new InvalidOperationException(new { i, x, m, brute }.ToString());
			}
		}

		private int MedianBrute(int[] input, int count)
		{
			var copy = input.Take(count).ToArray();
			Array.Sort(copy);
			return copy[(count - 1) / 2];
		}

		private IEnumerable<int> Medians(int[] input)
		{
			// Each x in the lowheap <= Each y in the hiheap

			var hiheap = new BinaryHeap();							// Min-heap
			var lowheap = new BinaryHeap((x, y) => y.CompareTo(x));	// Max-heap

			var first = input[0];
			var second = input[1];

			yield return first;

			lowheap.Insert(Math.Min(first, second));
			hiheap.Insert(Math.Max(first, second));
			
			yield return lowheap.Min;
			
			foreach (var i in input.Skip(2))
			{
				BinaryHeap insertme, notinsert;

				if (i < lowheap.Min)
				{
					insertme = lowheap;
					notinsert = hiheap;
				}
				else
				{
					insertme = hiheap;
					notinsert = lowheap;
				}

				insertme.Insert(i);

				if (insertme.Count > notinsert.Count + 1)
				{
					notinsert.Insert(insertme.Min);
					insertme.DeleteMin();
				}

				yield return
					lowheap.Count == hiheap.Count
						? (Math.Min(lowheap.Min, hiheap.Min))
						: lowheap.Count > hiheap.Count
							  ? lowheap.Min
							  : hiheap.Min;
			}
		}
	}
}