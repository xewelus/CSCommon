using System;
using System.Collections.Generic;
using System.Diagnostics;
using static System.Net.Mime.MediaTypeNames;
using System.Threading;

namespace Common
{
	public static class Misc
	{
		/// <summary>
		/// Возвращает True, если существует процесс запущенный из того же файла, что и текущий.
		/// </summary>
		public static bool IsSameProcessExists()
		{
			List<Process> processes = GetSameProcesses();
			return processes.Count > 0;
		}

		public static List<Process> GetSameProcesses()
		{
			List<Process> result = new List<Process>();
			Process currentProcess = Process.GetCurrentProcess();
			foreach (Process process in Process.GetProcessesByName(currentProcess.ProcessName))
			{
				if (process.Id != currentProcess.Id
				    && process.MainModule != null
				    && currentProcess.MainModule != null
				    && process.MainModule.FileName == currentProcess.MainModule.FileName)
				{
					result.Add(process);
				}
			}
			return result;
		}

		public static bool IsAlreadyStarted(string appGuid)
		{
			using (Mutex mutex = new Mutex(false, "Global\\" + appGuid))
			{
				return !mutex.WaitOne(0, false);
			}
		}

		private static string appGuid = "c0a76b5a-12ab-45c5-b9d9-d693faa6e7b9";
	}
}
