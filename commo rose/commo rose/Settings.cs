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
        private XmlDocument doc;
        public Settings(Form1 main, XmlDocument doc)
        {
            InitializeComponent();
            this.main = main;
            this.doc = doc;
            panel1.Width = main.Width;
            panel1.Height = main.Height;

            
            foreach (CustomButton button in main.Controls.OfType<CustomButton>().ToArray())
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

        public void set_settings(CustomButton[] buttons)
        {
            if (customButton != null)
            {
                var a = panel1.Controls.OfType<CustomButton>().ToArray();
                for (int i = 0; i < buttons.Length; i++)
                {
                    customButton.OverWrite(buttons[i], a[i]);
                }
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (customButton != null)
            {
                customButton.Text = textBox1.Text;
                customButton.Parameters = textBox2.Text;
                customButton.action_Type =
                    (Action_type)Enum.Parse(typeof(Action_type), comboBox1.SelectedItem.ToString());
                XmlNode node = doc.DocumentElement.SelectSingleNode("customButton1");
                node.Attributes["customButton1.Location.X"].Value = customButton.Location.X.ToString();
                node.Attributes["customButton1.Location.Y"].Value = customButton.Location.Y.ToString();
                doc.Save(Form1.settings_filename);
            }
        }
    }
}
