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
        static extern bool SetForegroundWindow(IntPtr hWnd);
        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        const int SW_SHOWNORMAL = 1, SW_HIDE = 0;
        private Keys action_button;
        private KeyHandler ghk;
        public Form1()
        {
            InitializeComponent();

            BackColor = Color.Lime;
            TransparencyKey = Color.Lime;
            FormBorderStyle = FormBorderStyle.None;
            ShowInTaskbar = false;

            customButton1.BackColor = Color.FromArgb(201, 120, 0);
            customButton1.ForeColor = Color.FromArgb(247, 218, 2);
            
            customButton1.Act = () => System.Diagnostics.Process.Start("cmd");

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
            SetForegroundWindow(this.Handle);
            ShowWindow(this.Handle, SW_SHOWNORMAL);
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
                ShowWindow(this.Handle, SW_HIDE);
                if(customButton1.Selected)
                {
                    customButton1.Act();
                }
            }
            e.Handled = true;
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ghk.Unregister();
            this.Close();
        }

        
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
        }
    }
}
