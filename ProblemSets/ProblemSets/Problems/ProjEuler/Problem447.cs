using System;

namespace ProblemSets
{
	public class Problem447
	{
		public static void Solve()
		{
//			CheckR();
			CheckF();

			// { N = 1000, F = 184078 }
			// { N = 10000, F = 18420910 }
			// { N = 30000, F = 165796897 }
			// { N = 100000, F = 1842135909 }
			// { N = 1000000, F = 184216236806 }		// 11161 ms

			const ulong N = 100;
			Console.WriteLine(new { N, F = F_2(N) });
		}

		private static ulong F_3(ulong N)
		{
			var result = N - 1;

			for (ulong bi = 2; bi <= N / 2; bi++)
			{
				var cnt = 0ul;
				for (ulong ci = 2; ci <= N / bi; ci++)
				{
					if (MyMath.ExtendedEuclidGcd(bi, ci) == 1)
						cnt++;
				}

				Console.WriteLine(new { bi, cnt, tot = CalcTotient_brute(bi) });

				result += bi * cnt;
			}

			return result;
		}

		private static ulong F_2(ulong N)
		{
			var result = N - 1;

			for (ulong bi = 2; bi <= N / 2; bi++)
			{
				for (ulong ci = 2; ci <= N / bi; ci++)
				{
					if (MyMath.ExtendedEuclidGcd(bi, ci) == 1)
					{
						result += bi;
					}
				}
			}

			return result;
		}

		private static ulong CalcTotient_brute(ulong n)
		{
			var result = 1ul;
			for (ulong a = 2; a < n; a++)
			{
				if (MyMath.ExtendedEuclidGcd(a, n) == 1)
					result++;
			}
			return result;
		}

		private static ulong F_1(ulong N, ulong M)
		{
			// O(n^3)

			var result = 0ul;

			var divisorsA = new ulong[N + 1]; // Len to long
			var divisorsA1 = new ulong[N + 1]; // Len to long

			divisorsA1[0] = 1;
			var divCntA1 = 1; 

			for (ulong a = 2; a < N; a++)
			{
				var divCntA = MyMath.FillDivisors(a, divisorsA);

				for (var i = 1; i < divCntA; i++)
				{
					var bi = divisorsA[i];

					for (var j = 1; j < divCntA1; j++)
					{
						var cj = divisorsA1[j];
						var mul = bi * cj;

						if (mul > a && mul <= M)
							result += bi;
					}
				}

				var tmp = divisorsA;
				divisorsA = divisorsA1;
				divisorsA1 = tmp;

				divCntA1 = divCntA;
			}

			return result + N - 1;
		}

		private static void CheckF()
		{
			for (ulong N = 2; N <= 150; N++)
			{
				var brute = F_brute(N);
//				var f1 = F_1(N, N);
				var f2 = F_2(N);
				var f3 = F_3(N);
//				Console.WriteLine(new { N, brute, f6 });
//				if (brute != f1) throw new InvalidOperationException("F_1 fail on N = " + N);
				if (brute != f2) throw new InvalidOperationException("F_2 fail on N = " + N);
				if (brute != f3) throw new InvalidOperationException("F_3 fail on N = " + N);
			}
			Console.WriteLine("F is ok");
		}

		public static void CheckR()
		{
			for (ulong n = 2; n <= 10; n++)
			{
				var brute = R_brute(n, true);
				var r2 = R_2(n);
				if (brute != r2)
					throw new InvalidOperationException(n.ToString());
			}
		}

		private static ulong F_brute(ulong N, bool log = false)
		{
			ulong sum = 0;
			for (ulong n = 2; n <= N; n++)
				sum += R_brute(n, log);
			return sum;
		}

		private static ulong R_2(ulong n)
		{
			// O(n^2)

			ulong cnt = 1;
			for (ulong a = 2; a < n; a++)
			{
				if ((a * a) % n != a) continue;

				var m = MyMath.ExtendedEuclidGcd(a, n);

				cnt += m;
			}
			return cnt;
		}

		private static ulong R_1(ulong n)
		{
			// O(n^2)

			ulong cnt = 1;
			for (ulong a = 2; a < n; a++)
			{
				var m = MyMath.ExtendedEuclidGcd(a, n);
				var mm = MyMath.ExtendedEuclidGcd(a - 1, n);

				if (m == 1) continue;
				if (mm == 1) continue;
				if ((m * mm) % n != 0)
					continue;

				cnt += m;
			}
			return cnt;
		}

		private static ulong R_brute(ulong n, bool log = false)
		{
			// O(n^3)

			ulong cnt = 0;
			for (ulong a = 1; a < n; a++)
				for (ulong b = 0; b < n; b++)
				{
					var isOk = true;
					for (ulong x = 0; x < n; x++)
					{
						var f = (a * x + b) % n;
						var ff = (a * f + b) % n;
						if (f != ff)
						{
							isOk = false;
							break;
						}
					}

					if (isOk)
					{
						if (log) Console.WriteLine(new { n, a, b });
						cnt++;
					}
				}
			return cnt;
		}
	}
}