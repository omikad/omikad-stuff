using System;
using System.Diagnostics;
using System.Numerics;

namespace ProblemSets
{
	public class Program
	{
		// Сумму делителей лучше считать через решето

		public static void Main()
		{
			try
			{
				var timer = Stopwatch.StartNew();
				Problem254.Solve();
				Console.WriteLine("Elapsed: " + timer.ElapsedMilliseconds);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}
		}
	}

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

	public class Problem175
	{
		private static ulong[] bits;
		private static byte[] bytebits;

		public static void Solve()
		{
			FillBits();
			for (BigInteger n = 0; n <= 20; n++)
				Console.WriteLine("{0,3} {1}", n, F(n));
		}

		private static void FillBits()
		{
			bits = new ulong[64];
			bits[0] = 1;
			for (var i = 1; i < 64; i++)
				bits[i] = bits[i - 1] << 1;
			bytebits = new byte[] {1, 2, 4, 8, 16, 32, 64, 128};
		}

		private static BigInteger F(BigInteger n)
		{
			if (n == 0)
				return 1;

			var bytes = n.ToByteArray();

			var isStarted = false;

			var result = BigInteger.Zero;
			
			for (var byteIndex = bytes.Length - 1; byteIndex >= 0; byteIndex--)
			{
				var byt = bytes[byteIndex];
				for (var bi = 7; bi >= 0; bi--)
				{
					if ((byt & bytebits[bi]) == 0)
					{
						result = result + 1;
					}
					else
					{
						if (!isStarted)
						{
							isStarted = true;
							result = BigInteger.One;
						}
					}
				}
			}

			return result;
		}
	}

	public class Problem439
	{
		public static void Solve()
		{
//			const ulong target = 100000;
//			const ulong target = 10000;   // 
//			const ulong target = 3000;    //    657104482 
//			const ulong target = 1000;    // 563576517282
			const ulong target = 100;     //     57316768
//			const ulong target = 10;      //         6424

			const uint mod = 1000000000;

			var sumDivisors = GetSumDivisors((target + 1) * (target + 1), mod);

			Console.WriteLine("Done precalc");

			Console.WriteLine(S_brute(target, mod, sumDivisors));
			Console.WriteLine(S_optimized1(target, mod, sumDivisors));

			var timer = Stopwatch.StartNew();
			Console.WriteLine(S_optimized2(target, mod, sumDivisors));
			Console.WriteLine("Elapsed: " + timer.ElapsedMilliseconds);

//			const int p = 7;
//			for (var i = 1; i * p <= 100; i++)
//				Console.WriteLine("100 % {0,2} = {1}", i * p, 100 % (i * p));

			CheckR();

			Console.WriteLine();
		}

		private static void CheckR()
		{
			const ulong n = 100;
			const uint mod = 1000000000;

			var rs = new ulong[n + 1];
			rs[1] = 1;

			for (ulong p = 2; p <= n; p++)
			{
				if (rs[p] == 0)
				{
					rs[p] = R_prime(p, n, 1000000000);
					Console.WriteLine("{0,3} {1}", p, rs[p]);
				}

				for (ulong s = 2; s <= n / p; s++)
				{
					var ps = s * p;
					if (rs[s] == 0 || rs[ps] > 0) continue;
					rs[ps] = R(p, s, n, mod, rs);

					var brute = R_brute(ps, n);

					Console.WriteLine("   {0,3} {1,3} {2} = {3}", ps, s, rs[ps], brute);
				}
			}

			for (ulong i = 1; i <= n; i++)
				if (rs[i] == 0)
					Console.WriteLine("NOOOO " + i);
		}

		private static void CheckR_prime()
		{
			for (ulong s = 1; s <= 100; s++)
				if (MyMath.IsPrime(s))
					for (ulong n = 1; n <= 100; n++)
					{
						var brute = R_brute(s, n);

						var calc = R_prime(s, n, 10000000);

						if (calc != brute)
							throw new InvalidOperationException(new {s, n, brute, calc}.ToString());
					}
		}

		private static ulong R(ulong p, ulong s, ulong n, ulong mod, ulong[] rs)
		{
			ulong bp = 0;
			var temp = s;
			while (temp % p == 0)
			{
				bp++;
				temp = temp / p;
			}

			var m1 = MyMath.Pow(p, bp + 1);
			var up = n * MyMath.ExtendedEuclidGcd(m1, s);

			ulong part2 = 0;
			for (var m = m1; m <= n * n; m += m1)
				part2 += (m * (up / m)) % mod;

			return (rs[s] + (p - 1) * part2) % mod;
		}

