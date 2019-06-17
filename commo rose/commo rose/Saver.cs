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
        private static XmlNode CustomButtons;
        private static readonly string path_to_settings_filename = 
            Path.Combine(Application.StartupPath, ".settings.xml");

        private static void create_new_settings_file()
        {
            XmlWriter writer = XmlWriter.Create(path_to_settings_filename);
            writer.WriteStartDocument();
            writer.WriteStartElement("Settings");
            writer.WriteAttributeString("global_backcolor", Color.White.ToArgb().ToString());
            writer.WriteAttributeString("global_textcolor", Color.Black.ToArgb().ToString());
            writer.WriteAttributeString("global_font", new FontConverter().ConvertToString(new Font("Consolas", 14.25F, FontStyle.Regular)));
            writer.WriteStartElement("Hook_target");
            writer.WriteString(Hook_target.Keyboard.ToString());
            writer.WriteEndElement();
            writer.WriteStartElement("Hook_key");
            writer.WriteString(VirtualKeyCode.NUMPAD0.ToString());
            writer.WriteEndElement();
            writer.WriteStartElement("CustomButtons");
            writer.WriteString("\n");
            CustomButton customButton;
            List<CustomButton> buttons;
            buttons = new List<CustomButton>();
            buttons.Add(new CustomButton());
            customButton = buttons[buttons.Count - 1];
            customButton.Name = "customButton" + buttons.Count.ToString();
            customButton.Location = new Point(33, 37);
            customButton.Text = "Ctrl+C";
            customButton.Parameters = "Ctrl+C";
            customButton.action_type = Action_type.Send;
            customButton.actions.Add(new CustomButton_Send
                (new List<VirtualKeyCode>() { VirtualKeyCode.CONTROL, VirtualKeyCode.VK_C }));

            buttons.Add(new CustomButton());
            customButton = buttons[buttons.Count - 1];
            customButton.Name = "customButton" + (buttons.Count + 1).ToString();
            customButton.Location = new Point(318, 37);
            customButton.Text = "Ctrl+V";
            customButton.Parameters = "Ctrl+V";
            customButton.action_type = Action_type.Send;
            customButton.actions.Add(new CustomButton_Send
                (new List<VirtualKeyCode>() { VirtualKeyCode.CONTROL, VirtualKeyCode.VK_V }));

            buttons.Add(new CustomButton());
            customButton = buttons[buttons.Count - 1];
            customButton.Name = "customButton" + (buttons.Count + 1).ToString();
            customButton.Location = new Point(165, 158);
            customButton.Text = "Ctrl+A";
            customButton.Parameters = "Ctrl+A";
            customButton.action_type = Action_type.Send;
            customButton.actions.Add(new CustomButton_Send
                (new List<VirtualKeyCode>() { VirtualKeyCode.CONTROL, VirtualKeyCode.VK_A }));
            foreach (CustomButton button in buttons)
            {
                writer.WriteStartElement(button.Name);
                writer.WriteAttributeString(button.Name + ".Location.X", button.Location.X.ToString());
                writer.WriteAttributeString(button.Name + ".Location.Y", button.Location.Y.ToString());
                writer.WriteAttributeString(button.Name + ".Text", button.Text);
                writer.WriteAttributeString(button.Name + ".action_type", button.action_type.ToString());
                writer.WriteAttributeString(button.Name + ".Parameters", button.Parameters);
                writer.WriteAttributeString(button.Name + ".BackColor", button.BackColor.ToArgb().ToString());
                writer.WriteAttributeString(button.Name + ".ForeColor", button.ForeColor.ToArgb().ToString());
                writer.WriteAttributeString(button.Name + ".Width", "92");
                writer.WriteAttributeString(button.Name + ".Height", "31");
                writer.WriteAttributeString(button.Name + ".Font", new FontConverter().ConvertToString(button.Font));
               
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
            writer.WriteEndDocument();
            writer.Flush();
            writer.Close();
        }

        public static void load_settings(Form1 main)
        {
            if (!File.Exists(path_to_settings_filename))
            {
                create_new_settings_file();
            }
            doc = new XmlDocument();
            doc.Load(path_to_settings_filename);
            XmlNode node, list_of_actions, modifiers_node, ordinary_node, process_node;
            Point point = new Point();

            node = doc.DocumentElement;
            main.global_backcolor = Color.FromArgb(Convert.ToInt32(node.Attributes["global_backcolor"].Value));
            main.global_textcolor = Color.FromArgb(Convert.ToInt32(node.Attributes["global_textcolor"].Value));
            main.global_font = (Font)new FontConverter().ConvertFromString(node.Attributes["global_font"].Value);
            node = doc.DocumentElement.SelectSingleNode("Hook_target");
            main.mouseOrKeyboardHook.set_hook_target((Hook_target)Enum.Parse(typeof(Hook_target), node.InnerText));
            node = doc.DocumentElement.SelectSingleNode("Hook_key");
            if(main.mouseOrKeyboardHook.hook_target == Hook_target.Keyboard)
            {
                main.mouseOrKeyboardHook.action_button_keyboard = (VirtualKeyCode)Enum.Parse(typeof(VirtualKeyCode), node.InnerText);
                main.mouseOrKeyboardHook.action_button_mouse = MouseButtons.XButton1;
            }
            else if(main.mouseOrKeyboardHook.hook_target == Hook_target.Mouse)
            {
                main.mouseOrKeyboardHook.action_button_mouse = (MouseButtons)Enum.Parse(typeof(MouseButtons), node.InnerText);
                main.mouseOrKeyboardHook.action_button_keyboard = VirtualKeyCode.NUMPAD0;
            }

            CustomButton customButton;
            CustomButtons = doc.DocumentElement.SelectSingleNode("CustomButtons");
            foreach (XmlNode item in CustomButtons.ChildNodes)
            {
                customButton = new CustomButton();
                customButton.Name = item.LocalName;
                point.X = int.Parse(item.Attributes[customButton.Name + ".Location.X"].Value);
                point.Y = int.Parse(item.Attributes[customButton.Name + ".Location.Y"].Value);
                customButton.Location = point;
                customButton.Text = item.Attributes[customButton.Name + ".Text"].Value;
                customButton.action_type =
                    (Action_type)Enum.Parse(typeof(Action_type), item.Attributes[customButton.Name + ".action_type"].Value);
                customButton.Parameters = item.Attributes[customButton.Name + ".Parameters"].Value;
                customButton.BackColor = Color.FromArgb(Convert.ToInt32(item.Attributes[customButton.Name + ".BackColor"].Value));
                customButton.ForeColor = Color.FromArgb(Convert.ToInt32(item.Attributes[customButton.Name + ".ForeColor"].Value));
                customButton.Width = int.Parse(item.Attributes[customButton.Name + ".Width"].Value);
                customButton.Height = int.Parse(item.Attributes[customButton.Name + ".Height"].Value);
                customButton.Font = (Font)new FontConverter().ConvertFromString(item.Attributes[customButton.Name + ".Font"].Value);

                list_of_actions = item.SelectSingleNode("List_of_Actions");
                foreach (XmlNode action_node in list_of_actions.ChildNodes)
                {
                    if(action_node.Attributes["IAction_type"].Value == "CustomButton_Send")
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
                    else if(action_node.Attributes["IAction_type"].Value == "CustomButton_Process")
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
                customButton.Parent = main;
                main.buttons_array.Add(customButton);
            }
        }
        
        public static void save_button_settings(CustomButton button, bool is_added)
        {
            if(is_added)
            {
                XmlElement node, list_of_actions, action_node, 
                    modifiers_node, ordinary_node, process_node;
                node = doc.CreateElement(button.Name);
                node.SetAttribute(button.Name + ".Location.X", button.Location.X.ToString());
                node.SetAttribute(button.Name + ".Location.Y", button.Location.Y.ToString());
                node.SetAttribute(button.Name + ".Text", button.Text);
                node.SetAttribute(button.Name + ".action_type", button.action_type.ToString());
                node.SetAttribute(button.Name + ".Parameters", button.Parameters);
                node.SetAttribute(button.Name + ".BackColor", button.BackColor.ToArgb().ToString());
                node.SetAttribute(button.Name + ".ForeColor", button.ForeColor.ToArgb().ToString());
                node.SetAttribute(button.Name + ".Font", new FontConverter().ConvertToString(button.Font));
                node.SetAttribute(button.Name + ".Width", button.Width.ToString());
                node.SetAttribute(button.Name + ".Height", button.Height.ToString());
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
                CustomButtons.AppendChild(node);
            }
            else
            {
                XmlNode node, list_of_actions;
                XmlElement action_node, modifiers_node, ordinary_node, process_node;
                node = CustomButtons.SelectSingleNode(button.Name);
                node.Attributes[button.Name + ".Location.X"].Value = button.Location.X.ToString();
                node.Attributes[button.Name + ".Location.Y"].Value = button.Location.Y.ToString();
                node.Attributes[button.Name + ".Text"].Value = button.Text;
                node.Attributes[button.Name + ".action_type"].Value = button.action_type.ToString();
                node.Attributes[button.Name + ".Parameters"].Value = button.Parameters;
                node.Attributes[button.Name + ".BackColor"].Value = button.BackColor.ToArgb().ToString();
                node.Attributes[button.Name + ".ForeColor"].Value = button.ForeColor.ToArgb().ToString();
                node.Attributes[button.Name + ".Font"].Value = new FontConverter().ConvertToString(button.Font);
                node.Attributes[button.Name + ".Width"].Value = button.Width.ToString();
                node.Attributes[button.Name + ".Height"].Value = button.Height.ToString();
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
            doc.Save(path_to_settings_filename);
        }

        public static void delete_button(CustomButton button)
        {
            CustomButtons.RemoveChild(CustomButtons.SelectSingleNode(button.Name));
            doc.Save(path_to_settings_filename);
        }

        public static void save_hook(Hook_target target, MouseButtons mbutton, VirtualKeyCode vk)
        {
            XmlNode node;
            node = doc.DocumentElement.SelectSingleNode("Hook_target");
            node.InnerText = target.ToString();
            node = doc.DocumentElement.SelectSingleNode("Hook_key");
            if (target == Hook_target.Keyboard)
            {
                node.InnerText = vk.ToString();
            }
            else if (target == Hook_target.Mouse)
            {
                node.InnerText = mbutton.ToString();
            }
            doc.Save(path_to_settings_filename);
        }

        public static void save_global_backcolor(Color color)
        {
            XmlNode node = doc.DocumentElement;
            node.Attributes["global_backcolor"].Value = color.ToArgb().ToString();
            doc.Save(path_to_settings_filename);
        }

        public static void save_global_textcolor(Color color)
        {
            XmlNode node = doc.DocumentElement;
            node.Attributes["global_textcolor"].Value = color.ToArgb().ToString();
            doc.Save(path_to_settings_filename);
        }

        public static void save_global_font(Font font)
        {
            XmlNode node = doc.DocumentElement;
            node.Attributes["global_font"].Value = new FontConverter().ConvertToString(font);
            doc.Save(path_to_settings_filename);
        }
    }
}
