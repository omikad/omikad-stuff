using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ProblemSets.ComputerScience.DataTypes;
using ProblemSets.Services;

namespace ProblemSets.ComputerScience
{
	public class TravelingSalesmanProblem
	{
		public void Go()
		{
			/*
n = 25; subsetLen = 16777216
Memory: 2192 Mb
Elapsed: 174089*/
		}

		public double CalcTSP_Euclidian(IEnumerable<string> coordinates)
		{
			const float INF = float.MaxValue;
			const float INF_COMPARER = float.MaxValue * 0.9f;

			var points = coordinates.Select(s => s.SplitBySpaces().Select(ss => ss.ToDouble()).ToArray())
				.Select(arr => new PointStruct<float>((float)arr[0], (float)arr[1]))
				.ToArray();

			var n = points.Length;

			var subsetLen = 1 << (n - 1);

			Console.WriteLine("n = {0}; subsetLen = {1}", n, subsetLen);

			var a = new float[subsetLen][];
			for (var i = 0; i < a.Length; i++)
			{
				var arr = new float[n];
				for (var j = 0; j < arr.Length; j++)
					arr[j] = INF;
				a[i] = arr;
			}
			a[0][0] = 0;

			var numberOfBits = new byte[subsetLen];
			for (var i = 0; i < numberOfBits.Length; i++)
				numberOfBits[i] = (byte)NumberOfSetBits(i);

			var bitMasks = new int[n];
			for (var i = 0; i < bitMasks.Length; i++)
				bitMasks[i] = 1 << i;

			Console.WriteLine("Memory: {0} Mb", GC.GetTotalMemory(false) / 1024 / 1024);

			for (var j = 1; j < n; j++)
				a[bitMasks[j - 1]][j] = EuclidianDistance(points[0], points[j]);

			for (var m = 2; m <= n - 1; m++)
			{
				for (var si = 0; si < subsetLen; si++)
				{
					if (numberOfBits[si] == m)
					{
						foreach (var j in EnumeratePointsInS(si, n, bitMasks))
						{
							var min = INF;

							var swoj = si - bitMasks[j - 1];

							foreach (var k in EnumeratePointsInS(si, n, bitMasks))
								if (k != j)
								{
									var len = EuclidianDistance(points[j], points[k]);

									var lenBefore = a[swoj][k];

									if (lenBefore > INF_COMPARER) continue;

									var curr = lenBefore + len;

									if (curr < min) min = curr;
								}

							a[si][j] = min;
						}
					}
				}
			}

			{
				var min = float.MaxValue;
				var allPointsSi = subsetLen - 1;
				for (var j = 1; j < n; j++)
				{
					var len = EuclidianDistance(points[j], points[0]);

					var curr = a[allPointsSi][j] + len;

					if (curr < min) min = curr;
				}

				return min;
			}
		}

		private static IEnumerable<int> EnumeratePointsInS(int si, int n, int[] bitMasks)
		{
			for (var j = 1; j < n; j++)
				if ((si & bitMasks[j - 1]) != 0)
					yield return j;
		}

		private static float EuclidianDistance(PointStruct<float> a, PointStruct<float> b)
		{
			var dx = a.X - b.X;
			var dy = a.Y - b.Y;
			return (float) Math.Sqrt(dx * dx + dy * dy);
		}

		private static int NumberOfSetBits(int i)
		{
			i = i - ((i >> 1) & 0x55555555);
			i = (i & 0x33333333) + ((i >> 2) & 0x33333333);
			return (((i + (i >> 4)) & 0x0F0F0F0F) * 0x01010101) >> 24;
		}
	}
}