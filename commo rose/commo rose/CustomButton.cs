using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace commo_rose
{
    class CustomButton : Button
    {
        private bool selected;
        Color forecolor, backcolor;
        public CustomButton() : base()
        {
            selected = false;
            forecolor = Color.Black;
            backcolor = Color.White;
            BackColor = backcolor;
            ForeColor = forecolor;
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderColor = BackColor;
            FlatAppearance.MouseDownBackColor = ForeColor;
            MouseEnter += new EventHandler(customButton_MouseEnter);
            MouseLeave += new EventHandler(customButton_MouseLeave);
        }

        public bool Selected => selected;

        public delegate void Button_action();
        public Button_action Act;

        private void customButton_MouseEnter(object sender, EventArgs e)
        {
            selected = true;
            BackColor = forecolor;
            ForeColor = backcolor;
            FlatAppearance.BorderColor = BackColor;
        }

        private void customButton_MouseLeave(object sender, EventArgs e)
        {
            selected = false;
            BackColor = backcolor;
            ForeColor = forecolor;
            FlatAppearance.BorderColor = BackColor;
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
