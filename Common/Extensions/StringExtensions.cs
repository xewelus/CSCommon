using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace Common
{
	public static class StringExtensions
	{
		/// <summary>
		/// Удаляет из строки символы, недопустимые в числе 
		/// </summary>
		public static decimal ToDecimal(this string @string, IFormatProvider provider)
		{
			string s = @string.Replace(",", ".");
			char[] array = s.ToCharArray();
			string toArray = string.Empty;

			foreach (char c in array)
			{
				if (Char.IsDigit(c) || c == '.')
				{
					toArray = toArray.Insert(toArray.Length, c.ToString());
				}
			}

			try
			{
				return decimal.Parse(toArray, provider);
			}
			catch (Exception ex)
			{
				throw new Exception(string.Format("Ошибка при обработке значения '{0}'.", @string), ex);
			}
		}

		public static decimal ToDecimal(this string s, int digits = 2)
		{
			s = s.Replace(".", ",").Replace(",", NumberFormatInfo.CurrentInfo.NumberDecimalSeparator);
			return Decimal.Parse(s);
		}

		public static void DivideNumeratedString(this string value, out string stringPart, out long? number)
		{
			stringPart = string.Empty;
			number = null;

			long multi = 1;
			for (int i = value.Length - 1; i >= 0; i--)
			{
				char c = value[i];
				if (char.IsDigit(c))
				{
					if (number == null)
					{
						number = 0;
					}
					number = number + int.Parse(c.ToString()) * multi;
					multi *= 10;
				}
				else
				{
					stringPart = value.Substring(0, i + 1);
					break;
				}
			}
		}

		public static string TrimSize(this string value, int max, string rest = null)
		{
			if (value == null) return null;
			if (value.Length <= max) return value;
			if (rest == null) return value.Substring(0, max);
			return value.Substring(0, max - rest.Length) + rest;
		}

		public static byte[] Sha512(this string text)
		{
			using (SHA512 hash = SHA512.Create())
			{
				byte[] data = Encoding.UTF8.GetBytes(text);
				return hash.ComputeHash(data);
			}
		}

		public static string Concat(string divider, bool allowNulls, params object[] parts)
		{
			StringBuilder sb = new StringBuilder();
			foreach (object part in parts)
			{
				if (!allowNulls)
				{
					if (part == null)
					{
						continue;
					}
				}

				if (sb.Length > 0)
				{
					sb.Append(divider);
				}

				sb.Append(part);
			}
			return sb.ToString();
		}
	}
}