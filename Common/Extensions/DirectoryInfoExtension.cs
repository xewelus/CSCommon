using System.IO;
using System.Linq;
using System.Security.AccessControl;

namespace Common.Extensions
{
	public static class DirectoryInfoExtension
	{
		/// <summary>
		/// Копирование папки.
		/// </summary>
		public static void CopyTo(this DirectoryInfo srcDir, string destDirPath, bool innerCopy = false, bool clearDest = false)
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
				string path = Path.Combine(destDirPath, dir.Name);
				if (!Directory.Exists(path))
				{
					Directory.CreateDirectory(path);
				}
				CopyTo(dir, path);
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
					file.SetAccessControl(new FileSecurity());
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
	}
}
