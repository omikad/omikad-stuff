using System;
using System.ComponentModel.Composition;
using ProblemSets.ComputerScience;

namespace ProblemSets.Problems
{
	[Export]
	public class SubstringContains
	{
		public void Go()
		{
			Solve("asdfzxcv", "dfzx");
		}

		private void Solve(string big, string small)
		{
			Console.WriteLine(big);
			Console.WriteLine(small);
			Console.WriteLine(RabinKarpRollingHash(big, small));
		}

		public int RabinKarpRollingHash(string big, string small)
		{
			// O(big_length + small_length) time, O(1) memory
			// Worst case: O(big_length * small_length)

			var searchLen = small.Length;

			var searchme = new RabinKarpRollingHash(searchLen).Initialize(small);

			var hash = new RabinKarpRollingHash(searchLen);

			var i = 0;

			for (; i < searchLen; i++)
				hash.Eat(big[i]);

			while (true)
			{
				if (hash.Hash == searchme)
				{
					var substring = big.Substring(i - searchLen, searchLen);

					Console.WriteLine("Hash hit: {0}", substring);

					if (substring == small)
						return i - searchLen;
				}

				if (i == big.Length)
					break;

				hash.Shift(big[i - searchLen], big[i]);
				i++;
			}

			return -1;
		}
	}
}