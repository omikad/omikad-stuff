using System;
using System.Collections.Generic;
using System.Linq;

namespace ProblemSets
{
	public class Problem254
	{
//		const int max = 20;  // 156
//		const int max = 39;  // 443
//		const int max = 50;  // 997
//		const int max = 60;	 // 2513
//		const int max = 65;	 // 4786
//		const int max = 74;	 // 30105
//		const int max = 81;	 // 152316
//		const int max = 90;	 // 1492545
//		const int max = 92;	 // 2732930
//		const int max = 101; // 27287531
//		const int max = 110; // 272824296
//		const int max = 128; // 27281754444
		const int max = 150; // 8184523820510

		private static ulong[] factorials;
		private static AnswerCode[] garr;

		private static List<AnswerCode> cache;

		public static void Solve()
		{
			factorials = new ulong[10];
			factorials[0] = 1;
			for (ulong i = 1; i < 10; i++) factorials[i] = factorials[i - 1] * i;

			garr = new AnswerCode[max + 1];

			const ulong fact9 = 362880;

			FillCache();
			Console.WriteLine("Cache filled with : {0} values", cache.Count);

			// cache.Max(c => c.TotalLen) == 36
			var toAdd = new List<AnswerCode>();
			foreach (var answerCode in cache)
				for (var i = 1ul; i <= 36 - answerCode.TotalLen; i++)
					toAdd.Add(new AnswerCode
						{
							LeftPart = answerCode.LeftPart,
							FKey = answerCode.FKey + fact9 * i,
							TotalLen = answerCode.TotalLen + i,
							NinesCount = i
						});
			cache.AddRange(toAdd);
			Console.WriteLine("Cache refilled, total : {0} values", cache.Count);

			// Set correct to i = 61
			foreach (var left in cache)
			{
				var fn = left.FKey;
				var sf = S(fn);

				if (sf <= max)
				{
					var old = garr[sf];

					if (old == null)
						garr[sf] = new AnswerCode
							{
								FKey = fn,
								LeftPart = left.LeftPart,
								TotalLen = left.TotalLen,
								NinesCount = left.NinesCount,
							};
					else if (left.CompareTo(old) < 0)
						garr[sf] = new AnswerCode
							{
								FKey = fn,
								LeftPart = left.LeftPart,
								TotalLen = left.TotalLen,
								NinesCount = left.NinesCount,
							};
				}
			}
			cache.RemoveAll(c => c.TotalLen < 36);
			Console.WriteLine("Cache cleaned, total : {0} values", cache.Count);

			cache = cache.OrderBy(a => a, new AnswerCodeComparer()).ToList();
			Console.WriteLine("Cache ordered");

//			foreach (var gr in cache.GroupBy(c => c.FKey))
//				if (gr.ToArray().Length > 1)
//					Console.WriteLine(new { gr.Key });

			foreach (var item in cache.Take(30))
				Console.WriteLine(new { item, fk = item.FKey });
			
			// i <= 300 : Set correct to i = 72
			for (var i = 1; i <= 300; i++)
			{
				foreach (var left in cache)
				{
					var fn = left.FKey;
					var sf = S(fn);

					if (sf <= max && garr[sf] == null)
					{
						garr[sf] = new AnswerCode
							{
								FKey = fn,
								LeftPart = left.LeftPart,
								TotalLen = left.TotalLen,
								NinesCount = left.NinesCount,
							};
					}

					left.TotalLen = left.TotalLen + 1;
					left.NinesCount = left.NinesCount + 1;
					left.FKey = left.FKey + fact9;
				}

				// Experimental 1
				if (i == 50)
				{
					cache.RemoveAll(c => c.FKey % 10 != 9);
					Console.WriteLine("Experiment cleaning : {0} values", cache.Count);
				}
			}

			var starts = new ulong[]
				{
					55,
					82,
					110,
					137,
					165,
					192,
					220,
					248,
					275,
				};

			var fright = 99999999ul;
			for (var cycle = 0ul; cycle <= 8; cycle++)
			{
				for (var d = 1ul; d <= 9; d++)
				{
					var ftarget = d * (fright + 1) + fright;
					var destination = 72 + (cycle * 9) + d;

					if (destination > max) break;

					var ninesBase = starts[d - 1] * 10;
					var rem = ftarget % fact9;
					var left = cache.Single(a => a.FKey % fact9 == rem);

					Console.WriteLine(new { ftarget, destination, ninesBase });

					for (var j = 0ul; j <= 9; j++)
					{
						var nines = ninesBase + j - left.NinesCount;

						var fn = left.FKey + nines * fact9;
						var sf = S(fn);
						if (sf == destination)
						{
							var old = garr[sf];
							if (old == null)
							{
								starts[d - 1] = ninesBase + j;
								garr[sf] = new AnswerCode
									{
										FKey = fn,
										LeftPart = left.LeftPart,
										TotalLen = left.TotalLen,
										NinesCount = ninesBase + j,
									};
								break;
							}
						}
					}
				}

				fright = fright * 10 + 9;
			}

			var sum = 0ul;
			for (var i = 1; i <= max; i++)
			{
				var g = garr[i];

				if (g == null)
				{
					Console.WriteLine("Unknown: " + i);
				}
				else
				{
					var sgi = S(g);

					sum += sgi;

					Console.WriteLine(new {i, nines_in_g = g.NinesCount, f = g.FKey});
				}
			}

			Console.WriteLine(new { sum });
		}

