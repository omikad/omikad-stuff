using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using ProblemSets.Services;

namespace ProblemSets.Problems.ProjEuler
{
	[Export]
	public class Problem146
	{
		private ulong[] firstFewPrimes;
		private ulong startCheckPrime;
		private List<Tuple<ulong, HashSet<ulong>>> allowedRemainders;

		public void Go()
		{
			const ulong maxprime = 50000000ul;
			var remaindersmiss = 0ul;
			var cnt = 0ul;
			var answer = 0ul;

//			const ulong max = 1000000ul;
//			const ulong max = 10000000ul;	
//			const ulong max = 50000000ul;	
//			const ulong max = 100000000ul;	
			const ulong max = 150000000ul;	

			var sieve = MyMath.CreatePrimesSieve(maxprime);
			firstFewPrimes = MyMath.ConvertSieveToPrimes(sieve).ToArray();
			startCheckPrime = firstFewPrimes[firstFewPrimes.Length - 1] + 2;
			Console.WriteLine("Start check prime boundary = " + startCheckPrime);

			CalcAllowedRemainders();
			Console.WriteLine("Allowed remainders length = " + allowedRemainders.Sum(t => t.Item2.Count));

			for (var n = 10ul; n <= max; n += 10)
			{
				var d7 = n % 7;
				if (d7 != 3 && d7 != 4)
					continue;

				if (n % 3 == 0 || n % 7 == 0 || n % 9 == 0 || n % 13 == 0 || n % 27 == 0)
					continue;

				if (!CheckAllowedRemainder(n))
					continue;

				remaindersmiss++;

				if (!IsSeqOfPrimes(n))
					continue;

				cnt++;
				answer += n;
				Console.WriteLine(new { n });
			}

			Console.WriteLine(new { max, cnt, answer, remaindersmiss });
		}

		private void CalcAllowedRemainders()
		{
			var additionals = new ulong[] {1, 3, 7, 9, 13, 27};

			allowedRemainders = new List<Tuple<ulong, HashSet<ulong>>>();
			foreach (var i in firstFewPrimes.Take(1000))
			{
				var allowed = new HashSet<ulong>();
				for (var j = 0ul; j < i; j++)
				{
					var jsqr = (j * j) % i;

					if (additionals.All(a => MyMath.ExtendedEuclidGcd(jsqr + a, i) == 1))
						allowed.Add(j);
				}
				allowedRemainders.Add(Tuple.Create(i, allowed));
			}
		}

		private bool CheckAllowedRemainder(ulong n)
		{
			foreach (var t in allowedRemainders)
			{
				if (t.Item1 >= n)
					return true;

				var rem = n % t.Item1;
				if (!t.Item2.Contains(rem))
					return false;
			}
			return true;
		}

		private bool IsSeqOfPrimes(ulong n)
		{
			var nsqr = n * n;

			if (!IsPrime(nsqr + 1)) return false;
			if (!IsPrime(nsqr + 3)) return false;
			if (!IsPrime(nsqr + 7)) return false;
			if (!IsPrime(nsqr + 9)) return false;
			if (!IsPrime(nsqr + 13)) return false;
			if (!IsPrime(nsqr + 27)) return false;

			if (IsPrime(nsqr + 5)) { Console.WriteLine(new { n, add = 5 }); return false; }  
			if (IsPrime(nsqr + 11)) { Console.WriteLine(new { n, add = 11 }); return false; }  
			if (IsPrime(nsqr + 15)) { Console.WriteLine(new { n, add = 15 }); return false; }  
			if (IsPrime(nsqr + 17)) { Console.WriteLine(new { n, add = 17 }); return false; }
			if (IsPrime(nsqr + 19)) { Console.WriteLine(new { n, add = 19 }); return false; }  
			if (IsPrime(nsqr + 21)) { Console.WriteLine(new { n, add = 21 }); return false; }  
			if (IsPrime(nsqr + 23)) { Console.WriteLine(new { n, add = 23 }); return false; }  
			if (IsPrime(nsqr + 25)) { Console.WriteLine(new { n, add = 25 }); return false; }  

			return true;
		}

		private bool IsPrime(ulong n)
		{
			foreach (var prime in firstFewPrimes)
			{
				if (prime >= n)
					break;

				if (n % prime == 0)
					return false;
			}

			for (var i = startCheckPrime; i <= Math.Sqrt(n) + 1; i += 2)
				if (n % i == 0)
					return false;

			return true;
		}
	}
}


// Output:

/*
Start check prime boundary = 9999993
Allowed remainders length = 5301209
{ n = 10 }
{ n = 315410 }
{ n = 927070 }
{ max = 1000000, cnt = 3, answer = 1242490, remaindersmiss = 14 }
Elapsed: 11264


Start check prime boundary = 9999993
Allowed remainders length = 269331
{ n = 10 }
{ n = 315410 }
{ n = 927070 }
{ n = 2525870 }
{ n = 8146100 }
{ max = 10000000, cnt = 5, answer = 11914460, remaindersmiss = 175 }
Elapsed: 2962


Start check prime boundary = 49999993
Allowed remainders length = 3676979
{ n = 10 }
{ n = 315410 }
{ n = 927070 }
{ n = 2525870 }
{ n = 8146100 }
{ n = 16755190 }
{ n = 39313460 }
{ max = 50000000, cnt = 7, answer = 67983110, remaindersmiss = 328 }
Elapsed: 26499



Start check prime boundary = 49999993
Allowed remainders length = 3676979
{ n = 10 }
{ n = 315410 }
{ n = 927070 }
{ n = 2525870 }
{ n = 8146100 }
{ n = 16755190 }
{ n = 39313460 }
{ n = 97387280 }
{ n = 119571820 }
{ n = 121288430 }
{ n = 130116970 }
{ n = 139985660 }
{ n = 144774340, add = 21 }
{ max = 150000000, cnt = 12, answer = 676333270, remaindersmiss = 940 }
Elapsed: 240493
*/