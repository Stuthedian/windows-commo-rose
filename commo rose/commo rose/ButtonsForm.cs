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
    public partial class ButtonsForm : Form
    {
        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();
        [DllImport("user32.dll", SetLastError = true)]
        static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        public const string app_name = "Commo rose";
        private const int WS_EX_TOPMOST = 0x00000008;
        private const int SW_SHOWNOACTIVATE = 4;

        private IntPtr form_handle;

        private bool auto_switch;
        private Preset _current_preset;
        public Preset current_preset
        {
            get { return _current_preset; }
            set
            {
                if(_current_preset != null)
                {
                    foreach (CustomButton button in _current_preset.buttons)
                    {
                        button.Parent = null;
                    }
                }
                _current_preset = value;
                foreach (CustomButton button in current_preset.buttons)
                {
                    button.Parent = this;
                }
            }
        }
        
        public ButtonsForm()
        {
            InitializeComponent();
            BackColor = Color.Lime;
            TransparencyKey = Color.Lime;
            FormBorderStyle = FormBorderStyle.None;
            ShowInTaskbar = false;
            form_handle = this.Handle;
            auto_switch = true;

            notifyIcon1.Text = app_name;
            notifyIcon1.Icon = SystemIcons.Application;
            notifyIcon1.ContextMenuStrip = contextMenuStrip1;

            Opacity = 0.0;
            ShowWindow(form_handle, SW_SHOWNOACTIVATE);

            if(Environment.OSVersion.Version.Major == 10)
                VirtualDesktop.Desktop.PinWindow(form_handle);
        }

        
        
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= WS_EX_TOPMOST;
                return cp;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
       
        private void activate_selected_button()
        {
            foreach (CustomButton button in current_preset.buttons)
            {
                if (button.Selected)
                {
                    button.Act();
                    break;
                }
            }
        }

        public void on_form_show()
        {
            Point center = MousePosition;
            center.X -= Width / 2;
            center.Y -= Height / 2;
            Location = center;

            //if(auto_switch)//move to settings ?
            //{
            //    uint process_id;
            //    GetWindowThreadProcessId(GetForegroundWindow(), out process_id);
            //    string foreground_process_name = System.Diagnostics.Process.GetProcessById((int)process_id).ProcessName;
            //    bool process_matched = false;

            //    foreach (Preset preset in presets_array)
            //    {
            //        if (preset.processes.Contains(foreground_process_name))
            //        {
            //            current_preset = preset;
            //            process_matched = true;
            //            break;
            //        }
            //    }
            //    if (!process_matched)
            //        current_preset = presets_array.Find(x => x.name == "Desktop");
            //}
            
            Opacity = 1.0;
        }

        public void on_form_hide()
        {
            Opacity = 0.0;
            activate_selected_button();
        }

        private void AutoSwitchMenuItem_Click(object sender, EventArgs e)
        {
            if(auto_switch == true)
            {
                auto_switch = false;
                contextMenuStrip1.Items["AutoSwitchMenuItem"].Text = "Enable preset auto-switch";
            }
            else
            {
                auto_switch = true;
                contextMenuStrip1.Items["AutoSwitchMenuItem"].Text = "Disable preset auto-switch";
            }
        }

        private void SettingsMenuItem_Click(object sender, EventArgs e)//outsource to settings form?
        {
            throw new Exception();
            //if (!settings.Visible)
            //    settings.Show();
            //else
            //{
            //    settings.WindowState = FormWindowState.Normal;
            //    settings.Activate();
            //}
        }

        private void ExitMenuItem_Click(object sender, EventArgs e)
        {
            //this.Close();
            Program.Close();
        }

    }

    public class Preset
    {
        public string name;
        public List<CustomButton> buttons;
        public List<string> processes;

        public Color default_backcolor;
        public Color default_textcolor;
        public Font default_font;

        public Preset()
        {
            buttons = new List<CustomButton>();
            processes = new List<string>();
        }

        public Preset Clone()
        {
            Preset result_preset = new Preset();
            result_preset.default_backcolor = this.default_backcolor;
            result_preset.default_textcolor = this.default_textcolor;
            result_preset.default_font = this.default_font;//Clone?
            foreach (var item in this.buttons)
            {
                result_preset.buttons.Add(item.Clone());
            }

            return result_preset;
        }
    }
}
