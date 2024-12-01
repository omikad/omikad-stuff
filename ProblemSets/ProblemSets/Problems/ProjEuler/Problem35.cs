using System;
using System.ComponentModel.Composition;
using ProblemSets.Services;

namespace ProblemSets.Problems.ProjEuler
{
	[Export]
	public class Problem35
	{
		public void Go()
		{
			const ulong max = 999999;

			var sieve = MyMath.CreatePrimesSieve(max);

			var cnt = 0;

			for (var i = 2ul; i < max; i++)
			{
				if (sieve[i]) continue;

				var numbersLen = (ulong)Math.Floor(Math.Log10(i));
				var rotatemod = MyMath.Pow(10, numbersLen);

				var isAllPrimes = true;

				var n = i;
				for (var r = 0ul; r <= numbersLen; r++)
				{
					n = (n % rotatemod) * 10 + (n / rotatemod);

					if (sieve[n])
					{
						isAllPrimes = false;
						break;
					}
				}

				if (isAllPrimes)
				{
					cnt++;
					Console.WriteLine(new { i, cnt });
				}
			}
		}
	}
}