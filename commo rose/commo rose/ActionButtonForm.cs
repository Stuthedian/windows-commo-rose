using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsInput.Native;

namespace commo_rose
{
    public partial class ActionButtonForm : Form
    {
        [System.Runtime.InteropServices.DllImport("user32.dll", EntryPoint = "MapVirtualKey", 
            ExactSpelling = false, CharSet = System.Runtime.InteropServices.CharSet.Auto, SetLastError = true)]
        public static extern int MapVirtualKey(uint ScanCode, int MAPVK_VK_TO_VSC);

        private Form1 main;
        private bool ignore_message;
        private int LShift, RShift, Ctrl, Alt;
        public ActionButtonForm(Form1 main)
        {
            InitializeComponent();
            this.main = main;
            MouseButtonsComboBox.Items.AddRange(new object[] {
            MouseButtons.Middle.ToString(),
            MouseButtons.XButton1.ToString(),
            MouseButtons.XButton2.ToString() });

            if (main.mouseOrKeyboardHook.hook_target == Hook_target.Keyboard)
            {
                MouseradioButton.Checked = false;
                KeyboardradioButton.Checked = true;
                MouseButtonsComboBox.Visible = false;
                ScanKeyTextBox.Visible = true;
                ScanKeyTextBox.Text = main.mouseOrKeyboardHook.action_button_keyboard.ToString();
            }
            else if (main.mouseOrKeyboardHook.hook_target == Hook_target.Mouse)
            {
                MouseradioButton.Checked = true;
                KeyboardradioButton.Checked = false;
                MouseButtonsComboBox.Visible = true;
                ScanKeyTextBox.Visible = false;
                MouseButtonsComboBox.SelectedItem = main.mouseOrKeyboardHook.action_button_mouse.ToString();
            }
            MouseradioButton.CheckedChanged += MouseradioButton_CheckedChanged;
            MouseButtonsComboBox.SelectedIndexChanged += MouseButtonsComboBox_SelectedIndexChanged;
            foreach (Control item in Controls)
            {
                item.TabStop = false;
            }
            ignore_message = true;
            LShift = MapVirtualKey((int)(Keys.LShiftKey), 0);
            RShift = MapVirtualKey((int)(Keys.RShiftKey), 0);
            Ctrl = MapVirtualKey((int)(Keys.ControlKey), 0);
            Alt = MapVirtualKey((int)(Keys.Menu), 0);
        }

        private void MouseradioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (main.mouseOrKeyboardHook.hook_target == Hook_target.Mouse)
            {
                main.mouseOrKeyboardHook.hook_target = Hook_target.Keyboard;
                main.mouseOrKeyboardHook.set_hook_target(main.mouseOrKeyboardHook.hook_target);
                MouseButtonsComboBox.Visible = false;
                ScanKeyTextBox.Visible = true;
            }
            else if (main.mouseOrKeyboardHook.hook_target == Hook_target.Keyboard)
            {
                main.mouseOrKeyboardHook.hook_target = Hook_target.Mouse;
                main.mouseOrKeyboardHook.set_hook_target(main.mouseOrKeyboardHook.hook_target);
                MouseButtonsComboBox.Visible = true;
                ScanKeyTextBox.Visible = false;
                MouseButtonsComboBox.SelectedItem = main.mouseOrKeyboardHook.action_button_mouse.ToString();
            }
            Saver.save_hook(main.mouseOrKeyboardHook.hook_target, main.mouseOrKeyboardHook.action_button_mouse, 
                main.mouseOrKeyboardHook.action_button_keyboard);
        }

        private void MouseButtonsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            main.mouseOrKeyboardHook.action_button_mouse =
                    (MouseButtons)Enum.Parse(typeof(MouseButtons), MouseButtonsComboBox.SelectedItem.ToString());
            Saver.save_hook(main.mouseOrKeyboardHook.hook_target, main.mouseOrKeyboardHook.action_button_mouse, 
                main.mouseOrKeyboardHook.action_button_keyboard);
        }

        protected override bool ProcessKeyPreview(ref Message m)
        {
            if (ignore_message)
                return base.ProcessKeyPreview(ref m);
            ignore_message = true;
            if (m.Msg == (int)KeyboardMessage.WM_KEYUP || m.Msg == (int)KeyboardMessage.WM_SYSKEYUP)
            {
                int KeyCode = (int)(((ulong)m.LParam & 0xFF0000) >> 16);
                bool extended = (int)((ulong)m.LParam & 0x01000000) != 0;
                if (KeyCode == LShift)
                {
                    main.mouseOrKeyboardHook.action_button_keyboard = VirtualKeyCode.LSHIFT;
                    ScanKeyTextBox.Text = VirtualKeyCode.LSHIFT.ToString();
                }
                else if (KeyCode == RShift)
                {
                    main.mouseOrKeyboardHook.action_button_keyboard = VirtualKeyCode.RSHIFT;
                    ScanKeyTextBox.Text = VirtualKeyCode.RSHIFT.ToString();
                }
                else if (KeyCode == Ctrl)
                {
                    if(extended)
                    {
                        main.mouseOrKeyboardHook.action_button_keyboard = VirtualKeyCode.RCONTROL;
                        ScanKeyTextBox.Text = VirtualKeyCode.RCONTROL.ToString();
                    }
                    else
                    {
                        main.mouseOrKeyboardHook.action_button_keyboard = VirtualKeyCode.LCONTROL;
                        ScanKeyTextBox.Text = VirtualKeyCode.LCONTROL.ToString();
                    }
                }
                else if (KeyCode == Alt)
                {
                    if (extended)
                    {
                        main.mouseOrKeyboardHook.action_button_keyboard = VirtualKeyCode.RMENU;
                        ScanKeyTextBox.Text = VirtualKeyCode.RMENU.ToString();
                    }
                    else
                    {
                        main.mouseOrKeyboardHook.action_button_keyboard = VirtualKeyCode.LMENU;
                        ScanKeyTextBox.Text = VirtualKeyCode.LMENU.ToString();
                    }
                }
                else throw new NotImplementedException();
                Saver.save_hook(main.mouseOrKeyboardHook.hook_target, main.mouseOrKeyboardHook.action_button_mouse,
                    main.mouseOrKeyboardHook.action_button_keyboard);
            }
            return base.ProcessKeyPreview(ref m);
        }

        private void ScanKeyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
            FakeLabel.Focus();
            if (e.KeyCode == Keys.ShiftKey || e.KeyCode == Keys.ControlKey || e.KeyCode == Keys.Menu)
            {
                ignore_message = false;
            }
            else
            {
                main.mouseOrKeyboardHook.action_button_keyboard = (VirtualKeyCode)e.KeyValue;
                ScanKeyTextBox.Text = ((VirtualKeyCode)e.KeyValue).ToString();
                ignore_message = true;
                Saver.save_hook(main.mouseOrKeyboardHook.hook_target, main.mouseOrKeyboardHook.action_button_mouse, 
                    main.mouseOrKeyboardHook.action_button_keyboard);
            }
        }

        private void ScanKeyTextBox_Enter(object sender, EventArgs e)
        {
            ScanKeyTextBox.Text = "";
            ScanKeyTextBox.Cue = "Press any key";
        }
    }
}
