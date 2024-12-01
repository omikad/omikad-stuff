using System;
using System.Numerics;

namespace ProblemSets.Services
{
	public struct FractionBigInt
	{
		public FractionBigInt(BigInteger numerator, BigInteger denumerator)
		{
			Numerator = numerator;
			Denumerator = denumerator;
		}

		public BigInteger Numerator;
		public BigInteger Denumerator;

		public FractionBigInt Simplify()
		{
			var gcd = BigInteger.GreatestCommonDivisor(Numerator, Denumerator);
			return new FractionBigInt(Numerator / gcd, Denumerator / gcd);
		}

		public static FractionBigInt operator +(FractionBigInt x, FractionBigInt y)
		{
			var num = x.Numerator * y.Denumerator + x.Denumerator * y.Numerator;
			var den = x.Denumerator * y.Denumerator;
			return new FractionBigInt(num, den);
		}

		public static FractionBigInt operator -(FractionBigInt x, FractionBigInt y)
		{
			var num = x.Numerator * y.Denumerator - x.Denumerator * y.Numerator;
			var den = x.Denumerator * y.Denumerator;
			return new FractionBigInt(num, den);
		}

		public override string ToString()
		{
			var numStr = Numerator.ToString();
			var denStr = Denumerator.ToString();

			var minLen = Math.Min(numStr.Length, denStr.Length);

			var n = Math.Max(0, minLen - 5);
			var num = n == 0 ? (double)Numerator : double.Parse(numStr.Substring(0, n));
			var den = n == 0 ? (double)Denumerator : double.Parse(denStr.Substring(0, n));

			return string.Format("{{ {0} / {1} = {2:0.000000} }}", Numerator, Denumerator, num / den);
		}
	}
}