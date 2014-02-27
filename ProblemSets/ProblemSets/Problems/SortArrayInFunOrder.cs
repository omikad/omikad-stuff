using System;
using System.ComponentModel.Composition;
using ProblemSets.Services;

namespace ProblemSets.Problems
{
	[Export]
	public class SortArrayInFunOrder
	{
		public void Go()
		{
			var rnd = new Random();

			for (var size = 3; size < 20; size++)
				for (var i = 0; i < 20; i++)
				{
					var arr = ArrayHelper.CreateRandomArr(size, rnd);
					Console.WriteLine(", ".Join(arr));
					SortAsBubble(arr);
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

		// FFFFfffuuuuuu
		private static void SortAsBubble(int[] arr)
		{
			// O(n)

			for (var i = 1; i < arr.Length; i++)
			{
				var cur = arr[i];
				var prev = arr[i - 1];

				if (i % 2 == 1 && cur < prev)
					arr.Swap(i, i - 1);
				if (i % 2 == 0 && cur > prev)
					arr.Swap(i, i - 1);
			}
			Console.WriteLine(", ".Join(arr));
		}

		// My great solution
		private static void SortByTriples(int[] arr)
		{
			// O(n)

			if (arr.Length <= 2) return;

			var isUpNotDown = true;

			var inserted = arr[0];
			var a = arr[1];
			var b = arr[2];
			EnumerableHelper.Sort3(ref a, ref inserted, ref b);

			arr[0] = inserted;

			for (var i = 3; i < arr.Length; i++)
			{
				// We should keep a < b to reduce number of SwapSort calls
				
				var c = arr[i];

//				EnumerableHelper.SwapSort(ref a, ref b);  // No need, because a < b already
				EnumerableHelper.SwapSort(ref a, ref c);
				EnumerableHelper.SwapSort(ref b, ref c);

				if (isUpNotDown)
				{
					if (a < inserted && b < inserted && c < inserted)
						throw new InvalidOperationException(new { i, a, b, c, inserted, isUpNotDown = true }.ToString());

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
						throw new InvalidOperationException(new { i, a, b, c, inserted, isUpNotDown = false }.ToString());

					if (b > inserted)
					{
						arr[i - 2] = a;
						inserted = a;
						a = b;
						b = c;
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

			arr[arr.Length - 2] = isUpNotDown ? b : a;
			arr[arr.Length - 1] = isUpNotDown ? a : b;
		}
	}
}