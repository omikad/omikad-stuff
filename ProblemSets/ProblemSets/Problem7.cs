using System;

namespace ProblemSets
{
	public class Problem7
	{
		public static void Solve()
		{
			const int num = 10001;
			var primes = 0;
			for (ulong i = 1;; i++)
			{
				if (IsPrime(i))
				{
					primes++;
					if (primes == num)
					{
						Console.WriteLine(i);
						break;
					}
				}
			}
		}

		private static bool IsPrime(ulong x)
		{
			for (ulong i = 2; i < Math.Sqrt(x) + 1; i++)
			{
				if (x % i == 0)
					return false;
			}
			return true;
		}
	}
}