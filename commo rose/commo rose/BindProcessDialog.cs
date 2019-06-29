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
    public partial class BindProcessDialog : Form
    {
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern IntPtr WindowFromPoint(System.Drawing.Point p);
        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        MouseOrKeyboardHook mouse_hook;

        private Cursor hand_cursor;
        private string old_process_name;
        private Preset current_preset;
        public BindProcessDialog()
        {
            InitializeComponent();
            hand_cursor = new Cursor(Properties.Resources.HandCursor.GetHicon());
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
            Saver.delete_process(current_preset.name, name);
            current_preset.processes.Remove(name);
        }

        private void DataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            object name = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
            if (name == null)
                return;
            if(old_process_name == "")
            {
                if (process_already_in_table(name.ToString(), e.RowIndex))
                {
                    dataGridView1.Rows.RemoveAt(e.RowIndex);
                    return;
                }
                    
                Saver.save_add_process(current_preset.name, name.ToString());
                current_preset.processes.Add(name.ToString());
            }
            else if(old_process_name != name.ToString())
            {
                if (process_already_in_table(name.ToString(), e.RowIndex))
                {
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = old_process_name;
                    return;
                }
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
            if (mouse_hook != null)
                mouse_hook.ClearHook();
            Cursor = Cursors.Default;
            
            IntPtr handle = WindowFromPoint(MousePosition);
            uint process_id;
            GetWindowThreadProcessId(handle, out process_id);
            string process_name = System.Diagnostics.Process.GetProcessById((int)process_id).ProcessName;
            if (process_already_in_table(process_name))
                return;

            dataGridView1.Rows.Add(process_name);
            Saver.save_add_process(current_preset.name, process_name.ToString());
            current_preset.processes.Add(process_name.ToString());
        }

        private bool process_already_in_table(string process_name, int ignore_index = -1)
        {
            bool in_table = false;
            foreach (DataGridViewRow item in dataGridView1.Rows)
            {
                if (item.Index == ignore_index)
                    continue;
                if (item.Cells[0].Value != null)
                {
                    if (item.Cells[0].Value.ToString() == process_name)
                    {
                        MessageBox.Show("Process already bound!", "Error");
                        in_table = true;
                        return in_table;
                    }
                }
            }
            return in_table;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            Cursor = hand_cursor;
            mouse_hook = new MouseOrKeyboardHook(Hook_target.Mouse, WindowsInput.Native.VirtualKeyCode.LBUTTON, 
                () => { }, get_window_handle, true);
        }
    }
}
