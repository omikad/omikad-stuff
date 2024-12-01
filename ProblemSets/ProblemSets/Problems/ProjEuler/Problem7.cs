using System;
using ProblemSets.Services;

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
				if (MyMath.IsPrime(i))
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


	}
}