namespace Common.InputHooks
{
	public class KeyHandlerEventArgs
	{
		public readonly int KeyCode;
		public readonly bool Control;
		public readonly bool Shift;
		public readonly bool Alt;
		public readonly bool CapsLock;
		public bool Handled;
		public bool SuppressKeyPress;

		public KeyHandlerEventArgs(int keyCode, bool control, bool shift, bool alt, bool capslock)
		{
			this.KeyCode = keyCode;
			this.Control = control;
			this.Shift = shift;
			this.Alt = alt;
			this.CapsLock = capslock;
		}
	}

	public delegate void KeyHandlerDelegate(KeyboardHook sender, KeyHandlerEventArgs args);
}