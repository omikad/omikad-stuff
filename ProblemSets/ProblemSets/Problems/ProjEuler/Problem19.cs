using System;
using System.Collections.Generic;
using System.Linq;

namespace ProblemSets.Problems.ProjEuler
{
	public class Problem19
	{
		public void Go()
		{
			Console.WriteLine(
				GetDatesByFramework().TakeWhile(d => d.Year <= 2000).Count());

			Console.WriteLine(
				GetDates().TakeWhile(d => d.Year <= 2000).Count());

			Console.WriteLine();

			Console.WriteLine(
				GetDates()
				.SkipWhile(d => d.Year == 1900)
				.TakeWhile(d => d.Year <= 2000)
				.Count(d => d.Day == 1 && d.DayOfWeek == DayOfWeek.Sunday));
		}

		private readonly Dictionary<int, int> daysInMonth = new Dictionary<int, int>
		{
			{ 1, 31 },  // jan
			{ 2, 28 },  // feb
			{ 3, 31 },  // mar
			{ 4, 30 },  // apr
			{ 5, 31 },  // may
			{ 6, 30 },  // jun
			{ 7, 31 },  // jul
			{ 8, 31 },  // aug
			{ 9, 30 },  // sep
			{ 10, 31 },  // okt
			{ 11, 30 },  // nov
			{ 12, 31 },  // dec
		};

		private static IEnumerable<DateTime> GetDatesByFramework()
		{
			var date = new DateTime(1900, 1, 1);
			while (true)
			{
				yield return date;
				date = date.AddDays(1);
			}
		}

		private IEnumerable<MyDateTime> GetDates()
		{
			var dow = DayOfWeek.Monday;
			var year = 1900;
			var month = 1;

			while (true)
			{
				var days = (month == 2 && IsLeap(year)) ? 29 : daysInMonth[month];

				for (var d = 1; d <= days; d++)
				{
					yield return new MyDateTime
					{
						Year = year,
						Month = month,
						Day = d,
						DayOfWeek = dow,
					};
					dow = (DayOfWeek)(((int)dow + 1) % 7);
				}

				month++;

				if (month == 13)
				{
					year++;
					month = 1;
				}
			}
		}

		private static bool IsLeap(int year)
		{
			if (year % 4 != 0) return false;
			if (year % 400 == 0) return true;
			if (year % 100 == 0) return false;
			return true;
		}

		private struct MyDateTime
		{
			public DayOfWeek DayOfWeek;
			public int Year;
			public int Month;
			public int Day;

			public override string ToString()
			{
				return new {Year, Month, Day, DayOfWeek}.ToString();
			}
		}
	}
}