using System;
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
        private List<CustomButton> previousbuttons;
        private CustomButton _currentButton;
        private CustomButton currentButton
        {
            get { return _currentButton; }
            set
            {
                if(_currentButton != null)
                {
                    currentButton.PropertyWatcherChanged -= CurrentButton_PropertyWatcherChanged;
                    currentButton.BackColorChanged -= CurrentButton_BackColorChanged;
                    currentButton.ForeColorChanged -= CurrentButton_ForeColorChanged;
                    currentButton.ParametersChanged -= CurrentButton_PropertyChanged;
                    currentButton.TextChanged -= CurrentButton_PropertyChanged;
                    currentButton.LocationChanged -= CurrentButton_PropertyChanged;
                    currentButton.SizeChanged -= CurrentButton_PropertyChanged;
                    currentButton.action_typeChanged -= CurrentButton_PropertyChanged;
                }
                _currentButton = value;
                if (currentButton != null)
                {
                    previousbuttons.Add(currentButton);
                    Editpanel.Enabled = true;
                    update_ApplyCancelpanel(currentButton.property_watcher);
                    currentButton.PropertyWatcherChanged += CurrentButton_PropertyWatcherChanged;
                    disable_editpanel_events();
                    ButtonTextBox.Text = currentButton.Text;
                    ButtonParametersBox.Text = currentButton.Parameters;
                    Action_typeBox.SelectedItem = currentButton.action_type.ToString();
                    BackColorpanel.BackColor = currentButton.BackColor;
                    TextColorpanel.BackColor = currentButton.ForeColor;
                    currentButton.BackColorChanged += CurrentButton_BackColorChanged;
                    currentButton.ForeColorChanged += CurrentButton_ForeColorChanged;
                    currentButton.ParametersChanged += CurrentButton_PropertyChanged;
                    currentButton.TextChanged += CurrentButton_PropertyChanged;
                    currentButton.LocationChanged += CurrentButton_PropertyChanged;
                    currentButton.SizeChanged += CurrentButton_PropertyChanged;
                    currentButton.action_typeChanged += CurrentButton_PropertyChanged;
                    enable_editpanel_events();
                }
                else
                {
                    Editpanel.Enabled = false;
                    disable_editpanel_events();
                    ButtonTextBox.Text = "";
                    Action_typeBox.SelectedIndex = 0;
                    ButtonParametersBox.Text = "";
                    BackColorpanel.BackColor = Color.White;
                    TextColorpanel.BackColor = Color.White;
                    enable_editpanel_events();
                    update_ApplyCancelpanel(false);
                }
            }
        }

        private List<CustomButton> main_buttons;
        private object[] mouse_buttons;
        private object[] keyboard_buttons;

        private int apply_counter;

        public Settings(Form1 main)
        {
            InitializeComponent();
            this.main = main;
            main_buttons = main.buttons_array;
            panel1.Width = main.Width;
            panel1.Height = main.Height;
            tabControl1.SelectTab("Style");
            Editpanel.Enabled = false;
            update_ApplyAllCancelAllpanel(false);
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
            RegistryKey subkey = Registry.CurrentUser.OpenSubKey
                    ("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", false);
            object value = subkey.GetValue(Form1.app_name);
            if (value != null && value.ToString() == Application.ExecutablePath)
            {
                checkBox1.Checked = true;
            }
            else checkBox1.Checked = false;
            checkBox1.CheckedChanged += checkBox1_CheckedChanged;

            if (main.hook_target == Hook_target.Keyboard)
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
                CustomButton b = (CustomButton)panel1.Controls[panel1.Controls.Count - 1];
                b.MouseDown += Button_MouseDown;
                b.MouseMove += Button_MouseMove;
                b.resizer.MouseDown += resizer_MouseDown;
                b.resizer.MouseMove += resizer_MouseMove;
                b.resizer.MouseUp += resizer_MouseUp;
                b.resizer.Cursor = Cursors.SizeNWSE;
                b.resizer.BringToFront();
            }

            currentButton = null;
            previousbuttons = new List<CustomButton>();
            previousbuttons.Add(currentButton);

            apply_counter = 0;
        }

        private Color check_color_is_transparency_key(Color color)
        {
            if(color.G == main.TransparencyKey.G)
            {
                if((color.R == 0 || color.R == 1) && (color.B == 0 || color.B == 1))
                {
                    color = Color.FromArgb(255, 2, 255, 1);
                }
            }
            return color;
        }

        private void CurrentButton_PropertyWatcherChanged(object sender, PropertyWatcherEventArgs e)
        {
            if (e.previous_state == false && currentButton.property_watcher == true)
            {
                apply_counter++;
            }
            else if (e.previous_state == true && currentButton.property_watcher == false)
            {
                apply_counter--;
                if (apply_counter == 0)
                    update_ApplyAllCancelAllpanel(false);
            }
            update_ApplyCancelpanel(currentButton.property_watcher);
        }

        private void CurrentButton_PropertyChanged(object sender, EventArgs e)
        {
            currentButton.property_watcher = true;
        }

        private void CurrentButton_BackColorChanged(object sender, EventArgs e)
        {
            BackColorpanel.BackColor = currentButton.BackColor;
        }

        private void CurrentButton_ForeColorChanged(object sender, EventArgs e)
        {
            TextColorpanel.BackColor = currentButton.ForeColor;
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
            Saver.save_tab_general(main.hook_target, main.action_button_mouse, main.action_button_keyboard);
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
            Saver.save_tab_general(main.hook_target, main.action_button_mouse, main.action_button_keyboard);
        }

        private void GlobalBackColorButton_Click(object sender, EventArgs e)
        {
            ColorPicker.Color = main.global_backcolor;
            if (DialogResult.OK == ColorPicker.ShowDialog())
            {
                main.global_backcolor = ColorPicker.Color;
                Saver.save_global_backcolor(main.global_backcolor);
                int c = previousbuttons.Count;
                CustomButton customButton = currentButton;
                foreach (CustomButton button in panel1.Controls.OfType<CustomButton>())
                {
                    currentButton = button;
                    currentButton.BackColor = check_color_is_transparency_key(ColorPicker.Color);
                    currentButton.property_watcher = true;
                }
                currentButton = customButton;
                previousbuttons.RemoveRange(c, previousbuttons.Count - c);
            }
        }

        private void GlobalTextColorButton_Click(object sender, EventArgs e)
        {
            ColorPicker.Color = main.global_textcolor;
            if (DialogResult.OK == ColorPicker.ShowDialog())
            {
                main.global_textcolor = ColorPicker.Color;
                Saver.save_global_textcolor(main.global_textcolor);
                int c = previousbuttons.Count;
                CustomButton customButton = currentButton;
                foreach (CustomButton button in panel1.Controls.OfType<CustomButton>())
                {
                    currentButton = button;
                    currentButton.ForeColor = check_color_is_transparency_key(ColorPicker.Color);
                    currentButton.property_watcher = true;
                }
                currentButton = customButton;
                previousbuttons.RemoveRange(c, previousbuttons.Count - c);
            }
        }

        private void GlobalFontButton_Click(object sender, EventArgs e)
        {
            FontPicker.Font = main.global_font;
            FontPicker.ShowApply = false;
            if (DialogResult.OK == FontPicker.ShowDialog())
            {
                main.global_font = FontPicker.Font;
                Saver.save_global_font(main.global_font);
                int c = previousbuttons.Count;
                CustomButton customButton = currentButton;
                foreach (CustomButton button in panel1.Controls.OfType<CustomButton>())
                {
                    currentButton = button;
                    currentButton.Font = FontPicker.Font;
                    currentButton.property_watcher = true;
                }
                currentButton = customButton;
                previousbuttons.RemoveRange(c, previousbuttons.Count - c);
            }
        }
        #endregion

        #region tab Style
        private void resizer_MouseDown(object sender, MouseEventArgs e)
        {
            currentButton = (CustomButton)((Control)sender).Parent;
            currentButton.mouseClicked = true;
            Editpanel.Enabled = true;
        }

        private void resizer_MouseUp(object sender, MouseEventArgs e)
        {
            currentButton.mouseClicked = false;
        }

        private void resizer_MouseMove(object sender, MouseEventArgs e)
        {
            if (currentButton != null && currentButton.mouseClicked)
            {
                currentButton.Height = currentButton.resizer.Top + e.Y;
                currentButton.Width = currentButton.resizer.Left + e.X;
            }
        }

        private void Button_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                MouseDownLocation = e.Location;
                currentButton = (CustomButton)sender;
            }
        }

        private void Button_MouseMove(object sender, MouseEventArgs e)
        {
            if (MouseDownLocation == e.Location) return;
            if (e.Button == MouseButtons.Left && currentButton != null)
            {
                currentButton.Left = e.X + currentButton.Left - MouseDownLocation.X;
                currentButton.Top = e.Y + currentButton.Top - MouseDownLocation.Y;
            }
        }

        private void Cancelbutton_Click(object sender, EventArgs e)
        {
            CustomButton[] a = main_buttons.Where(x => x.Name == currentButton.Name).ToArray();
            if (a.Length == 0)
            {
                delete_current_button_from_panel();
            }
            else if (a.Length == 1)
            {
                CustomButton target_button = a[0];
                CustomButton.OverWrite(currentButton, target_button);
                currentButton.property_watcher = false;
                disable_editpanel_events();
                ButtonTextBox.Text = currentButton.Text;
                ButtonParametersBox.Text = currentButton.Parameters;
                Action_typeBox.SelectedItem = currentButton.action_type.ToString();
                enable_editpanel_events();
            }
            else throw new Exception("Identity problem");
        }

        private void Applybutton_Click(object sender, EventArgs e)
        {
            apply_changes_to_button(currentButton);
        }

        private void ApplyAllbutton_Click(object sender, EventArgs e)
        {
            CustomButton[] panel_buttons = panel1.Controls.OfType<CustomButton>().ToArray();
            foreach (CustomButton button in panel_buttons)
            {
                if (button.property_watcher)
                    apply_changes_to_button(button);
            }
            apply_counter = 0;
            update_ApplyAllCancelAllpanel(false);
        }

        private void CancelAll_Click(object sender, EventArgs e)
        {
            CustomButton[] panel_buttons = panel1.Controls.OfType<CustomButton>().ToArray();
            foreach (CustomButton button in panel_buttons)
            {
                if (button.property_watcher)
                {
                    CustomButton[] a = main_buttons.Where(x => x.Name == button.Name).ToArray();
                    if (a.Length == 0)
                    {
                        delete_button_from_panel(button);
                        if(button == currentButton)
                            currentButton = previousbuttons.Last();
                    }
                    else if(a.Length == 1)
                    {
                        CustomButton.OverWrite(button, a[0]);
                        button.property_watcher = false;
                    }
                    else if (a.Length > 1)
                    {
                        throw new Exception("Identity problem");
                    }
                }
            }
            apply_counter = 0;
            update_ApplyAllCancelAllpanel(false);
        }

        private void apply_changes_to_button(CustomButton customButton)
        {
            List<IAction> actions_storage = customButton.actions.ToList();
            customButton.actions.Clear();
            if (!parse_button_command(customButton))
            {
                customButton.actions = actions_storage;
                return;
            }
            CustomButton target_button;
            var a = main_buttons.Where(x => x.Name == customButton.Name).ToArray();

            if (a.Length == 0)
            {
                target_button = new CustomButton();
                main_buttons.Add(target_button);
                target_button.Parent = main;
                Saver.save_button_settings(customButton, true);
            }
            else if (a.Length == 1)
            {
                target_button = a[0];
                Saver.save_button_settings(customButton, false);
            }
            else throw new Exception("Identity problem");
            CustomButton.OverWrite(target_button, customButton);
            customButton.property_watcher = false;
        }

        private void ButtonTextBox_TextChanged(object sender, EventArgs e)
        {
            currentButton.Text = ButtonTextBox.Text;
        }

        private void Action_typeBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentButton.action_type =
                (Action_type)Enum.Parse(typeof(Action_type), Action_typeBox.SelectedItem.ToString());
        }

        private void ButtonParametersBox_TextChanged(object sender, EventArgs e)
        {
            currentButton.Parameters = ButtonParametersBox.Text;
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
                update_ApplyAllCancelAllpanel(true);
            }
            else
            {
                Applybutton.Enabled = Cancelbutton.Enabled = false;
            }
        }

        private void update_ApplyAllCancelAllpanel(bool flag)
        {
            ApplyAllbutton.Enabled = CancelAllbutton.Enabled = flag;
            if (!flag)
                update_ApplyCancelpanel(false);
        }

        private bool parse_Send(CustomButton customButton, string input_text)
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
                customButton.actions.Add(customButton_Send);
                return true;
            }
            else
            {
                MessageBox.Show("Syntax error in send command");
                return false;
            }
        }

        private bool parse_Run(CustomButton customButton, string input_text, bool admin)
        {
            CustomButton_Process customButton_Process;
            string[] a = input_text.Split(new[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);
            if (a.Length == 0)
                return false;
            else if (a.Length == 1)
                customButton_Process = new CustomButton_Process(admin, a[0]);
            else
                customButton_Process = new CustomButton_Process(admin, a[0], a[1]);
            customButton.actions.Add(customButton_Process);

            return true;
        }

        private bool parse_generic(CustomButton customButton, string parameters)
        {
            MatchCollection matches = Regex.Matches(parameters, @"([sS]end|[rR]un)\(([^\)]+)\)");
            if (matches.Count != 0)
            {
                foreach (Match command in matches)
                {

                    switch (command.Groups[1].Value)
                    {
                        case "Send": case "send":
                            if (!parse_Send(customButton, command.Groups[2].Value))
                                return false; break;
                        case "Run": case "run":
                            if (!parse_Run(customButton, command.Groups[2].Value, false))
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

        private bool parse_button_command(CustomButton customButton)
        {
            switch (customButton.action_type)
            {
                case Action_type.Send:
                    return parse_Send(customButton, customButton.Parameters);
                case Action_type.Run:
                    return parse_Run(customButton, customButton.Parameters, false);
                case Action_type.RunAsAdmin:
                    return parse_Run(customButton, customButton.Parameters, true);
                case Action_type.Generic:
                    return parse_generic(customButton, customButton.Parameters);
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
                currentButton.BackColor = check_color_is_transparency_key(ColorPicker.Color);
                currentButton.property_watcher = true;
            }
        }

        private void TextColorpanel_Click(object sender, EventArgs e)
        {
            ColorPicker.Color = TextColorpanel.BackColor;
            if (DialogResult.OK == ColorPicker.ShowDialog())
            {
                currentButton.ForeColor = check_color_is_transparency_key(ColorPicker.Color);
                currentButton.property_watcher = true;
            }
        }

        private void Fontbutton_Click(object sender, EventArgs e)
        {
            FontPicker.Font = currentButton.Font;
            FontPicker.ShowApply = true;
            var a = currentButton.Font;
            if (DialogResult.OK == FontPicker.ShowDialog())
            {
                currentButton.Font = FontPicker.Font;
                currentButton.property_watcher = true;
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

        private void Addbutton_Click(object sender, EventArgs e)
        {
            CustomButton b = new CustomButton();
            panel1.Controls.Add(b);
            b.Name = "customButton" + (panel1.Controls.OfType<CustomButton>().Count() + 1).ToString();
            b.Text = "button";
            b.MouseDown += Button_MouseDown;
            b.MouseMove += Button_MouseMove;
            b.resizer.MouseDown += resizer_MouseDown;
            b.resizer.MouseMove += resizer_MouseMove;
            b.resizer.MouseUp += resizer_MouseUp;
            b.resizer.Cursor = Cursors.SizeNWSE;
            b.resizer.BringToFront();
            currentButton = b;
            currentButton.property_watcher = true;
        }

        private void Deletebutton_Click(object sender, EventArgs e)
        {
            CustomButton[] a = main_buttons.Where(x => x.Name == currentButton.Name).ToArray();
            if(a.Length == 0)
            {
                delete_current_button_from_panel();
            }
            else if (a.Length == 1)
            {
                if (DialogResult.OK == MessageBox.Show("Are you sure you want to delete this button?", "Warning",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
                {
                    Saver.delete_button(currentButton);
                    a[0].Parent = null;
                    main_buttons.Remove(a[0]);
                    delete_current_button_from_panel();
                }
            }
            else if (a.Length > 1)
            {
                throw new Exception("Identity problem");
            }
        }

        private void delete_current_button_from_panel()
        {
            currentButton.property_watcher = false;
            panel1.Controls.Remove(currentButton);
            for (int i = 1; i < previousbuttons.Count; i++)
            {
                if (previousbuttons[i].Name == currentButton.Name)
                {
                    previousbuttons.RemoveAt(i);
                    i--;
                }
            }
            currentButton = previousbuttons.Last();
        }

        private void delete_button_from_panel(CustomButton button)
        {
            button.property_watcher = false;
            panel1.Controls.Remove(button);
            for (int i = 1; i < previousbuttons.Count; i++)
            {
                if (previousbuttons[i].Name == button.Name)
                {
                    previousbuttons.RemoveAt(i);
                    i--;
                }
            }
        }
        #endregion
    }
}
