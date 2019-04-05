using System;
using System.Collections;
using System.Collections.Generic;

namespace Common
{
	public static class ListExtension
	{
		public static void Synchronize<T>(
			this IList current,
			ICollection collection,
			Func<T, T, bool> matchFunc = null)
		{
			current.Synchronize<T>(
				collection: collection, 
				addAction: obj => current.Add(obj), 
				removeAction: obj => current.Remove(obj));
		}

		/// <summary>
		/// Оптимизированно синхронизирует список с другой произвольной коллекцией без лишних созданий и удалений объектов.
		/// Не трогает записи удовлетворяющие matchFunc.
		/// Если остались неиспользованные объекты списка, то пытается заполнить их, вместо удаления.
		/// </summary>
		/// <typeparam name="T1">Тип элемента списка.</typeparam>
		/// <typeparam name="T2">Тип объекта коллекции с которой идет синхронизация.</typeparam>
		/// <param name="list">Список.</param>
		/// <param name="collection">Произвольная коллекция объектов любого типа.</param>
		/// <param name="fillFunc">Функция копирования данных из произвольного объекта в элемент списка.</param>
		/// <param name="matchFunc">Функция соответствия элемента списка и произвольного объекта.</param>
		public static void SyncOptimized<T1, T2>(
			this IList list,
			ICollection collection,
			Func<T1> createFunc,
			Action<T1, T2> fillFunc,
			Func<T1, T2, bool> matchFunc)
		{
			// свободные объекты списке
			Queue<T1> freeObjects = new Queue<T1>();

			// объект, которому не было найдено соотвествие в списке
			Queue<T2> needToAdd = new Queue<T2>();

			// элементы с найденным соответсвием
			List<T1> matched = new List<T1>();

			// синхронизация
			list.Synchronize<T1, T2>(
				collection: collection,
				addAction: other => needToAdd.Enqueue(other),
				removeAction: obj => freeObjects.Enqueue(obj),
				matchFunc: (obj, other) => 
				           {
					           if (matchFunc(obj, other))
					           {
						           if (!matched.Contains(obj))
						           {
									   matched.Add(obj);
									   if (fillFunc != null)
									   {
										   fillFunc(obj, other);
									   }
						           }
						           return true;
					           }
					           return false;
				           });

			// пока есть свободные элементы списка, используем их
			while (freeObjects.Count > 0 && needToAdd.Count > 0)
			{
				T1 obj = freeObjects.Dequeue();
				T2 other = needToAdd.Dequeue();

				if (fillFunc != null)
				{
					fillFunc(obj, other);
				}
			}

			// удаляем неиспользованные элементы списка
			while (freeObjects.Count > 0)
			{
				list.Remove(freeObjects.Dequeue());
			}

			// добавляем оставшиеся элементы в список
			while (needToAdd.Count > 0)
			{
				T2 other = needToAdd.Dequeue();

				T1 obj = createFunc();
				list.Add(obj);
				if (fillFunc != null)
				{
					fillFunc(obj, other);
				}
			}
		}

		/// <summary>
		/// Перемещает элемент на новое место.
		/// </summary>
		public static void Move<T>(this IList<T> list, T item, int index)
		{
			list.Remove(item);
			list.Insert(index, item);
		}

		public static void Sort<T>(this List<T> list, Func<T, T, int> method = null)
		{
			list.Sort(new Comparer<T>(method));
		}

		public static void SortNumeratedStrings(this List<string> list)
		{
			list.Sort(method: (x, y) =>
			                  {
				                  string xs, ys;
								  long? xi, yi;
								  x.DivideNumeratedString(out xs, out xi);
								  y.DivideNumeratedString(out ys, out yi);

				                  int result = Comparer.Default.Compare(xs, ys);
				                  return result == 0 ? Comparer.Default.Compare(xi, yi) : result;
			                  });
		}

		private class Comparer<T> : IComparer<T>
		{
			private readonly Func<T, T, int> compareMethod;
			public Comparer(Func<T, T, int> compareMethod)
			{
				this.compareMethod = compareMethod;
			}
			public int Compare(T x, T y)
			{
				return this.compareMethod == null ? Comparer.Default.Compare(x, y) : this.compareMethod(x, y);
			}
		}

		public static List<List<T>> Divide<T>(this IEnumerable<T> collection, int parts)
		{
			List<List<T>> result = new List<List<T>>();
			int next = 0;
			foreach (T item in collection)
			{
				List<T> list;
				if (next >= result.Count)
				{
					if (result.Count == parts)
					{
						next = 0;
						list = result[0];
					}
					else
					{
						list = new List<T>();
						result.Add(list);
						next++;
					}
				}
				else
				{
					list = result[next++];
				}
				list.Add(item);
			}
			return result;
		}
	}
}