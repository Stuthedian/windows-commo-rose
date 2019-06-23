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
    public partial class CopyButtonDialog : Form
    {
        private Settings settings;
        public CopyButtonDialog(Settings settings)
        {
            InitializeComponent();
            this.settings = settings;
        }

        public new void ShowDialog()
        {
            comboBox1.Items.Clear();
            PreservebackcolorcheckBox.Checked = false;
            PreservetextcolorcheckBox.Checked = false;
            PreservefontcheckBox.Checked = false;

            Preset[] presets = settings.presets_array.Where(x => x.name != settings.current_preset.name).ToArray();
            foreach (var item in presets)
            {
                comboBox1.Items.Add(item.name);
            }

            base.ShowDialog();
            //if (DialogResult.OK == )
            {
                //Preset a = settings.presets_array.Where(x => x.name == comboBox1.SelectedItem.ToString()).ToArray()[0];
                //a.buttons_array.Add(settings.currentButton.Clone());
                //CustomButton b = a.buttons_array.Last();
                //if (!PreservebackcolorcheckBox.Checked)
                //    b.BackColor = a.default_backcolor;
                //if (!PreservetextcolorcheckBox.Checked)
                //    b.ForeColor = a.default_textcolor;
                //if (!PreservefontcheckBox.Checked)
                //    b.Font = a.default_font;
            }
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            Preset preset = settings.presets_array.Where(x => x.name == comboBox1.SelectedItem.ToString()).ToArray()[0];
            preset.buttons_array.Add(settings.currentButton.Clone());
            CustomButton button = preset.buttons_array.Last();
            button.Name = "customButton" + (preset.buttons_array.Count() + 1).ToString();
            if (!PreservebackcolorcheckBox.Checked)
                button.BackColor = preset.default_backcolor;
            if (!PreservetextcolorcheckBox.Checked)
                button.ForeColor = preset.default_textcolor;
            if (!PreservefontcheckBox.Checked)
                button.Font = preset.default_font;
            Saver.save_button_settings(preset.name, button, true);

            this.Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
