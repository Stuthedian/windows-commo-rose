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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.ButtonTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ButtonParametersBox = new System.Windows.Forms.TextBox();
            this.Applybutton = new System.Windows.Forms.Button();
            this.Action_typeBox = new System.Windows.Forms.ComboBox();
            this.ApplyAllbutton = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.General = new System.Windows.Forms.TabPage();
            this.MouseKeyboardButtonsComboBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.KeyboardradioButton = new System.Windows.Forms.RadioButton();
            this.MouseradioButton = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.Style = new System.Windows.Forms.TabPage();
            this.SaveCancelAllpanel = new System.Windows.Forms.Panel();
            this.CancelAllbutton = new System.Windows.Forms.Button();
            this.Addbutton = new System.Windows.Forms.Button();
            this.ApplyCancelpanel = new System.Windows.Forms.Panel();
            this.Cancelbutton = new System.Windows.Forms.Button();
            this.Editpanel = new System.Windows.Forms.Panel();
            this.Deletebutton = new System.Windows.Forms.Button();
            this.Fontbutton = new System.Windows.Forms.Button();
            this.TextColorpanel = new System.Windows.Forms.Panel();
            this.BackColorpanel = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.ColorPicker = new System.Windows.Forms.ColorDialog();
            this.FontPicker = new System.Windows.Forms.FontDialog();
            this.GlobalBackColor = new System.Windows.Forms.Button();
            this.GlobalTextColor = new System.Windows.Forms.Button();
            this.GlobalFont = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.General.SuspendLayout();
            this.Style.SuspendLayout();
            this.SaveCancelAllpanel.SuspendLayout();
            this.ApplyCancelpanel.SuspendLayout();
            this.Editpanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new System.Drawing.Point(6, 6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(405, 232);
            this.panel1.TabIndex = 0;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Default;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(166, 110);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(12, 19);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // ButtonTextBox
            // 
            this.ButtonTextBox.Location = new System.Drawing.Point(53, 7);
            this.ButtonTextBox.Name = "ButtonTextBox";
            this.ButtonTextBox.Size = new System.Drawing.Size(100, 20);
            this.ButtonTextBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Text";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Action";
            // 
            // ButtonParametersBox
            // 
            this.ButtonParametersBox.Location = new System.Drawing.Point(6, 85);
            this.ButtonParametersBox.Name = "ButtonParametersBox";
            this.ButtonParametersBox.Size = new System.Drawing.Size(179, 20);
            this.ButtonParametersBox.TabIndex = 4;
            // 
            // Applybutton
            // 
            this.Applybutton.Location = new System.Drawing.Point(3, 3);
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
            this.Action_typeBox.Location = new System.Drawing.Point(46, 61);
            this.Action_typeBox.Name = "Action_typeBox";
            this.Action_typeBox.Size = new System.Drawing.Size(85, 21);
            this.Action_typeBox.TabIndex = 6;
            // 
            // ApplyAllbutton
            // 
            this.ApplyAllbutton.Location = new System.Drawing.Point(3, 3);
            this.ApplyAllbutton.Name = "ApplyAllbutton";
            this.ApplyAllbutton.Size = new System.Drawing.Size(75, 23);
            this.ApplyAllbutton.TabIndex = 7;
            this.ApplyAllbutton.Text = "Apply all";
            this.ApplyAllbutton.UseVisualStyleBackColor = true;
            this.ApplyAllbutton.Click += new System.EventHandler(this.ApplyAllbutton_Click);
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
            this.tabControl1.Size = new System.Drawing.Size(729, 271);
            this.tabControl1.TabIndex = 9;
            // 
            // General
            // 
            this.General.Controls.Add(this.GlobalBackColor);
            this.General.Controls.Add(this.GlobalTextColor);
            this.General.Controls.Add(this.GlobalFont);
            this.General.Controls.Add(this.MouseKeyboardButtonsComboBox);
            this.General.Controls.Add(this.label4);
            this.General.Controls.Add(this.KeyboardradioButton);
            this.General.Controls.Add(this.MouseradioButton);
            this.General.Controls.Add(this.label3);
            this.General.Controls.Add(this.checkBox1);
            this.General.Location = new System.Drawing.Point(4, 22);
            this.General.Name = "General";
            this.General.Padding = new System.Windows.Forms.Padding(3);
            this.General.Size = new System.Drawing.Size(721, 245);
            this.General.TabIndex = 0;
            this.General.Text = "General";
            this.General.UseVisualStyleBackColor = true;
            // 
            // MouseKeyboardButtonsComboBox
            // 
            this.MouseKeyboardButtonsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.MouseKeyboardButtonsComboBox.FormattingEnabled = true;
            this.MouseKeyboardButtonsComboBox.Location = new System.Drawing.Point(107, 127);
            this.MouseKeyboardButtonsComboBox.Name = "MouseKeyboardButtonsComboBox";
            this.MouseKeyboardButtonsComboBox.Size = new System.Drawing.Size(100, 21);
            this.MouseKeyboardButtonsComboBox.TabIndex = 7;
            this.MouseKeyboardButtonsComboBox.SelectedIndexChanged += new System.EventHandler(this.MouseButtonsBox_SelectedIndexChanged);
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
            // Style
            // 
            this.Style.Controls.Add(this.SaveCancelAllpanel);
            this.Style.Controls.Add(this.Addbutton);
            this.Style.Controls.Add(this.ApplyCancelpanel);
            this.Style.Controls.Add(this.Editpanel);
            this.Style.Controls.Add(this.panel1);
            this.Style.Location = new System.Drawing.Point(4, 22);
            this.Style.Name = "Style";
            this.Style.Padding = new System.Windows.Forms.Padding(3);
            this.Style.Size = new System.Drawing.Size(721, 245);
            this.Style.TabIndex = 1;
            this.Style.Text = "Style";
            this.Style.UseVisualStyleBackColor = true;
            // 
            // SaveCancelAllpanel
            // 
            this.SaveCancelAllpanel.Controls.Add(this.ApplyAllbutton);
            this.SaveCancelAllpanel.Controls.Add(this.CancelAllbutton);
            this.SaveCancelAllpanel.Location = new System.Drawing.Point(423, 166);
            this.SaveCancelAllpanel.Name = "SaveCancelAllpanel";
            this.SaveCancelAllpanel.Size = new System.Drawing.Size(165, 33);
            this.SaveCancelAllpanel.TabIndex = 12;
            // 
            // CancelAllbutton
            // 
            this.CancelAllbutton.Location = new System.Drawing.Point(84, 3);
            this.CancelAllbutton.Name = "CancelAllbutton";
            this.CancelAllbutton.Size = new System.Drawing.Size(75, 23);
            this.CancelAllbutton.TabIndex = 11;
            this.CancelAllbutton.Text = "Cancel all";
            this.CancelAllbutton.UseVisualStyleBackColor = true;
            this.CancelAllbutton.Click += new System.EventHandler(this.CancelAll_Click);
            // 
            // Addbutton
            // 
            this.Addbutton.Location = new System.Drawing.Point(638, 166);
            this.Addbutton.Name = "Addbutton";
            this.Addbutton.Size = new System.Drawing.Size(75, 23);
            this.Addbutton.TabIndex = 18;
            this.Addbutton.Text = "Add button";
            this.Addbutton.UseVisualStyleBackColor = true;
            this.Addbutton.Click += new System.EventHandler(this.Addbutton_Click);
            // 
            // ApplyCancelpanel
            // 
            this.ApplyCancelpanel.Controls.Add(this.Cancelbutton);
            this.ApplyCancelpanel.Controls.Add(this.Applybutton);
            this.ApplyCancelpanel.Location = new System.Drawing.Point(423, 205);
            this.ApplyCancelpanel.Name = "ApplyCancelpanel";
            this.ApplyCancelpanel.Size = new System.Drawing.Size(165, 33);
            this.ApplyCancelpanel.TabIndex = 10;
            // 
            // Cancelbutton
            // 
            this.Cancelbutton.Location = new System.Drawing.Point(84, 3);
            this.Cancelbutton.Name = "Cancelbutton";
            this.Cancelbutton.Size = new System.Drawing.Size(75, 23);
            this.Cancelbutton.TabIndex = 9;
            this.Cancelbutton.Text = "Cancel";
            this.Cancelbutton.UseVisualStyleBackColor = true;
            this.Cancelbutton.Click += new System.EventHandler(this.Cancelbutton_Click);
            // 
            // Editpanel
            // 
            this.Editpanel.Controls.Add(this.Deletebutton);
            this.Editpanel.Controls.Add(this.Fontbutton);
            this.Editpanel.Controls.Add(this.TextColorpanel);
            this.Editpanel.Controls.Add(this.BackColorpanel);
            this.Editpanel.Controls.Add(this.label5);
            this.Editpanel.Controls.Add(this.label6);
            this.Editpanel.Controls.Add(this.label1);
            this.Editpanel.Controls.Add(this.ButtonTextBox);
            this.Editpanel.Controls.Add(this.Action_typeBox);
            this.Editpanel.Controls.Add(this.label2);
            this.Editpanel.Controls.Add(this.ButtonParametersBox);
            this.Editpanel.Location = new System.Drawing.Point(417, 9);
            this.Editpanel.Name = "Editpanel";
            this.Editpanel.Size = new System.Drawing.Size(296, 139);
            this.Editpanel.TabIndex = 8;
            // 
            // Deletebutton
            // 
            this.Deletebutton.Location = new System.Drawing.Point(6, 109);
            this.Deletebutton.Name = "Deletebutton";
            this.Deletebutton.Size = new System.Drawing.Size(96, 23);
            this.Deletebutton.TabIndex = 19;
            this.Deletebutton.Text = "Delete button";
            this.Deletebutton.UseVisualStyleBackColor = true;
            this.Deletebutton.Click += new System.EventHandler(this.Deletebutton_Click);
            // 
            // Fontbutton
            // 
            this.Fontbutton.Location = new System.Drawing.Point(206, 84);
            this.Fontbutton.Name = "Fontbutton";
            this.Fontbutton.Size = new System.Drawing.Size(75, 23);
            this.Fontbutton.TabIndex = 17;
            this.Fontbutton.Text = "Font";
            this.Fontbutton.UseVisualStyleBackColor = true;
            this.Fontbutton.Click += new System.EventHandler(this.Fontbutton_Click);
            // 
            // TextColorpanel
            // 
            this.TextColorpanel.BackColor = System.Drawing.Color.White;
            this.TextColorpanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.TextColorpanel.Location = new System.Drawing.Point(264, 33);
            this.TextColorpanel.Name = "TextColorpanel";
            this.TextColorpanel.Size = new System.Drawing.Size(20, 20);
            this.TextColorpanel.TabIndex = 16;
            this.TextColorpanel.Click += new System.EventHandler(this.TextColorpanel_Click);
            // 
            // BackColorpanel
            // 
            this.BackColorpanel.BackColor = System.Drawing.Color.White;
            this.BackColorpanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.BackColorpanel.Location = new System.Drawing.Point(264, 7);
            this.BackColorpanel.Name = "BackColorpanel";
            this.BackColorpanel.Size = new System.Drawing.Size(20, 20);
            this.BackColorpanel.TabIndex = 15;
            this.BackColorpanel.Click += new System.EventHandler(this.BackColorpanel_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(202, 10);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "BackColor";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(204, 37);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "TextColor";
            // 
            // ColorPicker
            // 
            this.ColorPicker.AnyColor = true;
            this.ColorPicker.FullOpen = true;
            // 
            // FontPicker
            // 
            this.FontPicker.ShowApply = true;
            this.FontPicker.Apply += new System.EventHandler(this.FontPicker_Apply);
            // 
            // GlobalBackColor
            // 
            this.GlobalBackColor.Location = new System.Drawing.Point(14, 154);
            this.GlobalBackColor.Name = "GlobalBackColor";
            this.GlobalBackColor.Size = new System.Drawing.Size(121, 23);
            this.GlobalBackColor.TabIndex = 10;
            this.GlobalBackColor.Text = "Set global backcolor";
            this.GlobalBackColor.UseVisualStyleBackColor = true;
            this.GlobalBackColor.Click += new System.EventHandler(this.GlobalBackColor_Click);
            // 
            // GlobalTextColor
            // 
            this.GlobalTextColor.Location = new System.Drawing.Point(14, 183);
            this.GlobalTextColor.Name = "GlobalTextColor";
            this.GlobalTextColor.Size = new System.Drawing.Size(121, 23);
            this.GlobalTextColor.TabIndex = 11;
            this.GlobalTextColor.Text = "Set global textcolor";
            this.GlobalTextColor.UseVisualStyleBackColor = true;
            this.GlobalTextColor.Click += new System.EventHandler(this.GlobalTextColor_Click);
            // 
            // GlobalFont
            // 
            this.GlobalFont.Location = new System.Drawing.Point(14, 212);
            this.GlobalFont.Name = "GlobalFont";
            this.GlobalFont.Size = new System.Drawing.Size(121, 23);
            this.GlobalFont.TabIndex = 12;
            this.GlobalFont.Text = "Set global font";
            this.GlobalFont.UseVisualStyleBackColor = true;
            this.GlobalFont.Click += new System.EventHandler(this.GlobalFont_Click);
            // 
            // Settings
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(729, 271);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Settings";
            this.Text = "Settings";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.General.ResumeLayout(false);
            this.General.PerformLayout();
            this.Style.ResumeLayout(false);
            this.SaveCancelAllpanel.ResumeLayout(false);
            this.ApplyCancelpanel.ResumeLayout(false);
            this.Editpanel.ResumeLayout(false);
            this.Editpanel.PerformLayout();
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
        private System.Windows.Forms.Button ApplyAllbutton;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage General;
        private System.Windows.Forms.TabPage Style;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton KeyboardradioButton;
        private System.Windows.Forms.RadioButton MouseradioButton;
        private System.Windows.Forms.ComboBox MouseKeyboardButtonsComboBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel Editpanel;
        private System.Windows.Forms.Button Cancelbutton;
        private System.Windows.Forms.Panel ApplyCancelpanel;
        private System.Windows.Forms.Panel SaveCancelAllpanel;
        private System.Windows.Forms.Button CancelAllbutton;
        private System.Windows.Forms.Panel BackColorpanel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ColorDialog ColorPicker;
        private System.Windows.Forms.Panel TextColorpanel;
        private System.Windows.Forms.Button Fontbutton;
        private System.Windows.Forms.FontDialog FontPicker;
        private System.Windows.Forms.Button Addbutton;
        private System.Windows.Forms.Button Deletebutton;
        private System.Windows.Forms.Button GlobalBackColor;
        private System.Windows.Forms.Button GlobalTextColor;
        private System.Windows.Forms.Button GlobalFont;
    }
}