using System;
using System.Numerics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProblemSets.Services;

namespace Tests
{
	[TestClass]
	public class TestMillerRabinPrimality
	{
		[TestMethod]
		public void CanCalcPrimalityCorrectlyForUlong()
		{
			var millerRabinPrimality = new MillerRabinPrimality();

			const ulong total = 1000000ul;

			for (var x = 1ul; x <= total; x++)
			{
				var isMRPrime = millerRabinPrimality.IsPrime(x);
				var isPrime = MyMath.IsPrime(x);

				if (isMRPrime != isPrime)
					throw new InvalidOperationException(x.ToString());
			}
		}

		[TestMethod]
		public void CanCalcPrimalityCorrectlyForBigInteger()
		{
			var millerRabinPrimality = new MillerRabinPrimality();

			const ulong total = 100000ul;

			for (var x = BigInteger.One; x <= total; x++)
			{
				var isMRPrime = millerRabinPrimality.IsPrime(x);
				var isPrime = MyMath.IsPrime(x);

				if (isMRPrime != isPrime)
					throw new InvalidOperationException(x.ToString());
			}

			if (!millerRabinPrimality.IsPrime(new BigInteger(5977200007)))
				throw new InvalidOperationException(5977200007.ToString());
		}
	}
}