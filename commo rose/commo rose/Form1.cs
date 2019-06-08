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
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        
        public const string app_name = "Commo rose";
        private const int WS_EX_TOPMOST = 0x00000008;
        private const int WS_EX_COMPOSITED = 0x02000000;

        public MouseButtons action_button_mouse { get; set; }
        public MouseHook mouseHook;
        public VirtualKeyCode action_button_keyboard { get; set; }
        public KeyboardHook keyboardHook;
        public Hook_target hook_target;
        private bool key_hook_handled;
        private Settings settings;
        
        public IntPtr form_handle;

        public List<CustomButton> buttons_array;
        public Color global_backcolor;
        public Color global_textcolor;
        public Font global_font;

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
            key_hook_handled = false;
            action_button_keyboard = VirtualKeyCode.INSERT;

            Saver.load_settings(this);
            if (hook_target == Hook_target.Keyboard)
            {
                keyboardHook = new KeyboardHook(LowLevelKeyboardProc);
            }
            else if (hook_target == Hook_target.Mouse)
            {
                mouseHook = new MouseHook(LowLevelMouseProc);
            }
            settings = new Settings(this);
        }

        
        
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= WS_EX_TOPMOST;
                cp.ExStyle |= WS_EX_COMPOSITED;
                return cp;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void ExitMenuItem_Click(object sender, EventArgs e)
        {
            if (mouseHook != null)
                mouseHook.ClearHook();
            if (keyboardHook != null)
                keyboardHook.ClearHook();
            this.Close();
        }

        public int LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                KBDLLHOOKSTRUCT a = (KBDLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(KBDLLHOOKSTRUCT));
                
                KeyboardMessage wmMouse = (KeyboardMessage)wParam;
                if (wmMouse == KeyboardMessage.WM_KEYDOWN)
                {
                    if(a.vkCode == (int)action_button_keyboard)
                    {
                        if(key_hook_handled == false)
                        {
                            key_hook_handled = true;
                            on_form_show();
                        }
                        return 1;
                    }
                }
                else if (wmMouse == KeyboardMessage.WM_KEYUP)
                {
                    if (a.vkCode == (int)action_button_keyboard)
                    {
                        key_hook_handled = false;
                        on_form_hide();
                        return 1;
                    }
                }
            }
            return NativeMethods.CallNextHookEx(keyboardHook._hGlobalLlHook, nCode, wParam, lParam);
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

        //private void check_button_bounds()
        //{
        //    foreach (CustomButton button in Controls.OfType<CustomButton>().ToArray())
        //    {
        //        Rectangle screen = Screen.PrimaryScreen.WorkingArea;
        //        if (!screen.Contains(RectangleToScreen(button.Bounds)))
        //        {
        //            button.Visible = false;
        //        }
        //        else { button.Visible = true; }
        //    }
        //}

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
            const int SW_SHOWNOACTIVATE = 4;
            Point center = MousePosition;
            center.X -= Width / 2;
            center.Y -= Height / 2;
            Location = center;


            //check_button_bounds();
            ShowWindow(form_handle, SW_SHOWNOACTIVATE);
        }

        private void on_form_hide()
        {
            Hide();
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
