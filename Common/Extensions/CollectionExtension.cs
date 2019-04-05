using System;
using System.Collections;
using System.Collections.Generic;

namespace Common
{
	public static class CollectionExtension
	{
		public static Dictionary<TKey, TValue> Hash<TKey, TValue>(this ICollection current, Func<TValue, TKey> keyFunc)
		{
			Dictionary<TKey, TValue> result = new Dictionary<TKey, TValue>();
			foreach (TValue control in current)
			{
				result[keyFunc(control)] = control;
			}
			return result;
		}

		public static Diff<T> GetDifferences<T>(this ICollection current, ICollection collection, Func<T, T, bool> matchFunc = null)
		{
			Diff<T, T> diff = GetDifferences<T, T>(current, collection, matchFunc);
			return new Diff<T>
			{
				AbsentIn1 = diff.AbsentIn1, 
				AbsentIn2 = diff.AbsentIn2, 
				Existing = diff.Existing
			};
		}

		public static Diff<T1, T2> GetDifferences<T1, T2>(
			this ICollection current, 
			ICollection collection, 
			Func<T1, T2, bool> matchFunc = null)
		{
			CollectionCompareInfo<T1> result1 = CompareCollection(current, collection, matchFunc);

			CollectionCompareInfo<T2> result2 = CompareCollection<T2, T1>(
				c1: collection, 
				c2: current, 
				matchFunc: (o1, o2) => 
					matchFunc == null ? Equals(o1, o2) : matchFunc(o2, o1));

			return new Diff<T1, T2>
			{
				AbsentIn1 = result1.Absent,
				AbsentIn2 = result2.Absent,
				Existing = result1.Existing
			};
		}

		public static void Synchronize<T>(
			this ICollection current, 
			ICollection collection,
			Action<T> addAction,
			Action<T> removeAction = null,
			Func<T, T, bool> matchFunc = null)
		{
			current.Synchronize<T, T>(collection, addAction, removeAction, matchFunc);
		}

		public static void Synchronize<T1, T2>(
			this ICollection current, 
			ICollection collection,
			Action<T2> addAction,
			Action<T1> removeAction = null,
			Func<T1, T2, bool> matchFunc = null)
		{
			Diff<T1, T2> diff = current.GetDifferences(collection, matchFunc);
		
			foreach (T1 obj in diff.AbsentIn1)
			{
				if (removeAction != null)
				{
					removeAction(obj);
				}
			}

			foreach (T2 obj in diff.AbsentIn2)
			{
				if (addAction != null)
				{
					addAction(obj);
				}
			}
		}

		private static CollectionCompareInfo<T1> CompareCollection<T1, T2>(ICollection c1, ICollection c2, Func<T1, T2, bool> matchFunc = null)
		{
			CollectionCompareInfo<T1> result = new CollectionCompareInfo<T1>();
			foreach (T1 o1 in c1)
			{
				bool exists = false;
				foreach (T2 o2 in c2)
				{
					if (matchFunc == null && Equals(o1, o2) || matchFunc != null && matchFunc(o1, o2))
					{
						exists = true;
						break;
					}
				}

				if (exists)
				{
					result.Existing.Add(o1);
				}
				else
				{
					result.Absent.Add(o1);
				}
			}
			return result;
		}

		private class CollectionCompareInfo<T>
		{
			public readonly List<T> Absent = new List<T>();
			public readonly List<T> Existing = new List<T>();
		}
	}

	public class Diff<T> : Diff<T, T>
	{
	}

	public class Diff<T1, T2>
	{
		public List<T1> AbsentIn1;
		public List<T2> AbsentIn2;
		public List<T1> Existing;

		public bool ContainsDiffs
		{
			get
			{
				return this.AbsentIn1.Count > 0 || this.AbsentIn2.Count > 0;
			}
		}

		public override string ToString()
		{
			return string.Format("Нет 1: {0}, нет в 2: {1}, совпадений: {2}", 
				this.AbsentIn1 == null ? "?" : this.AbsentIn1.Count.ToString(), 
				this.AbsentIn2 == null ? "?" : this.AbsentIn2.Count.ToString(), 
				this.Existing == null ? "?" : this.Existing.Count.ToString());
		}
	}
}