using System;

namespace ProblemSets.Services
{
	public static class RandomHelper
	{
		public static T Random<T>(this T[] arr, Random random)
		{
			return arr[random.Next(arr.Length)];
		}
	}
}