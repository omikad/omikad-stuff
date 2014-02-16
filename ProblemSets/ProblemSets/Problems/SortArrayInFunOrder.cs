using System;
using System.ComponentModel.Composition;
using System.Linq;
using ProblemSets.Services;

namespace ProblemSets.Problems
{
	[Export]
	public class SortArrayInFunOrder
	{
		public void Go()
		{
			var rnd = new Random();

//			SortByTriples(new[] { 3, 0, 1, 2, 4 });

			for (var size = 3; size < 20; size++)
				for (var i = 0; i < 20; i++)
				{
					var arr = CreateRandomArr(size, rnd);
					Console.WriteLine(", ".Join(arr));
					SortByTriples(arr);
					Console.WriteLine(", ".Join(arr));
					Check(arr);
					Console.WriteLine();
				}
		}

		private static void Check(int[] arr)
		{
			for (var i = 1; i < arr.Length; i++)
			{
				if ((i % 2 == 1 && arr[i] < arr[i - 1])
				    || (i % 2 == 0 && arr[i] > arr[i - 1]))
					throw new InvalidOperationException(i.ToString());
			}
		}

		private static int[] CreateRandomArr(int size, Random rnd)
		{
			var arr = Enumerable.Range(0, size).ToArray();
			for (var i = 0; i < arr.Length; i++)
				arr.Swap(i, rnd.Next(i, arr.Length));
			return arr;
		}

		private static void SortByTriples(int[] arr)
		{
			// O(n)

			if (arr.Length <= 2) return;

			var isUpNotDown = true;

			var inserted = arr[2];
			var a = arr[0];
			var b = arr[1];
			Sort3(ref a, ref inserted, ref b);

			arr[0] = inserted;

			for (var i = 3; i < arr.Length; i++)
			{
				var c = arr[i];
				Sort3(ref a, ref b, ref c);

				// Keep element just inserted in 'inserted'
				// Keep elements not inserted in a, b
				if (isUpNotDown)
				{
					if (a < inserted && b < inserted && c < inserted)
						throw new InvalidOperationException(new { i, a, b = inserted, c = b, d = c, isUpNotDown = true }.ToString());

					if (b < inserted)
					{
						arr[i - 2] = c;
						inserted = c;
					}
					else
					{
						arr[i - 2] = b;
						inserted = b;
						b = c;
					}
				}
				else
				{
					if (a > inserted && b > inserted && c > inserted)
						throw new InvalidOperationException(new { i, a, b = inserted, c = b, d = c, isUpNotDown = true }.ToString());

					if (b > inserted)
					{
						arr[i - 2] = a;
						inserted = a;
						a = c;
					}
					else
					{
						arr[i - 2] = b;
						inserted = b;
						b = c;
					}
				}

				isUpNotDown = !isUpNotDown;
			}

			SwapSort(ref a, ref b);

			arr[arr.Length - 2] = isUpNotDown ? b : a;
			arr[arr.Length - 1] = isUpNotDown ? a : b;
		}

		private static void Sort4(ref int x0, ref int x1, ref int x2, ref int x3)
		{
			SwapSort(ref x0, ref x1); SwapSort(ref x2, ref x3);
			SwapSort(ref x0, ref x2); SwapSort(ref x1, ref x3);
			SwapSort(ref x1, ref x2);
		}

		private static void Sort3(ref int a, ref int b, ref int c)
		{
			SwapSort(ref a, ref b);
			SwapSort(ref a, ref c);
			SwapSort(ref b, ref c);
		}

		private static void SwapSort(ref int a, ref int b)
		{
			if (a < b) return;

			var tmp = a;
			a = b;
			b = tmp;
		}
	}
}