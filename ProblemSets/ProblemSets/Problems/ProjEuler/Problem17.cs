using System;

namespace ProblemSets
{
	public class Problem17
	{
		public static void Solve()
		{
			Console.WriteLine(NumberToText(115));
			Console.WriteLine(NumberToText(342));
			Console.WriteLine(NumberToText(300));

			var cnt = 0;
			for (var i = 1; i <= 1000; i++)
				cnt += NumberToText(i).Replace(" ", "").Length;

			Console.WriteLine(cnt);
		}

		public static string NumberToText(int n)
		{
			if (n < 0)
				return "Minus " + NumberToText(-n);
			if (n == 0)
				return "";
			if (n <= 19)
				return new[] {"One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", 
					"Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", 
					"Seventeen", "Eighteen", "Nineteen"}[n - 1] + " ";
			if (n <= 99)
				return new[] {"Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", 
					"Eighty", "Ninety"}[n / 10 - 2] + " " + NumberToText(n % 10);
			if (n <= 199)
				return "One Hundred "
				       + (n % 100 != 0 ? ("and " + NumberToText(n % 100)) : "");
			if (n <= 999)
				return NumberToText(n / 100) + "Hundred "
				       + (n % 100 != 0 ? ("and " + NumberToText(n % 100)) : "");
			if (n <= 1999)
				return "One Thousand " + NumberToText(n % 1000);
			if (n <= 999999)
				return NumberToText(n / 1000) + "Thousands " + NumberToText(n % 1000);
			if (n <= 1999999)
				return "One Million " + NumberToText(n % 1000000);
			if (n <= 999999999)
				return NumberToText(n / 1000000) + "Millions " + NumberToText(n % 1000000);
			if (n <= 1999999999)
				return "One Billion " + NumberToText(n % 1000000000);
			
			return NumberToText(n / 1000000000) + "Billions " + NumberToText(n % 1000000000);
		}
	}
}