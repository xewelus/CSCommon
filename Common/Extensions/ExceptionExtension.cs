using System;

namespace Common.Extensions
{
	public static class ExceptionExtension
	{
		public static string ToNiceString(this Exception exception)
		{
			string text = exception.ToString();

			text = text.Replace(" ---> ", "\r\n\r\n ---> ");
			text = text.Replace("--- End of inner exception stack trace ---", "\r\n--- End of inner exception stack trace ---");

			return text;
		}
	}
}
