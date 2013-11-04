using System;
using System.Collections.Generic;
using System.Linq;

namespace ProblemSets
{
	public class Problem2
	{
		public static void Solve()
		{
			var sum = Fib().TakeWhile(i => i < 4000000).Where(i => i % 2 == 0).Sum();
			Console.WriteLine(sum);
		}

		private static IEnumerable<int> Fib()
		{
			var x = 0;
			var y = 1;

			while (true)
			{
				var sum = x + y;
				yield return sum;
				x = y;
				y = sum;
			}
		}
	}
}