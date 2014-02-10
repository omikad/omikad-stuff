using System.Collections.Generic;

namespace ProblemSets
{
	public static class StringHelper
	{
		public static string Join<T>(this string separator, IEnumerable<T> values)
		{
			return string.Join(separator, values);
		}

		public static string CommonPrefix(this string s, string comparand)
		{
			if (s == null || comparand == null)
				return "";

			int equalLen;
			for (equalLen = 0; equalLen < s.Length && equalLen < comparand.Length; equalLen++)
				if (s[equalLen] != comparand[equalLen])
					break;

			return s.Substring(0, equalLen);
		}
	}
}
