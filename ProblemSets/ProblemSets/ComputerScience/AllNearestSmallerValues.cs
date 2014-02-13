using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using ProblemSets.Services;

namespace ProblemSets.ComputerScience
{
	// http://en.wikipedia.org/wiki/All_nearest_smaller_values

	[Export]
	public class AllNearestSmallerValues
	{
		public void Go()
		{
			var arr = new[] { 0, 8, 4, 12, 2, 10, 6, 14, 1, 9, 5, 13, 3, 11, 7, 15 };
			Solve(arr);
			Solve(new[] { 9, 3, 7, 1, 8, 12, 10, 20, 15, 18, 5 });
		}

		private void Solve(int[] arr)
		{
			Console.WriteLine();
			Console.WriteLine("arr:     " + ", ".Join(arr));

			Console.WriteLine("stack:   " + ", ".Join(SequentalStackSolution(arr)));
			Console.WriteLine("indices: " + ", ".Join(SequentalNoStackSolution(arr).Select(i => i == -1 ? -1 : arr[i])));
		}

		private static IEnumerable<int> SequentalStackSolution(int[] arr)
		{
			// total iterations <= 2 * n   ~ O(n)

			var stack = new Stack<int>();

			foreach (var x in arr)  // Outer loop - n iterations
			{
//				Console.WriteLine("x = {1}; Stack: {0}", (", ".Join(stack)), x);
				// В стеке лежат только кандидаты на числа меньшие x
				// То есть, удалены все числа, справа которых есть меньше

				while (stack.Count > 0 && stack.Peek() >= x)  // Inner loop - total iterations <= n
					stack.Pop();

				if (stack.Count == 0)
					yield return -1;
				else
					yield return stack.Peek();

				stack.Push(x);
			}
		}

		private static IEnumerable<int> SequentalNoStackSolution(int[] arr)
		{
			// Индексы результатов
			var result = new int[arr.Length];
			result[0] = -1;

			// Ближайшее число, левее x и меньшее x
			// Ищется так:
			//		Берем число слева от x=arr[i], и если оно подходит то пишем в индекс i-1 в результат
			//		Если не подходит, то все числа меньшее этого предыдущего можно искать по массиву результатов
			//			так как в нем лежат индексы чисел меньше искомого

			for (var i = 1; i < arr.Length; i++) // Outer loop - n-1 iterations
			{
				var j = i - 1;

				// Этот цикл работает точно также, как и стек в предыдущем решении, поэтому, его сложность <= n
				while (j > 0 && arr[j] > arr[i])
					j = result[j];

				if (j == -1 || (j == 0 && arr[j] > arr[i]))
					result[i] = -1;
				else
					result[i] = j;
			}

			return result;
		}
	}
}
