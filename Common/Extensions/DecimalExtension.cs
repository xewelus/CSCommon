using System;

namespace Common
{
	public static class DecimalExtension
	{
		public static decimal DecimalRound(this decimal d, int digits = 2)
		{
			decimal multiplier = 1;
			for (int i = 0; i < digits; i++)
			{
				multiplier = multiplier * 10;
			}

			decimal result = d * multiplier;

			if (result >= 0)
			{
				result = result + 0.5M;
			}
			else
			{
				result = result - 0.5M;
			}

			result = Decimal.Truncate(result);
			result = result / multiplier;
			return result;
		}
	}

	public static class DoubleExtension
	{
		public static bool IsAboutEqual(this double? d1, double? d2)
		{
			if (d1 == null && d2 == null) return true;
			if (d1 == null || d2 == null) return false;

			return IsAboutEqual(d1.Value, d2.Value);
		}

		public static bool IsAboutEqual(this double d1, double d2)
		{
			const double EPSILON = double.Epsilon * 10.0;
			return Math.Abs(d1 - d2) < EPSILON;
		}

		public static bool IsAboutZero(this double d)
		{
			return IsAboutEqual(d, 0.0);
		}
	}
}