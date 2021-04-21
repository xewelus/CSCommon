using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Common.Randomizer
{
	/// <summary>
	/// �����, ���������� ������ �� ��������� ��������� �������.
	/// </summary>
	public class Randomizer
	{
		/// <summary>
		/// ���������.
		/// </summary>
		public static readonly Random random = new Random();

		/// <summary>
		/// ���������� ��������� ������� ���� �� ������������� ��������.
		/// </summary>
		/// <typeparam name="T">��� ��������.</typeparam>
		/// <param name="objs">�������.</param>
		/// <returns>���� �� ��������.</returns>
		public static object GetOneOf(params object[] objs)
		{
			// ���������� ��������� ����� ������� �� ���-�� ��������
			int i = random.Next(objs.Length);
			return objs[i];
		}

		/// <summary>
		/// ���������� ��������� ������� ���� �� ������������� ��������.
		/// </summary>
		/// <typeparam name="T">��� ��������.</typeparam>
		/// <param name="objs">�������.</param>
		/// <returns>���� �� ��������.</returns>
		public static T GetOneOf<T>(params T[] objs)
		{
			// ���������� ��������� ����� ������� �� ���-�� ��������
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
		/// ���������� ��������� ����� �� ���������.
		/// (����������� ��� start ����� ���� ������ end)
		/// </summary>
		/// <param name="start">������ ������� ���������.</param>
		/// <param name="end">������ ������� ���������.</param>
		public static int GetNumberBetween(int start, int end)
		{
			// ����������� ��������
			int min = Math.Min(start, end);
			// ������������ ��������
			int max = Math.Max(start, end);

			// ��������� ��������
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
		/// ���������� ��������� �����.
		/// </summary>
		public static int GetDigit()
		{
			return GetNumberBetween(0, 9);
		}

		/// <summary>
		/// ���������� ���� ����� ����� ������.
		/// </summary>
		/// <param name="start">��������� ����.</param>
		/// <param name="end">�������� ����.</param>
		public static DateTime GetDateBetween(DateTime start, DateTime end)
		{
			TimeSpan ts = end.Subtract(start);
			return start.AddDays(GetNumberBetween(0, (int)ts.TotalDays));
		}

		/// <summary>
		/// ���������� ���� ����� ����� ������.
		/// </summary>
		/// <param name="start">��������� ����.</param>
		/// <param name="end">�������� ����.</param>
		public static DateTime Date(DateTime start, DateTime end)
		{
			return start.AddDays(Randomizer.GetNumberBetween(0, end.Subtract(start).Days));
		}

		/// <summary>
		/// ���������� ������������������ ����.
		/// </summary>
		/// <param name="count">���-�� ���� � ������������������.</param>
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
		/// ���������� true � ��������� ������������.
		/// </summary>
		/// <param name="chance">���� (0..1)</param>
		public static bool TestChance(float chance)
		{
			return random.NextDouble() < chance;
		}

		/// <summary>
		/// ���������� �������� � ��������� ������������.
		/// </summary>
		/// <typeparam name="T">��� ��������.</typeparam>
		/// <param name="chance">���� (0..1)</param>
		/// <param name="success">��������, � ������ ������.</param>
		/// <param name="fail">��������, � ������ �������.</param>
		public static object GetWithChance(float chance, object success, object fail)
		{
			return TestChance(chance) ? success : fail;
		}

		/// <summary>
		/// ���������� �������� � ��������� ������������.
		/// </summary>
		/// <typeparam name="T">��� ��������.</typeparam>
		/// <param name="chance">���� (0..1)</param>
		/// <param name="success">��������, � ������ ������.</param>
		/// <param name="fail">��������, � ������ �������.</param>
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
		/// ���������� ��������� ������.
		/// </summary>
		/// <param name="charLang">���������� ���������.</param>
		/// <param name="charCase">���������� ��������.</param>
		/// <param name="additional">�������������� �������.</param>
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
			return (string) GetOneOf("������", "������", "�������", "��������", "�������", "������", "��������", "�����", "�������", "�������", "������", "�������", "�������", "������", "������", "��������", "��������", "������", "������", "�������", "�������", "����������", "��������", "��������", "�������", "��������", "������", "�������", "�����", "�������", "�����", "�������", "�������", "�������");
		}

		public static string GetFirstName()
		{
			return (string)GetOneOf("����", "�����", "�������", "������", "������", "��������", "�������", "�������", "���������", "�������", "����", "����", "�������", "�������", "����", "�����", "��������", "������", "�����", "����", "������", "�������", "����", "������", "�����", "��������", "�����", "�����", "��������", "���������", "����", "������", "�������", "��������", "����������", "�����", "�����", "��������", "����", "������", "�����", "�������", "������", "���������", "�����", "��������", "����", "������", "��������", "�������", "����������", "�����", "������", "������", "���", "�����", "�����", "����", "������", "�����", "���������", "��������", "�������", "������", "�����", "�����", "������", "������", "������", "����", "�����", "������", "����", "�������", "������");
		}

		public static string GetMiddleName()
		{
			return (string) GetOneOf("�������������", "����������", "�����������", "���������", "���������", "����������", "����������", "���������", "������������", "����������", "����������", "����������", "������������", "������������", "����������", "����������", "�����������", "���������", "����������", "����������", "��������", "��������", "��������", "��������", "������", "���������", "����������", "��������������", "����������", "�������", "����������", "���������", "����������", "����������", "��������", "��������", "��������", "����������", "����������", "���������", "���������", "���������", "����������", "�������������", "���������", "����������", "���������", "����������", "����������", "����������", "�������", "���������", "������������");
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
				// ���� ��������� ��������, �� ��������� ��������� �������
				if ((charLang & CharLang.English) == CharLang.English)
				{
					// ����������� �� ���� ��������� ��������
					for (char c = 'a'; c <= 'z'; c++)
					{
						// ���� ��������� ������� � ������ ��������, ���������
						if ((charCase & CharCase.Lowercase) == CharCase.Lowercase)
						{
							this.Add(c);
						}
						// ���� ��������� ������� � ������� ��������, ���������
						if ((charCase & CharCase.Uppercase) == CharCase.Uppercase)
						{
							this.Add(char.ToUpper(c));
						}
					}
				}
				// ���� ��������� ���������, �� ��������� ������� ���������
				if ((charLang & CharLang.Russian) == CharLang.Russian)
				{
					// ����������� �� ���� �������� ���������
					for (char c = '�'; c <= '�'; c++)
					{
						// ���� ��������� ������� � ������ ��������, ���������
						if ((charCase & CharCase.Lowercase) == CharCase.Lowercase)
						{
							this.Add(c);
						}
						// ���� ��������� ������� � ������� ��������, ���������
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
		/// ������� ���������� ���������.
		/// </summary>
		[Flags]
		public enum CharLang
		{
			/// <summary>
			/// ��������� ������� ���������.
			/// </summary>
			Russian = 1,
			/// <summary>
			/// ��������� ��������� ���������.
			/// </summary>
			English = 2,
			/// <summary>
			/// ��������� �����.
			/// </summary>
			Digits = 4
		}

		/// <summary>
		/// ������� ���������� ���������.
		/// </summary>
		[Flags]
		public enum CharCase
		{
			/// <summary>
			/// �������� ������ �������.
			/// </summary>
			Lowercase = 1,
			/// <summary>
			/// �������� ������� �������.
			/// </summary>
			Uppercase = 2
		}
	}

	public class Rnd : Randomizer
	{
		/// <summary>
		/// ���������� ������������������ ����.
		/// </summary>
		/// <param name="count">���-�� ���� � ������������������.</param>
		public static string Digits(int? count = null)
		{
			return GetNumbersSequence(count ?? Between(1, 10));
		}

		/// <summary>
		/// ���������� ��������� ����� �� ���������.
		/// (����������� ��� start ����� ���� ������ end)
		/// </summary>
		/// <param name="start">������ ������� ���������.</param>
		/// <param name="end">������ ������� ���������.</param>
		public static int Between(int start, int end)
		{
			return GetNumberBetween(start, end);
		}
	}
}