namespace Common.InputHooks
{
	public class KeyPressArgs
	{
		public char Key;
		public bool Handled;

		public KeyPressArgs(char key)
		{
			this.Key = key;
		}
	}
	public delegate void KeyPressDelegate(KeyboardHook sender, KeyPressArgs args);
}