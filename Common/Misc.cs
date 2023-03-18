using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Common
{
	public static class Misc
	{
		///// <summary>
		///// Возвращает True, если существует процесс запущенный из того же файла, что и текущий.
		///// </summary>
		//public static bool IsSameProcessExists()
		//{
		//	List<Process> processes = GetSameProcesses();
		//	return processes.Count > 0;
		//}

		//[Obsolete("Too slow")]
		//public static List<Process> GetSameProcesses()
		//{
		//	List<Process> result = new List<Process>();
		//	Process currentProcess = Process.GetCurrentProcess();
		//	foreach (Process process in Process.GetProcessesByName(currentProcess.ProcessName))
		//	{
		//		if (process.Id != currentProcess.Id
		//		    && process.MainModule != null
		//		    && currentProcess.MainModule != null
		//		    && process.MainModule.FileName == currentProcess.MainModule.FileName)
		//		{
		//			result.Add(process);
		//		}
		//	}
		//	return result;
		//}
	}
}
