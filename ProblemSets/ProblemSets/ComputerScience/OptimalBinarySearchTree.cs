using System;
using System.ComponentModel.Composition;
using System.Globalization;
using System.Threading;
using ProblemSets.Services;

namespace ProblemSets.ComputerScience
{
	[Export]
	public class OptimalBinarySearchTree
	{
		public void Go()
		{
			Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

			var freq = new[] {.05, .4, .08, .04, .1, .1, .23};

			var n = freq.Length;

			var a = new double[n, n];

			for (var s = 0; s <= n - 1; s++)
			{
				for (var i = 0; i < n - s; i++)
				{
					var j = i + s;

					a[i, j] = double.MaxValue;

					var sum = Sum(freq, i, j);

					for (var r = i; r <= j; r++)
					{
						var c = ((r > i) ? a[i, r - 1] : 0) +
								((r < j) ? a[r + 1, j] : 0) +
								sum;

						if (c < a[i, j])
							a[i, j] = c;
					}
				}
			}

			Console.WriteLine(a[0, n - 1]);
			a.Print();
		}

		private static double Sum(double[] array, int start, int end)
		{
			var s = 0d;
			for (var i = start; i <= end; i++)
				s += array[i];
			return s;
		}
	}
}