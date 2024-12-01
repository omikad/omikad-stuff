using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProblemSets.ComputerScience;

namespace Tests
{
	[TestClass]
	public class TestKosarajuStronglyConnectedComponents
	{
		[TestMethod]
		public void CanFindSizesGraph1()
		{
			var input =
				new[]
				{
					"1 4",
					"2 8",
					"3 6",
					"4 7",
					"5 2",
					"6 9",
					"7 1",
					"8 5",
					"8 6",
					"9 7",
					"9 3",
				};

			var sizes = new KosarajuStronglyConnectedComponents()
				.CalcStronglyConnectedComponentSizes(PrepareInput(input));

			AssertHelper.Seq(new[] {3, 3, 3}, sizes);
		}

		[TestMethod]
		public void CanFindSizesGraph2()
		{
			var input =
				new[]
				{
					"1 2",
					"2 6",
					"2 3",
					"2 4",
					"3 1",
					"3 4",
					"4 5",
					"5 4",
					"6 5",
					"6 7",
					"7 6",
					"7 8",
					"8 5",
					"8 7",
				};

			var sizes = new KosarajuStronglyConnectedComponents()
				.CalcStronglyConnectedComponentSizes(PrepareInput(input));

			AssertHelper.Seq(new[] {3, 3, 2}, sizes);
		}

		[TestMethod]
		public void CanFindSizesGraph3()
		{
			var input =
				new[]
				{
					"1 2",
					"2 3",
					"3 1",
					"3 4",
					"5 4",
					"6 4",
					"8 6",
					"6 7",
					"7 8",
				};

			var sizes = new KosarajuStronglyConnectedComponents()
				.CalcStronglyConnectedComponentSizes(PrepareInput(input));

			AssertHelper.Seq(new[] {3, 3, 1, 1}, sizes);
		}

		[TestMethod]
		public void CanFindSizesGraph4()
		{
			var input =
				new[]
				{
					"1 2",
					"2 3",
					"2 4",
					"2 5",
					"3 6",
					"4 5",
					"4 7",
					"5 2",
					"5 6",
					"5 7",
					"6 3",
					"6 8",
					"7 8",
					"7 10",
					"8 7",
					"9 7",
					"10 9",
					"10 11",
					"11 12",
					"12 10",
				};

			var sizes = new KosarajuStronglyConnectedComponents()
				.CalcStronglyConnectedComponentSizes(PrepareInput(input));

			AssertHelper.Seq(new[] {6, 3, 2, 1}, sizes);
		}

		[TestMethod]
		public void CanFindSizesGraph5()
		{
			var input =
				new[]
				{
					"1 4",
					"4 3",
					"3 1",
					"4 6",
					"6 7",
					"6 10",
					"10 11",
					"11 10",
					"7 6",
					"2 5",
					"5 2",
					"5 7",
					"5 8",
					"8 9",
					"7 11",
				};

			var sizes = new KosarajuStronglyConnectedComponents()
				.CalcStronglyConnectedComponentSizes(PrepareInput(input));

			AssertHelper.Seq(new[] {3, 2, 2, 2, 1, 1}, sizes);
		}

