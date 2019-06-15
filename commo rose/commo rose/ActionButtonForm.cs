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
        private Form1 main;
        private object[] mouse_buttons;
        private object[] keyboard_buttons;
        public ActionButtonForm(Form1 main)
        {
            InitializeComponent();
            this.main = main;
            mouse_buttons = new object[] {
            MouseButtons.Middle.ToString(),
            MouseButtons.XButton1.ToString(),
            MouseButtons.XButton2.ToString() };
            keyboard_buttons = new object[]{
                VirtualKeyCode.SCROLL.ToString(),
                VirtualKeyCode.NUMPAD0.ToString(),
                VirtualKeyCode.NUMPAD1.ToString(),
                VirtualKeyCode.NUMPAD2.ToString(),
                VirtualKeyCode.NUMPAD3.ToString(),
                VirtualKeyCode.NUMPAD4.ToString(),
                VirtualKeyCode.NUMPAD5.ToString(),
                VirtualKeyCode.NUMPAD6.ToString(),
                VirtualKeyCode.NUMPAD7.ToString(),
                VirtualKeyCode.NUMPAD8.ToString(),
                VirtualKeyCode.NUMPAD9.ToString() };

            if (main.mouseOrKeyboardHook.hook_target == Hook_target.Keyboard)
            {
                MouseradioButton.Checked = false;
                KeyboardradioButton.Checked = true;
                MouseKeyboardButtonsComboBox.Items.AddRange(keyboard_buttons);
                MouseKeyboardButtonsComboBox.SelectedItem = main.mouseOrKeyboardHook.action_button_keyboard.ToString();
            }
            else if (main.mouseOrKeyboardHook.hook_target == Hook_target.Mouse)
            {
                MouseradioButton.Checked = true;
                KeyboardradioButton.Checked = false;
                MouseKeyboardButtonsComboBox.Items.AddRange(mouse_buttons);
                MouseKeyboardButtonsComboBox.SelectedItem = main.mouseOrKeyboardHook.action_button_mouse.ToString();
            }
            MouseradioButton.CheckedChanged += MouseradioButton_CheckedChanged;
            MouseKeyboardButtonsComboBox.SelectedIndexChanged += MouseButtonsComboBox_SelectedIndexChanged;
        }

        private void MouseradioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (main.mouseOrKeyboardHook.hook_target == Hook_target.Mouse)
            {
                main.mouseOrKeyboardHook.hook_target = Hook_target.Keyboard;
                main.mouseOrKeyboardHook.set_hook_target(main.mouseOrKeyboardHook.hook_target);
                MouseKeyboardButtonsComboBox.Items.Clear();
                MouseKeyboardButtonsComboBox.Items.AddRange(keyboard_buttons);
                MouseKeyboardButtonsComboBox.SelectedItem = main.mouseOrKeyboardHook.action_button_keyboard.ToString();
            }
            else if (main.mouseOrKeyboardHook.hook_target == Hook_target.Keyboard)
            {
                main.mouseOrKeyboardHook.hook_target = Hook_target.Mouse;
                main.mouseOrKeyboardHook.set_hook_target(main.mouseOrKeyboardHook.hook_target);
                MouseKeyboardButtonsComboBox.Items.Clear();
                MouseKeyboardButtonsComboBox.Items.AddRange(mouse_buttons);
                MouseKeyboardButtonsComboBox.SelectedItem = main.mouseOrKeyboardHook.action_button_mouse.ToString();
            }
            Saver.save_hook(main.mouseOrKeyboardHook.hook_target, main.mouseOrKeyboardHook.action_button_mouse, main.mouseOrKeyboardHook.action_button_keyboard);
        }

        private void MouseButtonsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (main.mouseOrKeyboardHook.hook_target == Hook_target.Mouse)
                main.mouseOrKeyboardHook.action_button_mouse =
                    (MouseButtons)Enum.Parse(typeof(MouseButtons), MouseKeyboardButtonsComboBox.SelectedItem.ToString());
            else if (main.mouseOrKeyboardHook.hook_target == Hook_target.Keyboard)
            {
                main.mouseOrKeyboardHook.action_button_keyboard =
                    (VirtualKeyCode)Enum.Parse(typeof(VirtualKeyCode), MouseKeyboardButtonsComboBox.SelectedItem.ToString());
            }
            Saver.save_hook(main.mouseOrKeyboardHook.hook_target, main.mouseOrKeyboardHook.action_button_mouse, main.mouseOrKeyboardHook.action_button_keyboard);
        }
    }
}
