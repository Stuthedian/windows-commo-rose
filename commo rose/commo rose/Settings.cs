﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using Microsoft.Win32;
using System.Text.RegularExpressions;
using WindowsInput.Native;
using System.Diagnostics;

namespace commo_rose
{
    public partial class Settings : Form
    {
        private Form1 main;
        private Point MouseDownLocation;
        private CustomButton currentButton;
        private CustomButton[] main_buttons;
        private object[] mouse_buttons;
        private object[] keyboard_buttons;
        public Settings(Form1 main)
        {
            InitializeComponent();
            this.main = main;
            main_buttons = main.Controls.OfType<CustomButton>().ToArray();
            panel1.Width = main.Width;
            panel1.Height = main.Height;
            tabControl1.SelectTab("Style");
            Editpanel.Enabled = false;
            update_SaveCancelAllpanel(false);
            update_ApplyCancelpanel(false);
            Point point = panel1.Location;
            point.X += panel1.Width /2;
            point.Y += panel1.Height /2;
            point.X -= pictureBox1.Width / 2;
            point.Y -= pictureBox1.Height / 2;
            pictureBox1.Location = point;
            mouse_buttons = new object[] {
            MouseButtons.Middle.ToString(),
            MouseButtons.XButton1.ToString(),
            MouseButtons.XButton2.ToString() };
            keyboard_buttons = new object[]{
                Keys.Scroll.ToString(),
                Keys.NumPad0.ToString(),
                Keys.NumPad1.ToString(),
                Keys.NumPad2.ToString(),
                Keys.NumPad3.ToString(),
                Keys.NumPad4.ToString(),
                Keys.NumPad5.ToString(),
                Keys.NumPad6.ToString(),
                Keys.NumPad7.ToString(),
                Keys.NumPad8.ToString(),
                Keys.NumPad9.ToString() };

            //RegistryKey subkey = Registry.CurrentUser.OpenSubKey
            //        ("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", false);
            //object value = subkey.GetValue(Form1.app_name);
            //if (value != null && value.ToString() == Application.ExecutablePath)
            //{
            //        checkBox1.Checked = true;
            //}   
            //else checkBox1.Checked = false;
            //checkBox1.CheckedChanged += checkBox1_CheckedChanged;

            if(main.hook_target == Hook_target.Keyboard)
            {
                MouseradioButton.Checked = false;
                KeyboardradioButton.Checked = true;
                MouseKeyboardButtonsComboBox.Items.AddRange(keyboard_buttons);
                MouseKeyboardButtonsComboBox.SelectedItem = main.action_button_keyboard.ToString();
            }
            else if (main.hook_target == Hook_target.Mouse)
            {
                MouseradioButton.Checked = true;
                KeyboardradioButton.Checked = false;
                MouseKeyboardButtonsComboBox.Items.AddRange(mouse_buttons);
                MouseKeyboardButtonsComboBox.SelectedItem = main.action_button_mouse.ToString();
            }
            MouseradioButton.CheckedChanged += MouseradioButton_CheckedChanged;
                     
            foreach (var item in Enum.GetNames(typeof(Action_type)))
            {
                Action_typeBox.Items.Add(item);
            }
            
            foreach (CustomButton button in main_buttons)
            {
                panel1.Controls.Add(button.Clone());
                panel1.Controls[panel1.Controls.Count - 1].MouseDown += Button_MouseDown;
                panel1.Controls[panel1.Controls.Count - 1].MouseMove += Button_MouseMove;
            }
        }
             


