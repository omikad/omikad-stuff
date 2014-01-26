using System;
using System.Linq;

namespace ProblemSets
{
	public class Problem6
	{
		public static void Solve()
		{
			const int max = 100;
			var s1 = Enumerable.Range(1, max).Aggregate(0ul, (s, x) => s + (ulong)x * (ulong)x);
			var s2sqrt = Enumerable.Range(1, max).Sum();
			Console.WriteLine(s1);
			Console.WriteLine(s2sqrt);
			Console.WriteLine((ulong)s2sqrt * (ulong)s2sqrt - s1);
		}
	}
}