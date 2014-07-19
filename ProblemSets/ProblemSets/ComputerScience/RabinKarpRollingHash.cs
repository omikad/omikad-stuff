using System;
using ProblemSets.Services;

namespace ProblemSets.ComputerScience
{
	public class RabinKarpRollingHash
	{
		private readonly uint biggestPow;		// a^(k - 1)
		private readonly uint a;
		private readonly uint aInverse;

		private uint nextPow;
		private uint hash;

		public uint Hash { get { return hash; } }

		public RabinKarpRollingHash(int length, uint a = 214013)
		{
			this.a = a;

			aInverse = (uint)MyMath.ModularInverse(a, 0x100000000);		// 2^32

			unchecked
			{
				if (a * aInverse != 1)
					throw new InvalidOperationException("Something wrong with the mod inverse");

				biggestPow = 1;
				for (var i = 0; i < length - 1; i++)
					biggestPow *= a;
			}

			nextPow = biggestPow;
		}

		public uint Initialize(string str)
		{
			foreach (var c in str)
				Eat(c);
			return hash;
		}

		public uint Eat(char c)
		{
			if (nextPow == 0)
				throw new InvalidOperationException("Can't eat more");

			unchecked
			{
				hash += c * nextPow;

				nextPow = nextPow == 1 ? 0 : (nextPow * aInverse);
			}

			return hash;
		}

		public uint Shift(char charOut, char charIn)
		{
			unchecked
			{
				hash = (hash - charOut * biggestPow) * a + charIn;
			}

			return hash;
		}
	}
}