        #region tab General
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            RegistryKey rk;
            try
            {
                rk = Registry.CurrentUser.OpenSubKey
                    ("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                if (checkBox1.Checked)
                {
                    rk.SetValue(Form1.app_name, Application.ExecutablePath);
                }
                else
                {
                    rk.DeleteValue(Form1.app_name, false);
                }
                rk.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void MouseradioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (main.hook_target == Hook_target.Mouse)
            {
                main.mouseHook.ClearHook();
                main.ghk = new KeyHandler(main.action_button_keyboard, main.form_handle);
                main.ghk.Register();
                main.hook_target = Hook_target.Keyboard;
                MouseKeyboardButtonsComboBox.Items.Clear();
                MouseKeyboardButtonsComboBox.Items.AddRange(keyboard_buttons);
                MouseKeyboardButtonsComboBox.SelectedItem = main.action_button_keyboard.ToString();
            }
            else if (main.hook_target == Hook_target.Keyboard)
            {
                main.ghk.Unregister();
                main.mouseHook = new MouseHook(main.LowLevelMouseProc);
                main.hook_target = Hook_target.Mouse;
                MouseKeyboardButtonsComboBox.Items.Clear();
                MouseKeyboardButtonsComboBox.Items.AddRange(mouse_buttons);
                MouseKeyboardButtonsComboBox.SelectedItem = main.action_button_mouse.ToString();
            }
        }

        private void MouseButtonsBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (main.hook_target == Hook_target.Mouse)
                main.action_button_mouse =
                    (MouseButtons)Enum.Parse(typeof(MouseButtons), MouseKeyboardButtonsComboBox.SelectedItem.ToString());
            else if (main.hook_target == Hook_target.Keyboard)
            {
                main.action_button_keyboard =
                    (Keys)Enum.Parse(typeof(Keys), MouseKeyboardButtonsComboBox.SelectedItem.ToString());
                main.ghk.Unregister();
                main.ghk = new KeyHandler(main.action_button_keyboard, main.form_handle);
                main.ghk.Register();
            }
                
        }
        #endregion

