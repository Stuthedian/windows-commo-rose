namespace commo_rose
{
    partial class Settings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Settings));
            this.panel1 = new System.Windows.Forms.Panel();
            this.ButtonTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ButtonParametersBox = new System.Windows.Forms.TextBox();
            this.Applybutton = new System.Windows.Forms.Button();
            this.Action_typeBox = new System.Windows.Forms.ComboBox();
            this.Savebutton = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.General = new System.Windows.Forms.TabPage();
            this.MouseButtonsBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.KeyboardradioButton = new System.Windows.Forms.RadioButton();
            this.MouseradioButton = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.ActionButtonBox = new System.Windows.Forms.TextBox();
            this.Style = new System.Windows.Forms.TabPage();
            this.tabControl1.SuspendLayout();
            this.General.SuspendLayout();
            this.Style.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Location = new System.Drawing.Point(6, 6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(405, 232);
            this.panel1.TabIndex = 0;
            // 
            // ButtonTextBox
            // 
            this.ButtonTextBox.Location = new System.Drawing.Point(467, 16);
            this.ButtonTextBox.Name = "ButtonTextBox";
            this.ButtonTextBox.Size = new System.Drawing.Size(100, 20);
            this.ButtonTextBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(417, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Text";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(417, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Action";
            // 
            // ButtonParametersBox
            // 
            this.ButtonParametersBox.Location = new System.Drawing.Point(558, 67);
            this.ButtonParametersBox.Name = "ButtonParametersBox";
            this.ButtonParametersBox.Size = new System.Drawing.Size(100, 20);
            this.ButtonParametersBox.TabIndex = 4;
            // 
            // Applybutton
            // 
            this.Applybutton.Location = new System.Drawing.Point(593, 215);
            this.Applybutton.Name = "Applybutton";
            this.Applybutton.Size = new System.Drawing.Size(75, 23);
            this.Applybutton.TabIndex = 5;
            this.Applybutton.Text = "Apply";
            this.Applybutton.UseVisualStyleBackColor = true;
            this.Applybutton.Click += new System.EventHandler(this.Applybutton_Click);
            // 
            // Action_typeBox
            // 
            this.Action_typeBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Action_typeBox.FormattingEnabled = true;
            this.Action_typeBox.Location = new System.Drawing.Point(467, 67);
            this.Action_typeBox.Name = "Action_typeBox";
            this.Action_typeBox.Size = new System.Drawing.Size(85, 21);
            this.Action_typeBox.TabIndex = 6;
            // 
            // Savebutton
            // 
            this.Savebutton.Location = new System.Drawing.Point(512, 215);
            this.Savebutton.Name = "Savebutton";
            this.Savebutton.Size = new System.Drawing.Size(75, 23);
            this.Savebutton.TabIndex = 7;
            this.Savebutton.Text = "Save";
            this.Savebutton.UseVisualStyleBackColor = true;
            this.Savebutton.Click += new System.EventHandler(this.Savebutton_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(6, 6);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(109, 17);
            this.checkBox1.TabIndex = 1;
            this.checkBox1.Text = "Launch at startup";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.General);
            this.tabControl1.Controls.Add(this.Style);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(681, 271);
            this.tabControl1.TabIndex = 9;
            // 
            // General
            // 
            this.General.Controls.Add(this.MouseButtonsBox);
            this.General.Controls.Add(this.label4);
            this.General.Controls.Add(this.KeyboardradioButton);
            this.General.Controls.Add(this.MouseradioButton);
            this.General.Controls.Add(this.label3);
            this.General.Controls.Add(this.ActionButtonBox);
            this.General.Controls.Add(this.checkBox1);
            this.General.Location = new System.Drawing.Point(4, 22);
            this.General.Name = "General";
            this.General.Padding = new System.Windows.Forms.Padding(3);
            this.General.Size = new System.Drawing.Size(673, 245);
            this.General.TabIndex = 0;
            this.General.Text = "General";
            this.General.UseVisualStyleBackColor = true;
            // 
            // MouseButtonsBox
            // 
            this.MouseButtonsBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.MouseButtonsBox.FormattingEnabled = true;
            this.MouseButtonsBox.Location = new System.Drawing.Point(107, 153);
            this.MouseButtonsBox.Name = "MouseButtonsBox";
            this.MouseButtonsBox.Size = new System.Drawing.Size(100, 21);
            this.MouseButtonsBox.TabIndex = 7;
            this.MouseButtonsBox.SelectedIndexChanged += new System.EventHandler(this.MouseButtonsBox_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 46);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Action button on:";
            // 
            // KeyboardradioButton
            // 
            this.KeyboardradioButton.AutoSize = true;
            this.KeyboardradioButton.Location = new System.Drawing.Point(25, 87);
            this.KeyboardradioButton.Name = "KeyboardradioButton";
            this.KeyboardradioButton.Size = new System.Drawing.Size(70, 17);
            this.KeyboardradioButton.TabIndex = 5;
            this.KeyboardradioButton.TabStop = true;
            this.KeyboardradioButton.Text = "Keyboard";
            this.KeyboardradioButton.UseVisualStyleBackColor = true;
            // 
            // MouseradioButton
            // 
            this.MouseradioButton.AutoSize = true;
            this.MouseradioButton.Location = new System.Drawing.Point(25, 63);
            this.MouseradioButton.Name = "MouseradioButton";
            this.MouseradioButton.Size = new System.Drawing.Size(57, 17);
            this.MouseradioButton.TabIndex = 4;
            this.MouseradioButton.TabStop = true;
            this.MouseradioButton.Text = "Mouse";
            this.MouseradioButton.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 131);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Action button key";
            // 
            // ActionButtonBox
            // 
            this.ActionButtonBox.Cursor = System.Windows.Forms.Cursors.Default;
            this.ActionButtonBox.Location = new System.Drawing.Point(107, 127);
            this.ActionButtonBox.Name = "ActionButtonBox";
            this.ActionButtonBox.ReadOnly = true;
            this.ActionButtonBox.Size = new System.Drawing.Size(100, 20);
            this.ActionButtonBox.TabIndex = 2;
            this.ActionButtonBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ActionButtonBox_MouseDown);
            // 
            // Style
            // 
            this.Style.Controls.Add(this.panel1);
            this.Style.Controls.Add(this.Action_typeBox);
            this.Style.Controls.Add(this.Savebutton);
            this.Style.Controls.Add(this.ButtonParametersBox);
            this.Style.Controls.Add(this.Applybutton);
            this.Style.Controls.Add(this.label2);
            this.Style.Controls.Add(this.label1);
            this.Style.Controls.Add(this.ButtonTextBox);
            this.Style.Location = new System.Drawing.Point(4, 22);
            this.Style.Name = "Style";
            this.Style.Padding = new System.Windows.Forms.Padding(3);
            this.Style.Size = new System.Drawing.Size(673, 245);
            this.Style.TabIndex = 1;
            this.Style.Text = "Style";
            this.Style.UseVisualStyleBackColor = true;
            // 
            // Settings
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(681, 271);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Settings";
            this.Text = "Settings";
            this.tabControl1.ResumeLayout(false);
            this.General.ResumeLayout(false);
            this.General.PerformLayout();
            this.Style.ResumeLayout(false);
            this.Style.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox ButtonTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox ButtonParametersBox;
        private System.Windows.Forms.Button Applybutton;
        private System.Windows.Forms.ComboBox Action_typeBox;
        private System.Windows.Forms.Button Savebutton;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage General;
        private System.Windows.Forms.TabPage Style;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox ActionButtonBox;
        private System.Windows.Forms.RadioButton KeyboardradioButton;
        private System.Windows.Forms.RadioButton MouseradioButton;
        private System.Windows.Forms.ComboBox MouseButtonsBox;
        private System.Windows.Forms.Label label4;
    }
}