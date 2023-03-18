using System.Collections.Generic;

namespace Common.Randomizer
{
	public class ChanceRandomizer<T>
	{
		public List<ChanceRecord<T>> Records = new List<ChanceRecord<T>>();
		public int Total;

		public void Add(int chance, T value)
		{
			ChanceRecord<T> rec = new ChanceRecord<T>(chance, value);
			this.Records.Add(rec);
			this.Total += chance;
		}

		public T Get()
		{
			int counter = Randomizer.GetNumberBetween(0, this.Total - 1);
			foreach (ChanceRecord<T> r in this.Records)
			{
				counter -= r.Chance;
				if (counter < 0)
				{
					return r.Value;
				}
			}

			return this.Records[this.Records.Count - 1].Value;
		}

		public class ChanceRecord<T>
		{
			public int Chance;
			public T Value;

			public ChanceRecord(int chance, T value)
			{
				this.Chance = chance;
				this.Value = value;
			}
		}
	}
}