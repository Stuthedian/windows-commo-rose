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
                writer.WriteAttributeString(button.Name + ".action_Type", button.action_type.ToString());
                writer.WriteAttributeString(button.Name + ".Parameters", button.Parameters);

                writer.WriteString("\n");
                writer.WriteStartElement("modifier_keys");
                foreach (var item in button.modifier_keys)
                {
                    writer.WriteString(item.ToString() + ' ');
                }
                writer.WriteEndElement();

                writer.WriteString("\n");
                writer.WriteStartElement("ordinary_keys");
                foreach (var item in button.ordinary_keys)
                {
                    writer.WriteString(item.ToString() + ' ');
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
            XmlNode node, modifiers_node, ordinary_node;
            Point point = new Point();
            foreach (CustomButton button in buttons)
            {
                node = doc.DocumentElement.SelectSingleNode(button.Name);
                point.X = int.Parse(node.Attributes[button.Name + ".Location.X"].Value);
                point.Y = int.Parse(node.Attributes[button.Name + ".Location.Y"].Value);
                button.Location = point;
                button.Text = node.Attributes[button.Name + ".Text"].Value;
                button.action_type =
                    (Action_type)Enum.Parse(typeof(Action_type), node.Attributes[button.Name + ".action_Type"].Value);
                button.Parameters = node.Attributes[button.Name + ".Parameters"].Value;

                modifiers_node = node.SelectSingleNode("modifier_keys");
                string[] a = modifiers_node.InnerText.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                List<VirtualKeyCode> v = new List<VirtualKeyCode>();
                foreach (var item in a)
                {
                    v.Add((VirtualKeyCode)Enum.Parse(typeof(VirtualKeyCode), item));
                }
                button.modifier_keys = v.AsEnumerable().ToList().AsEnumerable();

                ordinary_node = node.SelectSingleNode("ordinary_keys");
                a = ordinary_node.InnerText.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                v.Clear();
                foreach (var item in a)
                {
                    v.Add((VirtualKeyCode)Enum.Parse(typeof(VirtualKeyCode), item));
                }
                button.ordinary_keys = v.AsEnumerable().ToList().AsEnumerable();
            }
        }

        public static void save_settings(CustomButton[] buttons)
        {
            if (doc == null)
                return;
            XmlNode node, modifiers_node, ordinary_node;
            string a;
            foreach (CustomButton button in buttons)
            {
                node = doc.DocumentElement.SelectSingleNode(button.Name);
                node.Attributes[button.Name + ".Location.X"].Value = button.Location.X.ToString();
                node.Attributes[button.Name + ".Location.Y"].Value = button.Location.Y.ToString();
                node.Attributes[button.Name + ".Text"].Value = button.Text;
                node.Attributes[button.Name + ".action_Type"].Value = button.action_type.ToString();
                node.Attributes[button.Name + ".Parameters"].Value = button.Parameters;

                a = "";
                modifiers_node = node.SelectSingleNode("modifier_keys");
                foreach (var item in button.modifier_keys)
                {
                    a += item.ToString() + ' ';
                }
                modifiers_node.InnerText = a;

                a = "";
                ordinary_node = node.SelectSingleNode("ordinary_keys");
                foreach (var item in button.ordinary_keys)
                {
                    a += item.ToString() + ' ';
                }
                ordinary_node.InnerText = a;
                doc.Save(settings_filename);
            }
        }
    }
}
