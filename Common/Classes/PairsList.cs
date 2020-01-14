using System;
using System.Collections.Generic;

namespace Common.Classes
{
	/// <summary>
	/// Список пар "ключ"-"значение" упорядоченных по индексу.
	/// </summary>
	[Serializable]
	public class PairsList<TKey, TValue> : List<KeyValuePair<TKey, TValue>>
	{
		public void Add(TKey key, TValue value)
		{
			this.Add(new KeyValuePair<TKey, TValue>(key, value));
		}
	}
}
