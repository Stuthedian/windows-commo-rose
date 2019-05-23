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

        public const string app_name = "Commo rose";
        
        public Keys action_button_keyboard { get; set; }
        public MouseButtons action_button_mouse { get; set; }
        public XmlDocument doc;
        private Settings settings;
        public KeyHandler ghk;
        public MouseHook mouseHook;
        public Hook_target hook_target;
        private IntPtr current_window;
        public IntPtr form_handle;


        public Form1()
        {
            InitializeComponent();
            BackColor = Color.Lime;
            TransparencyKey = Color.Lime;
            FormBorderStyle = FormBorderStyle.None;
            ShowInTaskbar = false;
            form_handle = this.Handle;

            //mouseHook = new MouseHook(LowLevelMouseProc);


            //hook_target = Hook_target.Mouse;
            action_button_mouse = MouseButtons.XButton1;
            set_buttons_style();
            //set_buttons_actions();

            notifyIcon1.Text = app_name;
            notifyIcon1.Icon = SystemIcons.Application;
            notifyIcon1.ContextMenuStrip = contextMenuStrip1;

            action_button_keyboard = Keys.PrintScreen;
            KeyPreview = true;
            hook_target = Hook_target.Keyboard;
            ghk = new KeyHandler(action_button_keyboard, form_handle);
            ghk.Register();

            Saver.load_settings(Controls.OfType<CustomButton>().ToArray());
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
            return NativeMethods.CallNextHookEx(mouseHook._hGlobalLlMouseHook, nCode, wParam, lParam);
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
            foreach (CustomButton button in Controls.OfType<CustomButton>().ToArray())
            {
                if (button.Selected)
                {
                    button.Act();
                    break;
                }
            }
        }

        private void set_buttons_style()
        {
            foreach (CustomButton button in Controls.OfType<CustomButton>().ToArray())
            {

                button.BackColor = Color.FromArgb(201, 120, 0);
                button.ForeColor = Color.FromArgb(247, 218, 2);
            }
        }

        private void set_buttons_actions()
        {
            customButton1.action_type = Action_type.Run;
            customButton1.Parameters = "cmd";
            customButton6.action_type = Action_type.Run_as_admin;
            customButton6.Parameters = "cmd";
        }

        private void on_form_show()
        {
            const int SW_SHOWNORMAL = 1;
            Point center = MousePosition;
            center.X -= Width / 2;
            center.Y -= Height / 2;
            Location = center;


            //check_button_bounds();
            current_window = GetForegroundWindow();
            SetForegroundWindow(form_handle);
            ShowWindow(form_handle, SW_SHOWNORMAL);
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
