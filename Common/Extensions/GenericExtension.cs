using System.Linq;

namespace Common
{
	public static class GenericExtension
	{
		/// <summary>
		/// �������� �� ��������� �������� � ������ 
		/// </summary>
		public static bool In<T>(this T value, params T[] values)
		{
			return values.Contains(value);
		}
	}
}