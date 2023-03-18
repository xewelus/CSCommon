using System.Collections.Generic;
using System.Text;

namespace Common.Classes
{
	public class Translit
	{
		private static readonly Dictionary<char, string> table = new Dictionary<char, string>(); // правила WU

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
			table.Add('№', "#");
			table.Add('а', "a");
			table.Add('б', "b");
			table.Add('в', "v");
			table.Add('г', "g");
			table.Add('д', "d");
			table.Add('е', "e");
			table.Add('ё', "yo");
			table.Add('ж', "zh");
			table.Add('з', "z");
			table.Add('и', "i");
			table.Add('й', "y");
			table.Add('к', "k");
			table.Add('л', "l");
			table.Add('м', "m");
			table.Add('н', "n");
			table.Add('о', "o");
			table.Add('п', "p");
			table.Add('р', "r");
			table.Add('с', "s");
			table.Add('т', "t");
			table.Add('у', "u");
			table.Add('ф', "f");
			table.Add('х', "kh");
			table.Add('ц', "ts");
			table.Add('ч', "ch");
			table.Add('ш', "sh");
			table.Add('щ', "shh");
			table.Add('ъ', "");
			table.Add('ы', "y");
			table.Add('ь', "");
			table.Add('э', "e");
			table.Add('ю', "yu");
			table.Add('я', "ya");
		}
	}
}