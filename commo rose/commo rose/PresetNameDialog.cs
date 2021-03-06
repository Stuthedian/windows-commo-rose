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
    public partial class PresetNameDialog : Form
    {
        List<Preset> presets;
        string oldName;
        public PresetNameDialog(List<Preset> presets)
        {
            InitializeComponent();
            Icon = Properties.Resources.icon;
            this.presets = presets; 
        }

        private void Okbutton_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text.Trim();
            if (name == "")
            {
                MessageBox.Show("Invalid preset name!");
                return;
            }
            if(oldName != name && presets.Any(x => x.name == name))
            {
                MessageBox.Show("Preset name already in use!");
                return;
            }
            DialogResult = DialogResult.OK;
            Close();
        }

        public DialogResult ShowDialog(string name)
        {
            oldName = textBox1.Text = name;
            return ShowDialog();
        }
    }
}