		private static void FillCache()
		{
			cache = new List<AnswerCode>(362880);
			
			for (ulong d0 = 0; d0 <= 1; d0++)
				for (ulong d1 = 0; d1 <= 2; d1++)
					for (ulong d2 = 0; d2 <= 3; d2++)
						for (ulong d3 = 0; d3 <= 4; d3++)
							for (ulong d4 = 0; d4 <= 5; d4++)
								for (ulong d5 = 0; d5 <= 6; d5++)
									for (ulong d6 = 0; d6 <= 7; d6++)
										for (ulong d7 = 0; d7 <= 8; d7++)
										{
											var f =
												factorials[1] * d0 +
												factorials[2] * d1 +
												factorials[3] * d2 +
												factorials[4] * d3 +
												factorials[5] * d4 +
												factorials[6] * d5 +
												factorials[7] * d6 +
												factorials[8] * d7;

											cache.Add(new AnswerCode
												{
													LeftPart = new[] { d0, d1, d2, d3, d4, d5, d6, d7 }, 
													TotalLen = d0 + d1 + d2 + d3 + d4 + d5 + d6 + d7,
													NinesCount = 0,
													FKey = f,
												});
										}
		}

		public class AnswerCode
		{
			public ulong[] LeftPart;
			public ulong NinesCount;
			public ulong TotalLen;

			public ulong FKey;

			public override string ToString()
			{
				return string.Join("", LeftPart.Select((cnt, i) => new string((char) (i + 49), (int)cnt)))
				       + (NinesCount == 0 ? "" : string.Format("{{9x{0}}}", NinesCount));
			}

			public int CompareTo(AnswerCode other)
			{
				if (TotalLen < other.TotalLen) return -1;
				if (TotalLen > other.TotalLen) return 1;

				for (var i = 0; i < LeftPart.Length; i++)
				{
					var thisP = LeftPart[i];
					var otherP = other.LeftPart[i];

					if (thisP > otherP) return -1;
					if (thisP < otherP) return 1;
				}
				return 0;
			}
		}

		public class AnswerCodeComparer : IComparer<AnswerCode>
		{
			public int Compare(AnswerCode x, AnswerCode y)
			{
				return x.CompareTo(y);
			}
		}

		private static void ShowBestsInfo()
		{
			foreach (var d in new ulong[] {9, 99, 999, 9999, 99999, 999999, 9999999})
			{
				var best = GetNs(1, d).OrderByDescending(i => S(F(i))).First();
				Console.WriteLine(new {d, best, sfn = S(F(best))});

				//{ d = 9, best = 9, sfn = 27 }
				//{ d = 99, best = 39, sfn = 33 }
				//{ d = 999, best = 239, sfn = 35 }
				//{ d = 9999, best = 4479, sfn = 39 }
				//{ d = 99999, best = 14479, sfn = 40 }
				//{ d = 999999, best = 344479, sfn = 42 }
				//{ d = 9999999, best = 2378889, sfn = 44 }
			}
		}

