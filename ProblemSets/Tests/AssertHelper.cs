using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProblemSets.Services;

namespace Tests
{
	public static class AssertHelper
	{
		public static void Seq<T>(IEnumerable<T> expected, IEnumerable<T> actual)
		{
			if (expected == null)
				Assert.IsNull(actual);
			else 
			{
				Assert.IsNotNull(actual);

				var expArray = expected.ToArray();
				var actArray = actual.ToArray();

				if (!expArray.SequenceEqual(actArray))
					Assert.Fail(
						"\r\nExpected {0}\r\nActual {1}",
						", ".Join(expArray),
						", ".Join(actArray));
			}
		}

		public static void Equal<T>(T expected, T actual, Func<string> getMessage)
		{
			var msg = expected.Equals(actual) ? "" : getMessage();

			Assert.AreEqual(expected, actual, msg);
		}

		public static void DoubleIsNear(double expected, double actual, double epsilon = 0.00000000001)
		{
			Assert.IsTrue(Math.Abs(expected - actual) < epsilon, "Actual is " + actual);
		}
	}
}