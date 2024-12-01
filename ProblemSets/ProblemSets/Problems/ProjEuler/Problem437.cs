using System;
using System.Linq;
using ProblemSets.Services;

namespace ProblemSets.Problems.ProjEuler
{
	public class Problem437
	{
		public void Go()
		{
//			const ulong maxprime = 100;
			const ulong maxprime = 100000000;

			var sieve = MyMath.CreatePrimesSieve(maxprime);
			var primes = MyMath.ConvertSieveToPrimes(sieve);
			var invertedPrimeFactorsArray = new ulong[(int)Math.Sqrt(maxprime) + 1];

			Console.WriteLine("Done precalc");

			var sum = 0ul;

			foreach (var prime in primes.Skip(1))
			{
				var prime1 = prime - 1;

				ulong invertedPrimeFactorsLength;
				MyMath.FindInvertedPrimeFactors(prime1, primes, invertedPrimeFactorsArray, out invertedPrimeFactorsLength);

				var primitiveRoot = MyMath.FindPrimitiveRootForPrime(prime, invertedPrimeFactorsArray, invertedPrimeFactorsLength);

				sum += primitiveRoot;
			}

			Console.WriteLine(sum);
		}
	}
}