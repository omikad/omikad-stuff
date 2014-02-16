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
			const int size = 10;

			var arr = new[] { 6, 8, 9, 5, 2, 1, 4, 7, 0, 3 };

//			var arr = CreateRandomArr(size);

			Console.WriteLine(", ".Join(arr));

			Sort(arr);

			Console.WriteLine(", ".Join(arr));
		}

		private static int[] CreateRandomArr(int size)
		{
			var arr = Enumerable.Range(0, size).ToArray();
			var rnd = new Random();
			for (var i = 0; i < arr.Length; i++)
				arr.Swap(i, rnd.Next(i, arr.Length));
			return arr;
		}

		private static void Sort(int[] arr)
		{
			if (arr.Length <= 2) return;

			var isUpNotDown = true;

			var a = arr[0];
			var b = arr[1];
			var c = arr[2];
			Sort3(ref a, ref b, ref c);

			arr[0] = b;

			for (var i = 3; i < arr.Length; i++)
			{
				var d = arr[i];
				Sort3(ref a, ref c, ref d);

				// Keep elements not in array in a, c
				if (isUpNotDown)
				{
					if (a < b && c < b && d < b)
						throw new InvalidOperationException(new { i, a, b, c, d, isUpNotDown = true }.ToString());

					if (c < b)
						arr[i - 2] = d;
					else
					{
						arr[i - 2] = c;
						c = d;
					}
				}
				else
				{
					if (a > b && c > b && d > b)
						throw new InvalidOperationException(new { i, a, b, c, d, isUpNotDown = true }.ToString());

					if (c > b)
					{
						arr[i - 2] = a;
						a = d;
					}
					else
					{
						arr[i - 2] = c;
						c = d;
					}
				}

				isUpNotDown = !isUpNotDown;
			}
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