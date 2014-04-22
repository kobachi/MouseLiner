using System;
using System.Drawing;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MouseLiner {
	static class Program {
		static NotifyIcon notify;
		static ContextMenu context;
		static MenuItem transparentMenu;
		static MenuItem horizontalLineSubMenu;
		static MenuItem showHorizontalLineMenu;
		static MenuItem chooseHorizontalLineColorMenu;
		static MenuItem splitter2;
		static MenuItem narrowHorizontalLineMenu;
		static MenuItem mediumHorizontalLineMenu;
		static MenuItem thickHorizontalLineMenu;
		static MenuItem verticalLineSubMenu;
		static MenuItem showVerticalLineMenu;
		static MenuItem chooseVerticalLineColorMenu;
		static MenuItem splitter3;
		static MenuItem narrowVerticalLineMenu;
		static MenuItem mediumVerticalLineMenu;
		static MenuItem thickVerticalLineMenu;
		static MenuItem splitter1;
		static MenuItem exitMenu;
		static Timer timer;
		static FormHorizontalLine horizontalLine = null;
		static FormVerticalLine verticalLine = null;
		static HotkeyControl hotkey;

		/*
		static FormSpotLight spotlight;
		 */

		static System.Threading.Mutex mutex;

		[STAThread]
		static void Main() {
			bool newmutex = false;
			mutex = new System.Threading.Mutex(true, "MouseLiner", out newmutex);
			if(newmutex) {
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				//
				InitializeComponent();
				//
				Application.Run();
			}
			mutex.Close();
		}

		static void InitializeComponent() {
			exitMenu = new MenuItem(MouseLiner.Properties.Resources.ExitMenuLabel, new EventHandler(exit));
			splitter1 = new MenuItem("-");
			transparentMenu = new MenuItem(MouseLiner.Properties.Resources.TransparentMenuLabel, new EventHandler(toggleTransparent));
			transparentMenu.Checked = true;
			showHorizontalLineMenu = new MenuItem(MouseLiner.Properties.Resources.ShowHorizontalLineMenuLabel, new EventHandler(toggleShowHideHorizontalLine));
			chooseHorizontalLineColorMenu = new MenuItem(MouseLiner.Properties.Resources.ChooseHorizontalLineColorMenuLabel, new EventHandler(chooseHorizontalLineColor));
			splitter2 = new MenuItem("-");
			narrowHorizontalLineMenu = new MenuItem(MouseLiner.Properties.Resources.NarrowLineWidthMenuLabel, new EventHandler(setNarrowHorizontalLine));
			narrowHorizontalLineMenu.RadioCheck = true;
			mediumHorizontalLineMenu = new MenuItem(MouseLiner.Properties.Resources.MediumLineWidthMenuLabel, new EventHandler(setMediumHorizontalLine));
			mediumHorizontalLineMenu.RadioCheck = true;
			thickHorizontalLineMenu = new MenuItem(MouseLiner.Properties.Resources.ThickLineWidthMenuLabel, new EventHandler(setThickHorizontalLine));
			thickHorizontalLineMenu.RadioCheck = true;
			showVerticalLineMenu = new MenuItem(MouseLiner.Properties.Resources.ShowVerticalLineMenuLabel, new EventHandler(toggleShowHideVerticalLine));
			chooseVerticalLineColorMenu = new MenuItem(MouseLiner.Properties.Resources.ChooseVerticalLineColorMenuLabel, new EventHandler(chooseVerticalLineColor));
			splitter3 = new MenuItem("-");
			narrowVerticalLineMenu = new MenuItem(MouseLiner.Properties.Resources.NarrowLineWidthMenuLabel, new EventHandler(setNarrowVerticalLine));
			narrowVerticalLineMenu.RadioCheck = true;
			mediumVerticalLineMenu = new MenuItem(MouseLiner.Properties.Resources.MediumLineWidthMenuLabel, new EventHandler(setMediumVerticalLine));
			mediumVerticalLineMenu.RadioCheck = true;
			thickVerticalLineMenu = new MenuItem(MouseLiner.Properties.Resources.ThickLineWidthMenuLabel, new EventHandler(setThickVerticalLine));
			thickVerticalLineMenu.RadioCheck = true;
			//
			//
			horizontalLineSubMenu = new MenuItem(MouseLiner.Properties.Resources.HorizontalLineMenuLabel, new MenuItem[]{
				showHorizontalLineMenu,
				chooseHorizontalLineColorMenu,
				splitter2,
				narrowHorizontalLineMenu,
				mediumHorizontalLineMenu,
				thickHorizontalLineMenu
			});
			//
			verticalLineSubMenu = new MenuItem(MouseLiner.Properties.Resources.VerticalLineMenuLabel, new MenuItem[]{
				showVerticalLineMenu,
				chooseVerticalLineColorMenu,
				splitter3,
				narrowVerticalLineMenu,
				mediumVerticalLineMenu,
				thickVerticalLineMenu
			});
			//
			context = new ContextMenu(new MenuItem[]{
				horizontalLineSubMenu,
				verticalLineSubMenu,
				transparentMenu,
				splitter1,
				exitMenu
			});
			context.Popup += new EventHandler(context_Popup);
			//
			notify = new NotifyIcon();
			notify.Icon = global::MouseLiner.Properties.Resources.Enabled;
			notify.ContextMenu = context;
			notify.Visible = true;
			//
			timer = new Timer();
			timer.Interval = 25;
			timer.Tick += new EventHandler(timer_Tick);
			//
			hotkey = new HotkeyControl();
			hotkey.RegisterHotkey(false, false, false, true, Keys.Scroll);
			hotkey.HotkeyPressed += new HotkeyControl.GlobalHotkeyPressedHandler(hotkey_HotkeyPressed);
			hotkey.Show();
			//
			if(MouseLiner.Properties.Settings.Default.ShowHorizontalLine) {
				showHorizontalLine();
			}
			if(MouseLiner.Properties.Settings.Default.ShowVerticalLine) {
				showVerticalLine();
			}
			//
			/*
			spotlight = new FormSpotLight();
			spotlight.Show();
			 */
			//
			timer.Start();
		}

		static bool isHorizontalLineShown() {
			if(horizontalLine == null) {
				return false;
			}
			return horizontalLine.Visible;
		}

		static void showHorizontalLine() {
			if(horizontalLine == null) {
				horizontalLine = new FormHorizontalLine();
				horizontalLine.Show();
			}
			else {
				horizontalLine.Visible = true;
			}
			if(screen != null) {
				horizontalLine.Width = screen.Bounds.Width;
			}
		}

		static void hideHorizontalLine() {
			if(horizontalLine == null) {
				return;
			}
			horizontalLine.Visible = false;
		}

		static bool isVerticalLineShown() {
			if(verticalLine == null) {
				return false;
			}
			return verticalLine.Visible;
		}

		static void showVerticalLine() {
			if(verticalLine == null) {
				verticalLine = new FormVerticalLine();
				verticalLine.Show();
			}
			else {
				verticalLine.Visible = true;
			}
			if(screen != null) {
				verticalLine.Height = screen.Bounds.Height;
			}
		}

		static void hideVerticalLine() {
			if(verticalLine == null) {
				return;
			}
			verticalLine.Visible = false;
		}

		static void hotkey_HotkeyPressed(object sender, EventArgs e) {
			scrolllock = !scrolllock;
		}

		static void timer_Tick(object sender, EventArgs e) {
			move();
		}

		static bool scrolllock = false;

		static Screen screen;
		static Point point;

		static void move() {
			if(scrolllock) {
				return;
			}
			Point p = Cursor.Position;
			Screen s = Screen.FromPoint(p);
			//
			if(point == p) {
				return;
			}
			//
			int top = p.Y + MouseLiner.Properties.Settings.Default.DeltaY;
			int left = p.X + MouseLiner.Properties.Settings.Default.DeltaX;
			if(s.Bounds.Bottom < (top + MouseLiner.Properties.Settings.Default.HorizontalLineWidth)) {
				top = s.Bounds.Bottom - (int)MouseLiner.Properties.Settings.Default.HorizontalLineWidth;
			}
			if(s.Bounds.Right < (left + MouseLiner.Properties.Settings.Default.VerticalLineWidth)) {
				left = s.Bounds.Left - (int)MouseLiner.Properties.Settings.Default.VerticalLineWidth;
			}
			if(isHorizontalLineShown()) {
				if(horizontalLine.Top != top) {
					horizontalLine.Top = top;
				}
			}
			if(isVerticalLineShown()) {
				if(verticalLine.Left != left) {
					verticalLine.Left = left;
				}
			}
			/*
			spotlight.Top = p.Y - spotlight.Height / 2;
			spotlight.Left = p.X - spotlight.Width / 2;
			 */
			//
			if(screen != s) {
				if(isHorizontalLineShown()) {
					horizontalLine.Left = s.Bounds.Left;
					horizontalLine.Width = s.Bounds.Width;
				}
				if(isVerticalLineShown()) {
					verticalLine.Top = s.Bounds.Top;
					verticalLine.Height = s.Bounds.Height;
				}
				screen = s;
			}
			point = p;
		}

		static void context_Popup(object sender, EventArgs e) {
			if(MouseLiner.Properties.Settings.Default.HorizontalLineOpacity == 1) {
				transparentMenu.Checked = false;
			}
			else {
				transparentMenu.Checked = true;
			}
			showHorizontalLineMenu.Checked = isHorizontalLineShown();
			showVerticalLineMenu.Checked = isVerticalLineShown();
			//
			narrowHorizontalLineMenu.Checked = false;
			mediumHorizontalLineMenu.Checked = false;
			thickHorizontalLineMenu.Checked = false;
			switch(MouseLiner.Properties.Settings.Default.HorizontalLineWidth) {
				case 1:
					narrowHorizontalLineMenu.Checked = true;
					break;
				case 3:
					mediumHorizontalLineMenu.Checked = true;
					break;
				case 5:
					thickHorizontalLineMenu.Checked = true;
					break;
			}
			narrowVerticalLineMenu.Checked = false;
			mediumVerticalLineMenu.Checked = false;
			thickVerticalLineMenu.Checked = false;
			switch(MouseLiner.Properties.Settings.Default.VerticalLineWidth) {
				case 1:
					narrowVerticalLineMenu.Checked = true;
					break;
				case 3:
					mediumVerticalLineMenu.Checked = true;
					break;
				case 5:
					thickVerticalLineMenu.Checked = true;
					break;
			}
		}

		static void toggleShowHideHorizontalLine(object sender, EventArgs e) {
			if(isHorizontalLineShown()) {
				hideHorizontalLine();
			}
			else {
				showHorizontalLine();
			}
		}

		static void toggleShowHideVerticalLine(object sender, EventArgs e) {
			if(isVerticalLineShown()) {
				hideVerticalLine();
			}
			else {
				showVerticalLine();
			}
		}

		static void toggleTransparent(object sender, EventArgs e) {
			transparentMenu.Checked = !transparentMenu.Checked;
			if(transparentMenu.Checked) {
				MouseLiner.Properties.Settings.Default.HorizontalLineOpacity = 0.5;
				MouseLiner.Properties.Settings.Default.VerticalLineOpacity = 0.5;
			}
			else {
				MouseLiner.Properties.Settings.Default.HorizontalLineOpacity = 1;
				MouseLiner.Properties.Settings.Default.VerticalLineOpacity = 1;
			}
		}

		static void chooseHorizontalLineColor(object sender, EventArgs e) {
			ColorDialog cd = new ColorDialog();
			cd.Color = MouseLiner.Properties.Settings.Default.HorizontalLineColor;
			if(cd.ShowDialog() == DialogResult.OK) {
				MouseLiner.Properties.Settings.Default.HorizontalLineColor = cd.Color;
			}
			cd.Dispose();
		}

		static void setMediumHorizontalLine(object sender, EventArgs e) {
			MouseLiner.Properties.Settings.Default.HorizontalLineWidth = 3;
		}

		static void setNarrowHorizontalLine(object sender, EventArgs e) {
			MouseLiner.Properties.Settings.Default.HorizontalLineWidth = 1;
		}

		static void setThickHorizontalLine(object sender, EventArgs e) {
			MouseLiner.Properties.Settings.Default.HorizontalLineWidth = 5;
		}

		static void chooseVerticalLineColor(object sender, EventArgs e) {
			ColorDialog cd = new ColorDialog();
			cd.Color = MouseLiner.Properties.Settings.Default.VerticalLineColor;
			if(cd.ShowDialog() == DialogResult.OK) {
				MouseLiner.Properties.Settings.Default.VerticalLineColor = cd.Color;
			}
			cd.Dispose();
		}

		static void setMediumVerticalLine(object sender, EventArgs e) {
			MouseLiner.Properties.Settings.Default.VerticalLineWidth = 3;
		}

		static void setNarrowVerticalLine(object sender, EventArgs e) {
			MouseLiner.Properties.Settings.Default.VerticalLineWidth = 1;
		}

		static void setThickVerticalLine(object sender, EventArgs e) {
			MouseLiner.Properties.Settings.Default.VerticalLineWidth = 5;
		}

		static void exit(object sender, EventArgs e) {
			MouseLiner.Properties.Settings.Default.ShowHorizontalLine = isHorizontalLineShown();
			MouseLiner.Properties.Settings.Default.ShowVerticalLine = isVerticalLineShown();
			//
			hotkey.UnregisterHotkey();
			timer.Stop();
			hideHorizontalLine();
			hideVerticalLine();
			notify.Visible = false;
			//
			/*
			spotlight.Hide();
			spotlight.Dispose();
			 */
			//
			hotkey.Dispose();
			if(horizontalLine != null) {
				horizontalLine.Dispose();
			}
			if(verticalLine != null) {
				verticalLine.Dispose();
			}
			timer.Dispose();
			notify.Dispose();
			context.Dispose();
			horizontalLineSubMenu.Dispose();
			showHorizontalLineMenu.Dispose();
			chooseHorizontalLineColorMenu.Dispose();
			verticalLineSubMenu.Dispose();
			showVerticalLineMenu.Dispose();
			chooseVerticalLineColorMenu.Dispose();
			transparentMenu.Dispose();
			splitter1.Dispose();
			exitMenu.Dispose();
			//
			MouseLiner.Properties.Settings.Default.Save();
			//
			Application.Exit();
		}
	}
}
