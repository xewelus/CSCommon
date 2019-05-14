using System.Diagnostics;

namespace Common
{
	public static class Misc
	{
		/// <summary>
		/// Возвращает True, если существует процесс запущенный из того же файла, что и текущий.
		/// </summary>
		public static bool IsSameProcessExists()
		{
			Process currentProcess = Process.GetCurrentProcess();
			foreach (Process process in Process.GetProcessesByName(currentProcess.ProcessName))
			{
				if (process.Id != currentProcess.Id
				    && process.MainModule != null
				    && currentProcess.MainModule != null
				    && process.MainModule.FileName == currentProcess.MainModule.FileName)
				{
					return true;
				}
			}
			return false;
		}
	}
}
