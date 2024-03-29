using CalcOpener3000.Properties;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Input;
using Timer = System.Windows.Forms.Timer;
using Microsoft.Win32;
using System.Media;

namespace CalcOpener3000 {
	public partial class Form1 : Form {
		public static Form1 form1;
		public Form1() {
			InitializeComponent();
			load_settings();
			form1 = this;
			hookId = SetHook(HookCallback);

			timer.Tick += new EventHandler(timer_tick);
		}
		void timer_tick(object sender, EventArgs e) {
			if(Control.IsKeyLocked(Keys.NumLock)) {
				label_numlock.Text = "NumLock is ON";
			}
			else {
				label_numlock.Text = "NumLock is OFF";
				SetNumlockOn();
			}
		}
		private void Form1_FormClosing(object sender, FormClosingEventArgs e) {
			UnhookWindowsHookEx(hookId);
			timer.Stop();
			save_settings();
		}
		private void save_settings() {
			Settings.Default.calc_path = textBox1.Text;
			Settings.Default.open_calc = checkBox_opencalc.Checked;
			Settings.Default.dontopenmultiplecalcs = checkBox_nomultiplecalcs.Checked;
			Settings.Default.aero = aero;
			Settings.Default.Save();
		}
		private void load_settings() {
			try {
				textBox1.Text = Settings.Default.calc_path;
				checkBox_opencalc.Checked = Settings.Default.open_calc;
				checkBox_nomultiplecalcs.Checked = Settings.Default.dontopenmultiplecalcs;
				aero = Settings.Default.aero;
				label_aero.Visible = aero;
				autostart = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true).GetValue("CalcOpener3000") != null;
				linkLabel_autostart.Text = autostart ? "autostart: on" : "autostart: off";
				if(aero) {
					this.Text = "";
					label_numlock.ForeColor = Color.WhiteSmoke;
					checkBox_nomultiplecalcs.ForeColor = Color.WhiteSmoke;
					checkBox_opencalc.ForeColor = Color.WhiteSmoke;
				}
			}
			catch(Exception ex) {
				MessageBox.Show("Settings could not be loaded: " + ex.Message);
			}
		}
		protected override void OnHandleCreated(EventArgs e) {
			// Use e.g. Color.FromArgb(128, Color.Lime) for a 50% opacity green tint.
			if(aero)
				AeroUtil.EnableAcrylic(this, Color.Transparent);

			base.OnHandleCreated(e);
		}
		protected override void OnPaintBackground(PaintEventArgs e) {
			if(aero)
				e.Graphics.Clear(Color.Transparent);
			else
				base.OnPaintBackground(e);
		}
		private static void NumlockPressed() {
			if(Form1.form1.checkBox_opencalc.Checked) {
				string exe_path = Form1.form1.textBox1.Text;
				string exe_name = Path.GetFileNameWithoutExtension(exe_path);
				if(File.Exists(exe_path)) {
					if(!Form1.form1.checkBox_nomultiplecalcs.Checked || Process.GetProcessesByName(exe_name).Length == 0) {
						Process.Start(exe_path);
					}
					Process[] processes = Process.GetProcessesByName(exe_name);

					while(processes.Length == 0) {
						processes = Process.GetProcessesByName(exe_name);
						Thread.Sleep(50);
					}

					while(processes[0].MainWindowHandle == IntPtr.Zero) {
						FocusProcess(exe_name);
						Thread.Sleep(50);
					}
					//one more time for good measure
					Thread.Sleep(100);
					FocusProcess(exe_name);
				}
			}
		}


