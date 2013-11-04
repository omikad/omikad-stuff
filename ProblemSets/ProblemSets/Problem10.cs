using System;
using System.Diagnostics;

namespace ProblemSets
{
	public class Problem10
	{
		public static void Solve()
		{
			var timer = Stopwatch.StartNew();

			var sieve = MyMath.CreatePrimesSieve(2000000);
			var sum = 0ul;
			for (ulong i = 0; i < (ulong)sieve.Length; i++)
				if (!sieve[i])
					sum += i;

			Console.WriteLine("Elapsed: " + timer.ElapsedMilliseconds);
			Console.WriteLine(sum); // 142913828922
		}
	}
}