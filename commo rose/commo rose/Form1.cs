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

namespace commo_rose
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        
        public const string app_name = "Commo rose";
        private const int WS_EX_TOPMOST = 0x00000008;
        private const int WS_EX_COMPOSITED = 0x02000000;

        public MouseOrKeyboardHook mouseOrKeyboardHook;
        
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
            

            mouseOrKeyboardHook = new MouseOrKeyboardHook(on_form_show, on_form_hide);
            Saver.load_settings(this);
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
            if (mouseOrKeyboardHook != null)
                mouseOrKeyboardHook.ClearHook();
            this.Close();
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

        
    }
}
