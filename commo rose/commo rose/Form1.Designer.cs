namespace commo_rose
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.customButton1 = new commo_rose.CustomButton();
            this.customButton2 = new commo_rose.CustomButton();
            this.customButton3 = new commo_rose.CustomButton();
            this.customButton4 = new commo_rose.CustomButton();
            this.customButton5 = new commo_rose.CustomButton();
            this.customButton6 = new commo_rose.CustomButton();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(93, 26);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(92, 22);
            this.toolStripMenuItem1.Text = "Exit";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // customButton1
            // 
            this.customButton1.BackColor = System.Drawing.Color.White;
            this.customButton1.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.customButton1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.customButton1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.customButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButton1.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.customButton1.ForeColor = System.Drawing.Color.Black;
            this.customButton1.Location = new System.Drawing.Point(125, 12);
            this.customButton1.Name = "customButton1";
            this.customButton1.Size = new System.Drawing.Size(154, 30);
            this.customButton1.TabIndex = 1;
            this.customButton1.Text = "customButton1";
            this.customButton1.UseVisualStyleBackColor = true;
            // 
            // customButton2
            // 
            this.customButton2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButton2.Font = new System.Drawing.Font("Consolas", 14.25F);
            this.customButton2.Location = new System.Drawing.Point(12, 58);
            this.customButton2.Name = "customButton2";
            this.customButton2.Size = new System.Drawing.Size(105, 31);
            this.customButton2.TabIndex = 2;
            this.customButton2.Text = "fake";
            this.customButton2.UseVisualStyleBackColor = true;
            // 
            // customButton3
            // 
            this.customButton3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButton3.Font = new System.Drawing.Font("Consolas", 14.25F);
            this.customButton3.Location = new System.Drawing.Point(288, 58);
            this.customButton3.Name = "customButton3";
            this.customButton3.Size = new System.Drawing.Size(105, 31);
            this.customButton3.TabIndex = 3;
            this.customButton3.Text = "fake";
            this.customButton3.UseVisualStyleBackColor = true;
            // 
            // customButton4
            // 
            this.customButton4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButton4.Font = new System.Drawing.Font("Consolas", 14.25F);
            this.customButton4.Location = new System.Drawing.Point(288, 108);
            this.customButton4.Name = "customButton4";
            this.customButton4.Size = new System.Drawing.Size(105, 31);
            this.customButton4.TabIndex = 4;
            this.customButton4.Text = "fake";
            this.customButton4.UseVisualStyleBackColor = true;
            // 
            // customButton5
            // 
            this.customButton5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButton5.Font = new System.Drawing.Font("Consolas", 14.25F);
            this.customButton5.Location = new System.Drawing.Point(150, 167);
            this.customButton5.Name = "customButton5";
            this.customButton5.Size = new System.Drawing.Size(105, 31);
            this.customButton5.TabIndex = 5;
            this.customButton5.Text = "fake";
            this.customButton5.UseVisualStyleBackColor = true;
            // 
            // customButton6
            // 
            this.customButton6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButton6.Font = new System.Drawing.Font("Consolas", 14.25F);
            this.customButton6.Location = new System.Drawing.Point(12, 108);
            this.customButton6.Name = "customButton6";
            this.customButton6.Size = new System.Drawing.Size(105, 31);
            this.customButton6.TabIndex = 6;
            this.customButton6.Text = "fake";
            this.customButton6.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(405, 232);
            this.Controls.Add(this.customButton6);
            this.Controls.Add(this.customButton5);
            this.Controls.Add(this.customButton4);
            this.Controls.Add(this.customButton3);
            this.Controls.Add(this.customButton2);
            this.Controls.Add(this.customButton1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private CustomButton customButton1;
        private CustomButton customButton2;
        private CustomButton customButton3;
        private CustomButton customButton4;
        private CustomButton customButton5;
        private CustomButton customButton6;
    }
}

