using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Threading;

namespace Common.Classes
{
	public class SingleGlobalInstance : IDisposable
	{
		public readonly bool HasHandle;
		public readonly bool Abandoned;
		private readonly Mutex mutex;

		public SingleGlobalInstance(int? timeOut = 200, bool needError = false)
		{
			string mutexId = GetMutexId();
			this.mutex = new Mutex(false, mutexId);

			SecurityIdentifier securityIdentifier = new SecurityIdentifier(WellKnownSidType.WorldSid, null);
			MutexAccessRule allowEveryoneRule = new MutexAccessRule(securityIdentifier, MutexRights.FullControl, AccessControlType.Allow);
			MutexSecurity securitySettings = new MutexSecurity();
			securitySettings.AddAccessRule(allowEveryoneRule);
			this.mutex.SetAccessControl(securitySettings);

			try
			{
				if (timeOut == null)
				{
					this.HasHandle = this.mutex.WaitOne(Timeout.Infinite, false);
				}
				else
				{
					this.HasHandle = this.mutex.WaitOne(timeOut.Value, false);
				}

				if (!this.HasHandle && needError)
				{
					throw new TimeoutException("Timeout waiting for exclusive access.");
				}
			}
			catch (AbandonedMutexException)
			{
				this.Abandoned = true;
				this.HasHandle = true;
			}
		}

		private static string GetMutexId()
		{
			Assembly assembly = Assembly.GetExecutingAssembly();
			object[] customAttributes = assembly.GetCustomAttributes(typeof(GuidAttribute), false);
			GuidAttribute guidAttribute = (GuidAttribute)customAttributes.GetValue(0);
			string mutexId = string.Format("Global\\{{{0}}}", guidAttribute.Value);
			return mutexId;
		}

		public void Dispose()
		{
			if (this.mutex != null)
			{
				if (this.HasHandle) this.mutex.ReleaseMutex();
				this.mutex.Close();
			}
		}
	}
}