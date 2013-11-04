using System;
using System.Diagnostics;
using System.Linq;

namespace ProblemSets
{
	public class Program
	{
		// Сумму делителей лучше считать через решето

		public static void Main()
		{
			try
			{
				var timer = Stopwatch.StartNew();
				Problem21.Solve();
				Console.WriteLine("Elapsed: " + timer.ElapsedMilliseconds);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}
		}
	}

	public class Problem21
	{
		public static void Solve()
		{
			const ulong max = 10000;

			var sumDivisors = new ulong[max];

			for (ulong i = 1; i < max; i++)
				sumDivisors[i] = 1 - i;

			for (ulong i = 2; i < max; i++)
				for (var j = i; j < max; j += i)
					sumDivisors[j] = sumDivisors[j] + i;

//			for (ulong i = 0; i < 100; i++)
//				Console.WriteLine("{0,5} ... {1,5}", i, sumDivisors[i]);

			ulong result = 0;
			for (ulong a = 2; a < max; a++)
			{
				var b = sumDivisors[a];
				if (b < a && sumDivisors[b] == a)
				{
					result += a + b;
					Console.WriteLine("{0,5} ~ {1,5}", a, b);
				}
			}
			Console.WriteLine(result);

			// 31626
		}
	}
}
