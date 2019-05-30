using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;
using System.Threading;
using WindowsInput.Native;
using WindowsInput;

namespace commo_rose
{
    public class CustomButton : Button
    {
        public bool mouseClicked;
        public bool property_changed { get; private set; }
        public bool Selected { get; private set; }
        public Action_type action_type { get; set; }
        public string Parameters;
        public List<IAction> actions;
        public PictureBox resizer;


        public CustomButton() : base()
        {
            property_changed = false;
            Selected = false;
            FlatStyle = FlatStyle.Flat;
            Font = new Font("Consolas", 14.25F, FontStyle.Regular);
            Location = new Point(0, 0);
            TabStop = false;

            mouseClicked = false;
            resizer = new PictureBox();
            resizer.Parent = this;
            resizer.Width = 10;
            resizer.Height = 10;
            resizer.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            resizer.Location = new Point(Width - resizer.Width,
                Height - resizer.Height);
            resizer.BackColor = Color.Transparent;
            
            MouseEnter += switch_selection;
            MouseLeave += switch_selection;
            BackColorChanged += CustomButton_BackColorChanged;
            BackColor = Color.White;
            ForeColor = Color.Black;
            action_type = Action_type.Nothing;
            actions = new List<IAction>();            
        }

        protected override bool ShowFocusCues
        {
            get { return false; }
        }

        private void CustomButton_BackColorChanged(object sender, EventArgs e)
        {
            FlatAppearance.BorderColor = BackColor;
            FlatAppearance.MouseOverBackColor = BackColor;
            FlatAppearance.MouseDownBackColor = BackColor;
        }

        private void switch_selection(object sender, EventArgs e)
        {
            Selected = !Selected;
            Color tmp = BackColor;
            BackColor = ForeColor;
            ForeColor = tmp;
        }

        public void set_property_changed(bool flag, Action<bool> func)
        {
            property_changed = flag;
            func(flag);
        }

        public CustomButton Clone()
        {
            CustomButton customButton = new CustomButton();
            OverWrite(customButton, this);
            return customButton;
        }

        public static void OverWrite(CustomButton destination, CustomButton source)
        {
            destination.actions = source.actions.ToList();
            destination.BackColor = source.BackColor;
            destination.ForeColor = source.ForeColor;
            destination.Font = source.Font;
            destination.Width = source.Width;
            destination.Height = source.Height;
            destination.Name = source.Name;
            destination.Text = source.Text;
            destination.Location = source.Location;
            destination.action_type = source.action_type;
            destination.Parameters = source.Parameters;
        }

        public void Act()
        {
            try
            {
                switch (action_type)
                {
                    case Action_type.Send:
                    case Action_type.Run:
                    case Action_type.RunAsAdmin:
                        actions[0].exec();
                        break;
                    case Action_type.Generic:
                        Thread thread = new Thread(() =>
                        {
                            try
                            {
                                foreach (IAction action in actions)
                                {
                                    action.exec();
                                    Thread.Sleep(100);
                                }
                            }
                            catch (Exception e) { MessageBox.Show(e.Message); }
                        });
                        thread.IsBackground = true;
                        thread.Start();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception e) { MessageBox.Show(e.Message); }
        }
    }   

    public enum Action_type { Nothing, Send, Run, RunAsAdmin, Generic }

    public interface IAction
    {
        void exec();
    }

    public class CustomButton_Process : IAction
    {
        public Process process;
        public CustomButton_Process(bool admin, string command, string command_args = "")
        {
            process = new Process();
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.FileName = command;
            process.StartInfo.Arguments = command_args;
            if (admin)
                process.StartInfo.Verb = "runas";
        }
        public void exec()
        {
            process.Start();
        }
    }

    public class CustomButton_Send : IAction
    {
        private static InputSimulator inputSimulator;
        public IEnumerable<VirtualKeyCode> modifier_keys;
        public IEnumerable<VirtualKeyCode> ordinary_keys;

        private bool is_Vk_modifier_key(VirtualKeyCode vk)//Is correct?
        {
            if (vk == VirtualKeyCode.CONTROL || vk == VirtualKeyCode.LMENU || vk == VirtualKeyCode.SHIFT
                || vk == VirtualKeyCode.LWIN)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        static CustomButton_Send()
        {
            inputSimulator = new InputSimulator();
        }

        public CustomButton_Send(List<VirtualKeyCode> virtualKeys)
        {
            List<VirtualKeyCode> m = new List<VirtualKeyCode>();
            List<VirtualKeyCode> o = new List<VirtualKeyCode>();
            foreach (VirtualKeyCode vk in virtualKeys)
            {
                if (is_Vk_modifier_key(vk))
                {
                    m.Add(vk);
                }
                else { o.Add(vk); }
            }
            modifier_keys = m;
            ordinary_keys = o;
        }

        public void exec()
        {
            inputSimulator.Keyboard.ModifiedKeyStroke(modifier_keys, ordinary_keys);
        }
    }
}
