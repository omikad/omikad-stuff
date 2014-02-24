using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using ProblemSets.Services;

namespace ProblemSets.ComputerScience
{
	[Export]
	public class LongestIncreasingSubsequence
	{
		public void Go()
		{
			Solve(new[] { 0, 8, 4, 12, 2, 10, 6, 14, 1, 9, 5, 13, 3, 11, 7, 15 });
			Solve(new[] { 2, 6, 3, 4, 1, 2, 9, 5, 8 });
		}

		private void Solve(int[] arr)
		{
			// O(n log n)

			Console.WriteLine("Task: " + ", ".Join(arr));

			// s[k] - индекс наименьшего элемента такого
			// что существует возрастающая последовательность длины k
			// и эта последовательность заканчивается этим наименьшим элементом
			// иначе
			// это индекс правого конца искомой последовательности длины k
			
			// алгоритм:
			// берем элемент из массива, для него:
			// если он больше последнего элемента в s, то он увеличивает последовательность - рекордсмен
			// иначе, ищем последовательность какой длины этот элемент может улучшить (уменьшить правый край)

			var s = new List<int>(arr.Length) { 0 };
			
			var parent = new int[arr.Length];

			for (var i = 1; i < arr.Length; i++)
			{
				var item = arr[i];

				var lastIndex = s[s.Count - 1];

				if (item > arr[lastIndex])
				{
					s.Add(i);
					parent[i] = lastIndex;
				}
				else
				{
					var replaceIndex = FindReplaceIndex(arr, s, item);

					if (replaceIndex >= 0)
					{
						parent[i] = replaceIndex > 0 ? s[replaceIndex - 1] : 0;
						s[replaceIndex] = i;
					}
				}

				Console.WriteLine(", ".Join(s.Select(ii => arr[ii])));
			}

			var result = new Stack<int>(s.Count);
			for (int i = 0, ind = s[s.Count - 1]; i < s.Count; i++)
			{
				result.Push(arr[ind]);
				ind = parent[ind];
			}
			Console.WriteLine("LIS: " + ", ".Join(result.ToArray()));
			Console.WriteLine();
		}

		private static int FindReplaceIndex(int[] arr, List<int> s, int item)
		{
			// Binary search - O(log n)

			var left = 0;
			var right = s.Count - 1;

			while (left < right)
			{
				var mid = (left + right) / 2;
				var elem = arr[s[mid]];

				if (elem < item)
					left = mid + 1;
				else
					right = mid;
			}

			return arr[s[left]] >= item ? left : -1;
		}
	}
}
