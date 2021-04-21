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
				this.PassportIssuer = Randomizer.GetOneOf("УВД", "ОВД") + " " + Randomizer.GetOneOf("Московского", "Вахитовского", "Советского") + " района гор. " + this.City;
				this.Phone = GetPhone10();
				this.House = Randomizer.GetNumberBetween(1, 60).ToString();
				this.Flat = Randomizer.GetNumberBetween(1, 120).ToString();
				this.ShortAddress = string.Format("ул. {0} {1}-{2}", this.Street, this.House, this.Flat);
				this.Address = string.Format("г. {0}, {1}", this.City, this.ShortAddress);
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
				sb.AppendFormat("ФИО:\t{0}", this.FIO).AppendLine();
				sb.AppendFormat("Пол:\t{0}", this.IsMale ? "мужской" : "женский").AppendLine();
				sb.AppendFormat("Дата рождения:\t{0:dd.MM.yyyy}", this.Birthdate).AppendLine();
				sb.AppendFormat("Город:\t{0}", this.City).AppendLine();
				sb.AppendFormat("ИНН:\t{0}", this.Inn).AppendLine();
				sb.AppendFormat("Адрес:\t{0}", this.Address).AppendLine();
				sb.AppendFormat("Паспорт:\t{0} {1} выдан {2} {3:dd.MM.yyyy} код {4}", this.PassportSeries, this.PassportNumber, this.PassportIssuer, this.PassportDate, this.PassportCode);
				return sb.ToString();
			}

			public override string ToString()
			{
				return this.GetInfo();
			}
		}

		public class Male : Person
		{
			private const string LastNamesConst = "Иванов,Васильев,Петров,Смирнов,Михайлов,Фёдоров,Соколов,Яковлев,Попов,Андреев,Алексеев,Александров,Лебедев,Григорьев,Степанов,Семёнов,Павлов,Богданов,Николаев,Дмитриев,Егоров,Волков,Кузнецов,Никитин,Соловьёв,Тимофеев,Орлов,Афанасьев,Филиппов,Сергеев,Захаров,Матвеев,Виноградов,Кузьмин,Максимов,Козлов,Ильин,Герасимов,Марков,Новиков,Морозов,Романов";
			private const string FirstNamesConst = "Александр,Дмитрий,Максим,Сергей,Андрей,Алексей,Артём,Илья,Кирилл,Михаил,Никита,Матвей,Роман,Егор,Арсений,Иван,Денис,Евгений,Даниил,Тимофей,Владислав,Игорь,Владимир,Павел,Руслан,Марк,Константин,Тимур,Олег,Ярослав,Антон,Николай,Глеб,Данил,Савелий,Вадим,Степан,Юрий,Богдан,Артур,Семен,Макар,Лев,Виктор,Елисей,Виталий,Вячеслав,Захар,Мирон,Дамир";
			private const string MiddleNamesConst = "Александрович,Анатольевич,Аркадьевич,Алексеевич,Андреевич,Артемович,Альбертович,Антонович,Борисович,Вадимович,Васильевич,Владимирович,Валентинович,Вячеславович,Валерьевич,Викторович,Георгиевич,Геннадьевич,Григорьевич,Давидович,Денисович,Данилович,Дмитриевич,Евгеньевич,Егорович,Иванович,Ильич,Игоревич,Кириллович,Константинович,Макарович,Миронович,Максимович,Матвеевич,Михайлович,Наумович,Николаевич,Олегович,Павлович,Петрович,Платонович,Робертович,Романович,Рубенович,Русланович,Сергеевич,Степанович,Семенович,Тарасович,Тимофеевич,Тимурович,Федорович,Филиппович,Эдуардович,Эмануилович,Эльдарович,Юрьевич,Яковлевич,Ярославович";

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
			private const string LastNamesConst = "Ковалёва,Ильина,Гусева,Титова,Кузьмина,Кудрявцева,Баранова,Куликова,Алексеева,Степанова,Яковалева,Сорокина,Сергеева,Романова,Захарова,Борисова,Королева,Герасимова,Пономарева,Григорьева,Лазарева,Медведева,Ершова,Никитина,Соболева,Рябова,Полякова,Цветкова,Данилова,Жукова,Фролова,Журавлева,Николаева,Путина,Молчанова,Крылова,Максимова,Сидорова,Осипова,Белоусова,Федотова,Дорофеева,Егорова,Панина,Матвеева,Боброва,Дмитриева,Калинина,Анисимова,Петухова,Пугачева,Антонова,Тимофеева,Никифорова,Веселова,Филиппова,Романова,Маркова,Большакова,Суханова,Миронова,Александрова,Коновалова,Шестакова,Казакова,Ефимова,Денисова,Громова,Фомина,Андреева,Давыдова,Мельникова,Щербакова,Блинова,Колесникова,Иванова,Смирнова,Кузнецова,Попова,Соколова,Лебедева,Козлова,Новикова,Морозова,Петрова,Волкова,Соловаьева,Васильева,Зайцева,Павлова,Семенова,Голубева,Виноградова,Богданова,Воробьева,Федорова,Михайлова,Беляева,Тарасова,Белова,Комарова,Орлова,Киселева,Макарова,Андреева";
			private const string FirstNamesConst = "Аврора,Агата,Агнесса,Александра,Алена,Алина,Алиса,Алла,Анастасия,Ангелина,Анжела,Анна,Антонина,Анфиса,Астра,Белла,Берта,Богдана,Борислава,Бронислава,Валентина,Валерия,Ванда,Варвара,Венера,Вера,Вероника,Виктория,Виолетта,Виталина,Галина,Генриетта,Гертруда,Глафира,Глория,Дана,Дарья,Джульетта,Диана,Дарина,Ева,Евгения,Екатерина,Елена,Елизавета,Жанна,Жозефина,Зарина,Зинаида,Злата,Зоя,Инга,Ирма,Ирина,Инесса,Инна,Иванна,Изабелла,Изольда,Илона,Каролина,Карина,Кира,Клавдия,Клара,Кристина,Ксения, Оксана,Лада,Лайма,Лариса,Леся,Лиза,Лиана,Лидия,Лилия,Любовь,Людмила,Майя,Маргарита,Марина,Мария,Марта,Милена,Надежда,Нана,Наталья,Нелли,Ника,Нила,Нина,Нора,Олеся,Ольга,Оксана,Пелагея,Полина,Прасковья,Рада,Раиса,Регина,Рената,Римма,Роксана,Роза,Руслана,Саломея,Сабина,Сара,Снежана,Станислава,Светлана,Серафима,София,Стелла,Сусанна,Таисия,Тамара,Татьяна,Тереза,Ульяна,Фаина,Фекла,Фрида,Христина,Элеонора,Элла,Эльвира,Эльмира,Эльза,Эмилия,Эрика,Эвелин,Юлия,Юлианна,Юнона,Ядвига,Яна,Ярослава";
			private const string MiddleNamesConst = "Александровна,Андреевна,Архиповна,Алексеевна,Антоновна,Аскольдовна,Альбертовна,Аркадьевна,Афанасьевна,Анатольевна,Артемовна,Богдановна,Болеславовна,Борисовна,Вадимовна,Васильевна,Владимировна,Валентиновна,Вениаминовна,Владиславовна,Валериевна,Викторовна,Вячеславовна,Геннадиевна,Георгиевна,Геннадьевна,Григорьевна,Даниловна,Дмитриевна,Евгеньевна,Егоровны,Егоровна,Ефимовна,Ждановна,Захаровна,Ивановна,Игоревна,Ильинична,Кирилловна,Кузминична,Константиновна,Кузьминична,Леонидовна,Леоновна,Львовна,Макаровна,Матвеевна,Михайловна,Максимовна,Мироновна,Натановна,Никифоровна,Ниловна,Наумовна,Николаевна,Олеговна,Оскаровна,Павловна,Петровна,Робертовна,Рубеновна,Руслановна,Романовна,Рудольфовна,Святославовна,Сергеевна,Степановна,Семеновна,Станиславовна,Тарасовна,Тимофеевна,Тимуровна,Федоровна,Феликсовна,Филипповна,Харитоновна,Эдуардовна,Эльдаровна,Юльевна,Юрьевна,Яковлевна";

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