		[TestMethod]
		public void CanFindSizesGraph6()
		{
			var input =
				new[]
				{
					" 0  7",
					" 0 34",
					" 1 14",
					" 1 45",
					" 1 21",
					" 1 22",
					" 1 22",
					" 1 49",
					" 2 19",
					" 2 25",
					" 2 33",
					" 3  4",
					" 3 17",
					" 3 27",
					" 3 36",
					" 3 42",
					" 4 17",
					" 4 17",
					" 4 27",
					" 5 43",
					" 6 13",
					" 6 13",
					" 6 28",
					" 6 28",
					" 7 41",
					" 7 44",
					" 8 19",
					" 8 48",
					" 9  9",
					" 9 11",
					" 9 30",
					" 9 46",
					"10  0",
					"10  7",
					"10 28",
					"10 28",
					"10 28",
					"10 29",
					"10 29",
					"10 34",
					"10 41",
					"11 21",
					"11 30",
					"12  9",
					"12 11",
					"12 21",
					"12 21",
					"12 26",
					"13 22",
					"13 23",
					"13 47",
					"14  8",
					"14 21",
					"14 48",
					"15  8",
					"15 34",
					"15 49",
					"16  9",
					"17 20",
					"17 24",
					"17 38",
					"18  6",
					"18 28",
					"18 32",
					"18 42",
					"19 15",
					"19 40",
					"20  3",
					"20 35",
					"20 38",
					"20 46",
					"22  6",
					"23 11",
					"23 21",
					"23 22",
					"24  4",
					"24  5",
					"24 38",
					"24 43",
					"25  2",
					"25 34",
					"26  9",
					"26 12",
					"26 16",
					"27  5",
					"27 24",
					"27 32",
					"27 31",
					"27 42",
					"28 22",
					"28 29",
					"28 39",
					"28 44",
					"29 22",
					"29 49",
					"30 23",
					"30 37",
					"31 18",
					"31 32",
					"32  5",
					"32  6",
					"32 13",
					"32 37",
					"32 47",
					"33  2",
					"33  8",
					"33 19",
					"34  2", 
					"34 19",
					"34 40",
					"35  9",
					"35 37",
					"35 46",
					"36 20",
					"36 42",
					"37  5",
					"37  9",
					"37 35",
					"37 47",
					"37 47",
					"38 35",
					"38 37",
					"38 38",
					"39 18",
					"39 42",
					"40 15",
					"41 28",
					"41 44",
					"42 31",
					"43 37",
					"43 38",
					"44 39",
					"45  8",
					"45 14",
					"45 14",
					"45 15",
					"45 49",
					"46 16",
					"47 23",
					"47 30",
					"48 12",
					"48 21",
					"48 33",
					"48 33",
					"49 34",
					"49 22",
					"49 49",
				};

			var sizes = new KosarajuStronglyConnectedComponents()
				.CalcStronglyConnectedComponentSizes(PrepareInput(input, 0));

			AssertHelper.Seq(new[] {35, 7, 1, 1, 1, 1, 1, 1, 1, 1}, sizes);
		}

		[TestMethod]
		public void CanFindSizesGraph7()
		{
			var input =
				new[]
				{
					"1 9  ",
					"1 7  ",
					"2 6  ",
					"3 6  ",
					"4 1  ",
					"4 6  ",
					"14 2 ",
					"6 3  ",
					"7 9  ",
					"9 4  ",
					"3 1  ",
				};

			var sizes = new KosarajuStronglyConnectedComponents()
				.CalcStronglyConnectedComponentSizes(PrepareInput(input));

			AssertHelper.Seq(new[] { 6, 1, 1 }, sizes);
		}

		[TestMethod]
		public void CanFindSizesGraph8()
		{
			var input =
				new[]
				{
					"0 1",
					"1 2",
					"2 0",
					"3 4",
					"4 5",
					"5 4",
					"6 7",
				};

			var sizes = new KosarajuStronglyConnectedComponents()
				.CalcStronglyConnectedComponentSizes(PrepareInput(input, 0));

			AssertHelper.Seq(new[] {3, 2, 1, 1, 1}, sizes);
		}

		[TestMethod]
		public void CanFindSizesGraph9()
		{
			var input =
				new[]
				{
					"1 2",
					"1 3",
					"3 2",
				};

			var sizes = new KosarajuStronglyConnectedComponents()
				.CalcStronglyConnectedComponentSizes(PrepareInput(input, 0));

			AssertHelper.Seq(new[] {1, 1, 1}, sizes);
		}

		private static int[][] PrepareInput(string[] lines, int minus = 1)
		{
			return lines.Select(str => str.Trim().Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries))
				.Select(a => a.Select(str => int.Parse(str) - minus).ToArray())
				.ToArray();
		}
	}
}