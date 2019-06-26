using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using WindowsInput.Native;
using System.Drawing;
using System.Windows.Forms;

namespace commo_rose
{
    static class Saver
    {
        private static XmlDocument doc;
        private static XmlNode preset_node;
        private static readonly string path_to_settings_file = 
            Path.Combine(Application.StartupPath, ".settings.xml");

        private static void create_new_settings_file()
        {
            XmlWriter writer = XmlWriter.Create(path_to_settings_file);
            writer.WriteStartDocument();
            writer.WriteStartElement("Settings");
            writer.WriteAttributeString("Hook_target", Hook_target.Keyboard.ToString());
            writer.WriteAttributeString("Hook_key", VirtualKeyCode.NUMPAD0.ToString());
            writer.WriteStartElement("Presets");
            writer.WriteStartElement("Preset");
            writer.WriteAttributeString("Name", "Desktop");
            writer.WriteAttributeString("default_backcolor", Color.White.ToArgb().ToString());
            writer.WriteAttributeString("default_textcolor", Color.Black.ToArgb().ToString());
            writer.WriteAttributeString("default_font", new FontConverter().ConvertToString(new Font("Consolas", 14.25F, FontStyle.Regular)));
            writer.WriteStartElement("Processes");
            writer.WriteEndElement();
            writer.WriteStartElement("Buttons");
            writer.WriteString("\n");
            CustomButton customButton;
            List<CustomButton> buttons;
            buttons = new List<CustomButton>();
            buttons.Add(new CustomButton());
            customButton = buttons[buttons.Count - 1];
            customButton.Id = 0;
            customButton.Location = new Point(33, 37);
            customButton.Text = "Ctrl+C";
            customButton.Parameters = "Ctrl+C";
            customButton.action_type = Action_type.Send;
            customButton.actions.Add(new CustomButton_Send
                (new List<VirtualKeyCode>() { VirtualKeyCode.CONTROL, VirtualKeyCode.VK_C }));

            buttons.Add(new CustomButton());
            customButton = buttons[buttons.Count - 1];
            customButton.Id = 1;
            customButton.Location = new Point(318, 37);
            customButton.Text = "Ctrl+V";
            customButton.Parameters = "Ctrl+V";
            customButton.action_type = Action_type.Send;
            customButton.actions.Add(new CustomButton_Send
                (new List<VirtualKeyCode>() { VirtualKeyCode.CONTROL, VirtualKeyCode.VK_V }));

            buttons.Add(new CustomButton());
            customButton = buttons[buttons.Count - 1];
            customButton.Id = 2;
            customButton.Location = new Point(165, 158);
            customButton.Text = "Ctrl+A";
            customButton.Parameters = "Ctrl+A";
            customButton.action_type = Action_type.Send;
            customButton.actions.Add(new CustomButton_Send
                (new List<VirtualKeyCode>() { VirtualKeyCode.CONTROL, VirtualKeyCode.VK_A }));
            foreach (CustomButton button in buttons)
            {
                writer.WriteStartElement("Button");
                writer.WriteAttributeString("Id", button.Id.ToString());
                writer.WriteAttributeString("Location.X", button.Location.X.ToString());
                writer.WriteAttributeString("Location.Y", button.Location.Y.ToString());
                writer.WriteAttributeString("Text", button.Text);
                writer.WriteAttributeString("action_type", button.action_type.ToString());
                writer.WriteAttributeString("Parameters", button.Parameters);
                writer.WriteAttributeString("BackColor", button.BackColor.ToArgb().ToString());
                writer.WriteAttributeString("ForeColor", button.ForeColor.ToArgb().ToString());
                writer.WriteAttributeString("Width", "92");
                writer.WriteAttributeString("Height", "31");
                writer.WriteAttributeString("Font", new FontConverter().ConvertToString(button.Font));
               
                writer.WriteString("\n");
                writer.WriteStartElement("List_of_Actions");
                for(int i = 0; i < button.actions.Count; i++)
                {
                    writer.WriteString("\n");
                    writer.WriteStartElement("Action" + i.ToString());
                    var action = button.actions[i];
                    if(action is CustomButton_Send)
                    {
                        writer.WriteAttributeString("IAction_type", "CustomButton_Send");
                        writer.WriteString("\n");
                        writer.WriteStartElement("modifier_keys");
                        foreach (var item in ((CustomButton_Send)action).modifier_keys)
                        {
                            writer.WriteString(item.ToString() + ' ');
                        }
                        writer.WriteEndElement();

                        writer.WriteString("\n");
                        writer.WriteStartElement("ordinary_keys");
                        foreach (var item in ((CustomButton_Send)action).ordinary_keys)
                        {
                            writer.WriteString(item.ToString() + ' ');
                        }
                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
                

                writer.WriteEndElement();
                writer.WriteString("\n");
            }
            writer.WriteEndElement();
            writer.WriteEndElement();
            writer.WriteEndElement();
            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Flush();
            writer.Close();
        }

        //public static void load_settings(Form1 main)
        //{
        //    if (!File.Exists(path_to_settings_file))
        //    {
        //        create_new_settings_file();
        //    }
        //    doc = new XmlDocument();
        //    doc.Load(path_to_settings_file);
        //    XmlNode node, list_of_actions, modifiers_node, ordinary_node, process_node;
        //    Point point = new Point();

        //    node = doc.DocumentElement;
        //    main.mouseOrKeyboardHook.set_hook_target((Hook_target)Enum.Parse(typeof(Hook_target), node.Attributes["Hook_target"].Value));
        //    string key = node.Attributes["Hook_key"].Value;

        //    if (main.mouseOrKeyboardHook.hook_target == Hook_target.Keyboard)
        //    {
        //        main.mouseOrKeyboardHook.action_button_keyboard = (VirtualKeyCode)Enum.Parse(typeof(VirtualKeyCode), key);
        //        main.mouseOrKeyboardHook.action_button_mouse = MouseButtons.XButton1;
        //    }
        //    else if(main.mouseOrKeyboardHook.hook_target == Hook_target.Mouse)
        //    {
        //        main.mouseOrKeyboardHook.action_button_mouse = (MouseButtons)Enum.Parse(typeof(MouseButtons), key);
        //        main.mouseOrKeyboardHook.action_button_keyboard = VirtualKeyCode.NUMPAD0;
        //    }
        //    Preset preset;
        //    preset_node = doc.DocumentElement.SelectSingleNode("Presets");
        //    foreach (XmlNode preset_node_child in preset_node.ChildNodes)
        //    {
        //        preset = new Preset();
        //        preset.name = preset_node_child.Attributes["Name"].Value;
        //        preset.default_backcolor = Color.FromArgb(Convert.ToInt32(preset_node_child.Attributes["default_backcolor"].Value));
        //        preset.default_textcolor = Color.FromArgb(Convert.ToInt32(preset_node_child.Attributes["default_textcolor"].Value));
        //        preset.default_font = (Font)new FontConverter().ConvertFromString(preset_node_child.Attributes["default_font"].Value);

        //        XmlNode processes_node = preset_node_child.SelectSingleNode("Processes");
        //        foreach (XmlNode processes_node_child in processes_node.ChildNodes)
        //        {
        //            preset.processes.Add(processes_node_child.Attributes["Name"].Value);
        //        }
        //        CustomButton customButton;
        //        XmlNode buttons_node = preset_node_child.SelectSingleNode("Buttons");
        //        foreach (XmlNode buttons_node_child in buttons_node.ChildNodes)
        //        {
        //            customButton = new CustomButton();
        //            customButton.Id = int.Parse(buttons_node_child.Attributes["Id"].Value);
        //            point.X = int.Parse(buttons_node_child.Attributes["Location.X"].Value);
        //            point.Y = int.Parse(buttons_node_child.Attributes["Location.Y"].Value);
        //            customButton.Location = point;
        //            customButton.Text = buttons_node_child.Attributes["Text"].Value;
        //            customButton.action_type =
        //                (Action_type)Enum.Parse(typeof(Action_type), buttons_node_child.Attributes["action_type"].Value);
        //            customButton.Parameters = buttons_node_child.Attributes["Parameters"].Value;
        //            customButton.BackColor = Color.FromArgb(Convert.ToInt32(buttons_node_child.Attributes["BackColor"].Value));
        //            customButton.ForeColor = Color.FromArgb(Convert.ToInt32(buttons_node_child.Attributes["ForeColor"].Value));
        //            customButton.Width = int.Parse(buttons_node_child.Attributes["Width"].Value);
        //            customButton.Height = int.Parse(buttons_node_child.Attributes["Height"].Value);
        //            customButton.Font = (Font)new FontConverter().ConvertFromString(buttons_node_child.Attributes["Font"].Value);

        //            list_of_actions = buttons_node_child.SelectSingleNode("List_of_Actions");
        //            foreach (XmlNode action_node in list_of_actions.ChildNodes)
        //            {
        //                if (action_node.Attributes["IAction_type"].Value == "CustomButton_Send")
        //                {
        //                    CustomButton_Send customButton_Send;
        //                    modifiers_node = action_node.SelectSingleNode("modifier_keys");
        //                    string[] a = modifiers_node.InnerText.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        //                    List<VirtualKeyCode> v = new List<VirtualKeyCode>();
        //                    foreach (var item_ in a)
        //                    {
        //                        v.Add((VirtualKeyCode)Enum.Parse(typeof(VirtualKeyCode), item_));
        //                    }

        //                    ordinary_node = action_node.SelectSingleNode("ordinary_keys");
        //                    a = ordinary_node.InnerText.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        //                    foreach (var item_ in a)
        //                    {
        //                        v.Add((VirtualKeyCode)Enum.Parse(typeof(VirtualKeyCode), item_));
        //                    }
        //                    customButton_Send = new CustomButton_Send(v);
        //                    customButton.actions.Add(customButton_Send);
        //                }
        //                else if (action_node.Attributes["IAction_type"].Value == "CustomButton_Process")
        //                {
        //                    CustomButton_Process customButton_Process;
        //                    process_node = action_node.SelectSingleNode("process");
        //                    customButton_Process = new CustomButton_Process(
        //                        (Process_type)Enum.Parse(typeof(Process_type), process_node.Attributes["process_type"].Value),
        //                        process_node.Attributes["process.StartInfo.FileName"].Value,
        //                        process_node.Attributes["process.StartInfo.Arguments"].Value);
        //                    customButton.actions.Add(customButton_Process);
        //                }
        //            }
        //            preset.buttons_array.Add(customButton);
        //        }
        //        main.presets_array.Add(preset);
        //    }
        //    main.current_preset = main.presets_array.Find(x => x.name == "Desktop");
            
        //}

        public static (List<Preset>, Hook_target, VirtualKeyCode) load_settings()
        {
            if (!File.Exists(path_to_settings_file))
            {
                create_new_settings_file();
            }
            doc = new XmlDocument();
            doc.Load(path_to_settings_file);
            XmlNode node, list_of_actions, modifiers_node, ordinary_node, process_node;
            Point point = new Point();

            node = doc.DocumentElement;

            Hook_target target = (Hook_target)Enum.Parse(typeof(Hook_target), node.Attributes["Hook_target"].Value);
            string key = node.Attributes["Hook_key"].Value;
            VirtualKeyCode virtualKey = (VirtualKeyCode)Enum.Parse(typeof(VirtualKeyCode), key);

            List<Preset> presets = new List<Preset>();
            Preset preset;
            preset_node = doc.DocumentElement.SelectSingleNode("Presets");
            foreach (XmlNode preset_node_child in preset_node.ChildNodes)
            {
                preset = new Preset();
                preset.name = preset_node_child.Attributes["Name"].Value;
                preset.default_backcolor = Color.FromArgb(Convert.ToInt32(preset_node_child.Attributes["default_backcolor"].Value));
                preset.default_textcolor = Color.FromArgb(Convert.ToInt32(preset_node_child.Attributes["default_textcolor"].Value));
                preset.default_font = (Font)new FontConverter().ConvertFromString(preset_node_child.Attributes["default_font"].Value);

                XmlNode processes_node = preset_node_child.SelectSingleNode("Processes");
                foreach (XmlNode processes_node_child in processes_node.ChildNodes)
                {
                    preset.processes.Add(processes_node_child.Attributes["Name"].Value);
                }
                CustomButton customButton;
                XmlNode buttons_node = preset_node_child.SelectSingleNode("Buttons");
                foreach (XmlNode buttons_node_child in buttons_node.ChildNodes)
                {
                    customButton = new CustomButton();
                    customButton.Id = int.Parse(buttons_node_child.Attributes["Id"].Value);
                    point.X = int.Parse(buttons_node_child.Attributes["Location.X"].Value);
                    point.Y = int.Parse(buttons_node_child.Attributes["Location.Y"].Value);
                    customButton.Location = point;
                    customButton.Text = buttons_node_child.Attributes["Text"].Value;
                    customButton.action_type =
                        (Action_type)Enum.Parse(typeof(Action_type), buttons_node_child.Attributes["action_type"].Value);
                    customButton.Parameters = buttons_node_child.Attributes["Parameters"].Value;
                    customButton.BackColor = Color.FromArgb(Convert.ToInt32(buttons_node_child.Attributes["BackColor"].Value));
                    customButton.ForeColor = Color.FromArgb(Convert.ToInt32(buttons_node_child.Attributes["ForeColor"].Value));
                    customButton.Width = int.Parse(buttons_node_child.Attributes["Width"].Value);
                    customButton.Height = int.Parse(buttons_node_child.Attributes["Height"].Value);
                    customButton.Font = (Font)new FontConverter().ConvertFromString(buttons_node_child.Attributes["Font"].Value);

                    list_of_actions = buttons_node_child.SelectSingleNode("List_of_Actions");
                    foreach (XmlNode action_node in list_of_actions.ChildNodes)
                    {
                        if (action_node.Attributes["IAction_type"].Value == "CustomButton_Send")
                        {
                            CustomButton_Send customButton_Send;
                            modifiers_node = action_node.SelectSingleNode("modifier_keys");
                            string[] a = modifiers_node.InnerText.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            List<VirtualKeyCode> v = new List<VirtualKeyCode>();
                            foreach (var item_ in a)
                            {
                                v.Add((VirtualKeyCode)Enum.Parse(typeof(VirtualKeyCode), item_));
                            }

                            ordinary_node = action_node.SelectSingleNode("ordinary_keys");
                            a = ordinary_node.InnerText.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            foreach (var item_ in a)
                            {
                                v.Add((VirtualKeyCode)Enum.Parse(typeof(VirtualKeyCode), item_));
                            }
                            customButton_Send = new CustomButton_Send(v);
                            customButton.actions.Add(customButton_Send);
                        }
                        else if (action_node.Attributes["IAction_type"].Value == "CustomButton_Process")
                        {
                            CustomButton_Process customButton_Process;
                            process_node = action_node.SelectSingleNode("process");
                            customButton_Process = new CustomButton_Process(
                                (Process_type)Enum.Parse(typeof(Process_type), process_node.Attributes["process_type"].Value),
                                process_node.Attributes["process.StartInfo.FileName"].Value,
                                process_node.Attributes["process.StartInfo.Arguments"].Value);
                            customButton.actions.Add(customButton_Process);
                        }
                    }
                    preset.buttons_array.Add(customButton);
                }
                presets.Add(preset);
            }

            return (presets, target, virtualKey);
        }

        public static void save_button_settings(string preset_name, CustomButton button, bool is_added)
        {
            if(is_added)
            {
                XmlElement node, list_of_actions, action_node, 
                    modifiers_node, ordinary_node, process_node;
                node = doc.CreateElement("Button");
                node.SetAttribute("Id", button.Id.ToString());
                node.SetAttribute("Location.X", button.Location.X.ToString());
                node.SetAttribute("Location.Y", button.Location.Y.ToString());
                node.SetAttribute("Text", button.Text);
                node.SetAttribute("action_type", button.action_type.ToString());
                node.SetAttribute("Parameters", button.Parameters);
                node.SetAttribute("BackColor", button.BackColor.ToArgb().ToString());
                node.SetAttribute("ForeColor", button.ForeColor.ToArgb().ToString());
                node.SetAttribute("Font", new FontConverter().ConvertToString(button.Font));
                node.SetAttribute("Width", button.Width.ToString());
                node.SetAttribute("Height", button.Height.ToString());
                list_of_actions = doc.CreateElement("List_of_Actions");
                for (int i = 0; i < button.actions.Count; i++)
                {
                    action_node = doc.CreateElement("Action" + i.ToString());

                    var action = button.actions[i];
                    XmlAttribute attr = doc.CreateAttribute("IAction_type");
                    if (action is CustomButton_Send)
                    {
                        attr.Value = "CustomButton_Send";
                        action_node.SetAttributeNode(attr);
                        modifiers_node = doc.CreateElement("modifier_keys");
                        foreach (var item in ((CustomButton_Send)action).modifier_keys)
                        {
                            modifiers_node.InnerText += item.ToString() + ' ';
                        }

                        ordinary_node = doc.CreateElement("ordinary_keys");
                        foreach (var item in ((CustomButton_Send)action).ordinary_keys)
                        {
                            ordinary_node.InnerText += item.ToString() + ' ';
                        }
                        action_node.AppendChild(modifiers_node);
                        action_node.AppendChild(ordinary_node);
                    }
                    else if (action is CustomButton_Process)
                    {
                        attr.Value = "CustomButton_Process";
                        action_node.SetAttributeNode(attr);
                        process_node = doc.CreateElement("process");
                        attr = doc.CreateAttribute("process.StartInfo.FileName");
                        attr.Value = ((CustomButton_Process)action).process.StartInfo.FileName;
                        process_node.SetAttributeNode(attr);

                        attr = doc.CreateAttribute("process.StartInfo.Arguments");
                        attr.Value = ((CustomButton_Process)action).process.StartInfo.Arguments;
                        process_node.SetAttributeNode(attr);

                        attr = doc.CreateAttribute("process_type");
                        attr.Value = ((CustomButton_Process)action).process_type.ToString();
                        process_node.SetAttributeNode(attr);

                        action_node.AppendChild(process_node);
                    }
                    list_of_actions.AppendChild(action_node);
                }
                node.AppendChild(list_of_actions);
                XmlNode preset = preset_node.SelectSingleNode("Preset[@Name='" + preset_name + "']");
                preset.SelectSingleNode("Buttons").AppendChild(node);
            }
            else
            {
                XmlNode node, list_of_actions;
                XmlElement action_node, modifiers_node, ordinary_node, process_node;
                XmlNode preset = preset_node.SelectSingleNode("Preset[@Name='" + preset_name + "']");
                node = preset.SelectSingleNode("Buttons").SelectSingleNode("Button[@Id='" + button.Id.ToString() + "']");
                node.Attributes["Location.X"].Value = button.Location.X.ToString();
                node.Attributes["Location.Y"].Value = button.Location.Y.ToString();
                node.Attributes["Text"].Value = button.Text;
                node.Attributes["action_type"].Value = button.action_type.ToString();
                node.Attributes["Parameters"].Value = button.Parameters;
                node.Attributes["BackColor"].Value = button.BackColor.ToArgb().ToString();
                node.Attributes["ForeColor"].Value = button.ForeColor.ToArgb().ToString();
                node.Attributes["Font"].Value = new FontConverter().ConvertToString(button.Font);
                node.Attributes["Width"].Value = button.Width.ToString();
                node.Attributes["Height"].Value = button.Height.ToString();
                list_of_actions = node.SelectSingleNode("List_of_Actions");
                list_of_actions.RemoveAll();
                for (int i = 0; i < button.actions.Count; i++)
                {
                    action_node = doc.CreateElement("Action" + i.ToString());

                    var action = button.actions[i];
                    XmlAttribute attr = doc.CreateAttribute("IAction_type");
                    if (action is CustomButton_Send)
                    {
                        attr.Value = "CustomButton_Send";
                        action_node.SetAttributeNode(attr);
                        modifiers_node = doc.CreateElement("modifier_keys");
                        foreach (var item in ((CustomButton_Send)action).modifier_keys)
                        {
                            modifiers_node.InnerText += item.ToString() + ' ';
                        }

                        ordinary_node = doc.CreateElement("ordinary_keys");
                        foreach (var item in ((CustomButton_Send)action).ordinary_keys)
                        {
                            ordinary_node.InnerText += item.ToString() + ' ';
                        }
                        action_node.AppendChild(modifiers_node);
                        action_node.AppendChild(ordinary_node);
                    }
                    else if (action is CustomButton_Process)
                    {
                        attr.Value = "CustomButton_Process";
                        action_node.SetAttributeNode(attr);
                        process_node = doc.CreateElement("process");
                        attr = doc.CreateAttribute("process.StartInfo.FileName");
                        attr.Value = ((CustomButton_Process)action).process.StartInfo.FileName;
                        process_node.SetAttributeNode(attr);

                        attr = doc.CreateAttribute("process.StartInfo.Arguments");
                        attr.Value = ((CustomButton_Process)action).process.StartInfo.Arguments;
                        process_node.SetAttributeNode(attr);

                        attr = doc.CreateAttribute("process_type");
                        attr.Value = ((CustomButton_Process)action).process_type.ToString();
                        process_node.SetAttributeNode(attr);

                        action_node.AppendChild(process_node);
                    }
                    list_of_actions.AppendChild(action_node);
                }
            }
            doc.Save(path_to_settings_file);
        }//overloads?

        public static void delete_button(string preset_name, CustomButton button)
        {
            XmlNode preset = preset_node.SelectSingleNode("Preset[@Name='" + preset_name + "']").SelectSingleNode("Buttons");
            preset.RemoveChild(preset.SelectSingleNode("Button[@Id = '" + button.Id.ToString() + "']"));
            doc.Save(path_to_settings_file);
        }

        public static void save_new_preset(string preset_name)
        {
            XmlElement preset = doc.CreateElement("Preset");
            preset.SetAttribute("Name", preset_name);
            preset.SetAttribute("default_backcolor", Color.White.ToArgb().ToString());
            preset.SetAttribute("default_textcolor", Color.Black.ToArgb().ToString());
            preset.SetAttribute("default_font", new FontConverter().ConvertToString(new Font("Consolas", 14.25F, FontStyle.Regular)));
            preset.AppendChild(doc.CreateElement("Processes"));
            preset.AppendChild(doc.CreateElement("Buttons"));
            preset_node.AppendChild(preset);
            doc.Save(path_to_settings_file);
        }

        public static void update_preset_name(string old_preset_name, string new_preset_name)
        {
            XmlNode preset = preset_node.SelectSingleNode("Preset[@Name='" + old_preset_name + "']");
            preset.Attributes["Name"].Value = new_preset_name;
            doc.Save(path_to_settings_file);
        }

        public static void delete_preset(string preset_name)
        {
            XmlNode preset = preset_node.SelectSingleNode("Preset[@Name='" + preset_name + "']");
            preset_node.RemoveChild(preset);
            doc.Save(path_to_settings_file);
        }

        public static void save_add_process(string preset_name, string process_name)
        {
            XmlNode processes_node = preset_node.SelectSingleNode("Preset[@Name='" + preset_name + "']").SelectSingleNode("Processes");
            XmlElement process_node = doc.CreateElement("Process");
            process_node.SetAttribute("Name", process_name);
            processes_node.AppendChild(process_node);
            doc.Save(path_to_settings_file);
        }

        public static void update_process_name(string preset_name, string old_process_name, string new_process_name)
        {
            XmlNode processes_node = preset_node.SelectSingleNode("Preset[@Name='" + preset_name + "']").SelectSingleNode("Processes");
            XmlNode process_node = processes_node.SelectSingleNode("Process[@Name='" + old_process_name + "']");
            process_node.Attributes["Name"].Value = new_process_name;
            doc.Save(path_to_settings_file);
        }

        public static void delete_process_name(string preset_name, string process_name)
        {
            XmlNode processes_node = preset_node.SelectSingleNode("Preset[@Name='" + preset_name + "']").SelectSingleNode("Processes");
            XmlNode process_node = processes_node.SelectSingleNode("Process[@Name='" + process_name + "']");
            processes_node.RemoveChild(process_node);
            doc.Save(path_to_settings_file);
        }//rename to delete_process

        public static void save_hook(Hook_target target, MouseButtons mbutton, VirtualKeyCode vk)
        {
            XmlElement node = doc.DocumentElement;
            node.Attributes["Hook_target"].Value = target.ToString();
            if (target == Hook_target.Keyboard)
            {
                node.Attributes["Hook_key"].Value = vk.ToString();
            }
            else if (target == Hook_target.Mouse)
            {
                node.Attributes["Hook_key"].Value = mbutton.ToString();
            }
            doc.Save(path_to_settings_file);
        }//rewrite?

        public static void save_preset_default_backcolor(string preset_name, Color color)
        {
            XmlNode node = preset_node.SelectSingleNode("Preset[@Name='" + preset_name + "']");
            node.Attributes["default_backcolor"].Value = color.ToArgb().ToString();
            doc.Save(path_to_settings_file);
        }

        public static void save_preset_default_textcolor(string preset_name, Color color)
        {
            XmlNode node = preset_node.SelectSingleNode("Preset[@Name='" + preset_name + "']");
            node.Attributes["default_textcolor"].Value = color.ToArgb().ToString();
            doc.Save(path_to_settings_file);
        }

        public static void save_preset_default_font(string preset_name, Font font)
        {
            XmlNode node = preset_node.SelectSingleNode("Preset[@Name='" + preset_name + "']");
            node.Attributes["default_font"].Value = new FontConverter().ConvertToString(font);
            doc.Save(path_to_settings_file);
        }
    }
}
