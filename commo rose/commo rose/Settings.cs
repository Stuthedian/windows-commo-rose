using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace commo_rose
{
    public partial class Settings : Form
    {
        private Form1 main;
        public Settings(Form1 main)
        {
            InitializeComponent();
            //panel1.Width = main.Width / 2;
            //panel1.Height = main.Height / 2;
            this.main = main;
            panel1.Width = main.Width;
            panel1.Height = main.Height;
            foreach (CustomButton button in main.Controls.OfType<CustomButton>().ToArray())
            {
                //PictureBox pictureBox = new PictureBox();
                //pictureBox.BackColor = Color.Red;
                //pictureBox.Location = button.Location;
                //pictureBox.Width = button.Width;
                //pictureBox.Height = button.Height;
                //pictureBox.MouseDown += pictureBox1_MouseDown;
                //pictureBox.MouseMove += pictureBox1_MouseMove;
                panel1.Controls.Add(button.Clone());
                //button.MouseDown += Button_MouseDown;
                //button.MouseMove += Button_MouseMove;
                panel1.Controls[panel1.Controls.Count - 1].MouseDown += pictureBox1_MouseDown;
                panel1.Controls[panel1.Controls.Count - 1].MouseMove += pictureBox1_MouseMove;
            }
        }

        public void set_settings(CustomButton[] buttons)
        {
            var a = panel1.Controls.OfType<CustomButton>().ToArray();
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].Location = a[i].Location;
            }
        }
        private Point MouseDownLocation;
        private CustomButton box;


        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                MouseDownLocation = e.Location;
                box = (CustomButton)sender;
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left && box != null)
            {
                box.Left = e.X + box.Left - MouseDownLocation.X;
                box.Top = e.Y + box.Top - MouseDownLocation.Y;
            }
        }
    }
}
