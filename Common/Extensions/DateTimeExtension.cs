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
						case 1: return "€нварь";
						case 2: return "февраль";
						case 3: return "март";
						case 4: return "апрель";
						case 5: return "май";
						case 6: return "июнь";
						case 7: return "июль";
						case 8: return "август";
						case 9: return "сент€брь";
						case 10: return "окт€брь";
						case 11: return "но€брь";
						case 12: return "декабрь";
						default: throw new NotSupportedException(datetime.Month.ToString());
					}
				case Cases.Genitivus:
					switch (datetime.Month)
					{
						case 1: return "€нвар€";
						case 2: return "феврал€";
						case 3: return "марта";
						case 4: return "апрел€";
						case 5: return "ма€";
						case 6: return "июн€";
						case 7: return "июл€";
						case 8: return "августа";
						case 9: return "сент€бр€";
						case 10: return "окт€бр€";
						case 11: return "но€бр€";
						case 12: return "декабр€";
						default: throw new NotSupportedException(datetime.Month.ToString());
					}
				case Cases.Praepositionalis:
					switch (datetime.Month)
					{
						case 1: return "€нваре";
						case 2: return "феврале";
						case 3: return "марте";
						case 4: return "апреле";
						case 5: return "мае";
						case 6: return "июне";
						case 7: return "июле";
						case 8: return "августе";
						case 9: return "сент€бре";
						case 10: return "окт€бре";
						case 11: return "но€бре";
						case 12: return "декабре";
						default: throw new NotSupportedException(datetime.Month.ToString());
					}
			}
			return null;
		}

		public enum Cases
		{
			/// <summary>
			/// »менительный.
			/// </summary>
			Nominativus,
			/// <summary>
			/// –одительный.
			/// </summary>
			Genitivus,
			/// <summary>
			/// ѕредложный.
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