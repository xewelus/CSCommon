using System;
using System.Collections.Generic;
using System.Diagnostics;
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

		private static string projectFolder;
		public static string ProjectFolder
		{
			get
			{
				if (projectFolder == null)
				{
					string folder = AppFolder;
					if (Debugger.IsAttached)
					{
						while (true)
						{
							if (Directory.GetFiles(folder, "*.csproj").Any())
							{
								break;
							}
							string parent = Path.GetDirectoryName(folder);
							if (folder == parent)
							{
								folder = AppFolder;
								break;
							}

							folder = parent;

							if (string.IsNullOrEmpty(folder))
							{
								folder = AppFolder;
								break;
							}
						}
					}
					projectFolder = folder;
				}
				return projectFolder;
			}
		}

		public static string GetAppPath(params string[] paths)
		{
			return GetPath(AppFolder, paths);
		}

		public static string GetProjectPath(params string[] paths)
		{
			return GetPath(ProjectFolder, paths);
		}

		public static string GetPath(string folder, params string[] paths)
		{
			string path = Path.Combine(paths);
			if (!Path.IsPathRooted(path))
			{
				if (string.IsNullOrEmpty(path))
				{
					path = folder;
				}
				else
				{
					// если путь задан как отностильный, то помещаем файл в папку сервиса
					path = Path.Combine(folder, path);
				}
			}
			return Path.GetFullPath(path);
		}

		/// <summary>
		/// Check folder existing. Creates if need.
		/// </summary>
		public static void EnsureFolder(string folder)
		{
			string folder0 = folder;

			try
			{
				while (true)
				{
					if (Directory.Exists(folder)) return;
					string parent = Path.GetDirectoryName(folder);
					if (parent == folder)
					{
						throw new Exception(folder);
					}
					EnsureFolder(parent);
					Directory.CreateDirectory(folder);
				}
			}
			catch (Exception ex)
			{
				throw new Exception($"Error while processing '{folder0}'.", ex);
			}
		}


		/// <summary>
		/// Check existing folder of file. Creates folder if need.
		/// </summary>
		public static void EnsureFileFolder(string file)
		{
			string folder = Path.GetDirectoryName(file);
			EnsureFolder(folder);
		}

		/// <summary>
		/// Opens file or folder in Windows default app.
		/// </summary>
		public static void OpenInDefaultApp(string path)
		{
			ProcessStartInfo startInfo = new ProcessStartInfo($"\"{path}\"");
			startInfo.Verb = "open";
			Process.Start(startInfo);
		}
	}
}
