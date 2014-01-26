using System.Collections.Generic;

namespace ProblemSets
{
	public static class Helper
	{
		public static string Join<T>(this string separator, IEnumerable<T> values)
		{
			return string.Join(separator, values);
		}
	}
}
