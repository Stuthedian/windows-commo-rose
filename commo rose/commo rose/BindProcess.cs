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
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern IntPtr WindowFromPoint(System.Drawing.Point p);
        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        MouseOrKeyboardHook mouse_hook;

        private string old_process_name;
        private Preset current_preset;
        public BindProcess()
        {
            InitializeComponent();
        }

        public DialogResult ShowDialog(Preset preset)
        {
            dataGridView1.CellBeginEdit -= DataGridView1_CellBeginEdit;
            dataGridView1.CellEndEdit -= DataGridView1_CellEndEdit;
            dataGridView1.UserDeletingRow -= DataGridView1_UserDeletingRow;
            current_preset = preset;
            dataGridView1.Rows.Clear();
            foreach (string item in current_preset.processes)
            {
                dataGridView1.Rows.Add(item);
            }
            dataGridView1.CellBeginEdit += DataGridView1_CellBeginEdit;
            dataGridView1.CellEndEdit += DataGridView1_CellEndEdit;
            dataGridView1.UserDeletingRow += DataGridView1_UserDeletingRow;
            return ShowDialog();
        }

        private void DataGridView1_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            string name = e.Row.Cells[0].Value.ToString();
            Saver.delete_process_name(current_preset.name, name);
            current_preset.processes.Remove(name);
        }

        private void DataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            object name = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
            if (name == null)
                return;
            if(old_process_name == "")
            {
                Saver.save_add_process(current_preset.name, name.ToString());
                current_preset.processes.Add(name.ToString());
            }
            else if(old_process_name != name.ToString())
            {
                Saver.update_process_name(current_preset.name, old_process_name, name.ToString());
                int i = current_preset.processes.IndexOf(old_process_name);
                current_preset.processes.Insert(i, name.ToString());
                current_preset.processes.Remove(old_process_name);
            }
        }

        private void DataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            object name = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
            old_process_name = name == null ? "" : name.ToString();
        }

        private void get_window_handle()
        {
            label1.BackColor = Color.Yellow;
            if (mouse_hook != null)
                mouse_hook.ClearHook();
            Cursor = Cursors.Default;

            IntPtr handle = WindowFromPoint(MousePosition);
            uint process_id;
            GetWindowThreadProcessId(handle, out process_id);
            string process_name = System.Diagnostics.Process.GetProcessById((int)process_id).ProcessName;
            foreach (DataGridViewRow item in dataGridView1.Rows)
            {
                if(item.Cells[0].Value != null)
                {
                    if(item.Cells[0].Value.ToString() == process_name)
                    {
                        MessageBox.Show("Process already bound!", "Error");
                        return;
                    }
                }
            }
            dataGridView1.Rows.Add(process_name);
        }

        private void FindWindowButton_MouseDown(object sender, MouseEventArgs e)
        {
            label1.BackColor = Color.Red;
            Cursor = Cursors.Hand;
            mouse_hook = new MouseOrKeyboardHook(() => { }, get_window_handle, true);
            mouse_hook.action_button_mouse = MouseButtons.Left;
            mouse_hook.set_hook_target(Hook_target.Mouse);
        }
    }
}
