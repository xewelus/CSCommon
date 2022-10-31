using CommonWpf.Classes.UI;

namespace CommonWpf.Classes
{
	public class AppInitializer
	{
		/// <summary>
		/// Main initilizatioin method.
		/// </summary>
		public static void Initialize()
		{
			UIHelper.InRuntime = true;
			ExceptionHandler.Init();
		}
	}
}
