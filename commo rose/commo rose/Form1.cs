using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Xml;
using System.IO;
using WindowsInput.Native;

namespace commo_rose
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();
        [DllImport("user32.dll")]
        static extern bool SetForegroundWindow(IntPtr hWnd);
        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        [DllImport("user32.dll")]
        static extern uint GetWindowThreadProcessId(IntPtr hWnd, IntPtr ProcessId);
        [DllImport("kernel32.dll")]
        static extern uint GetCurrentThreadId();
        [DllImport("user32.dll")]
        static extern bool AttachThreadInput(uint idAttach, uint idAttachTo, bool fAttach);
        [DllImport("user32.dll", SetLastError = true)]
        static extern bool BringWindowToTop(IntPtr hWnd);

        public const string app_name = "Commo rose";
        
        public Keys action_button_keyboard { get; set; }
        public KeyHandler ghk;
        public MouseButtons action_button_mouse { get; set; }
        public MouseHook mouseHook;
        public Hook_target hook_target;

        private Settings settings;
        
        private IntPtr current_window;
        public IntPtr form_handle;

        public List<CustomButton> buttons_array;

        public Form1()
        {
            InitializeComponent();
            BackColor = Color.Lime;
            TransparencyKey = Color.Lime;
            FormBorderStyle = FormBorderStyle.None;
            ShowInTaskbar = false;
            form_handle = this.Handle;
            buttons_array = new List<CustomButton>();

            notifyIcon1.Text = app_name;
            notifyIcon1.Icon = SystemIcons.Application;
            notifyIcon1.ContextMenuStrip = contextMenuStrip1;
            KeyPreview = true;

            Saver.load_settings(this);
            if(hook_target == Hook_target.Keyboard)
            {
                ghk = new KeyHandler(action_button_keyboard, form_handle);
                ghk.Register();
            }
            else if(hook_target == Hook_target.Mouse)
            {
                mouseHook = new MouseHook(LowLevelMouseProc);
            }
            settings = new Settings(this);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void HandleHotkey()
        {
            on_form_show();
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == Constants.WM_HOTKEY_MSG_ID)
                HandleHotkey();
            base.WndProc(ref m);
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == action_button_keyboard)
            {
                on_form_hide();
            }
            e.Handled = true;
        }

        private void ExitMenuItem_Click(object sender, EventArgs e)
        {
            if (ghk != null)
                ghk.Unregister();
            if (mouseHook != null)
                mouseHook.ClearHook();
            this.Close();
        }

        public int LowLevelMouseProc(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                MSLLHOOKSTRUCT a = (MSLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(MSLLHOOKSTRUCT));
                int xbutton_id = a.mouseData >> 16;
                MouseMessage wmMouse = (MouseMessage)wParam;
                if (wmMouse == mouse_button_to_message_down(action_button_mouse))
                {

                    if (wmMouse != MouseMessage.WM_MBUTTONDOWN)
                    {
                        int id = mouse_xbutton_to_id(action_button_mouse);
                        if (xbutton_id == id)
                        {
                            on_form_show();
                            return 1;
                        }
                    }
                    else
                    {
                        on_form_show();
                        return 1;
                    }
                }
                if (wmMouse == mouse_button_to_message_up(action_button_mouse))
                {
                    if (wmMouse != MouseMessage.WM_MBUTTONUP)
                    {
                        int id = mouse_xbutton_to_id(action_button_mouse);
                        if (xbutton_id == id)
                        {
                            on_form_hide();
                            return 1;
                        }
                    }
                    else
                    {
                        on_form_hide();
                        return 1;
                    }
                }
            }
            return NativeMethods.CallNextHookEx(mouseHook._hGlobalLlHook, nCode, wParam, lParam);
        }

        private void check_button_bounds()
        {
            foreach (CustomButton button in Controls.OfType<CustomButton>().ToArray())
            {
                Rectangle screen = Screen.PrimaryScreen.WorkingArea;
                if (!screen.Contains(RectangleToScreen(button.Bounds)))
                {
                    button.Visible = false;
                }
                else { button.Visible = true; }
            }
        }

        private void activate_selected_button()
        {
            foreach (CustomButton button in buttons_array)
            {
                if (button.Selected)
                {
                    button.Act();
                    break;
                }
            }
        }

        private void on_form_show()
        {
            const int SW_SHOW = 5;
            Point center = MousePosition;
            center.X -= Width / 2;
            center.Y -= Height / 2;
            Location = center;


            //check_button_bounds();
            current_window = GetForegroundWindow();
            uint currentlyFocusedWindowProcessId = GetWindowThreadProcessId(current_window, IntPtr.Zero);
            uint appThread = GetCurrentThreadId();

            if (currentlyFocusedWindowProcessId != appThread)
            {
                AttachThreadInput(currentlyFocusedWindowProcessId, appThread, true);
                BringWindowToTop(form_handle);
                ShowWindow(form_handle, SW_SHOW);
                AttachThreadInput(currentlyFocusedWindowProcessId, appThread, false);
            }
        }

        private void on_form_hide()
        {
            Hide();
            SetForegroundWindow(current_window);
            activate_selected_button();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void SettingsMenuItem_Click(object sender, EventArgs e)
        {
            if(!settings.Visible)
                settings.ShowDialog();
        }

        public void change_action_button(Keys key)
        {
            ghk.Unregister();
            action_button_keyboard = key;
            ghk = new KeyHandler(action_button_keyboard, form_handle);
            ghk.Register();
        }

        private MouseMessage mouse_button_to_message_down(MouseButtons button)
        {
            if (button == MouseButtons.Middle)
                return MouseMessage.WM_MBUTTONDOWN;
            else if (button == MouseButtons.XButton1 || button == MouseButtons.XButton2)
                return MouseMessage.WM_XBUTTONDOWN;
            else throw new NotImplementedException();
        }

        private MouseMessage mouse_button_to_message_up(MouseButtons button)
        {
            if (button == MouseButtons.Middle)
                return MouseMessage.WM_MBUTTONUP;
            else if (button == MouseButtons.XButton1 || button == MouseButtons.XButton2)
                return MouseMessage.WM_XBUTTONUP;
            else throw new NotImplementedException();
        }

        private int mouse_xbutton_to_id(MouseButtons button)
        {
            const int XBUTTON1 = 0x0001;
            const int XBUTTON2 = 0x0002;
            if (button == MouseButtons.XButton1)
                return XBUTTON1;
            else if (button == MouseButtons.XButton2)
                return XBUTTON2;
            else throw new NotImplementedException();
        }
    }

    public enum Hook_target { Mouse, Keyboard }
}
