using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Common.Randomizer
{
	/// <summary>
	/// Класс, включающий методы по генерации случайных величин.
	/// </summary>
	public class Randomizer
	{
		/// <summary>
		/// Генератор.
		/// </summary>
		public static readonly Random random = new Random();

		/// <summary>
		/// Возвращает случайным образом один из перечисленных объектов.
		/// </summary>
		/// <typeparam name="T">Тип объектов.</typeparam>
		/// <param name="objs">Объекты.</param>
		/// <returns>Один из объектов.</returns>
		public static object GetOneOf(params object[] objs)
		{
			// определяем случайный номер объекта из кол-ва объектов
			int i = random.Next(objs.Length);
			return objs[i];
		}

		/// <summary>
		/// Возвращает случайным образом один из перечисленных объектов.
		/// </summary>
		/// <typeparam name="T">Тип объектов.</typeparam>
		/// <param name="objs">Объекты.</param>
		/// <returns>Один из объектов.</returns>
		public static T GetOneOf<T>(params T[] objs)
		{
			// определяем случайный номер объекта из кол-ва объектов
			int i = random.Next(objs.Length);
			return objs[i];
		}

		public static T GetOneOf<T>(IList list)
		{
			int i = random.Next(list.Count);
			return (T)list[i];
		}

		public static bool GetBoolean()
		{
			return GetOneOf(true, false);
		}

		public static bool TestInt(int count)
		{
			return GetNumberBetween(0, count-1) == 0;
		}

		/// <summary>
		/// Возвращает случайное целое из диапазона.
		/// (учитывается что start может быть больше end)
		/// </summary>
		/// <param name="start">Первая граница диапазона.</param>
		/// <param name="end">Вторая граница диапазона.</param>
		public static int GetNumberBetween(int start, int end)
		{
			// минимальное значение
			int min = Math.Min(start, end);
			// максимальное значение
			int max = Math.Max(start, end);

			// случайное значение
			return start + random.Next(max - min + 1);
		}

		public static int GetHashedNumberBetween(string hash, int start, int end)
		{
			byte[] input;
			if (string.IsNullOrEmpty(hash))
			{
				input = new byte[] {0};
			}
			else
			{
				input = Encoding.UTF8.GetBytes(hash);
			}

			SHA1 sha256 = SHA1.Create();
			byte[] output = sha256.ComputeHash(input);
			int i = output[2] << (8 * 2) + output[1] << (8 * 1) + output[0];
			int rest = i % (end - start);
			return start + rest;
		}

		/// <summary>
		/// Возвращает случайную цифру.
		/// </summary>
		public static int GetDigit()
		{
			return GetNumberBetween(0, 9);
		}

		/// <summary>
		/// Возвращает дату между двумя датами.
		/// </summary>
		/// <param name="start">Начальная дата.</param>
		/// <param name="end">Конечная дата.</param>
		public static DateTime GetDateBetween(DateTime start, DateTime end)
		{
			TimeSpan ts = end.Subtract(start);
			return start.AddDays(GetNumberBetween(0, (int)ts.TotalDays));
		}

		/// <summary>
		/// Возвращает дату между двумя датами.
		/// </summary>
		/// <param name="start">Начальная дата.</param>
		/// <param name="end">Конечная дата.</param>
		public static DateTime Date(DateTime start, DateTime end)
		{
			return start.AddDays(Randomizer.GetNumberBetween(0, end.Subtract(start).Days));
		}

		/// <summary>
		/// Возвращает последовательность цифр.
		/// </summary>
		/// <param name="count">Кол-во цифр в последовательности.</param>
		public static string GetNumbersSequence(int count)
		{
			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < count; i++)
			{
				sb.Append(GetDigit());
			}
			return sb.ToString();
		}

		/// <summary>
		/// Возвращает true с указанной вероятностью.
		/// </summary>
		/// <param name="chance">Шанс (0..1)</param>
		public static bool TestChance(float chance)
		{
			return random.NextDouble() < chance;
		}

		/// <summary>
		/// Возвращает значения с указанной вероятностью.
		/// </summary>
		/// <typeparam name="T">Тип значений.</typeparam>
		/// <param name="chance">Шанс (0..1)</param>
		/// <param name="success">Значение, в случае успеха.</param>
		/// <param name="fail">Значение, в случае провала.</param>
		public static object GetWithChance(float chance, object success, object fail)
		{
			return TestChance(chance) ? success : fail;
		}

		/// <summary>
		/// Возвращает значения с указанной вероятностью.
		/// </summary>
		/// <typeparam name="T">Тип значений.</typeparam>
		/// <param name="chance">Шанс (0..1)</param>
		/// <param name="success">Значение, в случае успеха.</param>
		/// <param name="fail">Значение, в случае провала.</param>
		public static T GetWithChance<T>(float chance, T success, T fail)
		{
			return TestChance(chance) ? success : fail;
		}

		public static T GetWithChance<T>(params ChanceRecord<T>[] records)
		{
			float total = 0f;
			foreach (ChanceRecord<T> r in records)
			{
				total += r.Chance;
			}

			float f = (float) random.NextDouble() * total;
			float level = 0;
			foreach (ChanceRecord<T> r in records)
			{
				level += r.Chance;
				if (f < level)
				{
					return r.Value;
				}
			}
			return records[records.Length - 1].Value;
		}

		public static T GetWithChance<T>(ChanceCollection<T> records)
		{
			return GetWithChance(records.Items.ToArray());
		}

		public class ChanceCollection<T> : IEnumerable
		{
			public readonly List<ChanceRecord<T>> Items = new List<ChanceRecord<T>>();
			public void Add(T value, float chance)
			{
				this.Items.Add(new ChanceRecord<T>(chance, value));
			}

			public IEnumerator GetEnumerator()
			{
				throw new NotImplementedException();
			}
		}

		//public class ChanceRecord
		//{
		//	public float Chance;
		//	public object Value;

		//	public ChanceRecord(float chance, object value)
		//	{
		//		this.Chance = chance;
		//		this.Value = value;
		//	}
		//}

		public class ChanceRecord<T>
		{
			public float Chance;
			public T Value;

			public ChanceRecord(float chance, T value)
			{
				this.Chance = chance;
				this.Value = value;
			}
		}

		/// <summary>
		/// Возвращает случайный символ.
		/// </summary>
		/// <param name="charLang">Допустимые раскладки.</param>
		/// <param name="charCase">Допустимые регистры.</param>
		/// <param name="additional">Дополнительные символы.</param>
		public static char GetChar(CharLang charLang, CharCase charCase, params char[] additional)
		{
			StringSettings stringSettings = new StringSettings();
			stringSettings.Add(charLang, charCase);
			stringSettings.Add(additional);
			return GetChar(stringSettings);
		}

		public static char GetChar(StringSettings stringSettings)
		{
			object[] chars = (object[]) stringSettings.Chars.ToArray(typeof(object));
			return (char) GetOneOf(chars);
		}

		public static string GetString(StringSettings settings, int minlength, int maxlength)
		{
			int len = Randomizer.GetNumberBetween(minlength, maxlength);
			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < len; i++)
			{
				sb.Append(GetChar(settings));
			}
			return sb.ToString();
		}

		public static string GetLastName()
		{
			return (string) GetOneOf("Иванов", "Петров", "Сидоров", "Николаев", "Смирнов", "Иванов", "Кузнецов", "Попов", "Соколов", "Лебедев", "Козлов", "Новиков", "Морозов", "Петров", "Волков", "Соловьев", "Васильев", "Зайцев", "Павлов", "Семенов", "Голубев", "Виноградов", "Богданов", "Воробьев", "Федоров", "Михайлов", "Беляев", "Тарасов", "Белов", "Комаров", "Орлов", "Киселев", "Макаров", "Андреев");
		}

		public static string GetFirstName()
		{
			return (string)GetOneOf("Адам", "Вадим", "Евгений", "Никита", "Адольф", "Валентин", "Евдоким", "Николай", "Александр", "Валерий", "Егор", "Олег", "Алексей", "Василий", "Ефим", "Павел", "Анатолий", "Виктор", "Захар", "Петр", "Андрей", "Виталий", "Иван", "Прохор", "Антон", "Владимир", "Игорь", "Роман", "Аристарх", "Владислав", "Илья", "Руслан", "Аркадий", "Всеволод", "Иннокентий", "Семен", "Арсен", "Вячеслав", "Карл", "Сергей", "Артем", "Гавриил", "Кирилл", "Станислав", "Артур", "Геннадий", "Клим", "Степан", "Афанасий", "Георгий", "Константин", "Тарас", "Богдан", "Герман", "Лев", "Тимур", "Борис", "Глеб", "Леонид", "Федор", "Бронислав", "Григорий", "Леонтий", "Филипп", "Давид", "Макар", "Эдуард", "Даниил", "Максим", "Юрий", "Денис", "Мартин", "Яков", "Дмитрий", "Михаил");
		}

		public static string GetMiddleName()
		{
			return (string) GetOneOf("Александрович", "Алексеевич", "Анатольевич", "Андреевич", "Антонович", "Аркадьевич", "Богданович", "Борисович", "Валентинович", "Валерьевич", "Васильевич", "Викторович", "Владимирович", "Вячеславович", "Генадиевич", "Георгиевич", "Григорьевич", "Данилович", "Дмитриевич", "Евгеньевич", "Егорович", "Ефимович", "Иванович", "Игоревич", "Ильичи", "Иосифович", "Кириллович", "Константинович", "Леонидович", "Львович", "Максимович", "Матвеевич", "Михайлович", "Николаевич", "Олегович", "Павлович", "Петрович", "Платонович", "Робертович", "Романович", "Семенович", "Сергеевич", "Степанович", "Станиславович", "Тарасович", "Тимофеевич", "Федорович", "Феликсович", "Филиппович", "Эдуардович", "Юрьевич", "Яковлевич", "Ярославочвич");
		}

		public static string GetFIO()
		{
			return PeopleRandomizer.GetFIO();
		}

		public static string[] GetResourceLines(string resource)
		{
			Stream stream = typeof(Randomizer).Assembly.GetManifestResourceStream(resource);
			if (stream == null)
			{
				throw new NullReferenceException(resource);
			}

			byte[] data = new byte[stream.Length];
			stream.Read(data, 0, (int)stream.Length);

			string text = Encoding.UTF8.GetString(data);
			text = text.Replace("\r\n", "\n");

			return text.Split('\n');
		}

		public class StringSettings
		{
			public ArrayList Chars = new ArrayList();

			public StringSettings Add(params char[] chars)
			{
				this.Chars.AddRange(chars);
				return this;
			}

			public StringSettings AddRange(char c1, char c2)
			{
				for (char c = c1; c <= c2; c++)
				{
					this.Add(c);
				}
				return this;
			}

			public StringSettings Add(CharLang charLang, CharCase charCase)
			{
				// если допустима латиница, то добавляем латинские символы
				if ((charLang & CharLang.English) == CharLang.English)
				{
					// пробегаемся по всем латинским символам
					for (char c = 'a'; c <= 'z'; c++)
					{
						// если допустимы символы в нижнем регистре, добавляем
						if ((charCase & CharCase.Lowercase) == CharCase.Lowercase)
						{
							this.Add(c);
						}
						// если допустимы символы в верхнем регистре, добавляем
						if ((charCase & CharCase.Uppercase) == CharCase.Uppercase)
						{
							this.Add(char.ToUpper(c));
						}
					}
				}
				// если допустима кириллица, то добавляем символы кириллицы
				if ((charLang & CharLang.Russian) == CharLang.Russian)
				{
					// пробегаемся по всем символам кириллицы
					for (char c = 'а'; c <= 'я'; c++)
					{
						// если допустимы символы в нижнем регистре, добавляем
						if ((charCase & CharCase.Lowercase) == CharCase.Lowercase)
						{
							this.Add(c);
						}
						// если допустимы символы в верхнем регистре, добавляем
						if ((charCase & CharCase.Uppercase) == CharCase.Uppercase)
						{
							this.Add(char.ToUpper(c));
						}
					}
				}

				return this;
			}

		}

		/// <summary>
		/// Выборка допустимых раскладок.
		/// </summary>
		[Flags]
		public enum CharLang
		{
			/// <summary>
			/// Допустима русская раскладка.
			/// </summary>
			Russian = 1,
			/// <summary>
			/// Допустима латинская раскладка.
			/// </summary>
			English = 2,
			/// <summary>
			/// Допустимы цифры.
			/// </summary>
			Digits = 4
		}

		/// <summary>
		/// Выборка допустимых регистров.
		/// </summary>
		[Flags]
		public enum CharCase
		{
			/// <summary>
			/// Допустим нижний регистр.
			/// </summary>
			Lowercase = 1,
			/// <summary>
			/// Допустим верхний регистр.
			/// </summary>
			Uppercase = 2
		}
	}

	public class Rnd : Randomizer
	{
		/// <summary>
		/// Возвращает последовательность цифр.
		/// </summary>
		/// <param name="count">Кол-во цифр в последовательности.</param>
		public static string Digits(int? count = null)
		{
			return GetNumbersSequence(count ?? Between(1, 10));
		}

		/// <summary>
		/// Возвращает случайное целое из диапазона.
		/// (учитывается что start может быть больше end)
		/// </summary>
		/// <param name="start">Первая граница диапазона.</param>
		/// <param name="end">Вторая граница диапазона.</param>
		public static int Between(int start, int end)
		{
			return GetNumberBetween(start, end);
		}
	}
}