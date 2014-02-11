using System;
using System.ComponentModel.Composition;
using System.Linq;

namespace ProblemSets.Problems
{
	[Export]
	public class FindLongestPalindrome
	{
		public void Go()
		{
//			Console.WriteLine(ManachersAlgorithm("rooooooooooooooooooooor"));
//			Console.WriteLine();
//			Console.WriteLine(ManachersAlgorithm("ABCD abbccbbaa iamai oselokoleso redrum & murder EFG xyzyx rooooor xyzyx"));

			Console.WriteLine(Solution2("rooooooooooooooooooooor"));
			Console.WriteLine();
			Console.WriteLine(Solution2("ABCD abbccbbaa iamai oselokoleso redrum & murder EFG xyzyx rooooor xyzyx"));
		}

		// http://en.wikipedia.org/wiki/Longest_palindromic_substring
		// With a my little refactoring for better understanding
		private static string ManachersAlgorithm(string s)
		{
			Console.WriteLine(s);

			if (string.IsNullOrEmpty(s)) return "";

			var s2 = AddBoundaries(s);

			var p = new int[s2.Length];

			int c = 0, r = 0; // Here the first element in s2 has been processed.
			int m = 0, n = 0; // The walking indices to compare if two elements are the same

			for (var i = 1; i < s2.Length; i++)
			{
				if (i > r)
				{
					p[i] = 0;
					m = i - 1;
					n = i + 1;
				}
				else
				{
					var i2 = c * 2 - i;
					if (p[i2] < (r - i))
					{
						p[i] = p[i2];
						m = -1; // This signals bypassing the while loop below. 
					}
					else
					{
						p[i] = r - i;
						n = r + 1;
						m = i * 2 - n;
					}
				}
				while (m >= 0 && n < s2.Length && s2[m] == s2[n])
				{
					p[i]++;
					m--;
					n++;
				}
				if ((i + p[i]) > r)
				{
					c = i;
					r = i + p[i];
				}
			}

			Console.WriteLine(Environment.NewLine.Join(s2.Select((ch, ci) => new { ch, p = p[ci] })));

			var bestPalindrome =
				p.Select((length, center) => new { length, center})
					.OrderByDescending(a => a.length)
					.First();

			var bestWithBoundaries = s2.Substring(bestPalindrome.center - bestPalindrome.length, 2 * bestPalindrome.length + 1);

			return RemoveBoundaries(bestWithBoundaries);
		}

		private static string AddBoundaries(string s)
		{
			if (string.IsNullOrEmpty(s)) return "||";

			var s2 = new char[s.Length * 2 + 1];
			for (var i = 0; i < s2.Length - 1; i += 2)
			{
				s2[i] = '|';
				s2[i + 1] = s[i / 2];
			}
			s2[s2.Length - 1] = '|';
			return new string(s2);
		}

		private static string RemoveBoundaries(string cs)
		{
			if (cs == null || cs.Length < 3)
				return "";

			var cs2 = new char[(cs.Length - 1) / 2];
			for (var i = 0; i < cs2.Length; i++)
				cs2[i] = cs[i * 2 + 1];

			return new string(cs2);
		}  

		private static void Solution1(string str)
		{
			// O(n^2)

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
			public int Center { get; private set; }
			public int Length { get; private set; }

			public int TotalLength { get { return 2 * Length + 1; } }

			public int Right { get { return Center + Length; } }
			public int Left { get { return Center - Length; } }

			public Palindrome(int center, int len)
			{
				Center = center;
				Length = len;
			}

			public int GetMirrorCenter(int index)
			{
				return Math.Max(0, 2 * Center - index);
			}

			public void Expand()
			{
				Length++;
			}
		}

		public static string Solution2(string str)
		{
			Console.WriteLine(str);

			if (string.IsNullOrEmpty(str))
				return str;

			var s2 = AddBoundaries(str);

			var palindromes = new Palindrome[s2.Length];

			var current = new Palindrome(0, 0);

			for (var i = 0; i < s2.Length; i++)
			{
				var min = current.Right > i
					? Math.Min(current.Right - i, palindromes[current.GetMirrorCenter(i)].Length)
					: 0;

				palindromes[i] = new Palindrome(i, min);

				current = palindromes[i];

				while (current.Left - 1 >= 0 
					&& current.Right + 1 < s2.Length
					&& s2[current.Left - 1] == s2[current.Right + 1])
				{
					current.Expand();
				}
			}

			var largest = palindromes.OrderByDescending(p => p.Length).First();

			return RemoveBoundaries(s2.Substring(largest.Left, largest.TotalLength));
		}
	}
}