using System;
using System.Text;

namespace Common
{
	public static class DateTimeExtension
	{
		public static string GetString(this DateTime datetime, DatePresentation presentation = DatePresentation.Full)
		{
			int value = (int)presentation;

			StringBuilder sb = new StringBuilder();
			if ((value & (int)DatePresentation.Day) == (int)DatePresentation.Day)
			{
				
			}

			throw new NotImplementedException();
		}

		public static DateTime GetWithoutTime(this DateTime datetime)
		{
			return new DateTime(datetime.Year, datetime.Month, datetime.Day);
		}

		public static string GetMonthString(this DateTime datetime, Cases cases = Cases.Nominativus)
		{
			switch (cases)
			{ 
				case Cases.Nominativus:
					switch (datetime.Month)
					{
						case 1: return "������";
						case 2: return "�������";
						case 3: return "����";
						case 4: return "������";
						case 5: return "���";
						case 6: return "����";
						case 7: return "����";
						case 8: return "������";
						case 9: return "��������";
						case 10: return "�������";
						case 11: return "������";
						case 12: return "�������";
						default: throw new NotSupportedException(datetime.Month.ToString());
					}
				case Cases.Genitivus:
					switch (datetime.Month)
					{
						case 1: return "������";
						case 2: return "�������";
						case 3: return "�����";
						case 4: return "������";
						case 5: return "���";
						case 6: return "����";
						case 7: return "����";
						case 8: return "�������";
						case 9: return "��������";
						case 10: return "�������";
						case 11: return "������";
						case 12: return "�������";
						default: throw new NotSupportedException(datetime.Month.ToString());
					}
				case Cases.Praepositionalis:
					switch (datetime.Month)
					{
						case 1: return "������";
						case 2: return "�������";
						case 3: return "�����";
						case 4: return "������";
						case 5: return "���";
						case 6: return "����";
						case 7: return "����";
						case 8: return "�������";
						case 9: return "��������";
						case 10: return "�������";
						case 11: return "������";
						case 12: return "�������";
						default: throw new NotSupportedException(datetime.Month.ToString());
					}
			}
			return null;
		}

		public enum Cases
		{
			/// <summary>
			/// ������������.
			/// </summary>
			Nominativus,
			/// <summary>
			/// �����������.
			/// </summary>
			Genitivus,
			/// <summary>
			/// ����������.
			/// </summary>
			Praepositionalis
		}

		[Flags]
		public enum DatePresentation
		{
			Day = 1,
			DayString = 2,
			Month = 4,
			MonthString = 8,
			Year2 = 16,
			Year4 = 32,
			YearString = 64,
			Full = Day | MonthString | Year4,
			Simple = Day | Month | Year4,
			WithoutDay = MonthString | Year4,
			WithoutYear = Day | MonthString
		}
	}
}