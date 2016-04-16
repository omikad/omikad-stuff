using System;

namespace ProblemSets.ComputerScience.DataTypes
{
	public class BitArrayX
	{
		private readonly int[] arr;
		public int Length { get; }

		public BitArrayX(int length, bool setAllValue)
		{
			arr = new int[GetArrayLength(length, 32)];
			Length = length;

			if (setAllValue)
			{
				const int fillValue = unchecked(((int)0xffffffff));
				for (var i = 0; i < arr.Length; i++)
					arr[i] = fillValue;
			}

			if (arr.Length > 0 && length % 32 != 0)
			{
				var lastIntLen = length - ((arr.Length - 1) * 32);
				var mask = (1 << lastIntLen) - 1;
				arr[arr.Length - 1] &= mask;
			}
		}

		public BitArrayX(int length)
		{
			arr = new int[GetArrayLength(length, 32)];
			Length = length;
			for (var i = 0; i < arr.Length; i++)
				arr[i] = 0;
		}

		public BitArrayX(BitArrayX bits)
		{
			var arrayLength = GetArrayLength(bits.Length, 32);

			arr = new int[arrayLength];
			Length = bits.Length;

			Array.Copy(bits.arr, arr, arrayLength);
		}

		public BitArrayX(BitArrayX bits, int additionalLength)
		{
			Length = bits.Length + additionalLength;

			arr = new int[GetArrayLength(Length, 32)];

			Array.Copy(bits.arr, arr, bits.arr.Length);
		}

		public BitArrayX(bool[] values)
		{
			arr = new int[GetArrayLength(values.Length, 32)];
			Length = values.Length;

			for (var i = 0; i < values.Length; i++)
				if (values[i])
					arr[i / 32] |= (1 << (i % 32));
		}

		public bool[] ToArray()
		{
			var result = new bool[Length];
			for (var i = 0; i < result.Length; i++)
				result[i] = this[i];
			return result;
		}

		public bool this[int index]
		{
			get
			{
				return (arr[index / 32] & (1 << (index % 32))) != 0;
			}
			set
			{
				if (value)
					arr[index / 32] |= (1 << (index % 32));
				else
					arr[index / 32] &= ~(1 << (index % 32));
			}
		}

		public bool AllZero
		{
			get
			{
				foreach (var i in arr)
					if (i != 0)
						return false;
				return true;
			}
		}

		public BitArrayX And(BitArrayX value)
		{
			if (Length != value.Length)
				throw new ArgumentException("Lengths are differ");

			var ints = GetArrayLength(Length, 32);
			for (var i = 0; i < ints; i++)
				arr[i] &= value.arr[i];

			return this;
		}

		public BitArrayX Not()
		{
			var ints = GetArrayLength(Length, 32);
			for (var i = 0; i < ints; i++)
				arr[i] = ~arr[i];

			return this;
		}

		private static int GetArrayLength(int n, int div)
		{
			return n > 0 ? (((n - 1) / div) + 1) : 0;
		}

		public static bool Equals(BitArrayX x, BitArrayX y)
		{
			if (x.Length != y.Length) return false;

			for (var i = 0; i < x.arr.Length; i++)
				if (x.arr[i] != y.arr[i])
					return false;

			return true;
		}
	}
}
