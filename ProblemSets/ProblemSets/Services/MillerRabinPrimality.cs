using System.ComponentModel.Composition;
using System.Numerics;

namespace ProblemSets.Services
{
	[Export]
	public class MillerRabinPrimality
	{
		private const ulong smallThreshold = 4759123141;
		private readonly ulong[] small;

		private const ulong normalThreshold = 341550071728321;
		private readonly ulong[] normal;

		private readonly ulong[] big;

		public MillerRabinPrimality()
		{
			big = new ulong[] {2, 3, 5, 7, 11, 13, 17, 19, 23};
			normal = new ulong[] {2, 3, 5, 7, 11, 13, 17};
			small = new ulong[] {2, 7, 61};
		}

		public bool IsPrime(BigInteger n)
		{
			if (n == 2) return true;

			if (n < 2 || n.IsEven)
				return false;

			var d = n - 1;
			var s = 0ul;
			while (d % 2 == 0)
			{
				d /= 2;
				s += 1;
			}
			// n-1 = 2^s * d

			var arr =
				n < smallThreshold ? small
				: n < normalThreshold ? normal
				: big;

			foreach (var a in arr)
			{
				//				var a = (ulong)(rnd.NextDouble() * (n - 1) + 2);

				if (a >= n) return true;

				var x = BigInteger.ModPow(a, d, n);

				if (x == 1) continue;
				if (x == n - 1) continue;

				for (var r = 1ul; r < s; r++)
				{
					x = BigInteger.ModPow(x, 2, n);

					if (x == 1) return false;

					if (x == n - 1) break;
				}

				if (x != n - 1) return false;
			}
			return true;
		}


		public bool IsPrime(ulong n)
		{
			if (n == 2) return true;

			if (n < 2 || n % 2 == 0)
				return false;

			var d = n - 1;
			var s = 0ul;
			while (d % 2 == 0)
			{
				d /= 2;
				s += 1;
			}
			// n-1 = 2^s * d

			var arr = 
				n < smallThreshold ? small
				: n < normalThreshold ? normal
				: big;

			foreach (var a in arr)
			{
//				var a = (ulong)(rnd.NextDouble() * (n - 1) + 2);

				if (a >= n) return true;

				var x = MyMath.ModularPow(a, d, n);

				if (x == 1) continue;
				if (x == n - 1) continue;

				for (var r = 1ul; r < s; r++)
				{
					x = MyMath.ModularPow(x, 2, n);

					if (x == 1) return false;

					if (x == n - 1) break;
				}

				if (x != n - 1) return false;
			}
			return true;
		}
	}
}