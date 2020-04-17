using System;

namespace CommonWinForms
{
	public static class ExcHandler
	{
		public static void Catch(Exception ex)
		{
			UIHelper.ShowError(ex.ToString());
		}
	}
}
