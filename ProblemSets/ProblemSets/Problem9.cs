using System;
using System.Linq;

namespace ProblemSets
{
	public class Problem9
	{
		public static void Solve()
		{
			var q =
				from a in Enumerable.Range(2, 999)
				from b in Enumerable.Range(2, 999)
				let c = 1000 - a - b
				where a * a + b * b == c * c
				select new {a, b, c, mul = a * b * c};

			foreach (var sol in q)
				Console.WriteLine(sol);
		}
	}
}