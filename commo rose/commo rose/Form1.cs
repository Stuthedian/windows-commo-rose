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

        const int SW_SHOWNORMAL = 1;
        private Settings settings;
        private Keys action_button;
        private KeyHandler ghk;
        private MouseHook mouseHook;
        private IntPtr current_window;
        public Form1()
        {
            InitializeComponent();

            BackColor = Color.Lime;
            TransparencyKey = Color.Lime;
            FormBorderStyle = FormBorderStyle.None;
            ShowInTaskbar = false;

            mouseHook = new MouseHook(LowLevelMouseProc);

            set_buttons_style();
            set_buttons_actions();
            
            notifyIcon1.Text = "Commo rose";
            notifyIcon1.Icon = SystemIcons.Application;
            notifyIcon1.ContextMenuStrip = contextMenuStrip1;
            
            action_button = Keys.PrintScreen;
            KeyPreview = true;
            ghk = new KeyHandler(action_button, this);
            ghk.Register();
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
            if(e.KeyCode == action_button)
            {
                on_form_hide();
            }
            e.Handled = true;
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ghk.Unregister();
            this.Close();
        }

        public int LowLevelMouseProc(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                // Get the mouse WM from the wParam parameter
                var wmMouse = (MouseMessage)wParam;
                if (wmMouse == MouseMessage.WM_XBUTTONDOWN)
                {
                    on_form_show();
                    return 1;
                }
                if (wmMouse == MouseMessage.WM_XBUTTONUP)
                {
                    on_form_hide();
                    return 1;
                }
            }

            // Pass the hook information to the next hook procedure in chain
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
                if(button.Selected)
                {
                    button.Act(current_window);
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
            customButton1.action_Type = Action_type.Run;
            customButton1.Parameters = "cmd";
            customButton6.action_Type = Action_type.Run_as_admin;
            customButton6.Parameters = "cmd";
        }

        private void on_form_show()
        {
            Point center = MousePosition;
            center.X -= Width / 2;
            center.Y -= Height / 2;
            Location = center;

            check_button_bounds();
            if (settings != null)
                settings.set_settings(Controls.OfType<CustomButton>().ToArray());
            current_window = GetForegroundWindow();
            SetForegroundWindow(this.Handle);
            ShowWindow(this.Handle, SW_SHOWNORMAL);
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

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            settings = new Settings(this);
            settings.ShowDialog();
        }
    }
}
