using System;

namespace Common
{
	public static class ObjectExtension
	{
		public static string ToHRString(this object obj, string format = null)
		{
			if (obj is DateTime)
			{
				DateTime d = (DateTime)obj;
				if (format == null)
				{
					if (d.Date == d)
					{
						return d.ToString("dd.MM.yyyy");
					}
					return d.ToString("dd.MM.yyyy HH:mm:ss");
				}
				return d.ToString(format);
			}

			if (obj is decimal)
			{
				decimal d = (decimal)obj;
				if (format == null)
				{
					return d.ToString("0.00##");
				}
				return d.ToString(format);
			}

			if (obj is Enum)
			{
				return Enum.GetName(obj.GetType(), obj);
			}

			return obj.ToString();
		}

		public static bool IsNumber(this string s)
		{
			foreach (char c in s)
			{
				if (!char.IsNumber(c)) return false;
			}
			return true;
		}
	}
}