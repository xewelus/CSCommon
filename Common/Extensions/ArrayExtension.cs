using System;

namespace Common
{
	public static class ArrayExtension
	{
		public static bool ContainsString(this string[] strings, string s)
		{
			return Array.IndexOf(strings, s) != -1;
		}

		public static string ToBase64(this byte[] bytes)
		{
			return Convert.ToBase64String(bytes);
		}
	}
}