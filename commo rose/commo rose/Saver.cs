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
        private static readonly string path_to_settings_file = Path.Combine(Application.StartupPath, ".settings.xml");
        
        public static (List<Preset>, Hook_target, VirtualKeyCode) load_settings()
        {
            if (!File.Exists(path_to_settings_file))
            {
                File.WriteAllText(path_to_settings_file, Properties.Resources._settings);
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
                    preset.buttons.Add(customButton);
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

        public static void save_add_preset(string preset_name)
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

        public static void delete_process(string preset_name, string process_name)
        {
            XmlNode processes_node = preset_node.SelectSingleNode("Preset[@Name='" + preset_name + "']").SelectSingleNode("Processes");
            XmlNode process_node = processes_node.SelectSingleNode("Process[@Name='" + process_name + "']");
            processes_node.RemoveChild(process_node);
            doc.Save(path_to_settings_file);
        }

        public static void save_hook(Hook_target target, VirtualKeyCode vk)
        {
            XmlElement node = doc.DocumentElement;
            node.Attributes["Hook_target"].Value = target.ToString();
            node.Attributes["Hook_key"].Value = vk.ToString();

            doc.Save(path_to_settings_file);
        }

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
