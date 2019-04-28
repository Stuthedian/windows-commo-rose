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

        private KeyHandler ghk;
        public Form1()
        {
            InitializeComponent();
            ghk = new KeyHandler(Keys.PrintScreen, this);
            ghk.Register();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void HandleHotkey()
        {
            //const int SW_SHOWNORMAL = 1;
            // Do stuff...
            //MessageBox.Show("Reacted!");
            SetForegroundWindow(this.Handle);
            ShowWindow(this.Handle, 1);
            //GetAsyncKeyState
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == Constants.WM_HOTKEY_MSG_ID)
                HandleHotkey();
            base.WndProc(ref m);
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.PrintScreen)
            {
                ShowWindow(this.Handle, 0);//0 — hide
            }
        }
    }
}
