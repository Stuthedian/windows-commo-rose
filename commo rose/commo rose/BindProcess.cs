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
    public partial class BindProcess : Form
    {
        private Preset current_preset;
        public BindProcess()
        {
            InitializeComponent();
            
        }

        private void dataGridView1_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            //Saver.save_add_process(current_preset_name, e.Row.Cells[0].Value.ToString());
        }

        public DialogResult ShowDialog(Preset preset)
        {
            dataGridView1.CellValueChanged -= dataGridView1_CellValueChanged;
            current_preset = preset;
            dataGridView1.Rows.Clear();
            foreach (var item in current_preset.processes)
            {
                dataGridView1.Rows.Add(item);
            }
            dataGridView1.CellValueChanged += dataGridView1_CellValueChanged;
            return ShowDialog();
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            string process_name = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            Saver.save_add_process(current_preset.name, process_name);
            current_preset.processes.Add(process_name);
        }
    }
}