		private static ulong R_prime(ulong s, ulong n, ulong mod)
		{
			ulong part1 = 0;
			for (ulong m = 1; m <= n - 1; m++)
				part1 += (m * (m % s == 0 ? (n * s / m) : (n / m))) % mod;

			var part2 = (n * ((n % s == 0) ? s : 1)) % mod;

			ulong part3 = 0;
			var rem = (n + 1) % s;
			var part3start = rem == 0 ? (n + 1) : (n + 1 - rem + s);
			for (var m = part3start; m <= n * n; m += s)
				part3 += (m * (n * s / m)) % mod;

			return part1 + part2 + part3;
		}

		private static ulong R_brute(ulong s, ulong n)
		{
			ulong result = 0;
			for (ulong m = 1; m <= n * n; m++)
			{
				var gcd = MyMath.ExtendedEuclidGcd(m, s);
				var fhat = (n * gcd) / m;
				result += m * fhat;
			}
			return result;
		}

		private static void CheckFHat()
		{
			for (ulong m = 1; m <= 10; m++)
				for (ulong s = 1; s <= 10; s++)
					for (ulong n = 1; n <= 100; n++)
					{
						var brute = F_hat_brute(m, s, n);

						var gcd = MyMath.ExtendedEuclidGcd(m, s);
						var calc = (n * gcd) / m;

						if (calc != brute)
							throw new InvalidOperationException(new { m, s, n, brute, calc }.ToString());
					}
		}

		private static ulong S_optimized2(ulong x, uint mod, uint[] sumDivisors)
		{
			ulong fsum = 0;
			for (ulong n = 2; n <= x; n++)
				fsum += F(n);

			ulong remaindersum = 0;
			for (ulong n = 2; n <= x; n++)
				remaindersum += sumDivisors[n * n];

			return 1 + 2 * fsum - remaindersum;
		}

		private static ulong F(ulong n)
		{
			ulong part1 = 0;
			ulong part2 = 0;

			for (ulong b = 1; b <= n; b++)
			{
				var gcd = MyMath.ExtendedEuclidGcd(b, n);

				part1 += gcd;

				var u = n * gcd;

				for (ulong a = 0; a < n; a++)
					part2 += u % (a * n + b);
			}

			return n * n * part1 - part2;
		}

		private static ulong F_hat_brute(ulong div, ulong step, ulong max)
		{
			ulong cnt = 0;
			for (ulong i = 1; i <= max; i++)
				if ((i * step) % div == 0)
					cnt++;
			return cnt;
		}

		private static ulong S_optimized1(ulong x, uint mod, uint[] sumDivisors)
		{
			ulong s = 1;
			for (ulong n = 2; n <= x; n++)
			{
				ulong add = 0;
				for (ulong i = 1; i <= n; i++)
					add = (add + sumDivisors[i * n]) % mod;
				s = (s + 2 * add - sumDivisors[n * n]) % mod;
			}
			return s;
		}

		private static ulong S_brute(ulong x, uint mod, uint[] sumDivisors)
		{
			ulong result = 0;
			for (ulong i = 1; i <= x; i++)
				for (ulong j = 1; j <= x; j++)
					result = (result + sumDivisors[i * j]) % mod;
			return result;
		}

		private static uint[] GetSumDivisors(ulong max, uint mod)
		{
			var sumDivisors = new uint[max];

			for (uint i = 1; i < max; i++)
				sumDivisors[i] = 1;

			for (uint i = 2; i < max; i++)
				for (var j = i; j < max; j += i)
					sumDivisors[j] = (sumDivisors[j] + i) % mod;

			return sumDivisors;
		}

		private static ulong G(ulong x, ulong N)
		{
			var cnt = N / x;
			var rem = N % x;

			if (rem == 0)
				return G_brute(x, x, x) * cnt * cnt;

			return 
				G_brute(x, x, x) * cnt * cnt
				+ G_brute(x, x, rem) * 2 * cnt
				+ G_brute(x, rem, rem);
		}

		private static ulong G_brute(ulong x, ulong xmax, ulong ymax)
		{
			ulong g = 0;
			for (ulong i = 1; i <= xmax; i++)
				for (ulong j = 1; j <= ymax; j++)
					if ((i * j) % x == 0)
						g++;
			return g;
		}
	}
}
