using System;
using System.Linq;
using System.Runtime.CompilerServices;
using ProblemSets.Services;

namespace ProblemSets.ComputerScience.DataTypes
{
	public class HashTable_OpenAddressing
	{
		internal class Slot
		{
			public long Item;
			public uint Hash;
		}

		private readonly uint len;
		private readonly Slot[] arr;

		public HashTable_OpenAddressing(uint len = 0x800000)
		{
			this.len = len;
			arr = new Slot[len];
		}

		public void Add(long n)
		{
			var hash = GetHash(n);

			// Quadratic probing

			unchecked
			{
				var d = (uint)1;
				var i = hash;
				while (arr[i] != null)
				{
					i = (i + d) % len;
					d += 2;
				}

				arr[i] = new Slot
				{
					Hash = hash,
					Item = n
				};
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint GetHash(long n)
		{
			return MyMath.HashKnuthMultiplicative(n, len);
		}

		public void ShowStats()
		{
			Console.WriteLine("Len = {0}", len);
			Console.WriteLine("Fill factor = {0}", 1 - (float)arr.Count(a => a == null) / arr.Length);
			Console.WriteLine("Memory = {0} KB", GC.GetTotalMemory(false) / 1024);
		}

		public bool Contains(long n)
		{
			var hash = GetHash(n);

			unchecked
			{
				var d = (uint)1;
				var i = hash;
				var slot = arr[i];
				while (slot != null && slot.Item != n)
				{
					i = (i + d) % len;
					d += 2;
					slot = arr[i];
				}

				return slot != null;
			}
		}
	}

}
