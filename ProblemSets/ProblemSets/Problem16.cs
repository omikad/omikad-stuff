using System;
using System.Linq;
using System.Numerics;

namespace ProblemSets
{
	public class Problem16
	{
		public static void Solve()
		{
			Console.WriteLine((new BigInteger(1) << 1000).ToString().Sum(c => c - 48));
		}
	}
}