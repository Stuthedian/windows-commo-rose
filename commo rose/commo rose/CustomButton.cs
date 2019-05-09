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
        public CustomButton() : base()
        {
            Selected = false;
            Act = () => { };
            FlatStyle = FlatStyle.Flat;
            Font = new Font("Consolas", 14.25F, FontStyle.Regular);
            MouseEnter += customButton_MouseEnter;
            MouseLeave += customButton_MouseLeave;
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

        public bool Selected { get; private set; }

        public delegate void Button_action();
        public Button_action Act;

        private void customButton_MouseEnter(object sender, EventArgs e)
        {
            Selected = true;
            Color tmp = BackColor;
            BackColor = ForeColor;
            ForeColor = tmp;
        }

        private void customButton_MouseLeave(object sender, EventArgs e)
        {
            Selected = false;
            Color tmp = BackColor;
            BackColor = ForeColor;
            ForeColor = tmp;
        }

        public CustomButton Clone()
        {
            CustomButton customButton = new CustomButton();
            customButton.BackColor = BackColor;
            customButton.ForeColor = ForeColor;
            customButton.Width = Width;
            customButton.Height = Height;
            customButton.Text = Text;
            customButton.Location = Location;
            return customButton;
        }
        /*
        protected override void OnPaint(PaintEventArgs pevent)
        {
            GraphicsPath graphicsPath = new GraphicsPath();
            graphicsPath.AddEllipse(0, 0, ClientSize.Width, ClientSize.Height);
            this.Region = new System.Drawing.Region(graphicsPath);
            base.OnPaint(pevent);
        }
        */
    }
}
