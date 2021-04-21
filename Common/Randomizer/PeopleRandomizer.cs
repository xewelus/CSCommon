using System;
using System.Text;
using Common.Unsorted;

namespace Common.Randomizer
{
	public class PeopleRandomizer
	{
		public static string GetFIO()
		{
			Person person = Person.GetPerson(true);
			return person.FIO;
		}

		public static Person GetPerson(bool onlyFio = false)
		{
			return Person.GetPerson(onlyFio);
		}

		public abstract class Person
		{
			public string LastName;
			public string FirstName;
			public string MiddleName;
			public bool IsMale;

			public string FIO
			{
				get
				{
					return string.Format("{0} {1} {2}", this.LastName, this.FirstName, this.MiddleName);
				}
			}
			public DateTime Birthdate;
			public string PassportNumber;
			public string PassportSeries;
			public DateTime PassportDate;
			public string PassportIssuer;
			public string PassportCode;
			public string Inn;
			public string City;
			public string Phone;
			public string Address;
			public string ShortAddress;
			public string Street;
			public string House;
			public string Flat;
			public string PostalCode;
			public string Email;

			public static Person GetPerson(bool onlyFio = false)
			{
				if (Randomizer.GetOneOf(true, false))
				{
					return Male.GetRandom(onlyFio);
				}
				else
				{
					return Female.GetRandom(onlyFio);
				}
			}

			protected void FillRandom(bool onlyFio)
			{
				this.LastName = this.GetRandomLastName();
				this.FirstName = this.GetRandomFirstName();
				this.MiddleName = this.GetRandomMiddleName();

				if (onlyFio)
				{
					return;
				}

				this.Birthdate = GetRandomDate();
				this.PassportSeries = "92 " + Randomizer.GetNumbersSequence(2);
				this.PassportNumber = Randomizer.GetNumbersSequence(6);
				this.PassportCode = Randomizer.GetNumbersSequence(3) + "-" + Randomizer.GetNumbersSequence(3);
				this.PassportDate = Randomizer.GetDateBetween(DateTime.Today.AddYears(-1), DateTime.Today.AddDays(-30)).Date;
				this.PassportIssuer = Randomizer.GetOneOf("���", "���") + " " + Randomizer.GetOneOf("�����������", "������������", "����������") + " ������ ���. " + this.City;
				this.Phone = GetPhone10();
				this.House = Randomizer.GetNumberBetween(1, 60).ToString();
				this.Flat = Randomizer.GetNumberBetween(1, 120).ToString();
				this.ShortAddress = string.Format("��. {0} {1}-{2}", this.Street, this.House, this.Flat);
				this.Address = string.Format("�. {0}, {1}", this.City, this.ShortAddress);
				this.PostalCode = "420" + Randomizer.GetNumbersSequence(3);
				this.Email = Translit.Convert(this.FirstName).ToLower()
				             + Randomizer.GetOneOf("", ".", "_")
				             + Translit.Convert(this.LastName).ToLower()
							 + Randomizer.GetOneOf(
								string.Empty,
								string.Empty,
								this.Birthdate.Year.ToString(),
								this.Birthdate.Year.ToString("00"),
								Randomizer.GetNumberBetween(1, 99).ToString())
				             + "@" + Randomizer.GetOneOf("gmail.com", "mail.ru", "ya.ru", "hotmail.com");
			}

			public static string GetRandomName(string[] array)
			{
				ChanceRandomizer<string> chances = new ChanceRandomizer<string>();
				for (int i = 0; i < array.Length; i++)
				{
					chances.Add(i + array.Length, array[i]);
				}
				return chances.Get();
			}

			public string GetRandomLastName()
			{
				return GetRandomName(this.GetLastNames());
			}
			public string GetRandomFirstName()
			{
				return GetRandomName(this.GetFirstNames());
			}
			public string GetRandomMiddleName()
			{
				return GetRandomName(this.GetMiddleNames());
			}
			public string GetRandomFIO()
			{
				return string.Format("{0} {1} {2}", this.GetRandomLastName(), this.GetRandomFirstName(), this.GetRandomMiddleName());
			}
			public static DateTime GetRandomDate(int minYears = 18, int maxYears = 70)
			{
				int days = Randomizer.GetNumberBetween(minYears * 365, maxYears * 365);
				return DateTime.Now.AddDays(-days).Date;
			}

			public static string GetPhone10()
			{
				return Randomizer.GetNumberBetween(900, 999).ToString() + Randomizer.GetNumbersSequence(7);
			}

			protected abstract string[] GetLastNames();
			protected abstract string[] GetFirstNames();
			protected abstract string[] GetMiddleNames();

			public string GetInfo()
			{
				StringBuilder sb = new StringBuilder();
				sb.AppendFormat("���:\t{0}", this.FIO).AppendLine();
				sb.AppendFormat("���:\t{0}", this.IsMale ? "�������" : "�������").AppendLine();
				sb.AppendFormat("���� ��������:\t{0:dd.MM.yyyy}", this.Birthdate).AppendLine();
				sb.AppendFormat("�����:\t{0}", this.City).AppendLine();
				sb.AppendFormat("���:\t{0}", this.Inn).AppendLine();
				sb.AppendFormat("�����:\t{0}", this.Address).AppendLine();
				sb.AppendFormat("�������:\t{0} {1} ����� {2} {3:dd.MM.yyyy} ��� {4}", this.PassportSeries, this.PassportNumber, this.PassportIssuer, this.PassportDate, this.PassportCode);
				return sb.ToString();
			}

