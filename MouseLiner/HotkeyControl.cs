using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace MouseLiner {
	public partial class HotkeyControl: UserControl {
		#region Win32API
		[DllImport("user32.dll", SetLastError = true)]
		static extern bool RegisterHotKey(IntPtr handle, int atom, int modifiers, Keys key);
		[DllImport("user32.dll", SetLastError = true)]
		static extern bool UnregisterHotKey(IntPtr handle, int atom);
		[DllImport("kernel32")]
		static extern int GlobalAddAtom(string atomstring);
		[DllImport("kernel32")]
		static extern int GlobalDeleteAtom(int atom);

		const int WM_HOTKEY = 0x0312;

		const int MOD_ALT = 1;
		const int MOD_CONTROL = 2;
		const int MOD_SHIFT = 4;
		const int MOD_WIN = 8;
		#endregion

		public HotkeyControl() {
			InitializeComponent();
			//
			HotkeyPressed = new GlobalHotkeyPressedHandler(OnHotkeyPressed);
		}

		int atom = -1;

		public void RegisterHotkey(bool shift, bool ctrl, bool alt, bool win, Keys key) {
			if(atom != -1) {
				UnregisterHotkey();
			}
			int mod = 0;
			if(shift)
				mod |= MOD_SHIFT;
			if(ctrl)
				mod |= MOD_CONTROL;
			if(alt)
				mod |= MOD_ALT;
			if(win)
				mod |= MOD_WIN;
			atom = GlobalAddAtom("GlobalHotkeyFor" + this.Handle.ToString());
			if(!RegisterHotKey(this.Handle, atom, mod, key)) {
				throw new Win32Exception(Marshal.GetLastWin32Error());
			}
		}

		public void UnregisterHotkey() {
			UnregisterHotKey(this.Handle, atom);
			if(GlobalDeleteAtom(atom) != 0) {
				throw new Win32Exception("GlobalDeleteAtom() return non zero value.");
			}
		}

		public delegate void GlobalHotkeyPressedHandler(object sender, EventArgs e);

		/// <summary>
		/// ホットキーが押されたときに呼ばれます
		/// </summary>
		public event GlobalHotkeyPressedHandler HotkeyPressed;

		protected void OnHotkeyPressed(object sender, EventArgs e) {
		}

		protected override void WndProc(ref Message m) {
			if(m.Msg == WM_HOTKEY) {
				HotkeyPressed(this, new EventArgs());
			}
			base.WndProc(ref m);
		}
	}
}
