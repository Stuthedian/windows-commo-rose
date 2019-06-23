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

            List<Preset> presets = settings.presets_array;
            foreach (var item in presets)
            {
                comboBox1.Items.Add(item.name);
            }
            comboBox1.SelectedItem = "Desktop";
            base.ShowDialog();
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            Preset preset = settings.presets_array.Where(x => x.name == comboBox1.SelectedItem.ToString()).ToArray()[0];
            preset.buttons_array.Add(settings.currentButton.Clone());
            CustomButton button = preset.buttons_array.Last();
            button.Location = new Point(0, 0);
            if (!PreservebackcolorcheckBox.Checked)
                button.BackColor = preset.default_backcolor;
            if (!PreservetextcolorcheckBox.Checked)
                button.ForeColor = preset.default_textcolor;
            if (!PreservefontcheckBox.Checked)
                button.Font = preset.default_font;
            if(preset == settings.current_preset)
            {
                button.Id = settings.get_new_id();
                settings.add_button_to_panel(button.Clone());
            }
            else
                button.Id = settings.get_new_id(preset);
            Saver.save_button_settings(preset.name, button, true);

            this.Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