        #region tab Style
        private void Button_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                MouseDownLocation = e.Location;
                currentButton = (CustomButton)sender;
                Editpanel.Enabled = true;
                update_ApplyCancelpanel(currentButton.property_changed);
                disable_editpanel_events();
                ButtonTextBox.Text = currentButton.Text;
                ButtonParametersBox.Text = currentButton.Parameters;
                Action_typeBox.SelectedItem = currentButton.action_type.ToString();
                BackColorpanel.BackColor = currentButton.ForeColor;
                TextColorpanel.BackColor = currentButton.BackColor;
                enable_editpanel_events();
            }
        }

        private void Button_MouseMove(object sender, MouseEventArgs e)
        {
            if (MouseDownLocation == e.Location) return;
            if (e.Button == MouseButtons.Left && currentButton != null)
            {
                currentButton.Left = e.X + currentButton.Left - MouseDownLocation.X;
                currentButton.Top = e.Y + currentButton.Top - MouseDownLocation.Y;
                currentButton.property_changed = true;
                update_ApplyCancelpanel(currentButton.property_changed);
            }
        }

        private void Cancelbutton_Click(object sender, EventArgs e)
        {
            CustomButton target_button = main_buttons.Where(x => x.Name == currentButton.Name).ToArray()[0];
            CustomButton.OverWrite(currentButton, target_button);
            currentButton.property_changed = false;
            update_ApplyCancelpanel(currentButton.property_changed);
            disable_editpanel_events();
            ButtonTextBox.Text = currentButton.Text;
            ButtonParametersBox.Text = currentButton.Parameters;
            Action_typeBox.SelectedItem = currentButton.action_type.ToString();
            enable_editpanel_events();
        }

        private void Applybutton_Click(object sender, EventArgs e)
        {
            List<IAction> actions_storage = currentButton.actions.ToList();
            currentButton.actions.Clear();
            if(!parse_button_command())
            {
                currentButton.actions = actions_storage;
                return;
            }
                   
            CustomButton target_button = main_buttons.Where(x => x.Name == currentButton.Name).ToArray()[0];
            CustomButton.OverWrite(target_button, currentButton);
            currentButton.property_changed = false;
            update_ApplyCancelpanel(currentButton.property_changed);
        }

        private void Savebutton_Click(object sender, EventArgs e)
        {
            CustomButton[] panel_buttons = panel1.Controls.OfType<CustomButton>().ToArray();
            foreach (CustomButton button in panel_buttons)
            {
                currentButton = button;
                Applybutton_Click(new object(), EventArgs.Empty);
            }
            Saver.save_settings(panel_buttons);
            update_SaveCancelAllpanel(false);
        }

        private void CancelAll_Click(object sender, EventArgs e)
        {
            foreach (CustomButton button in main_buttons)
            {
                CustomButton b = (CustomButton)panel1.Controls.Find(button.Name, false)[0];
                CustomButton.OverWrite(b, button);
            }
            update_SaveCancelAllpanel(false);
        }

        private void ButtonTextBox_TextChanged(object sender, EventArgs e)
        {
            currentButton.Text = ButtonTextBox.Text;
            Applybutton.Enabled =
            currentButton.property_changed = true;
            update_ApplyCancelpanel(currentButton.property_changed);
        }

        private void Action_typeBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentButton.action_type =
                (Action_type)Enum.Parse(typeof(Action_type), Action_typeBox.SelectedItem.ToString());
            currentButton.property_changed = true;
            update_ApplyCancelpanel(currentButton.property_changed);
        }

        private void ButtonParametersBox_TextChanged(object sender, EventArgs e)
        {
            currentButton.Parameters = ButtonParametersBox.Text;
            currentButton.property_changed = true;
            update_ApplyCancelpanel(currentButton.property_changed);
        }

        private void disable_editpanel_events()
        {
            ButtonTextBox.TextChanged -= ButtonTextBox_TextChanged;
            Action_typeBox.SelectedIndexChanged -= Action_typeBox_SelectedIndexChanged;
            ButtonParametersBox.TextChanged -= ButtonParametersBox_TextChanged;
        }

        private void enable_editpanel_events()
        {
            ButtonTextBox.TextChanged += ButtonTextBox_TextChanged;
            Action_typeBox.SelectedIndexChanged += Action_typeBox_SelectedIndexChanged;
            ButtonParametersBox.TextChanged += ButtonParametersBox_TextChanged;
        }

        private void update_ApplyCancelpanel(bool flag)
        {
            if (flag)
            {
                Applybutton.Enabled = Cancelbutton.Enabled = true;
                update_SaveCancelAllpanel(true);
            }
            else
            {
                Applybutton.Enabled = Cancelbutton.Enabled = false;
            }
        }

        private void update_SaveCancelAllpanel(bool flag)
        {
            Savebutton.Enabled = CancelAllbutton.Enabled = flag;
            if (!flag)
                update_ApplyCancelpanel(false);
        }

        private bool parse_Send(string input_text)
        {
            CustomButton_Send customButton_Send;
            List<VirtualKeyCode> vk = new List<VirtualKeyCode>();
            string pattern = @"^(?:(?:(?:(\w+)\+)+(\w+))|(\w+))$";//Is sufficient?
            Match match = Regex.Match(input_text, pattern);
            if (match.Success)
            {
                foreach (Capture capture in match.Groups[1].Captures)//only group 1 have multiple captures
                {
                    vk.Add(capture_to_VK(capture.Value));
                }
                foreach (Capture capture in match.Groups[2].Captures)
                {
                    vk.Add(capture_to_VK(capture.Value));
                }
                foreach (Capture capture in match.Groups[3].Captures)
                {
                    vk.Add(capture_to_VK(capture.Value));
                }
                customButton_Send = new CustomButton_Send(vk);
                currentButton.actions.Add(customButton_Send);
                return true;
            }
            else
            {
                MessageBox.Show("Syntax error in send command");
                return false;
            }
        }

        private bool parse_Run(string input_text, bool admin)
        {
            CustomButton_Process customButton_Process;
            string[] a = input_text.Split(new[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);
            if (a.Length == 0)
                return false;
            else if (a.Length == 1)
                customButton_Process = new CustomButton_Process(admin, a[0]);
            else
                customButton_Process = new CustomButton_Process(admin, a[0], a[1]);
            currentButton.actions.Add(customButton_Process);

            return true;
        }

        private bool parse_generic(string parameters)
        {
            MatchCollection matches = Regex.Matches(parameters, @"([sS]end|[rR]un)\(([^\)]+)\)");
            if (matches.Count != 0)
            {
                foreach (Match command in matches)
                {

                    switch (command.Groups[1].Value)
                    {
                        case "Send": case "send":
                            if (!parse_Send(command.Groups[2].Value))
                                return false; break;
                        case "Run": case "run":
                            if (!parse_Run(command.Groups[2].Value, false))
                                return false; break;
                        default: break;
                    }
                }
                return true;
            }
            else
            {
                MessageBox.Show("Syntax error in generic command");
                return false;
            }
        }

        private static VirtualKeyCode capture_to_VK(string capture)//Incomplete
        {
            string lowstr = capture.ToLower();
            if (lowstr.Length == 1 && lowstr[0] >= 'a' && lowstr[0] <= 'z')
            {
                return (VirtualKeyCode)Enum.Parse(typeof(VirtualKeyCode), "VK_" + lowstr.ToUpper()[0]);
            }
            switch (lowstr)
            {
                case "ctrl":
                    return VirtualKeyCode.CONTROL;
                case "shift":
                    return VirtualKeyCode.SHIFT;
                case "alt":
                    return VirtualKeyCode.LMENU;
                case "tab":
                    return VirtualKeyCode.TAB;
                case "esc":
                    return VirtualKeyCode.ESCAPE;
                case "home":
                    return VirtualKeyCode.HOME;
                case "end":
                    return VirtualKeyCode.END;
                case "insert":
                    return VirtualKeyCode.INSERT;
                case "delete":
                    return VirtualKeyCode.DELETE;
                case "prtscn":
                    return VirtualKeyCode.SNAPSHOT;
                case "win":
                    return VirtualKeyCode.LWIN;
                default:
                    break;
            }
            return VirtualKeyCode.NONAME;
        }

        private bool parse_button_command()
        {
            switch (currentButton.action_type)
            {
                case Action_type.Send:
                    return parse_Send(currentButton.Parameters);
                case Action_type.Run:
                    return parse_Run(currentButton.Parameters, false);
                case Action_type.RunAsAdmin:
                    return parse_Run(currentButton.Parameters, true);
                case Action_type.Generic:
                    return parse_generic(currentButton.Parameters);
                case Action_type.Nothing:
                    return true;
                default: return false;
            }
        }

        private void BackColorpanel_Click(object sender, EventArgs e)
        {
            ColorPicker.Color = BackColorpanel.BackColor;
            if (DialogResult.OK == ColorPicker.ShowDialog())
            {
                BackColorpanel.BackColor = ColorPicker.Color;
                currentButton.BackColor = BackColorpanel.BackColor;
                currentButton.property_changed = true;
                update_ApplyCancelpanel(currentButton.property_changed);
            }
        }

        private void TextColorpanel_Click(object sender, EventArgs e)
        {
            ColorPicker.Color = TextColorpanel.BackColor;
            if (DialogResult.OK == ColorPicker.ShowDialog())
            {
                TextColorpanel.BackColor = ColorPicker.Color;
                currentButton.ForeColor = TextColorpanel.BackColor;
                currentButton.property_changed = true;
                update_ApplyCancelpanel(currentButton.property_changed);
            }
        }

        private void Fontbutton_Click(object sender, EventArgs e)
        {
            FontPicker.Font = currentButton.Font;
            var a = currentButton.Font;
            if (DialogResult.OK == FontPicker.ShowDialog())
            {
                currentButton.property_changed = true;
                update_ApplyCancelpanel(currentButton.property_changed);
            }
            else
            {
                currentButton.Font = a;
            }
        }

        private void FontPicker_Apply(object sender, EventArgs e)
        {
            currentButton.Font = FontPicker.Font;
        }
        #endregion

    }
}
