﻿using System;
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
            this.main = main;
            panel1.Width = main.Width;
            panel1.Height = main.Height;
            
            
            foreach (CustomButton button in main.Controls.OfType<CustomButton>().ToArray())
            {
                panel1.Controls.Add(button.Clone());
                panel1.Controls[panel1.Controls.Count - 1].MouseDown += Button_MouseDown;
                panel1.Controls[panel1.Controls.Count - 1].MouseMove += Button_MouseMove;
            }
        }

        public void set_settings(CustomButton[] buttons)
        {
            if (customButton != null)
            {
                var a = panel1.Controls.OfType<CustomButton>().ToArray();
                for (int i = 0; i < buttons.Length; i++)
                {
                    //buttons[i].Location = a[i].Location;
                    //buttons[i].Text = a[i].Text;
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
                if(customButton.action_Type == Action_type.Run
                    || customButton.action_Type == Action_type.Run_as_admin)
                {
                    textBox2.Text = customButton.Parameters;
                }
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
            }
        }
    }
}
