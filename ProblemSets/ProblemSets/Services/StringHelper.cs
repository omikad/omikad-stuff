using System;
using System.Collections.Generic;

namespace ProblemSets.Services
{
	public static class StringHelper
	{
		public static string Join<T>(this string separator, IEnumerable<T> values)
		{
			return string.Join(separator, values);
		}

		public static string CommonPrefix(this string s1, string s2)
		{
			if (s1 == null || s2 == null)
				return "";

			int equalLen;
			for (equalLen = 0; equalLen < s1.Length && equalLen < s2.Length; equalLen++)
				if (s1[equalLen] != s2[equalLen])
					break;

			return s1.Substring(0, equalLen);
		}

		private static readonly string[] newLineArray = new[] { Environment.NewLine };
		public static string[] SplitToLines(this string s)
		{
			return s.Split(newLineArray, StringSplitOptions.RemoveEmptyEntries);
		}
	}
}
