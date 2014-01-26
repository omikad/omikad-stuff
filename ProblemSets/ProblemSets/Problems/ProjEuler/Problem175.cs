using System;
using System.Numerics;

namespace ProblemSets
{
	public class Problem175
	{
		private static ulong[] bits;
		private static byte[] bytebits;

		public static void Solve()
		{
			FillBits();
			for (BigInteger n = 0; n <= 20; n++)
				Console.WriteLine("{0,3} {1}", n, F(n));
		}

		private static void FillBits()
		{
			bits = new ulong[64];
			bits[0] = 1;
			for (var i = 1; i < 64; i++)
				bits[i] = bits[i - 1] << 1;
			bytebits = new byte[] {1, 2, 4, 8, 16, 32, 64, 128};
		}

		private static BigInteger F(BigInteger n)
		{
			if (n == 0)
				return 1;

			var bytes = n.ToByteArray();

			var isStarted = false;

			var result = BigInteger.Zero;
			
			for (var byteIndex = bytes.Length - 1; byteIndex >= 0; byteIndex--)
			{
				var byt = bytes[byteIndex];
				for (var bi = 7; bi >= 0; bi--)
				{
					if ((byt & bytebits[bi]) == 0)
					{
						result = result + 1;
					}
					else
					{
						if (!isStarted)
						{
							isStarted = true;
							result = BigInteger.One;
						}
					}
				}
			}

			return result;
		}
	}
}