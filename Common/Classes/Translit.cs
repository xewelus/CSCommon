using System.Collections.Generic;
using System.Text;

namespace Common.Classes
{
	public class Translit
	{
		private static readonly Dictionary<char, string> table = new Dictionary<char, string>(); // ������� WU

		public static string Convert(string text)
		{
			if (string.IsNullOrEmpty(text)) return text;

			StringBuilder sb = new StringBuilder();

			for (int i = 0; i < text.Length; i++)
			{
				char c = text[i];
				string s;
				if (table.TryGetValue(char.ToLower(c), out s))
				{
					if (string.IsNullOrEmpty(s)) continue;

					if (char.IsUpper(c))
					{
						if (s.Length == 1 
							|| i == text.Length - 1
							|| char.IsUpper(text[i + 1]))
						{
							s = s.ToUpper();
						}
						else
						{
							s = char.ToUpper(s[0]) + s.Substring(1);
						}
					}

					sb.Append(s);
				}
				else
				{
					sb.Append(c);
				}
			}
			return sb.ToString();
		}

	    static Translit()
		{
			table.Add('�', "#");
			table.Add('�', "a");
			table.Add('�', "b");
			table.Add('�', "v");
			table.Add('�', "g");
			table.Add('�', "d");
			table.Add('�', "e");
			table.Add('�', "yo");
			table.Add('�', "zh");
			table.Add('�', "z");
			table.Add('�', "i");
			table.Add('�', "y");
			table.Add('�', "k");
			table.Add('�', "l");
			table.Add('�', "m");
			table.Add('�', "n");
			table.Add('�', "o");
			table.Add('�', "p");
			table.Add('�', "r");
			table.Add('�', "s");
			table.Add('�', "t");
			table.Add('�', "u");
			table.Add('�', "f");
			table.Add('�', "kh");
			table.Add('�', "ts");
			table.Add('�', "ch");
			table.Add('�', "sh");
			table.Add('�', "shh");
			table.Add('�', "");
			table.Add('�', "y");
			table.Add('�', "");
			table.Add('�', "e");
			table.Add('�', "yu");
			table.Add('�', "ya");
		}
	}
}