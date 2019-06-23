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
        private const int WS_EX_COMPOSITED = 0x02000000;

        private Form1 main;
        private ActionButtonDialog actionButtonForm;
        private PresetNameDialog presetName;
        private BindProcessDialog bindProcess;
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
                    disable_editpanel_events();
                    ButtonTextBox.Text = currentButton.Text;
                    ButtonParametersBox.Text = currentButton.Parameters;
                    Action_typeBox.SelectedItem = currentButton.action_type.ToString();
                    ButtonParametersBox.Enabled = currentButton.action_type == Action_type.Nothing ? false : true;
                    BackColorpanel.BackColor = currentButton.BackColor;
                    TextColorpanel.BackColor = currentButton.ForeColor;
                    currentButton.BackColorChanged += CurrentButton_BackColorChanged;
                    currentButton.ForeColorChanged += CurrentButton_ForeColorChanged;
                    currentButton.ParametersChanged += CurrentButton_PropertyChanged;
                    currentButton.TextChanged += CurrentButton_PropertyChanged;
                    currentButton.LocationChanged += CurrentButton_PropertyChanged;
                    currentButton.SizeChanged += CurrentButton_PropertyChanged;
                    currentButton.action_typeChanged += CurrentButton_PropertyChanged;
                    update_cue();
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

        private Preset current_preset;
        public List<Preset> presets_array;
        private object[] mouse_buttons;
        private object[] keyboard_buttons;

        private int apply_counter;

        public Settings(Form1 main)
        {
            InitializeComponent();
            actionButtonForm = new ActionButtonDialog(main);
            presetName = new PresetNameDialog(this);
            bindProcess = new BindProcessDialog();
            this.main = main;
            presets_array = main.presets_array;
            current_preset = presets_array.Where(x => x.name == "Desktop").ToArray()[0];
            RenamePresetButton.Enabled = false;
            DeletePresetButton.Enabled = false;
            BindPresetButton.Enabled = false;
            
            foreach (var item in presets_array)
            {
                PresetComboBox.Items.Add(item.name);
            }
            PresetComboBox.SelectedItem = current_preset.name;
            PresetComboBox.SelectedIndexChanged += PresetComboBox_SelectedIndexChanged;
            panel1.Width = main.Width;
            panel1.Height = main.Height;
            Editpanel.Enabled = false;
            update_ApplyAllCancelAllpanel(false);
            update_ApplyCancelpanel(false);
            Point point = new Point(0, 0);
            point.X += panel1.Width /2;
            point.Y += panel1.Height /2;
            CursorpictureBox.Location = point;
            
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
            RegistryKey subkey = Registry.CurrentUser.OpenSubKey
                    ("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", false);
            object value = subkey.GetValue(Form1.app_name);
            if (value != null && value.ToString() == Application.ExecutablePath)
                yesToolStripMenuItem.Checked = true;
            else
                noToolStripMenuItem.Checked = true;

            yesToolStripMenuItem.Click += yesNoToolStripMenuItem_Click;
            noToolStripMenuItem.Click += yesNoToolStripMenuItem_Click;
                     
            foreach (var item in Enum.GetNames(typeof(Action_type)))
            {
                Action_typeBox.Items.Add(item);
            }
            
            foreach (CustomButton button in main.current_preset.buttons_array)
            {
                panel1.Controls.Add(button.Clone());
                CustomButton b = (CustomButton)panel1.Controls[panel1.Controls.Count - 1];
                b.PropertyWatcherChanged += Button_PropertyWatcherChanged;
                b.MouseDown += Button_MouseDown;
                b.MouseMove += Button_MouseMove;
                b.resizer.MouseDown += resizer_MouseDown;
                b.resizer.MouseMove += resizer_MouseMove;
                b.resizer.MouseUp += resizer_MouseUp;
                b.resizer.Cursor = Cursors.SizeNWSE;
                b.resizer.BringToFront();
                b.BringToFront();
            }

            currentButton = null;
            previousbuttons = new List<CustomButton>();
            previousbuttons.Add(currentButton);
            apply_counter = 0;
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= WS_EX_COMPOSITED;
                return cp;
            }
        }

        private void Settings_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(e.CloseReason == CloseReason.UserClosing)
            {
                Hide();
                e.Cancel = true;
            }
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

        private void Button_PropertyWatcherChanged(object sender, PropertyWatcherEventArgs e)
        {
            if (e.previous_state == false && ((CustomButton)sender).property_watcher == true)
            {
                apply_counter++;
                update_ApplyAllCancelAllpanel(true);
            }
            else if (e.previous_state == true && ((CustomButton)sender).property_watcher == false) 
            {
                apply_counter--;
                if (apply_counter == 0)
                    update_ApplyAllCancelAllpanel(false);
            }
            if((CustomButton)sender == currentButton)
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
                var a = currentButton.resizer.Top + e.Y;
                var b = currentButton.resizer.Left + e.X;
                Point lower_right_corner = new Point(currentButton.Location.X + b,
                    currentButton.Location.Y + a);
                if (!panel1.Bounds.Contains(this.PointToClient(panel1.PointToScreen(lower_right_corner))))
                    return;
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
                Rectangle test_rect = currentButton.Bounds;
                test_rect.X += e.X - MouseDownLocation.X;
                test_rect.Y += e.Y - MouseDownLocation.Y;
                if (!panel1.Bounds.Contains(this.RectangleToClient(panel1.RectangleToScreen(test_rect))))
                    return;
                currentButton.Location = test_rect.Location;
            }
        }

        private void Applybutton_Click(object sender, EventArgs e)
        {
            string error_message = apply_changes_to_button(currentButton);
            if (error_message != "")
                MessageBox.Show(error_message, "Error occured");
        }

        private void ApplyAllbutton_Click(object sender, EventArgs e)
        {
            string error_message = "";
            string temp;
            CustomButton[] panel_buttons = panel1.Controls.OfType<CustomButton>().ToArray();
            foreach (CustomButton button in panel_buttons)
            {
                if (button.property_watcher)
                {
                    temp = apply_changes_to_button(button);
                    if (temp != "")
                        error_message += temp + '\n';
                }
            }
            if(error_message != "")
                MessageBox.Show(error_message, "Error occured");
        }

        private void Cancelbutton_Click(object sender, EventArgs e)
        {
            CustomButton[] a = current_preset.buttons_array.Where(x => x.Name == currentButton.Name).ToArray();
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

        private void CancelAll_Click(object sender, EventArgs e)
        {
            CustomButton[] panel_buttons = panel1.Controls.OfType<CustomButton>().ToArray();
            foreach (CustomButton button in panel_buttons)
            {
                if (button.property_watcher)
                {
                    CustomButton[] a = current_preset.buttons_array.Where(x => x.Name == button.Name).ToArray();
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
                        if (button == currentButton)
                            currentButton = button;
                    }
                    else if (a.Length > 1)
                    {
                        throw new Exception("Identity problem");
                    }
                }
            }
        }

        private string apply_changes_to_button(CustomButton customButton)
        {
            List<IAction> actions_storage = customButton.actions.ToList();
            customButton.actions.Clear();
            string error_message = parse_button_command(customButton);
            if (error_message != "")
            {
                customButton.actions = actions_storage;
                return error_message;
            }
            CustomButton target_button;
            var a = current_preset.buttons_array.Where(x => x.Name == customButton.Name).ToArray();

            if (a.Length == 0)
            {
                target_button = new CustomButton();
                current_preset.buttons_array.Add(target_button);
                if(current_preset == main.current_preset)
                    target_button.Parent = main;
                Saver.save_button_settings(current_preset.name, customButton, true);
            }
            else if (a.Length == 1)
            {
                target_button = a[0];
                Saver.save_button_settings(current_preset.name, customButton, false);
            }
            else throw new Exception("Identity problem");
            CustomButton.OverWrite(target_button, customButton);
            customButton.property_watcher = false;
            return "";
        }

        private void ButtonTextBox_TextChanged(object sender, EventArgs e)
        {
            currentButton.Text = ButtonTextBox.Text;
        }

        private void Action_typeBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentButton.action_type =
                (Action_type)Enum.Parse(typeof(Action_type), Action_typeBox.SelectedItem.ToString());

            ButtonParametersBox.Enabled =
                currentButton.action_type == Action_type.Nothing ? false : true;
            update_cue();
        }

        private void update_cue()
        {
            switch (currentButton.action_type)
            {
                case Action_type.Nothing:
                    ButtonParametersBox.Cue = "";
                    break;
                case Action_type.Send:
                    ButtonParametersBox.Cue = "Ctrl+C";
                    break;
                case Action_type.Run:
                case Action_type.RunAsAdmin:
                case Action_type.RunSilent:
                    ButtonParametersBox.Cue = "cmd";
                    break;
                case Action_type.Generic:
                    ButtonParametersBox.Cue = "send(Ctrl+V) run(cmd) send(win+e)";
                    break;
                default:throw new NotImplementedException(); break;
            }
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

        private string parse_button_command(CustomButton customButton)
        {
            switch (customButton.action_type)
            {
                case Action_type.Send:
                    return parse_Send(customButton, customButton.Parameters);
                case Action_type.Run:
                    return parse_Run(customButton, customButton.Parameters, Process_type.Normal);
                case Action_type.RunAsAdmin:
                    return parse_Run(customButton, customButton.Parameters, Process_type.Admin);
                case Action_type.RunSilent:
                    return parse_Run(customButton, customButton.Parameters, Process_type.Silent);
                case Action_type.Generic:
                    return parse_generic(customButton, customButton.Parameters);
                case Action_type.Nothing:
                    return "";
                default: return "Error";
            }
        }

        private string parse_Send(CustomButton customButton, string input_text)
        {
            CustomButton_Send customButton_Send;
            List<VirtualKeyCode> vk = new List<VirtualKeyCode>();
            const string pattern = @"^(?:(?:(?:(?:\s*)(\w+)(?:\s*)\+(?:\s*))+(\w+)(?:\s*))|(?:\s*)(\w+)(?:\s*))$";
            Match match = Regex.Match(input_text, pattern);
            if (match.Success)
            {
                foreach (Capture capture in match.Groups[1].Captures)//only group 1 have multiple captures
                {
                    VirtualKeyCode KeyCode = capture_to_VK(capture.Value);
                    if (KeyCode == VirtualKeyCode.NONAME)
                        return "Button [" + customButton.Text + "] — Syntax error in send command"
                            + ": unknown key name — " + capture.Value;
                    else
                        vk.Add(KeyCode);
                }
                foreach (Capture capture in match.Groups[2].Captures)
                {
                    VirtualKeyCode KeyCode = capture_to_VK(capture.Value);
                    if (KeyCode == VirtualKeyCode.NONAME)
                        return "Button [" + customButton.Text + "] — Syntax error in send command"
                            + ": unknown key name — " + capture.Value;
                    else
                        vk.Add(KeyCode);
                }
                foreach (Capture capture in match.Groups[3].Captures)
                {
                    VirtualKeyCode KeyCode = capture_to_VK(capture.Value);
                    if (KeyCode == VirtualKeyCode.NONAME)
                        return "Button [" + customButton.Text + "] — Syntax error in send command"
                            + ": unknown key name — " + capture.Value;
                    else
                        vk.Add(KeyCode);
                }
                customButton_Send = new CustomButton_Send(vk);
                customButton.actions.Add(customButton_Send);
                return "";
            }
            else
            {
                return "Button [" + customButton.Text + "] — Syntax error in send command";
            }
        }

        private string parse_Run(CustomButton customButton, string input_text, Process_type process_type)
        {
            CustomButton_Process customButton_Process;
            string[] a = input_text.Split(new[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);
            if (a.Length == 0)
                return "Button [" + customButton.Text + "] — Syntax error in " 
                    + customButton.action_type.ToString() + " command";
                
            else if (a.Length == 1)
                customButton_Process = new CustomButton_Process(process_type, a[0]);
            else
                customButton_Process = new CustomButton_Process(process_type, a[0], a[1]);
            customButton.actions.Add(customButton_Process);

            return "";
        }

        private string parse_generic(CustomButton customButton, string parameters)
        {
            Match match = Regex.Match(parameters,
                @"^(\s*([sS]end|[rR]un(?:[aA]s[aA]dmin|[sS]ilent)?)\(([^\)]+)\)\s*)+$");
            string error_message = "";
            if (match.Success)
            {
                for (int i = 0; i < match.Groups[2].Captures.Count; i++)
                {
                    switch (match.Groups[2].Captures[i].Value)
                    {
                        case "Send": case "send":
                            error_message = parse_Send(customButton, match.Groups[3].Captures[i].Value);
                            if (error_message != "")
                                return error_message;
                            break;
                        case "Run": case "run":
                            error_message = parse_Run(customButton, match.Groups[3].Captures[i].Value, Process_type.Normal);
                            if (error_message != "")
                                return error_message;
                            break;
                        case "RunAsAdmin":case "RunasAdmin":case "RunAsadmin":case "Runasadmin":
                        case "runAsAdmin":case "runasAdmin":case "runAsadmin":case "runasadmin":
                            error_message = parse_Run(customButton, match.Groups[3].Captures[i].Value, Process_type.Admin);
                            if (error_message != "")
                                return error_message;
                            break;
                        case "RunSilent":case "Runsilent":case "runSilent":case "runsilent":
                            error_message = parse_Run(customButton, match.Groups[3].Captures[i].Value, Process_type.Silent);
                            if (error_message != "")
                                return error_message;
                            break;
                        default: throw new NotImplementedException(); break;
                    }
                }
                return error_message;
            }
            else
            {
                return "Button [" + customButton.Text + "] — Syntax error in generic command";
            }
        }

        private static VirtualKeyCode capture_to_VK(string capture)
        {
            if (capture.Length == 1 && 
                ((capture[0] >= 'a' && capture[0] <= 'z') || (capture[0] >= 'A' && capture[0] <= 'Z')))
            {
                return (VirtualKeyCode)Enum.Parse(typeof(VirtualKeyCode), "VK_" + capture.ToUpper()[0]);
            }
            if(capture[0] == 'f' || capture[0] == 'F')
            {
                int n;
                if(int.TryParse(capture.Substring(1), out n) && n >= 1 && n <= 12)
                    return (VirtualKeyCode)Enum.Parse(typeof(VirtualKeyCode), "F" + n.ToString());
            }
            switch (capture)
            {
                case "ctrl":
                case "Ctrl":
                    return VirtualKeyCode.CONTROL;
                case "shift":
                case "Shift":
                    return VirtualKeyCode.SHIFT;
                case "alt":
                case "Alt":
                    return VirtualKeyCode.LMENU;
                case "tab":
                case "Tab":
                    return VirtualKeyCode.TAB;
                case "esc":
                case "Esc":
                case "escape":
                case "Escape":
                    return VirtualKeyCode.ESCAPE;
                case "home":
                case "Home":
                    return VirtualKeyCode.HOME;
                case "end":
                case "End":
                    return VirtualKeyCode.END;
                case "insert":
                case "Insert":
                case "ins":
                case "Ins":
                    return VirtualKeyCode.INSERT;
                case "delete":
                case "Delete":
                case "del":
                case "Del":
                    return VirtualKeyCode.DELETE;
                case "prtscn":
                case "PrtScn":
                case "Prtscn":
                case "prtScn":
                    return VirtualKeyCode.SNAPSHOT;
                case "win":
                case "Win":
                case "windows":
                case "Windows":
                    return VirtualKeyCode.LWIN;
                case "enter":
                case "Enter":
                    return VirtualKeyCode.RETURN;
                case "backspace":
                case "Backspace":
                    return VirtualKeyCode.BACK;
                case "caps":
                case "Caps":
                case "CapsLock":
                case "capslock":
                case "Capslock":
                case "capsLock":
                    return VirtualKeyCode.CAPITAL;
                case "num":
                case "Num":
                case "numLock":
                case "NumLock":
                case "numlock":
                case "Numlock":
                    return VirtualKeyCode.NUMLOCK;
                case "scroll":
                case "Scroll":
                case "scrollLock":
                case "ScrollLock":
                case "scrolllock":
                case "Scrolllock":
                    return VirtualKeyCode.SCROLL;
                default: break;
            }
            return VirtualKeyCode.NONAME;
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
            b.BackColor = current_preset.default_backcolor;
            b.ForeColor = current_preset.default_textcolor;
            b.Font = current_preset.default_font;
            b.PropertyWatcherChanged += Button_PropertyWatcherChanged;
            b.MouseDown += Button_MouseDown;
            b.MouseMove += Button_MouseMove;
            b.resizer.MouseDown += resizer_MouseDown;
            b.resizer.MouseMove += resizer_MouseMove;
            b.resizer.MouseUp += resizer_MouseUp;
            b.resizer.Cursor = Cursors.SizeNWSE;
            b.resizer.BringToFront();
            b.BringToFront();
            currentButton = b;
            currentButton.property_watcher = true;
        }

        private void Deletebutton_Click(object sender, EventArgs e)
        {
            CustomButton[] a = current_preset.buttons_array.Where(x => x.Name == currentButton.Name).ToArray();
            if(a.Length == 0)
            {
                delete_current_button_from_panel();
            }
            else if (a.Length == 1)
            {
                if (DialogResult.OK == MessageBox.Show("Are you sure you want to delete this button?", "Warning",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
                {
                    Saver.delete_button(current_preset.name, currentButton);
                    a[0].Parent = null;
                    current_preset.buttons_array.Remove(a[0]);
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

        private void defaultBackcolorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorPicker.Color = current_preset.default_backcolor;
            if (DialogResult.OK == ColorPicker.ShowDialog())
            {
                current_preset.default_backcolor = check_color_is_transparency_key(ColorPicker.Color);
                Saver.save_preset_default_backcolor(current_preset.name, current_preset.default_backcolor);
                foreach (CustomButton button in panel1.Controls.OfType<CustomButton>())
                {
                    button.BackColor = current_preset.default_backcolor;
                    button.property_watcher = true;
                }
            }
        }

        private void defaultTextcolorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorPicker.Color = current_preset.default_textcolor;
            if (DialogResult.OK == ColorPicker.ShowDialog())
            {
                current_preset.default_textcolor = check_color_is_transparency_key(ColorPicker.Color);
                Saver.save_preset_default_textcolor(current_preset.name, current_preset.default_textcolor);
                foreach (CustomButton button in panel1.Controls.OfType<CustomButton>())
                {
                    button.ForeColor = current_preset.default_textcolor;
                    button.property_watcher = true;
                }
            }
        }

        private void defaultFontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontPicker.Font = current_preset.default_font;
            FontPicker.ShowApply = false;
            if (DialogResult.OK == FontPicker.ShowDialog())
            {
                current_preset.default_font = FontPicker.Font;
                Saver.save_preset_default_font(current_preset.name, current_preset.default_font);
                foreach (CustomButton button in panel1.Controls.OfType<CustomButton>())
                {
                    button.Font = FontPicker.Font;
                    button.property_watcher = true;
                }
            }
        }

        private void yesNoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            yesToolStripMenuItem.Checked = !yesToolStripMenuItem.Checked;
            noToolStripMenuItem.Checked = !noToolStripMenuItem.Checked;
            RegistryKey rk;
            try
            {
                rk = Registry.CurrentUser.OpenSubKey
                    ("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                if (yesToolStripMenuItem.Checked)
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

        private void actionButtonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            actionButtonForm.ShowDialog();
        }

        private void AddPresetButton_Click(object sender, EventArgs e)
        {
            if(DialogResult.OK == presetName.ShowDialog(""))
            {
                Preset preset = new Preset();
                preset.name = presetName.textBox1.Text.Trim();
                preset.buttons_array = new List<CustomButton>();
                preset.default_backcolor = Color.White;
                preset.default_textcolor = Color.Black;
                preset.default_font = new Font("Consolas", 14.25F, FontStyle.Regular);
                presets_array.Add(preset);
                PresetComboBox.Items.Add(preset.name);
                PresetComboBox.SelectedItem = preset.name;
                Saver.save_new_preset(preset.name);
            }
            
        }

        private void DeletePresetButton_Click(object sender, EventArgs e)
        {
            if (current_preset.name != "Desktop")
            {
                if (DialogResult.OK == MessageBox.Show("Are you sure you want to delete [" + current_preset.name 
                    +"] preset?", "Warning",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
                {
                    Saver.delete_preset(current_preset.name);
                    presets_array.Remove(current_preset);
                    PresetComboBox.Items.Remove(current_preset.name);
                    PresetComboBox.SelectedItem = "Desktop";
                }
            }
            else
                MessageBox.Show("Desktop preset can't be deleted", "Error!");
        }

        private void RenamePresetButton_Click(object sender, EventArgs e)
        {
            if (current_preset.name != "Desktop")
            {
                if (DialogResult.OK == presetName.ShowDialog(current_preset.name))
                {
                    string newName = presetName.textBox1.Text.Trim();
                    Saver.update_preset_name(current_preset.name, newName);
                    PresetComboBox.SelectedIndexChanged -= PresetComboBox_SelectedIndexChanged;
                    PresetComboBox.Items[PresetComboBox.SelectedIndex] = newName;
                    PresetComboBox.SelectedIndexChanged += PresetComboBox_SelectedIndexChanged;
                    presets_array.Find(x => x.name == current_preset.name).name = newName;
                    current_preset.name = newName;
                }
            }
            else
                MessageBox.Show("Desktop preset can't be renamed", "Error!");
        }

        private void PresetComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            current_preset = presets_array.Where(x => x.name == PresetComboBox.SelectedItem.ToString()).ToArray()[0];
            main.current_preset = current_preset;

            if(current_preset.name == "Desktop")
            {
                RenamePresetButton.Enabled = false;
                DeletePresetButton.Enabled = false;
                BindPresetButton.Enabled = false;
            }
            else
            {
                RenamePresetButton.Enabled = true;
                DeletePresetButton.Enabled = true;
                BindPresetButton.Enabled = true;
            }

            panel1.Controls.Clear();
            panel1.Controls.Add(CursorpictureBox);
            foreach (CustomButton button in current_preset.buttons_array)
            {
                panel1.Controls.Add(button.Clone());
                CustomButton b = (CustomButton)panel1.Controls[panel1.Controls.Count - 1];
                b.PropertyWatcherChanged += Button_PropertyWatcherChanged;
                b.MouseDown += Button_MouseDown;
                b.MouseMove += Button_MouseMove;
                b.resizer.MouseDown += resizer_MouseDown;
                b.resizer.MouseMove += resizer_MouseMove;
                b.resizer.MouseUp += resizer_MouseUp;
                b.resizer.Cursor = Cursors.SizeNWSE;
                b.resizer.BringToFront();
                b.BringToFront();
            }
            currentButton = null;
            previousbuttons = new List<CustomButton>();
            previousbuttons.Add(currentButton);
            update_ApplyCancelpanel(false);
            update_ApplyAllCancelAllpanel(false);
            apply_counter = 0;
        }

        private void BindButton_Click(object sender, EventArgs e)
        {
            if(current_preset.name != "Desktop")
                bindProcess.ShowDialog(current_preset);
            else
                MessageBox.Show("Desktop preset can't be bound to a process", "Error!");
        }

        private void CopyPresetButton_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == presetName.ShowDialog(""))
            {
                Preset preset = current_preset.Clone();
                preset.name = presetName.textBox1.Text.Trim();
                presets_array.Add(preset);
                PresetComboBox.Items.Add(preset.name);
                PresetComboBox.SelectedItem = preset.name;
                Saver.save_new_preset(preset.name);
                foreach (var item in preset.buttons_array)
                {
                    Saver.save_button_settings(preset.name, item, true);
                }
            }
        }
    }
}
