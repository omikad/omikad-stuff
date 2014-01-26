using System;
using System.Linq;
using System.Numerics;

namespace ProblemSets
{
	public class Problem20
	{
		public static void Solve()
		{
			var fact = Enumerable.Range(1, 100)
				.Aggregate(BigInteger.One, (f, i) => f * i);

			Console.WriteLine(fact.ToString().Sum(c => c - 48));
		}
	}
}