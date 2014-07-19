using System;

namespace ProblemSets.Services
{
	public static class ParseHelper
	{
		public static int ToInt(this string s)
		{
			int result;
			if (!int.TryParse(s, out result))
				throw new FormatException("Can not parse integer from string '" + s + "'");
			return result;
		}

		public static long ToLong(this string s)
		{
			long result;
			if (!long.TryParse(s, out result))
				throw new FormatException("Can not parse integer from string '" + s + "'");
			return result;
		}
	}
}
