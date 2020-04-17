using System;

namespace Common.Classes
{
	[Serializable]
	public class SumAndCount
	{
		public decimal Sum;
		public int Count;

		public override string ToString()
		{
			return string.Format("{0:0.00} ({1})", this.Sum, this.Count);
		}
	}

	[Serializable]
	public struct MonthYear
	{
		public int Month;
		public int Year;

		public MonthYear(int month, int year)
		{
			this.Month = month;
			this.Year = year;
		}

		public MonthYear(DateTime dateTime) : this(dateTime.Month, dateTime.Year)
		{
			this.Month = dateTime.Month;
			this.Year = dateTime.Year;
		}
	}

	public static class Tuple
	{
		public static Tuple<T1, T2> Create<T1, T2>(T1 obj1, T2 obj2)
		{
			return new Tuple<T1, T2>(obj1, obj2);
		}
	}

	[Serializable]
	public struct Tuple<T1, T2>
	{
		public T1 Obj1;
		public T2 Obj2;

		public Tuple(T1 obj1, T2 obj2)
		{
			this.Obj1 = obj1;
			this.Obj2 = obj2;
		}
	}

	[Serializable]
	public struct Pair<T>
	{
		public T Obj1;
		public T Obj2;

		public Pair(T obj1, T obj2)
		{
			this.Obj1 = obj1;
			this.Obj2 = obj2;
		}
	}
}
