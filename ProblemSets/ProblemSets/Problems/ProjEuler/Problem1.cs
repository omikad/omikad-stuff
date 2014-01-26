using System;

namespace ProblemSets
{
	public class Problem1
	{
		public static void Solve()
		{
			var sum = 0;
			for (var i = 2; i < 1000; i++)
			{
				if (i % 3 == 0 || i % 5 == 0)
					sum += i;
			}
			Console.WriteLine(sum);
		}
	}
}