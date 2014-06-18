using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using ProblemSets.Services;

namespace ProblemSets.ComputerScience.DataTypes
{
	public class HashTable_Chaining
	{
		private const uint Len = 0x800000;

		private readonly List<long>[] arr;

		public HashTable_Chaining()
		{
			arr = new List<long>[Len];
			for (var i = 0; i < arr.Length; i++)
				arr[i] = new List<long>();
		}

		public void Add(long n)
		{
			arr[GetHash(n)].Add(n);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint GetHash(long n)
		{
			return MyMath.HashKnuthMultiplicative(n, Len);
		}

		public void ShowStats()
		{
			Console.WriteLine("Len = {0}", Len);
			Console.WriteLine("Collisions = {0}", arr.Count(a => a.Count > 1));
			Console.WriteLine("Max chain len = {0}", arr.Max(a => a.Count));
			Console.WriteLine("Fill factor = {0}", 1 - (float)arr.Count(a => a.Count == 0) / arr.Length);
			Console.WriteLine("Memory = {0} KB", GC.GetTotalMemory(false) / 1024);
		}

		public bool Contains(long n)
		{
			var list = arr[GetHash(n)];
			return list.Contains(n);
		}
	}
}