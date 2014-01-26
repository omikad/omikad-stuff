using System;
using System.Diagnostics;

namespace ProblemSets
{
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