		private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e) {
			this.Show();
		}

		private void Form1_Leave(object sender, EventArgs e) {
			this.Hide();
		}

		private void exitToolStripMenuItem_Click(object sender, EventArgs e) {
			Application.Exit();
		}
		private bool autostart = false;
		private void linkLabel_autostart_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
			bool autostart_status = show_taskdialog_autostart(autostart);

			if(autostart_status) {
				linkLabel_autostart.Text = "autostart: on";
				//register the autostart
				RegistryKey rk = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
				rk.SetValue("CalcOpener3000", Application.ExecutablePath.ToString());
			}
			else {
				autostart = false;
				linkLabel_autostart.Text = "autostart: off";
				//unregister the autostart
				RegistryKey rk = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
				rk.DeleteValue("CalcOpener3000", false);
			}

			this.Show();
			this.Focus();
		}


		private List<Keys> konamiCode = new List<Keys> { Keys.Up, Keys.Up, Keys.Down, Keys.Down, Keys.Left, Keys.Right, Keys.Left, Keys.Right, Keys.B, Keys.A };
		private int konamiIndex = 0;
		bool aero = false;
		private void konami(Keys KeyCode) {
			if(KeyCode == konamiCode[konamiIndex]) {
				konamiIndex++;
				if(konamiIndex == konamiCode.Count) {
					konamiIndex = 0;
					konami_triggered();
				}
			}
			else {
				konamiIndex = 0; // reset the index if a wrong key is pressed
			}
		}
		private void konami_triggered() {
			var result = MessageBox.Show("Konami code triggered!\n" +
				"Do you want to toggle Acrylic? Current status: " + (aero ? "ON" : "OFF") + "\n" +
				"Changes will be visible after restartìšøèžýø", "ACRYLIC", MessageBoxButtons.YesNo);
			if(result == DialogResult.Yes) {
				aero = !aero;

			}
		}




		//LOW LEVEL MAGIC, DONT ASK

		private static bool pressed_by_app = false;

		private const int WH_KEYBOARD_LL = 13;
		private const int WM_KEYDOWN = 0x0100;
		private static IntPtr hookId = IntPtr.Zero;
		private static IntPtr SetHook(LowLevelKeyboardProc proc) {
			using(Process curProcess = Process.GetCurrentProcess())
			using(ProcessModule curModule = curProcess.MainModule) {
				return SetWindowsHookEx(WH_KEYBOARD_LL, proc, GetModuleHandle(curModule.ModuleName), 0);
			}
		}

		private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

		private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam) {
			if(nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN) {
				int vkCode = Marshal.ReadInt32(lParam);
				if(vkCode == (int)Keys.NumLock) {
					if(!pressed_by_app) NumlockPressed();
				}
				else if(Form.ActiveForm == Form1.form1) {
					Keys key = (Keys)vkCode;
					form1.konami(key);
				}
			}
			return CallNextHookEx(hookId, nCode, wParam, lParam);
		}

		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool UnhookWindowsHookEx(IntPtr hhk);

		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern IntPtr GetModuleHandle(string lpModuleName);

		[DllImport("user32.dll")]
		private static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);

		private const int KEYEVENTF_EXTENDEDKEY = 0x0001;
		private const int KEYEVENTF_KEYUP = 0x0002;
		private const byte VK_NUMLOCK = 0x90;

		public static void SetNumlockOn() {
			if(!Control.IsKeyLocked(Keys.NumLock)) {
				pressed_by_app = true;
				keybd_event(VK_NUMLOCK, 0, KEYEVENTF_EXTENDEDKEY, UIntPtr.Zero);
				keybd_event(VK_NUMLOCK, 0, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP, UIntPtr.Zero);
				pressed_by_app = false;
			}
		}

		private void checkBox_opencalc_CheckedChanged(object sender, EventArgs e) {
			if(checkBox_opencalc.Checked) {
				textBox1.Enabled = true;
			}
			else {
				textBox1.Enabled = false;
			}
		}


		//another magic low level stuff (obviously stolen from stackoverflow :p)
		[DllImport("user32.dll")]
		public static extern bool ShowWindowAsync(HandleRef hWnd, int nCmdShow);
		[DllImport("user32.dll")]
		public static extern bool SetForegroundWindow(IntPtr WindowHandle);
		public const int SW_RESTORE = 9;

		private static void FocusProcess(string procName) {
			Process[] objProcesses = System.Diagnostics.Process.GetProcessesByName(procName);
			if(objProcesses.Length > 0) {
				IntPtr hWnd = IntPtr.Zero;
				hWnd = objProcesses[0].MainWindowHandle;
				ShowWindowAsync(new HandleRef(null, hWnd), SW_RESTORE);
				SetForegroundWindow(objProcesses[0].MainWindowHandle);
			}
		}

		private void button1_Click(object sender, EventArgs e) {

		}
		private bool show_taskdialog_autostart(bool current_status) {
			TaskDialogButton buttonYes = new("Yes");
			TaskDialogButton buttonNo = new("No");
			TaskDialogExpander expander = new() {
				Position = TaskDialogExpanderPosition.AfterFootnote,
				Text = current_status ? "This will stop the app from auto starting when you login into Windows" : "This will autostart the app when you login into Windows. \nPlease note that this doesnt copy the program anywhere. Do not delete or move the app files anywhere, as that would prevent the app from auto starting. \nIf you need instructions on how to fix the autostart after deleting the files, check out the enclosed instruction book"
			};
			TaskDialogPage page = new() {
				Heading = "Autostart",
				Text = current_status ? "Do you want to disable autostart?" : "Do you want to enable autostart?",
				Icon = TaskDialogIcon.Information,
				Caption = "Caption",
				Buttons = { buttonYes, buttonNo },
				Expander = expander
			};

			SoundPlayer player = new SoundPlayer(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("CalcOpener3000.notify.wav"));
			player.Play();

			var result = TaskDialog.ShowDialog(page);


			//return whether yes or no button was clicked
			if(current_status) {
				return result == buttonNo;
			}
			else {
				return result == buttonYes;
			}

		}
	}
}
