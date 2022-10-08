using System;
using System.IO;

namespace Common.Classes.Diagnostic
{
	/// <summary>
	/// Contains helpful methods for problems diagnostics.
	/// </summary>
	public static class Diag
	{
		/// <summary>
		/// Save text in file (usually as error) and open this file in default application.
		/// </summary>
		public static string SaveAndOpenLog(string text, string prefix = "error_", string extension = ".log")
		{
			string path = FS.GetAppPath($"{prefix}{DateTime.Now:yyMMdd_HHmmss_fff}{extension}");

			try
			{
				File.AppendAllText(path, text);
				FS.OpenInDefaultApp(path);
				return path;
			}
			catch (Exception ex)
			{
				throw new Exception($"Error while processing log '{path}'.", ex);
			}
		}
	}
}
