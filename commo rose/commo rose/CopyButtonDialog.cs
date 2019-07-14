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
        private ButtonsForm buttons_form;
        private Settings settings;
        public CopyButtonDialog(Settings settings, ButtonsForm buttons_form)
        {
            InitializeComponent();
            Icon = Properties.Resources.icon;
            this.buttons_form = buttons_form;
            this.settings = settings;
        }

        public new void ShowDialog()
        {
            comboBox1.Items.Clear();
            PreservebackcolorcheckBox.Checked = false;
            PreservetextcolorcheckBox.Checked = false;
            PreservefontcheckBox.Checked = false;

            List<Preset> presets = settings.presets;
            foreach (var item in presets)
            {
                comboBox1.Items.Add(item.name);
            }
            comboBox1.SelectedItem = "Desktop";
            base.ShowDialog();
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            Preset preset = settings.presets.Where(x => x.name == comboBox1.SelectedItem.ToString()).Single();
            CustomButton button = settings.currentButton.Clone();
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
            if (preset == buttons_form.current_preset)
                buttons_form.add_button_if_auto_switch_disabled(button);
            preset.buttons.Add(button);
            Saver.save_button_settings(preset.name, button, true);

            this.Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
