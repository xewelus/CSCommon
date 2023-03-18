using System;
using System.IO;
using System.Linq;
using System.Security.AccessControl;

namespace Common.Extensions
{
	public static class DirectoryInfoExtension
	{
		/// <summary>
		/// Copy folder.
		/// </summary>
		/// <param name="srcDir">Source path.</param>
		/// <param name="destDirPath">Destination path.</param>
		/// <param name="innerCopy">???</param>
		/// <param name="clearDest">True, if need override destination folder.</param>
		/// <param name="exceptDirs">List of ignorable folder names.</param>
		public static void CopyTo(
			this DirectoryInfo srcDir,
			string destDirPath,
			bool innerCopy = false, 
			bool clearDest = false,
			string[] exceptDirs = null)
		{
			if (clearDest)
			{
				DirectoryInfo destDir = new DirectoryInfo(destDirPath);
				if (destDir.Exists)
				{
					destDir.DeleteForce(innerCopy);
				}
			}

			foreach (FileInfo file in srcDir.GetFiles())
			{
				string path = Path.Combine(destDirPath, file.Name);
				file.CopyTo(path, true);
			}

			foreach (DirectoryInfo dir in srcDir.GetDirectories())
			{
				if (exceptDirs != null && exceptDirs.Contains(dir.Name))
				{
					continue;
				}

				string path = Path.Combine(destDirPath, dir.Name);
				if (!Directory.Exists(path))
				{
					Directory.CreateDirectory(path);
				}
				CopyTo(dir, path, exceptDirs: exceptDirs);
			}
		}

		/// <summary>
		/// Рекурсивное удаление папки даже с защищенными от удаления файлами.
		/// </summary>
		public static bool DeleteForce(this DirectoryInfo info, bool innerDelete = false, string[] exceptDirs = null, string[] exceptFiles = null)
		{
			bool needDeleteDir = true;
			foreach (FileInfo file in info.GetFiles())
			{
				if (exceptFiles != null && exceptFiles.Contains(file.Name))
				{
					needDeleteDir = false;
				}
				else
				{
					File.SetAttributes(file.FullName, FileAttributes.Normal);
					if (OperatingSystem.IsWindows())
					{
						file.SetAccessControl(new FileSecurity());
					}
					file.Delete();
				}
			}

			foreach (DirectoryInfo dir in info.GetDirectories())
			{
				if (exceptDirs != null && exceptDirs.Contains(dir.Name))
				{
					needDeleteDir = false;
				}
				else
				{
					if (!dir.DeleteForce(false, exceptDirs, exceptFiles))
					{
						needDeleteDir = false;
					}
				}
			}

			if (!innerDelete && needDeleteDir)
			{
				info.Delete();
				return true;
			}

			return false;
		}

		public static void Recursive(this DirectoryInfo info, Action<(string path, bool isFile)> action)
		{
			foreach (DirectoryInfo dir in info.GetDirectories())
			{
				try
				{
					action((dir.FullName, false));
					dir.Recursive(action);
				}
				catch (Exception ex)
				{
					throw new Exception($"Error while processing folder '{dir.FullName}'.", ex);
				}
			}

			foreach (FileInfo file in info.GetFiles())
			{
				try
				{
					action((file.FullName, true));
				}
				catch (Exception ex)
				{
					throw new Exception($"Error while processing file '{file.FullName}'.", ex);
				}
			}
		}

		public static void RecursiveProcessFiles(this DirectoryInfo info, Action<string> action)
		{
			Recursive(
				info: info,
				action: t =>
				        {
					        if (t.isFile)
					        {
						        action.Invoke(t.path);
					        }
				        });
		}
	}
}
