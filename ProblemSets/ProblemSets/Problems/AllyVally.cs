using System;
using System.Collections.Generic;
using System.Linq;

namespace ProblemSets.Problems
{
	/*
	 У некоторого султана было два мудреца: Али-ибн-Вали и Вали-ибн-Али. Желая убедиться в их мудрости, султан призвал мудрецов к себе и сказал:
«Я задумал два числа. Оба они целые, каждое больше единицы, но меньше ста. Я перемножил эти числа и результат сообщу Али и при этом Вали я скажу сумму этих чисел. 
Если вы и вправду так мудры, как о вас говорят, то сможете узнать исходные числа».

Мудрецы задумались. Первым нарушил молчание Али.
— Я не знаю этих чисел, — сказал он, опуская голову.
— Я это знал, — подал голос Вали.
— Тогда я знаю эти числа, — обрадовался Али.
— Тогда и я знаю! — воскликнул Вали.
И мудрецы сообщили пораженному царю задуманные им числа.
	 */

	public class AllyVally
	{
		public static void Solve()
		{
			var solution =
				from sol in AllCandidates()

				// Ali doesn't know the numbers
				where AliCandidates(sol)
					 .Uncertain()

				// Vali knows that Ali doesn't know the numbers
				where ValiCandidates(sol)
					 .All(vc => AliCandidates(vc).Uncertain())

				// Then Ali had guess on this numbers
				where AliCandidates(sol)
					 .Where(ac => ValiCandidates(ac).All(vc => AliCandidates(vc).Uncertain()))
					 .Confident()

				// Then Vali had guess on this numbers
				where ValiCandidates(sol)
					 .Where(vc => AliCandidates(vc).Where(ac => ValiCandidates(ac).All(vvc => AliCandidates(vvc).Uncertain())).Confident())
					 .Confident()

				select sol;


			foreach (var sol in solution)
				Console.WriteLine(sol);
		}

		private static IEnumerable<Solution> AliCandidates(Solution sol)
		{
			return
				from vc in AllCandidates()
				where vc.X * vc.Y == sol.Mul
				select vc;
		}

		private static IEnumerable<Solution> ValiCandidates(Solution sol)
		{
			return
				from ac in AllCandidates()
				where ac.X + ac.Y == sol.Sum
				select ac;
		}

		private static IEnumerable<Solution> AllCandidates()
		{
			return
				from x in Enumerable.Range(2, 98)
				from y in Enumerable.Range(2, x - 1)
				select new Solution(x, y);
		}

		public struct Solution
		{
			public Solution(int x, int y)
				: this()
			{
				X = x;
				Y = y;
				Mul = x * y;
				Sum = x + y;
			}

			public int Mul;
			public int Sum;
			public int X;
			public int Y;

			public override string ToString()
			{
				return string.Format("X = {0}, Y = {1}", X, Y);
			}
		}
	}
}