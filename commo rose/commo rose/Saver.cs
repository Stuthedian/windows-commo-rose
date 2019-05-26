using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using WindowsInput.Native;
using System.Drawing;

namespace commo_rose
{
    static class Saver
    {
        private static XmlDocument doc;
        private const string settings_filename = ".settings.xml";

        private static void create_new_settings_file(CustomButton[] buttons)
        {
            XmlWriter writer = XmlWriter.Create(settings_filename);
            writer.WriteStartDocument();
            writer.WriteStartElement("CustomButtons");
            writer.WriteString("\n");
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
                    else if (action is CustomButton_Process)
                    {
                        writer.WriteAttributeString("IAction_type", "CustomButton_Process");
                        writer.WriteString("\n");
                        writer.WriteStartElement("process");
                        writer.WriteAttributeString("process.StartInfo.FileName",
                            ((CustomButton_Process)action).process.StartInfo.FileName);
                        writer.WriteAttributeString("process.StartInfo.Arguments",
                            ((CustomButton_Process)action).process.StartInfo.Arguments);
                        writer.WriteAttributeString("process.StartInfo.Verb",
                            ((CustomButton_Process)action).process.StartInfo.Verb);
                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
                

                writer.WriteEndElement();
                writer.WriteString("\n");
            }
            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Flush();
            writer.Close();
        }

        public static void load_settings(CustomButton[] buttons)
        {
            if (!File.Exists(settings_filename))
            {
                create_new_settings_file(buttons);
            }
            doc = new XmlDocument();
            doc.Load(settings_filename);
            XmlNode node, list_of_actions, modifiers_node, ordinary_node, process_node;
            Point point = new Point();
            foreach (CustomButton button in buttons)
            {
                node = doc.DocumentElement.SelectSingleNode(button.Name);
                point.X = int.Parse(node.Attributes[button.Name + ".Location.X"].Value);
                point.Y = int.Parse(node.Attributes[button.Name + ".Location.Y"].Value);
                button.Location = point;
                button.Text = node.Attributes[button.Name + ".Text"].Value;
                button.action_type =
                    (Action_type)Enum.Parse(typeof(Action_type), node.Attributes[button.Name + ".action_type"].Value);
                button.Parameters = node.Attributes[button.Name + ".Parameters"].Value;
                button.BackColor = Color.FromArgb(Convert.ToInt32(node.Attributes[button.Name + ".BackColor"].Value));
                button.ForeColor = Color.FromArgb(Convert.ToInt32(node.Attributes[button.Name + ".ForeColor"].Value));
                button.Font = (Font)new FontConverter().ConvertFromString(node.Attributes[button.Name + ".Font"].Value);

                list_of_actions = node.SelectSingleNode("List_of_Actions");
                foreach (XmlNode action_node in list_of_actions.ChildNodes)
                {
                    if(action_node.Attributes["IAction_type"].Value == "CustomButton_Send")
                    {
                        CustomButton_Send customButton_Send;
                        modifiers_node = action_node.SelectSingleNode("modifier_keys");
                        string[] a = modifiers_node.InnerText.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        List<VirtualKeyCode> v = new List<VirtualKeyCode>();
                        foreach (var item in a)
                        {
                            v.Add((VirtualKeyCode)Enum.Parse(typeof(VirtualKeyCode), item));
                        }

                        ordinary_node = action_node.SelectSingleNode("ordinary_keys");
                        a = ordinary_node.InnerText.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (var item in a)
                        {
                            v.Add((VirtualKeyCode)Enum.Parse(typeof(VirtualKeyCode), item));
                        }
                        customButton_Send = new CustomButton_Send(v);
                        button.actions.Add(customButton_Send);
                    }
                    else if(action_node.Attributes["IAction_type"].Value == "CustomButton_Process")
                    {
                        CustomButton_Process customButton_Process;
                        process_node = action_node.SelectSingleNode("process");
                        customButton_Process = new CustomButton_Process(
                            process_node.Attributes["process.StartInfo.Verb"].Value == "" ? false : true,
                            process_node.Attributes["process.StartInfo.FileName"].Value,
                            process_node.Attributes["process.StartInfo.Arguments"].Value);
                        button.actions.Add(customButton_Process);
                    }
                }
                

                
            }
        }

        public static void save_settings(CustomButton[] buttons)
        {
            if (doc == null)
                return;
            XmlNode node, list_of_actions, modifiers_node, ordinary_node;
            XmlElement action_node, process_node;
            foreach (CustomButton button in buttons)
            {
                node = doc.DocumentElement.SelectSingleNode(button.Name);
                node.Attributes[button.Name + ".Location.X"].Value = button.Location.X.ToString();
                node.Attributes[button.Name + ".Location.Y"].Value = button.Location.Y.ToString();
                node.Attributes[button.Name + ".Text"].Value = button.Text;
                node.Attributes[button.Name + ".action_type"].Value = button.action_type.ToString();
                node.Attributes[button.Name + ".Parameters"].Value = button.Parameters;
                node.Attributes[button.Name + ".BackColor"].Value = button.BackColor.ToArgb().ToString();
                node.Attributes[button.Name + ".ForeColor"].Value = button.ForeColor.ToArgb().ToString();
                node.Attributes[button.Name + ".Font"].Value = new FontConverter().ConvertToString(button.Font);

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

                        attr = doc.CreateAttribute("process.StartInfo.Verb");
                        attr.Value = ((CustomButton_Process)action).process.StartInfo.Verb;
                        process_node.SetAttributeNode(attr);
                        action_node.AppendChild(process_node);
                    }
                    list_of_actions.AppendChild(action_node);
                }
                doc.Save(settings_filename);
            }
        }
    }
}
