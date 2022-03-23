using System;

namespace CommonWinForms
{
	public static class ExcHandler
	{
		public static Action<Exception> OnError;
		public static void Catch(Exception ex)
		{
			OnError?.Invoke(ex);
			UIHelper.ShowError(ex.ToString());
		}
	}
}