			public override string ToString()
			{
				return this.GetInfo();
			}
		}

		public class Male : Person
		{
			private const string LastNamesConst = "������,��������,������,�������,��������,Ը�����,�������,�������,�����,�������,��������,�����������,�������,���������,��������,������,������,��������,��������,��������,������,������,��������,�������,��������,��������,�����,���������,��������,�������,�������,�������,����������,�������,��������,������,�����,���������,������,�������,�������,�������";
			private const string FirstNamesConst = "���������,�������,������,������,������,�������,����,����,������,������,������,������,�����,����,�������,����,�����,�������,������,�������,���������,�����,��������,�����,������,����,����������,�����,����,�������,�����,�������,����,�����,�������,�����,������,����,������,�����,�����,�����,���,������,������,�������,��������,�����,�����,�����";
			private const string MiddleNamesConst = "�������������,�����������,����������,����������,���������,���������,�����������,���������,���������,���������,����������,������������,������������,������������,����������,����������,����������,�����������,�����������,���������,���������,���������,����������,����������,��������,��������,�����,��������,����������,��������������,���������,���������,����������,���������,����������,��������,����������,��������,��������,��������,����������,����������,���������,���������,����������,���������,����������,���������,���������,����������,���������,���������,����������,����������,�����������,����������,�������,���������,�����������";

			public readonly static string[] LastNames = LastNamesConst.Split(',');
			public readonly static string[] FirstNames = FirstNamesConst.Split(',');
			public readonly static string[] MiddleNames = MiddleNamesConst.Split(',');

			protected override string[] GetLastNames()
			{
				return LastNames;
			}

			protected override string[] GetFirstNames()
			{
				return FirstNames;
			}

			protected override string[] GetMiddleNames()
			{
				return MiddleNames;
			}

			public static Male GetRandom(bool onlyFio = false)
			{
				Male result = new Male();
				result.IsMale = true;
				result.FillRandom(onlyFio);
				return result;
			}
		}

		public class Female : Person
		{
			private const string LastNamesConst = "�������,������,������,������,��������,����������,��������,��������,���������,���������,���������,��������,��������,��������,��������,��������,��������,����������,����������,����������,��������,���������,������,��������,��������,������,��������,��������,��������,������,�������,���������,���������,������,���������,�������,���������,��������,�������,���������,��������,���������,�������,������,��������,�������,���������,��������,���������,��������,��������,��������,���������,����������,��������,���������,��������,�������,����������,��������,��������,������������,����������,���������,��������,�������,��������,�������,������,��������,��������,����������,���������,�������,�����������,�������,��������,���������,������,��������,��������,�������,��������,��������,�������,�������,����������,���������,�������,�������,��������,��������,�����������,���������,���������,��������,���������,�������,��������,������,��������,������,��������,��������,��������";
			private const string FirstNamesConst = "������,�����,�������,����������,�����,�����,�����,����,���������,��������,������,����,��������,������,�����,�����,�����,�������,���������,����������,���������,�������,�����,�������,������,����,��������,��������,��������,��������,������,���������,��������,�������,������,����,�����,���������,�����,������,���,�������,���������,�����,���������,�����,��������,������,�������,�����,���,����,����,�����,������,����,������,��������,�������,�����,��������,������,����,�������,�����,��������,������, ������,����,�����,������,����,����,�����,�����,�����,������,�������,����,���������,������,�����,�����,������,�������,����,�������,�����,����,����,����,����,�����,�����,������,�������,������,���������,����,�����,������,������,�����,�������,����,�������,�������,������,����,�������,����������,��������,��������,�����,������,�������,������,������,�������,������,������,�����,�����,�����,��������,��������,����,�������,�������,�����,������,�����,������,����,�������,�����,������,���,��������";
			private const string MiddleNamesConst = "�������������,���������,���������,����������,���������,�����������,�����������,����������,�����������,�����������,���������,����������,������������,���������,���������,����������,������������,������������,������������,�������������,����������,����������,������������,�����������,����������,�����������,�����������,���������,����������,����������,��������,��������,��������,��������,���������,��������,��������,���������,����������,����������,��������������,�����������,����������,��������,�������,���������,���������,����������,����������,���������,���������,�����������,�������,��������,����������,��������,���������,��������,��������,����������,���������,����������,���������,�����������,�������������,���������,����������,���������,�������������,���������,����������,���������,���������,����������,����������,�����������,����������,����������,�������,�������,���������";

			public readonly static string[] LastNames = LastNamesConst.Split(',');
			public readonly static string[] FirstNames = FirstNamesConst.Split(',');
			public readonly static string[] MiddleNames = MiddleNamesConst.Split(',');

			protected override string[] GetLastNames()
			{
				return LastNames;
			}

			protected override string[] GetFirstNames()
			{
				return FirstNames;
			}

			protected override string[] GetMiddleNames()
			{
				return MiddleNames;
			}

			public static Female GetRandom(bool onlyFio = false)
			{
				Female result = new Female();
				result.IsMale = false;
				result.FillRandom(onlyFio);
				return result;
			}
		}

		public enum PhoneFormat
		{
			Format_8,
			Format_Plus7
		}
	}
}