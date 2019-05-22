using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;
using System.Runtime.InteropServices;
//
using System.Text.RegularExpressions;
using System.Threading;
using WindowsInput.Native;
using WindowsInput;

namespace commo_rose
{
    public class CustomButton : Button
    {
        public bool property_changed;
        public bool Selected { get; private set; }
        public Action_type action_type;
        public string Parameters;
        private IEnumerable<VirtualKeyCode> modifier_keys;
        private IEnumerable<VirtualKeyCode> ordinary_keys;
        private InputSimulator sim;

        public CustomButton() : base()
        {
            property_changed = false;
            Selected = false;
            FlatStyle = FlatStyle.Flat;
            Font = new Font("Consolas", 14.25F, FontStyle.Regular);
            MouseEnter += switch_selection;
            MouseLeave += switch_selection;
            BackColorChanged += CustomButton_BackColorChanged;
            BackColor = Color.White;
            ForeColor = Color.Black;
            action_type = Action_type.Null;
            modifier_keys = Enumerable.Empty<VirtualKeyCode>();
            ordinary_keys = Enumerable.Empty<VirtualKeyCode>();
            sim = new InputSimulator();
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
               
        public CustomButton Clone()
        {
            CustomButton customButton = new CustomButton();
            OverWrite(customButton, this);
            return customButton;
        }

        public static void OverWrite(CustomButton destination, CustomButton source)
        {
            destination.modifier_keys = source.modifier_keys;
            destination.ordinary_keys = source.ordinary_keys;
            destination.BackColor = source.BackColor;
            destination.ForeColor = source.ForeColor;
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
                    case Action_type.Send_keys:
                        //SendKeys.SendWait(Parameters);
                        Thread thread = new Thread(() =>
                        sim.Keyboard.ModifiedKeyStroke(modifier_keys, ordinary_keys));
                        thread.IsBackground = true;
                        thread.Start();
                        break;
                    case Action_type.Run:
                        Process.Start(Parameters);
                        break;
                    case Action_type.Run_as_admin:
                        ProcessStartInfo startInfo = new ProcessStartInfo(Parameters);
                        startInfo.Verb = "runas";
                        Process.Start(startInfo);
                        break;
                    case Action_type.Send_and_Run:
                        //Thread thread = new Thread(() => interpret(Parameters));
                        //Thread thread = new Thread(() => parse_generic(Parameters));
                        //Thread thread = new Thread(() => parse_send(Parameters));
                        //thread.IsBackground = true;
                        //thread.Start();
                        ////interpret(Parameters);
                        ////create new thread for simulating keystrokes
                        //sim.Keyboard.ModifiedKeyStroke(modifier_keys, ordinary_keys);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception e) { MessageBox.Show(e.Message); }
        }

        private void interpret(string parameters)
        {
            //InputSimulator sim = new InputSimulator();
            string[] a = parameters.Split(' ');
            foreach (string item in a)
            {
                if(item[0] == '^')
                {
                    var b = item.Substring(1);
                    if(b.Substring(0,3) == "str")
                    {
                        var c = b.Substring(3);
                        sim.Keyboard.TextEntry(c);
                    }
                    switch (b)
                    {
                        case "copy":
                            sim.Keyboard.ModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_C);
                            //SendKeys.SendWait("^c");
                            Thread.Sleep(100);
                            break;
                        case "paste":
                            Thread.Sleep(100);
                            sim.Keyboard.ModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_V);
                            //SendKeys.SendWait("^v");
                            break;
                        case "lang":
                            //Class1.lang();
                            //sim.Keyboard.KeyPress(VirtualKeyCode.MENU);
                            //sim.Keyboard.KeyPress(VirtualKeyCode.SHIFT);
                            //sim.Keyboard.KeyUp(VirtualKeyCode.MENU);
                            //sim.Keyboard.KeyUp(VirtualKeyCode.SHIFT);
                            sim.Keyboard.ModifiedKeyStroke(new[] { VirtualKeyCode.SHIFT, VirtualKeyCode.LMENU },
                                Enumerable.Empty<VirtualKeyCode>());
                            //sim.Keyboard.TextEntry("Hello World");
                            Thread.Sleep(100);
                            break;
                        default: break;
                    }

                }
                else
                {
                    Process process = new Process();
                    process.StartInfo.CreateNoWindow = true;
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.FileName = item;
                    //Process.Start(startInfo);
                    process.Start();
                }
            }
        }

        private void parse_generic(string parameters)//move to settings form;
        {
            MatchCollection matches = Regex.Matches(parameters, @"([sS]end|[rR]un)\((\w+)\)");
            if (matches.Count != 0)
            {
                foreach (Match command in matches)
                {
                    
                    switch(command.Groups[1].Value)
                    {
                        case "Send":case "send":
                            MessageBox.Show(command.Groups[2].Value); break;
                        default:MessageBox.Show("switchErr-" + command.Groups[1].Value); break;
                    }
                }
            }
            else { MessageBox.Show("EmptyMatches"); }
        }

        

        public void collect_VKs(VirtualKeyCode vk)//Is correct?
        {
            if(vk == VirtualKeyCode.CONTROL || vk == VirtualKeyCode.LMENU || vk == VirtualKeyCode.SHIFT)
            {
                List<VirtualKeyCode> a = modifier_keys.ToList();
                a.Add(vk);
                modifier_keys = a.AsEnumerable();
            }
            else
            {
                List<VirtualKeyCode> a = ordinary_keys.ToList();
                a.Add(vk);
                ordinary_keys = a.AsEnumerable();
            }
        }

        public void clear_VKs()
        {
            modifier_keys = Enumerable.Empty<VirtualKeyCode>();
            ordinary_keys = Enumerable.Empty<VirtualKeyCode>();
        }
    }

    public enum Action_type { Null, Send_keys, Run, Run_as_admin, Send_and_Run }
}
