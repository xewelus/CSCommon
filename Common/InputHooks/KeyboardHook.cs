using System;
using System.Runtime.InteropServices;

namespace Common.InputHooks
{
	/// <summary>
	/// Captures global keyboard events
	/// https://github.com/GerhardSchreurs/MouseKeyboardLibrary
	/// </summary>
	public class KeyboardHook : GlobalHook
	{
		private static KeyboardHook current;

		public static KeyboardHook Current
		{
			get
			{
				if (current == null) throw new NullReferenceException(nameof(current));
				return current;
			}
			set
			{
				current = value;
			}
		}

		public event KeyHandlerDelegate KeyDown;
		public event KeyHandlerDelegate KeyUp;
		public event KeyPressDelegate KeyPress;

		public KeyboardHook(ExitFunc exitFunc) : base(exitFunc)
		{
			this._hookType = WH_KEYBOARD_LL;
		}

		protected override int HookCallbackProcedure(int nCode, int wParam, IntPtr lParam)
		{
			bool handled = false;

			if (nCode > -1 && (this.KeyDown != null || this.KeyUp != null || this.KeyPress != null))
			{
				KeyboardHookStruct keyboardHookStruct = (KeyboardHookStruct)Marshal.PtrToStructure(lParam, typeof(KeyboardHookStruct));

				// Is Control being held down?
				bool control = ((GetKeyState(VK_LCONTROL) & 0x80) != 0) || ((GetKeyState(VK_RCONTROL) & 0x80) != 0);

				// Is Shift being held down?
				bool shift = ((GetKeyState(VK_LSHIFT) & 0x80) != 0) || ((GetKeyState(VK_RSHIFT) & 0x80) != 0);

				// Is Alt being held down?
				bool alt = ((GetKeyState(VK_LALT) & 0x80) != 0) || ((GetKeyState(VK_RALT) & 0x80) != 0);

				// Is CapsLock on?
				bool capslock = GetKeyState(VK_CAPITAL) != 0;

				// Create event using keycode and control/shift/alt values found above
				KeyHandlerEventArgs e = new KeyHandlerEventArgs(keyboardHookStruct.vkCode, control, shift, alt, capslock);

				// Handle KeyDown and KeyUp events
				switch (wParam)
				{

					case WM_KEYDOWN:
					case WM_SYSKEYDOWN:
						if (this.KeyDown != null)
						{
							this.KeyDown(this, e);
							handled = e.Handled;
						}
						break;
					case WM_KEYUP:
					case WM_SYSKEYUP:
						if (this.KeyUp != null)
						{
							this.KeyUp(this, e);
							handled = e.Handled;
						}
						break;
				}

				// Handle KeyPress event
				if (wParam == WM_KEYDOWN &&
				   !handled &&
				   !e.SuppressKeyPress && this.KeyPress != null)
				{

					byte[] keyState = new byte[256];
					byte[] inBuffer = new byte[2];
					GetKeyboardState(keyState);

					if (ToAscii(keyboardHookStruct.vkCode,
							  keyboardHookStruct.scanCode,
							  keyState,
							  inBuffer,
							  keyboardHookStruct.flags) == 1)
					{

						char key = (char)inBuffer[0];
						if ((capslock ^ shift) && char.IsLetter(key))
						{
							key = char.ToUpper(key);
						}

						KeyPressArgs e2 = new KeyPressArgs(key);
						this.KeyPress(this, e2);
						handled = e.Handled;

					}
				}
			}

			if (handled)
			{
				return 1;
			}
			return CallNextHookEx(this._handleToHook, nCode, wParam, lParam);
		}
	}
}
