using System;
using System.ComponentModel.Composition;

namespace ProblemSets.Problems
{
	[Export]
	public class FindLongestPalindrome
	{
		public void Go()
		{
			SolveNaive("rooooooooooooooooooooor");
			Console.WriteLine();
			SolveNaive("ABCD abbccbbaa iamai oselokoleso redrum & murder EFG xyzyx rooooor xyzyx");
		}

		private static void SolveNaive(string str)
		{
			Console.WriteLine(str);

			for (var i = 1; i < str.Length; i++)
			{
				Palindrome found = null;

				if (str[i - 1] == str[i])
					found = FindPalindrome(str, i - 1, i);

				if (i + 1 < str.Length && str[i - 1] == str[i + 1])
				{
					var another = FindPalindrome(str, i - 1, i + 1);
					if (found == null)
						found = another;
					else if (found.Length < another.Length)
						found = another;
				}

//				if (found != null && found.Length > 2)
//					i = found.Right - 1;

				if (found != null)
					Console.WriteLine(new { found.Left, found.Right, anagram = str.Substring(found.Left, found.Length) });
			}
		}

		private static Palindrome FindPalindrome(string str, int l, int r)
		{
			while (l > 0 && r + 1 < str.Length)
			{
				if (str[l - 1] != str[r + 1])
					break;
				l--;
				r++;
			}
			return new Palindrome(l, r);
		}

		private class Palindrome
		{
			public Palindrome(int left, int right)
			{
				Left = left;
				Right = right;
			}

			public readonly int Left;
			public readonly int Right;

			public int Length { get { return Right - Left + 1; }}
		}
	}
}