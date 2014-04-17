using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace ProblemSets.Problems.ProjEuler
{
	public class Problem267
	{
		// not 0.000000000059
		// not 0.000000000073
		// not 0.000000000077
		// not 000000000059
		// not 0.999992836186
		// not 999992836186

		public void Go()
		{
			for (var f = 0.02; f <= 1; f += 0.02)
			{
				var kstart = GetKstart(f, 1000000000, 1000);
				Console.WriteLine(new { f, kstart });
			}

			// min:
			// 601.4389325870183890199707939, {f -> 0.2028778651740367780399415878}}}

			var factorials = new Dictionary<int, BigInteger>();
			factorials[0] = BigInteger.One;
			for (var i = 1; i <= 1000; i++)
				factorials[i] = factorials[i - 1] * i;

			var nfact = factorials[1000];

			var coeffs = factorials.ToDictionary(
				kvp => kvp.Key,
				kvp => nfact / (factorials[kvp.Key] * factorials[1000 - kvp.Key]));

			var sum = coeffs.Where(kvp => kvp.Key >= 432).Select(kvp => kvp.Value).Aggregate(BigInteger.Zero, (x, y) => x + y);
			var overall = BigInteger.Pow(2, 1000);

			Console.WriteLine(sum);
			Console.WriteLine(overall);

			const ulong shift = 10000000000;

			var resultBig = sum * new BigInteger(shift) * new BigInteger(shift) / overall;

			var resultDouble = (double) resultBig;
			resultDouble /= shift;
			resultDouble /= shift;

			Console.WriteLine("{0:0.000000000000 00}", resultDouble);
		}

		private static double GetKstart(double f, double a, ulong n)
		{
			var logBase = (1 + 2 * f) / (1 - f);
			
			var kstart = Math.Log(a, logBase) - n * Math.Log(1 - f, logBase);
			
			return kstart;
		}
	}
}