using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace commo_rose
{
    public partial class CueTextbox : TextBox
    {
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, string lp);

        private const int EM_SETCUEBANNER = 0x1501;
        private string mCue;
        public string Cue
        {
            get { return mCue; }
            set { mCue = value; updateCue(); }
        }

        public CueTextbox()
        {
            InitializeComponent();
        }
                

        private void updateCue()
        {
            if (IsHandleCreated && mCue != null)
            {
                SendMessage(Handle, 0x1501, (IntPtr)1, mCue);
            }
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            updateCue();
        }
    }
}
