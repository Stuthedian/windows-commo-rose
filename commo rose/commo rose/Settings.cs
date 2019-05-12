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

namespace commo_rose
{
    public partial class Settings : Form
    {
        private Form1 main;
        private Point MouseDownLocation;
        private CustomButton currentButton;
        private CustomButton[] buttons;

        public Settings(Form1 main)
        {
            InitializeComponent();
            this.main = main;
            buttons = main.Controls.OfType<CustomButton>().ToArray();
            panel1.Width = main.Width;
            panel1.Height = main.Height;
            tabControl1.SelectTab("Style");

            RegistryKey subkey = Registry.CurrentUser.OpenSubKey
                    ("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", false);
            object value = subkey.GetValue(Form1.app_name);
            if (value != null && value.ToString() == Application.ExecutablePath)
            {
                    checkBox1.Checked = true;
            }   
            else checkBox1.Checked = false;
            checkBox1.CheckedChanged += checkBox1_CheckedChanged;

            ActionButtonBox.Text = main.action_button.ToString();

            MouseButtonsBox.Location = ActionButtonBox.Location;
            MouseButtonsBox.Width = ActionButtonBox.Width;
            MouseButtonsBox.Height = ActionButtonBox.Height;
            KeyboardradioButton.Checked = true;
            MouseButtonsBox.Visible = false;
            ActionButtonBox.Visible = true;

            foreach (CustomButton button in buttons)
            {
                panel1.Controls.Add(button.Clone());
                panel1.Controls[panel1.Controls.Count - 1].MouseDown += Button_MouseDown;
                panel1.Controls[panel1.Controls.Count - 1].MouseMove += Button_MouseMove;
            }
            foreach (var item in Enum.GetNames(typeof(Action_type)))
            {
                Action_typeBox.Items.Add(item);
            }
        }

        private void set_setting()
        {
            if (currentButton != null)
            {
                var target_button = buttons.Where(x => x.Name == currentButton.Name).ToArray()[0];
                CustomButton.OverWrite(target_button, currentButton);
            }
        }


        private void Button_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                MouseDownLocation = e.Location;
                currentButton = (CustomButton)sender;
                ButtonTextBox.Text = currentButton.Text;
                ButtonParametersBox.Text = currentButton.Parameters;
                Action_typeBox.SelectedItem = currentButton.action_Type.ToString();
            }
        }

        private void Button_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && currentButton != null)
            {
                currentButton.Left = e.X + currentButton.Left - MouseDownLocation.X;
                currentButton.Top = e.Y + currentButton.Top - MouseDownLocation.Y;
            }
        }

        private void Applybutton_Click(object sender, EventArgs e)
        {
            if (currentButton != null)
            {
                currentButton.Text = ButtonTextBox.Text;
                currentButton.Parameters = ButtonParametersBox.Text;
                currentButton.action_Type =
                    (Action_type)Enum.Parse(typeof(Action_type), Action_typeBox.SelectedItem.ToString());
                //button location already set 
                set_setting();
            }
        }

        private void Savebutton_Click(object sender, EventArgs e)
        {
            Applybutton_Click(new object(), EventArgs.Empty);
            XmlNode node;
            foreach (CustomButton button in panel1.Controls.OfType<CustomButton>().ToArray())
            {
                node = main.doc.DocumentElement.SelectSingleNode(button.Name);
                node.Attributes[button.Name + ".Location.X"].Value = button.Location.X.ToString();
                node.Attributes[button.Name + ".Location.Y"].Value = button.Location.Y.ToString();
                node.Attributes[button.Name + ".Text"].Value = button.Text;
                node.Attributes[button.Name + ".action_Type"].Value = button.action_Type.ToString();
                node.Attributes[button.Name + ".Parameters"].Value = button.Parameters;
                main.doc.Save(Form1.settings_filename);
            }
        }

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

        private void ActionButtonBox_MouseDown(object sender, MouseEventArgs e)
        {
            ActionButtonBox.KeyDown += ActionButtonBox_KeyDown;
        }

        private void ActionButtonBox_KeyDown(object sender, KeyEventArgs e)
        {
            string received_key = e.KeyCode.ToString();
            ActionButtonBox.Text = received_key == "Next" ? "PageDown" : received_key;
            ActionButtonBox.KeyDown -= ActionButtonBox_KeyDown;
            main.change_action_button(e.KeyCode);
        }

        private void MouseradioButton_CheckedChanged(object sender, EventArgs e)
        {
            MouseButtonsBox.Visible = !MouseButtonsBox.Visible;
            ActionButtonBox.Visible = !ActionButtonBox.Visible;
        }
    }
}
