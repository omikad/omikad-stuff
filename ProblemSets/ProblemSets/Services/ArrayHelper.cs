using System;
using System.Collections.Generic;
using System.Linq;

namespace ProblemSets.Services
{
	public static class ArrayHelper
	{
		public static TOut[,] ToTwoDimensional<TIn, TOut>(this TIn[] source, Func<TIn, IEnumerable<TOut>> getInnerData)
		{
			if (source.Length == 0) return new TOut[0, 0];

			var firstInnerData = getInnerData(source[0]).ToArray();

			var result = new TOut[source.Length, firstInnerData.Length];

			result.FillRow(0, firstInnerData);

			for (var i = 1; i < source.Length; i++)
			{
				var item = source[i];
				var innerData = getInnerData(item);
				result.FillRow(i, innerData);
			}

			return result;
		}

		public static void FillRow<T>(this T[,] arr, int rowIndex, IEnumerable<T> items)
		{
			var i = 0;
			foreach (var item in items)
				arr[rowIndex, i++] = item;
		}

		public static IEnumerable<IEnumerable<T>> EnumerateRows<T>(this T[,] arr)
		{
			for (var i = 0; i < arr.GetLength(0); i++)
			{
				var row = i;
				yield return Enumerable.Range(0, arr.GetLength(1)).Select(j => arr[row, j]);
			}
		}

		public static bool AreIndicesAllowed<T>(this T[,] arr, int i, int j)
		{
			if (i < 0 || j < 0) return false;
			if (i >= arr.GetLength(0) || j >= arr.GetLength(1)) return false;
			return true;
		}

		public static T ShallowCopy<T>(this T arr)
		{
			var asArray = arr as Array;

			if (asArray == null)
				throw new InvalidOperationException(typeof(T).ToString());

			return (T) asArray.Clone();
		}

		public static void Print(this char[,] charArray)
		{
			foreach (var row in charArray.EnumerateRows())
				Console.WriteLine(new string(row.ToArray()));
			Console.WriteLine();
		}

		public static void Swap<T>(this T[] arr, int i, int j)
		{
			var tmp = arr[i];
			arr[i] = arr[j];
			arr[j] = tmp;
		}

		public static int[] CreateRandomArr(int size, Random rnd)
		{
			var arr = Enumerable.Range(0, size).ToArray();
			for (var i = 0; i < arr.Length; i++)
				arr.Swap(i, rnd.Next(i, arr.Length));
			return arr;
		}
	}
}