using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace commo_rose
{
    public class CustomButton : Button
    {
        public delegate void Button_action();
        public Button_action Act;
        public bool Selected { get; private set; }
        public Action_type action_Type;
        public string Parameters;

        public CustomButton() : base()
        {
            Selected = false;
            Act = () => { };
            FlatStyle = FlatStyle.Flat;
            Font = new Font("Consolas", 14.25F, FontStyle.Regular);
            MouseEnter += switch_selection;
            MouseLeave += switch_selection;
            BackColorChanged += CustomButton_BackColorChanged;
            BackColor = Color.White;
            ForeColor = Color.Black;
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

        public void OverWrite(CustomButton destination, CustomButton source)
        {
            destination.BackColor = source.BackColor;
            destination.ForeColor = source.ForeColor;
            destination.Width = source.Width;
            destination.Height = source.Height;
            destination.Text = source.Text;
            destination.Location = source.Location;
            destination.action_Type = source.action_Type;
            destination.Parameters = source.Parameters;
        }
    }

    public enum Action_type { Send_keys, Run, Run_as_admin }
}