		private static IEnumerable<ulong> GetNs(ulong minimum, ulong maximum)
		{
			for (var n = minimum; n <= maximum; n++)
				yield return n;
		}

		private static ulong F(IEnumerable<ulong> digits)
		{
			var sum = 0ul;
			foreach (var d in digits)
				sum += factorials[d];
			return sum;
		}

		private static ulong F(ulong n)
		{
			var sum = 0ul;
			while (n > 0)
			{
				var d = n % 10;
				n = n / 10;
				sum += factorials[d];
			}
			return sum;
		}

		private static ulong S(ulong n)
		{
			var sum = 0ul;
			while (n > 0)
			{
				var d = n % 10;
				n = n / 10;
				sum += d;
			}
			return sum;
		}

		private static ulong S(AnswerCode n)
		{
			var sum = 0ul;
			for (var i = 0; i < n.LeftPart.Length; i++)
				sum += ((ulong) i + 1) * n.LeftPart[i];
			sum += 9 * n.NinesCount;
			return sum;
		}
	}
}


/*
{ i = 1, g = 1, f = 1 }
{ i = 2, g = 2, f = 2 }
{ i = 3, g = 5, f = 120 }
{ i = 4, g = 15, f = 121 }
{ i = 5, g = 25, f = 122 }
{ i = 6, g = 3, f = 6 }
{ i = 7, g = 13, f = 7 }
{ i = 8, g = 23, f = 8 }
{ i = 9, g = 6, f = 720 }
{ i = 10, g = 16, f = 721 }
{ i = 11, g = 26, f = 722 }
{ i = 12, g = 44, f = 48 }
{ i = 13, g = 144, f = 49 }
{ i = 14, g = 256, f = 842 }
{ i = 15, g = 36, f = 726 }
{ i = 16, g = 136, f = 727 }
{ i = 17, g = 236, f = 728 }
{ i = 18, g = 67, f = 5760 }
{ i = 19, g = 167, f = 5761 }
{ i = 20, g = 267, f = 5762 }
{ i = 21, g = 34{9x1}, f = 362910 }
{ i = 22, g = 134{9x1}, f = 362911 }
{ i = 23, g = 234{9x1}, f = 362912 }
{ i = 24, g = 4{9x1}, f = 362904 }
{ i = 25, g = 14{9x1}, f = 362905 }
{ i = 26, g = 24{9x1}, f = 362906 }
{ i = 27, g = {9x1}, f = 362880 }
{ i = 28, g = 1{9x1}, f = 362881 }
{ i = 29, g = 2{9x1}, f = 362882 }
{ i = 30, g = 12{9x1}, f = 362883 }
{ i = 31, g = 22{9x1}, f = 362884 }
{ i = 32, g = 122{9x1}, f = 362885 }
{ i = 33, g = 3{9x1}, f = 362886 }
{ i = 34, g = 13{9x1}, f = 362887 }
{ i = 35, g = 23{9x1}, f = 362888 }
{ i = 36, g = 123{9x1}, f = 362889 }
{ i = 37, g = 1333{9x1}, f = 362899 }
{ i = 38, g = 235{9x2}, f = 725888 }
{ i = 39, g = 447{9x1}, f = 367968 }
{ i = 40, g = 1447{9x1}, f = 367969 }
{ i = 41, g = 235567{9x1}, f = 368888 }
{ i = 42, g = 34447{9x1}, f = 367998 }
{ i = 43, g = 134447{9x1}, f = 367999 }
{ i = 44, g = 237888{9x1}, f = 488888 }
{ i = 45, g = 1237888{9x1}, f = 488889 }
{ i = 46, g = 13337888{9x1}, f = 488899 }
{ i = 47, g = 23568888{9x2}, f = 887888 }
{ i = 48, g = 123568888{9x2}, f = 887889 }
{ i = 49, g = 1333568888{9x2}, f = 887899 }
{ i = 50, g = 122456778888{9x2}, f = 897989 }
{ i = 51, g = 344466668888{9x2}, f = 889998 }
{ i = 52, g = 1344466668888{9x2}, f = 889999 }
{ i = 53, g = 122455788{9x8}, f = 2988989 }
{ i = 54, g = 1233455788{9x8}, f = 2988999 }
{ i = 55, g = 13336667{9x11}, f = 3998899 }
{ i = 56, g = 122455566667{9x11}, f = 3999989 }
{ i = 57, g = 1233455566667{9x11}, f = 3999999 }
{ i = 58, g = 133357{9x19}, f = 6899899 }
{ i = 59, g = 12245667{9x22}, f = 7989989 }
{ i = 60, g = 123345667{9x22}, f = 7989999 }
{ i = 61, g = 1344466777{9x22}, f = 7999999 }
{ i = 62, g = 12245555588888{9x27}, f = 9999989 }
{ i = 63, g = 123345555588888{9x27}, f = 9999999 }
{ i = 64, g = 13444555568{9x55}, f = 19999999 }
{ i = 65, g = 122333444455566888888{9x82}, f = 29999999 }
{ i = 66, g = 1233455566688{9x110}, f = 39999999 }
{ i = 67, g = 134445566668888888{9x137}, f = 49999999 }
{ i = 68, g = 1223334444566666888{9x165}, f = 59999999 }
{ i = 69, g = 12334566666688888888{9x192}, f = 69999999 }
{ i = 70, g = 1344478888{9x220}, f = 79999999 }
{ i = 71, g = 1223334444555557{9x248}, f = 89999999 }
{ i = 72, g = 12334555556788888{9x275}, f = 99999999 }
 * 
{ i = 73, g = 134445555666778{9x551}, f = 199999999 }
{ i = 74, g = 122333444455566666777888888{9x826}, f = 299999999 }
{ i = 75, g = 123345557777788{9x1102}, f = 399999999 }
{ i = 76, g = 1344455667777778888888{9x1377}, f = 499999999 }
{ i = 77, g = 1223334444566667777777888{9x1653}, f = 599999999 }
{ i = 78, g = 123345666666{9x1929}, f = 699999999 }
{ i = 79, g = 1344467788888{9x2204}, f = 799999999 }
{ i = 80, g = 122333444455555667778{9x2480}, f = 899999999 }
{ i = 81, g = 123345555566667777888888{9x2755}, f = 999999999 }
 * 
{ i = 82, g = 1344455556678888{9x5511}, f = 1999999999 }
{ i = 83, g = 12233344445557777778{9x8267}, f = 2999999999 }
{ i = 84, g = 12334555666667788888888{9x11022}, f = 3999999999 }
{ i = 85, g = 1344455666777777788888{9x13778}, f = 4999999999 }
{ i = 86, g = 1223334444567777888{9x16534}, f = 5999999999 }
{ i = 87, g = 1233456666668{9x19290}, f = 6999999999 }
{ i = 88, g = 134446666777778888888{9x22045}, f = 7999999999 }
{ i = 89, g = 12233344445555567788888{9x24801}, f = 8999999999 }
{ i = 90, g = 123345555566666677777788{9x27557}, f = 9999999999 }
 * 
{ i = 91, g = 1344455556666667777788888{9x55114}, f = 19999999999 }
{ i = 92, g = 1223334444555666666777788888888{9x82671}, f = 29999999999 }
{ i = 93, g = 1233455566666677788{9x110229}, f = 39999999999 }
{ i = 94, g = 13444556666667788888{9x137786}, f = 49999999999 }
{ i = 95, g = 12233344445666666788888888{9x165343}, f = 59999999999 }
{ i = 96, g = 12334566666688{9x192901}, f = 69999999999 }
{ i = 97, g = 1344466666677777778888{9x220458}, f = 79999999999 }
{ i = 98, g = 122333444455555666667777778888888{9x248015}, f = 89999999999 }
{ i = 99, g = 123345555566666777778{9x275573}, f = 99999999999 }
 * 
{ i = 100, g = 1344455556666777888{9x551146}, f = 199999999999 }
{ i = 101, g = 1223334444555666788888{9x826719}, f = 299999999999 }
{ i = 102, g = 12334555667777777888888{9x1102292}, f = 399999999999 }
{ i = 103, g = 134445567777788888888{9x1377865}, f = 499999999999 }
{ i = 104, g = 122333444457778{9x1653439}, f = 599999999999 }
{ i = 105, g = 123345666666888{9x1929012}, f = 699999999999 }
{ i = 106, g = 13444666667777778888{9x2204585}, f = 799999999999 }
{ i = 107, g = 1223334444555556667777888888{9x2480158}, f = 899999999999 }
{ i = 108, g = 1233455555667788888888{9x2755731}, f = 999999999999 }
 * 
{ i = 109, g = 1344455556666677778888888{9x5511463}, f = 1999999999999 }
{ i = 110, g = 122333444455567777777888888{9x8267195}, f = 2999999999999 }
{ i = 111, g = 1233455566667888888{9x11022927}, f = 3999999999999 }
{ i = 112, g = 1344455777788888{9x13778659}, f = 4999999999999 }
{ i = 113, g = 122333444456667777778888{9x16534391}, f = 5999999999999 }
{ i = 114, g = 1233456666668888{9x19290123}, f = 6999999999999 }
{ i = 115, g = 1344466777888{9x22045855}, f = 7999999999999 }
{ i = 116, g = 12233344445555566667777788{9x24801587}, f = 8999999999999 }
{ i = 117, g = 123345555588{9x27557319}, f = 9999999999999 }
 * 
{ i = 118, g = 13444555568888{9x55114638}, f = 19999999999999 }
{ i = 119, g = 122333444455566888888{9x82671957}, f = 29999999999999 }
{ i = 120, g = 1233455566688888888{9x110229276}, f = 39999999999999 }
{ i = 121, g = 134445566668{9x137786596}, f = 49999999999999 }
{ i = 122, g = 1223334444566666888{9x165343915}, f = 59999999999999 }
{ i = 123, g = 12334566666688888{9x192901234}, f = 69999999999999 }
{ i = 124, g = 1344478888888{9x220458553}, f = 79999999999999 }
{ i = 125, g = 1223334444555557{9x248015873}, f = 89999999999999 }
{ i = 126, g = 12334555556788{9x275573192}, f = 99999999999999 }
 * 
{ i = 127, g = 134445555666778888{9x551146384}, f = 199999999999999 }
{ i = 128, g = 122333444455566666777888888{9x826719576}, f = 299999999999999 }
{ i = 129, g = 123345557777788888888{9x1102292768}, f = 399999999999999 }
{ i = 130, g = 1344455667777778{9x1377865961}, f = 499999999999999 }
{ i = 131, g = 1223334444566667777777888{9x1653439153}, f = 599999999999999 }
{ i = 132, g = 123345666666888888{9x1929012345}, f = 699999999999999 }
{ i = 133, g = 1344467788888888{9x2204585537}, f = 799999999999999 }
{ i = 134, g = 122333444455555667778{9x2480158730}, f = 899999999999999 }
{ i = 135, g = 123345555566667777888{9x2755731922}, f = 999999999999999 }
 * 
{ i = 136, g = 1344455556678888888{9x5511463844}, f = 1999999999999999 }
{ i = 137, g = 12233344445557777778{9x8267195767}, f = 2999999999999999 }
{ i = 138, g = 12334555666667788888{9x11022927689}, f = 3999999999999999 }
{ i = 139, g = 1344455666777777788888888{9x13778659611}, f = 4999999999999999 }
{ i = 140, g = 1223334444567777888{9x16534391534}, f = 5999999999999999 }
{ i = 141, g = 1233456666668888888{9x19290123456}, f = 6999999999999999 }
{ i = 142, g = 134446666777778{9x22045855379}, f = 7999999999999999 }
{ i = 143, g = 12233344445555567788888{9x24801587301}, f = 8999999999999999 }
 * 
{ i = 144, g = 123345555566666677777788888888{9x27557319223}, f = 9999999999999999 }
{ i = 145, g = 1344455556666667777788888888{9x55114638447}, f = 19999999999999999 }
{ i = 146, g = 1223334444555666666777788888888{9x82671957671}, f = 29999999999999999 }
{ i = 147, g = 1233455566666677788888888{9x110229276895}, f = 39999999999999999 }
{ i = 148, g = 13444556666667788888888{9x137786596119}, f = 49999999999999999 }
{ i = 149, g = 12233344445666666788888888{9x165343915343}, f = 59999999999999999 }
{ i = 150, g = 12334566666688888888{9x192901234567}, f = 69999999999999999 }
*/