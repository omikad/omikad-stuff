using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using ProblemSets.ComputerScience.DataTypes;

namespace ProblemSets.ComputerScience
{
	[Export]
	public class TwoSumAlgorithm
	{
		// By sorting:
		//		Elapsed: 825

		// By hashtable:
		//		Elapsed: 1697772		.NET HashSet<>				Memory: 119002 KB
		//		Elapsed: 959776			HashTable_OpenAddressing	Memory: 109509 KB

		public void Go()
		{
		}

		private void CaclNumberOfDistinctTwoSums_BySorting(IEnumerable<long> nums, long min, long max)
		{
			var numbers = nums.ToList();
			numbers.Sort();

			var results = new HashSet<long>();
			
			foreach (var x in numbers)
			{
				var left = numbers.BinarySearch(0, numbers.Count, min - x, null);
				var right = numbers.BinarySearch(0, numbers.Count, max - x, null);

				if (left < 0) left = Math.Max(0, ~left - 1);
				if (right < 0) right = Math.Min(numbers.Count - 1, ~right + 1);

				for (var j = left; j <= right; j++)
				{
					var y = numbers[j];
					var t = x + y;
					if (t >= min && t <= max && x != y)
						results.Add(t);
				}
			}

			Console.WriteLine();
			Console.WriteLine(results.Count);
		}

		private void CalcNumberOfDistinctTwoSums(IEnumerable<long> nums, long min, long max)
		{
			var hashset = new HashTable_OpenAddressing();
			var numbers = new List<long>();

			foreach (var num in nums)
			{
				hashset.Add(num);
				numbers.Add(num);
			}

			hashset.ShowStats();

			var cnt = 0;

			for (var t = min; t <= max; t++)
			{
				if (t % 100 == 0) Console.WriteLine(t);

				foreach (var x in numbers)
				{
					var y = t - x;
					if (x != y && hashset.Contains(y))
					{
						cnt++;
						break;
					}
				}
			}

			Console.WriteLine();
			Console.WriteLine(cnt);
		}
	}
}
