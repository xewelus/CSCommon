using System;
using System.Collections.Generic;

namespace Common
{
	public static class EnumerableExtension
	{
		public static List<TGroup> Group<TGroup, TKey, TItem>(
			this IEnumerable<TItem> list,
			Func<TItem, TKey> keyMethod,
			Action<TGroup, TItem> initMethod = null,
			Action<TGroup, TItem> updateMethod = null) where TGroup : Group<TKey, TItem>, new()
		{
			List<TGroup> result = new List<TGroup>();

			Dictionary<TKey, TGroup> dict = new Dictionary<TKey, TGroup>();
			foreach (TItem item in list)
			{
				TKey key = keyMethod(item);

				TGroup group;
				if (!dict.TryGetValue(key, out group))
				{
					group = new TGroup();
					group.Key = key;
					dict[key] = group;
					result.Add(group);

					if (initMethod != null)
					{
						initMethod(group, item);
					}
				}
				group.Add(item);

				if (updateMethod != null)
				{
					updateMethod(group, item);
				}
			}

			return result;
		}

		public static List<Group<TKey, TItem>> Group<TKey, TItem>(
			this IEnumerable<TItem> list,
			Func<TItem, TKey> keyMethod,
			Action<Group<TKey, TItem>, TItem> updateMethod = null)
		{
			return Group<Group<TKey, TItem>, TKey, TItem>(
				list: list, 
				keyMethod: keyMethod, 
				updateMethod: updateMethod);
		}
	}

	public class Group<TKey, TItem> : List<TItem>
	{
		public TKey Key;
	}
}