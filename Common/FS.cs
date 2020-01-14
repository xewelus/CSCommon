using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Common
{
	public static class FS
	{
		public static string GetRelativePath(string fullPath, string basePath)
		{
			fullPath = Path.GetFullPath(fullPath);
			basePath = Path.GetFullPath(basePath);

			const char DIVIDER = '\\';
			string[] fullPathParts = fullPath.Trim(DIVIDER).Split(DIVIDER);
			string[] basePathParts = basePath.Trim(DIVIDER).Split(DIVIDER);

			bool sameRoot = true;
			int diffIndex = basePathParts.Length;
			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < basePathParts.Length; i++)
			{
				if (sameRoot)
				{
					string fullPart = fullPathParts[i];
					string basePart = basePathParts[i];

					if (!string.Equals(basePart, fullPart, StringComparison.InvariantCultureIgnoreCase))
					{
						diffIndex = i;
						sameRoot = false;
					}
				}

				if (!sameRoot)
				{
					if (sb.Length > 0)
					{
						sb.Append(DIVIDER);
					}
					sb.Append("..");
				}
			}

			for (int i = diffIndex; i < fullPathParts.Length; i++)
			{
				string fullPart = fullPathParts[i];
				if (sb.Length > 0)
				{
					sb.Append(DIVIDER);
				}
				sb.Append(fullPart);
			}

			return sb.ToString();
		}

		public static bool IsAnotherDrive(string path1, string path2)
		{
			if (Path.IsPathRooted(path1) && Path.IsPathRooted(path2))
			{
				return path1[0] != path2[0];
			}
			return false;
		}

		public static string Combine(params string[] paths)
		{
			if (paths.Length == 0) return string.Empty;

			string result = paths[0];
			for (int i = 1; i < paths.Length; i++)
			{
				result = Path.Combine(result, paths[i]);
			}
			return result;
		}

		private static string appFolder;
		public static string AppFolder
		{
			get
			{
				if (appFolder == null)
				{
					appFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
				}
				return appFolder;
			}
		}

		public static string GetAppFolder(string path, bool trim = false)
		{
			if (!Path.IsPathRooted(path))
			{
				if (string.IsNullOrEmpty(path))
				{
					path = AppFolder;
				}
				else
				{
					if (trim)
					{
						path = path.Trim();
					}

					// если путь задан как отностильный, то помещаем файл в папку сервиса
					path = Path.Combine(AppFolder, path);
				}
			}
			return Path.GetFullPath(path);
		}
	}
}
