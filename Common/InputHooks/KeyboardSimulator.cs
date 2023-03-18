using System.Runtime.InteropServices;

namespace Common.InputHooks
{
	/// <summary>
	/// Simulate keyboard key presses
	/// https://github.com/GerhardSchreurs/MouseKeyboardLibrary
	/// </summary>
	public static class KeyboardSimulator
	{
		#region Windows API Code

		const int KEYEVENTF_EXTENDEDKEY = 0x1;
		const int KEYEVENTF_KEYUP = 0x2;

		[DllImport("user32.dll")]
		static extern void keybd_event(byte key, byte scan, int flags, int extraInfo);

		#endregion

		#region Methods

		public static void KeyDown(Keys key)
		{
			keybd_event(ParseKey(key), 0, 0, 0);
		}

		public static void KeyUp(Keys key)
		{
			keybd_event(ParseKey(key), 0, KEYEVENTF_KEYUP, 0);
		}

		public static void KeyPress(Keys key)
		{
			KeyDown(key);
			KeyUp(key);
		}

		private static byte ParseKey(Keys key)
		{
			//todo: combinations
			if (key.Alt) return 18;
			if (key.Control) return 17;
			if (key.Shift) return 16;

			return key.KeyCode;
		}

		#endregion

		public class Keys
		{
			public bool Alt;
			public bool Control;
			public bool Shift;
			public byte KeyCode;
		}
	}
}
