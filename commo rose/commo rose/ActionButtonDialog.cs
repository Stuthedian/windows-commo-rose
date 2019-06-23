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
    public partial class ActionButtonDialog : Form
    {
        [System.Runtime.InteropServices.DllImport("user32.dll", EntryPoint = "MapVirtualKey", 
            ExactSpelling = false, CharSet = System.Runtime.InteropServices.CharSet.Auto, SetLastError = true)]
        public static extern int MapVirtualKey(uint ScanCode, int MAPVK_VK_TO_VSC);

        private Form1 main;
        private bool ignore_message;
        private int LShift, RShift, Ctrl, Alt;
        public ActionButtonDialog(Form1 main)
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
                ScanKeyTextBox.Text = vk_to_appropriate_string(main.mouseOrKeyboardHook.action_button_keyboard);
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
                ScanKeyTextBox.Text = vk_to_appropriate_string(main.mouseOrKeyboardHook.action_button_keyboard);
                MouseButtonsComboBox.Visible = false;
                ScanKeyTextBox.Visible = true;
            }
            else if (main.mouseOrKeyboardHook.hook_target == Hook_target.Keyboard)
            {
                main.mouseOrKeyboardHook.hook_target = Hook_target.Mouse;
                main.mouseOrKeyboardHook.set_hook_target(main.mouseOrKeyboardHook.hook_target);
                MouseButtonsComboBox.SelectedItem = main.mouseOrKeyboardHook.action_button_mouse.ToString();
                MouseButtonsComboBox.Visible = true;
                ScanKeyTextBox.Visible = false;
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
                    ScanKeyTextBox.Text = vk_to_appropriate_string(VirtualKeyCode.LSHIFT);
                }
                else if (KeyCode == RShift)
                {
                    main.mouseOrKeyboardHook.action_button_keyboard = VirtualKeyCode.RSHIFT;
                    ScanKeyTextBox.Text = vk_to_appropriate_string(VirtualKeyCode.RSHIFT);
                }
                else if (KeyCode == Ctrl)
                {
                    if(extended)
                    {
                        main.mouseOrKeyboardHook.action_button_keyboard = VirtualKeyCode.RCONTROL;
                        ScanKeyTextBox.Text = vk_to_appropriate_string(VirtualKeyCode.RCONTROL);
                    }
                    else
                    {
                        main.mouseOrKeyboardHook.action_button_keyboard = VirtualKeyCode.LCONTROL;
                        ScanKeyTextBox.Text = vk_to_appropriate_string(VirtualKeyCode.LCONTROL);
                    }
                }
                else if (KeyCode == Alt)
                {
                    if (extended)
                    {
                        main.mouseOrKeyboardHook.action_button_keyboard = VirtualKeyCode.RMENU;
                        ScanKeyTextBox.Text = vk_to_appropriate_string(VirtualKeyCode.RMENU);
                    }
                    else
                    {
                        main.mouseOrKeyboardHook.action_button_keyboard = VirtualKeyCode.LMENU;
                        ScanKeyTextBox.Text = vk_to_appropriate_string(VirtualKeyCode.LMENU);
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
                ScanKeyTextBox.Text = vk_to_appropriate_string((VirtualKeyCode)e.KeyValue);
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

        private string vk_to_appropriate_string(VirtualKeyCode vk)
        {
            if(vk >= VirtualKeyCode.VK_0 && vk <= VirtualKeyCode.VK_Z)
            {
                return vk.ToString()[3].ToString();
            }
            else if(vk >= VirtualKeyCode.NUMPAD0 && vk <= VirtualKeyCode.NUMPAD9)
            {
                return vk.ToString()[0] + vk.ToString().Substring(1).ToLower();
            }
            else if (vk >= VirtualKeyCode.LEFT && vk <= VirtualKeyCode.DOWN)
            {
                return vk.ToString()[0] + vk.ToString().Substring(1).ToLower() + " Arrow";
            }
            switch (vk)
            {
                case VirtualKeyCode.NUMLOCK:
                    return "Num Lock";
                case VirtualKeyCode.CAPITAL:
                    return "Caps Lock";
                case VirtualKeyCode.SCROLL:
                    return "Scroll Lock";
                case VirtualKeyCode.LCONTROL:
                case VirtualKeyCode.RCONTROL:
                    return (vk.ToString()[0] == 'L' ? "Left" : "Right") + " Ctrl";
                case VirtualKeyCode.LMENU:
                case VirtualKeyCode.RMENU:
                    return (vk.ToString()[0] == 'L' ? "Left" : "Right") + " Alt";
                case VirtualKeyCode.LSHIFT:
                case VirtualKeyCode.RSHIFT:
                    return (vk.ToString()[0] == 'L' ? "Left" : "Right") + " Shift";
                case VirtualKeyCode.PAUSE:
                case VirtualKeyCode.HOME:
                case VirtualKeyCode.END:
                case VirtualKeyCode.INSERT:
                case VirtualKeyCode.DELETE:
                case VirtualKeyCode.SPACE:
                case VirtualKeyCode.ESCAPE:
                case VirtualKeyCode.TAB:
                    return vk.ToString()[0] + vk.ToString().Substring(1).ToLower();
                case VirtualKeyCode.SNAPSHOT:
                    return "Print Screen";
                case VirtualKeyCode.NEXT:
                    return "Page Down";
                case VirtualKeyCode.PRIOR:
                    return "Page Up";
                case VirtualKeyCode.RETURN:
                    return "Enter";
                case VirtualKeyCode.BACK:
                    return "Backspace";
                case VirtualKeyCode.OEM_1:
                    return ":;";
                case VirtualKeyCode.OEM_2:
                    return "?/";
                case VirtualKeyCode.OEM_3:
                    return "~`";
                case VirtualKeyCode.OEM_4:
                    return "{[";
                case VirtualKeyCode.OEM_5:
                    return "|\\";
                case VirtualKeyCode.OEM_6:
                    return "}]";
                case VirtualKeyCode.OEM_7:
                    return "\" \'";
                case VirtualKeyCode.OEM_PLUS:
                    return "+";
                case VirtualKeyCode.OEM_MINUS:
                    return "-";
                case VirtualKeyCode.OEM_COMMA:
                    return ",";
                case VirtualKeyCode.OEM_PERIOD:
                    return ".";
                case VirtualKeyCode.CLEAR:
                    return "Numpad Clear";
                case VirtualKeyCode.ADD:
                    return "Numpad +";
                case VirtualKeyCode.SUBTRACT:
                    return "Numpad -";
                case VirtualKeyCode.MULTIPLY:
                    return "Numpad *";
                case VirtualKeyCode.DIVIDE:
                    return "Numpad /";
                case VirtualKeyCode.DECIMAL:
                    return "Numpad .";
                case VirtualKeyCode.MEDIA_PLAY_PAUSE:
                    return "Play/Pause";
                case VirtualKeyCode.VOLUME_DOWN:
                case VirtualKeyCode.VOLUME_MUTE:
                case VirtualKeyCode.VOLUME_UP:
                    return "Volume " + vk.ToString()[7] + vk.ToString().Substring(8).ToLower();
                default:break;
            }
            
            return vk.ToString().ToUpper()[0] + vk.ToString().Substring(1).Replace('_', ' ').ToLower();
        }
    }
}
