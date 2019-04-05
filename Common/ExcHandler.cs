using System;

namespace Common
{
	public static class ExcHandler
	{
		public static void Catch(Exception ex)
		{
			UIHelper.ShowError(ex.ToString());
		}
	}
}
