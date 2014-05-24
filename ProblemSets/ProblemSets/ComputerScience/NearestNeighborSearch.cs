using System;
using System.Collections.Generic;
using ProblemSets.Services;

namespace ProblemSets.ComputerScience
{
	public class NearestNeighborSearch
	{
		// http://www1.cs.columbia.edu/CAVE/publications/pdfs/Nene_TR95.pdf

		// NOTE: если нет автоопределения эпсилона, и запрашиваемая точка далеко от всех остальных, то предоставляемого epsilon
		//       может не хватить, и алгоритм не найдет ближайших

		// TODO: ... should be chosen such that the numbers of sandwiched points between hyperplane pairs are in ascending order

		// TODO: auto find epsilon

		// pointSet: double[d][n]
		// orderedSet: double[d][n]
		// bmap: int[n]
		// fmap: int[d][n]

		private readonly double[][] pointSet;
		private readonly int d;
		private readonly int n;
		private readonly double[][] orderedSet;
		private readonly int[] bmap;
		private readonly int[][] fmap;

		public NearestNeighborSearch(double[][] pointSet)
		{
			this.pointSet = pointSet;

			d = pointSet.Length;
			n = pointSet[0].Length;

			orderedSet = ArrayHelper.CreateJaggedArray<double>(d, n);

			bmap = new int[n];

			fmap = ArrayHelper.CreateJaggedArray<int>(d, n);

			Preprocess();
		}

		public int Closest(double[] p, double epsilon)
		{
			// p: double[d]

			var bottom = BinarySearch_Bottom(orderedSet[0], p[0] - epsilon);
			var top = BinarySearch_Top(orderedSet[0], p[0] + epsilon, bottom);

			var list = new Queue<int>();

			for (var i = bottom; i <= top; i++)
				list.Enqueue(bmap[i]);

			for (var i = 1; i < p.Length; i++)
			{
				bottom = BinarySearch_Bottom(orderedSet[i], p[i] - epsilon);
				top = BinarySearch_Top(orderedSet[i], p[i] + epsilon, bottom);

				var c = list.Count;
				for (var j = 0; j < c; j++)
				{
					var el = fmap[i][list.Dequeue()];
					if (el >= bottom && el <= top)
						list.Enqueue(el);
				}
			}

			var max = double.MaxValue;
			var pos = -1;

			foreach (var i in list)
			{
				var t = 0d;
				for (var j = 0; j < p.Length; j++)
					t += Math.Pow(p[j] - orderedSet[j][fmap[j][i]], 2);

				if (t < max)
				{
					max = t;
					pos = i;
				}
			}

			return pos;
		}

		private void Preprocess()
		{
			var tempMap = new int[n];

			Sort(pointSet[0], tempMap);
			for (var i = 0; i < n; i++)
			{
				orderedSet[0][i] = pointSet[0][tempMap[i]];
				bmap[i] = tempMap[i];
				fmap[0][tempMap[i]] = i;
			}

			for (var i = 1; i < d; i++)
			{
				Sort(pointSet[i], tempMap);
				for (var j = 0; j < n; j++)
				{
					orderedSet[i][j] = pointSet[i][tempMap[j]];
					fmap[i][tempMap[j]] = j;
				}
			}
		}

		private static int BinarySearch_Bottom(double[] orderedSet, double v)
		{
			var bottom = 0;
			var top = orderedSet.Length - 1;

			while (top > bottom + 1)
			{
				var center = (bottom + top) / 2;
				if (v < orderedSet[center])
					top = center;
				else
					bottom = center;
			}

			return bottom;
		}

		public static int BinarySearch_Top(double[] orderedSet, double v, int bottom)
		{
			var top = orderedSet.Length - 1;

			while (top > bottom + 1)
			{
				var center = (bottom + top) / 2;
				if (v < orderedSet[center])
					top = center;
				else
					bottom = center;
			}

			return top;
		}

		#region Sort

		private static void Sort(double[] pointSet, int[] indices)
		{
			// pointSet: double[n]
			// indices: int[n]

			for (var i = 0; i < indices.Length; i++)
				indices[i] = i;

			QuickSort(pointSet, indices, 0, indices.Length - 1);
		}

		private static void QuickSort(double[] pointSet, int[] indices, int start, int end)
		{
			if (start >= end) return;

			var pivotIndex = CalcMedian(pointSet, indices, start, end);

			pivotIndex = Partition(pointSet, indices, start, end, pivotIndex);

			QuickSort(pointSet, indices, start, pivotIndex - 1);
			QuickSort(pointSet, indices, pivotIndex + 1, end);
		}

		private static int CalcMedian(double[] pointSet, int[] indices, int start, int end)
		{
			var median = (start + end) / 2;

			var s = pointSet[indices[start]];
			var m = pointSet[indices[median]];
			var e = pointSet[indices[end]];

			var mLessS = m < s;

			return
				(!mLessS && s < e)
					? (m < e ? median : end)
					: (mLessS && m < e)
						  ? (s < e ? start : end)
						  : (mLessS ? median : start);
		}

		private static int Partition(double[] pointSet, int[] indices, int start, int end, int pivotIndex)
		{
			var pivot = pointSet[indices[pivotIndex]];

			indices.Swap(start, pivotIndex);

			int i;
			int j;

			for (i = j = start + 1; j <= end; j++)
			{
				var element = pointSet[indices[j]];

				if (element < pivot)
				{
					indices.Swap(i, j);
					i++;
				}
			}

			indices.Swap(start, i - 1);

			return i - 1;
		}

		#endregion

	}
}
