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

		public override string ToString()
		{
			return string.Format("{{ {0}  \r\n  {1} }}", Numerator, Denumerator);
		}
	}
}