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

namespace commo_rose
{
    public partial class Settings : Form
    {
        private Form1 main;
        private CustomButton[] buttons;

        public Settings(Form1 main)
        {
            InitializeComponent();
            this.main = main;
            buttons = main.Controls.OfType<CustomButton>().ToArray();
            panel1.Width = main.Width;
            panel1.Height = main.Height;

            
            foreach (CustomButton button in buttons)
            {
                panel1.Controls.Add(button.Clone());
                panel1.Controls[panel1.Controls.Count - 1].MouseDown += Button_MouseDown;
                panel1.Controls[panel1.Controls.Count - 1].MouseMove += Button_MouseMove;
            }
            foreach (var item in Enum.GetNames(typeof(Action_type)))
            {
                comboBox1.Items.Add(item);
            }
        }

        private void set_setting()
        {
            if (customButton != null)
            {
                var target_button = buttons.Where(x => x.Name == customButton.Name).ToArray()[0];
                CustomButton.OverWrite(target_button, customButton);
            }
        }
        private Point MouseDownLocation;
        private CustomButton customButton;


        private void Button_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                MouseDownLocation = e.Location;
                customButton = (CustomButton)sender;
                textBox1.Text = customButton.Text;
                textBox2.Text = customButton.Parameters;
                comboBox1.SelectedItem = customButton.action_Type.ToString();
            }
        }

        private void Button_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && customButton != null)
            {
                customButton.Left = e.X + customButton.Left - MouseDownLocation.X;
                customButton.Top = e.Y + customButton.Top - MouseDownLocation.Y;
            }
        }

        private void Applybutton_Click(object sender, EventArgs e)
        {
            if (customButton != null)
            {
                customButton.Text = textBox1.Text;
                customButton.Parameters = textBox2.Text;
                customButton.action_Type =
                    (Action_type)Enum.Parse(typeof(Action_type), comboBox1.SelectedItem.ToString());
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
    }
}
