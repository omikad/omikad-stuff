using System;
using System.Collections.Generic;
using System.Linq;

namespace ProblemSets.Services
{
	public static class ArrayHelper
	{
		public static T[][] CreateJaggedArray<T>(int len0, int len1)
		{
			var result = new T[len0][];
			for (var i = 0; i < result.Length; i++)
				result[i] = new T[len1];
			return result;
		}

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

		public static T[,] SubArray<T>(this T[,] arr, int startRow, int countRows, int startCol, int countCols)
		{
			var result = new T[countRows, countCols];
			for (var i = 0; i < countRows; i++)
				for (var j = 0; j < countCols; j++)
					result[i, j] = arr[i + startRow, j + countRows];
			return result;
		}

		public static T[] CreateArray<T>(int length, Func<T> createItem)
		{
			var result = new T[length];
			for (var i = 0; i < result.Length; i++)
				result[i] = createItem();
			return result;
		}

		public static void FillArray<T>(this T[,] arr, T item)
		{
			for (var i = 0; i < arr.GetLength(0); i++)
				for (var j = 0; j < arr.GetLength(1); j++)
					arr[i, j] = item;
		}

		public static void Fill<T>(this T[] arr, T item)
		{
			for (var i = 0; i < arr.Length; i++)
					arr[i] = item;
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

		public static void Print<T>(this T[,] array)
		{
			foreach (var row in array.EnumerateRows())
				Console.WriteLine(", ".Join(row));
			Console.WriteLine();
		}

		public static void Swap<T>(this IList<T> arr, int i, int j)
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