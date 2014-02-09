using System;
using System.ComponentModel.Composition;

namespace ProblemSets.Problems
{
	[Export]
	public class FindLongestAnagram
	{
		public void Go()
		{
			// TODO: Fix

			Solve("rooooooooooooooooooooor");
//			Solve("ABCD abbccbbaa iamai oselokoleso redrum & murder EFG xyzyx rooooor xyzyx");
		}

		private static void Solve(string str)
		{
			Console.WriteLine(str);

			for (var i = 1; i < str.Length; i++)
			{
				Anagram found = null;

				if (str[i - 1] == str[i])
					found = FindAnagram(str, i - 1, i);

				if (i + 1 < str.Length && str[i - 1] == str[i + 1])
				{
					var another = FindAnagram(str, i - 1, i + 1);
					if (found == null)
						found = another;
					else if (found.Length < another.Length)
						found = another;
				}

				if (found != null && found.Length > 2)
					i = found.Right - 1;

				if (found != null)
					Console.WriteLine(new { found.Left, found.Right, anagram = str.Substring(found.Left, found.Length) });
			}
		}

		private static Anagram FindAnagram(string str, int l, int r)
		{
			while (l > 0 && r + 1 < str.Length)
			{
				if (str[l - 1] != str[r + 1])
					break;
				l--;
				r++;
			}
			return new Anagram(l, r);
		}

		private class Anagram
		{
			public Anagram(int left, int right)
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