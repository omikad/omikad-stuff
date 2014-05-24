using System;
using System.Collections.Generic;
using System.Linq;
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

			var list = new List<int>();

			for (var i = bottom; i <= top; i++)
				list.Add(bmap[i]);

			for (var i = 1; i < p.Length; i++)
			{
				bottom = BinarySearch_Bottom(orderedSet[i], p[i] - epsilon);
				top = BinarySearch_Top(orderedSet[i], p[i] + epsilon, bottom);

				var old = list.ToArray();
				list.Clear();
				foreach (var j in old)
					if (fmap[i][j] >= bottom && fmap[i][j] <= top)
						list.Add(j);
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
			var tempMap = Sort(pointSet[0]);

			for (var i = 0; i < n; i++)
			{
				orderedSet[0][i] = pointSet[0][tempMap[i]];
				bmap[i] = tempMap[i];
				fmap[0][tempMap[i]] = i;
			}

			for (var i = 1; i < d; i++)
			{
				tempMap = Sort(pointSet[i]);
				for (var j = 0; j < n; j++)
				{
					orderedSet[i][j] = pointSet[i][tempMap[j]];
					fmap[i][tempMap[j]] = j;
				}
			}
		}

		// result: int[n]
		private static int[] Sort(double[] pointSet)
		{
			// TODO: optimize preprocess, by sorting in-place

			// pointSet: double[n]

			return pointSet
				.Select((p, i) => new { p, i })
				.OrderBy(a => a.p)
				.Select(a => a.i)
				.ToArray();
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
	}
}
