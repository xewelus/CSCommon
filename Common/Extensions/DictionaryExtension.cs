using System.Collections.Generic;

namespace Common
{
	public static class DictionaryExtension
	{
		public static TValue Get<TKey, TValue>(this Dictionary<TKey, TValue> dic, TKey key)
		{
			TValue value;
			dic.TryGetValue(key, out value);
			return value;
		}

		public static TValue GetOrCreate<TKey, TValue>(this Dictionary<TKey, TValue> dic, TKey key) where TValue : new()
		{
			TValue value;
			if (!dic.TryGetValue(key, out value))
			{
				value = new TValue();
				dic[key] = value;
			}
			return value;
		}

		public static void AddToList<TKey, TValue>(this Dictionary<TKey, List<TValue>> dic, TKey key, TValue value)
		{
			List<TValue> list = dic.GetOrCreate(key);
			list.Add(value);
		}

		public static void RemoveFromList<TKey, TValue>(this Dictionary<TKey, List<TValue>> dic, TKey key, TValue value)
		{
			List<TValue> list = dic.Get(key);
			if (list != null)
			{
				list.Remove(value);
				if (list.Count == 0)
				{
					dic.Remove(key);
				}
			}
		}
	}
}
