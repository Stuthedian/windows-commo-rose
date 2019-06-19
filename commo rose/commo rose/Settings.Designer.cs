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
            this.CursorpictureBox = new System.Windows.Forms.PictureBox();
            this.ButtonTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Applybutton = new System.Windows.Forms.Button();
            this.Action_typeBox = new System.Windows.Forms.ComboBox();
            this.ApplyAllbutton = new System.Windows.Forms.Button();
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
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.launchAtStartupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.yesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.noToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.actionButtonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.globalBackcolorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.globalTextcolorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.globalFontToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PresetComboBox = new System.Windows.Forms.ComboBox();
            this.PresetPanel = new System.Windows.Forms.Panel();
            this.DeletePresetButton = new System.Windows.Forms.Button();
            this.AddPresetButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.RenamePresetButton = new System.Windows.Forms.Button();
            this.ButtonParametersBox = new commo_rose.CueTextbox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CursorpictureBox)).BeginInit();
            this.SaveCancelAllpanel.SuspendLayout();
            this.ApplyCancelpanel.SuspendLayout();
            this.Editpanel.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.PresetPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.CursorpictureBox);
            this.panel1.Location = new System.Drawing.Point(9, 66);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(405, 232);
            this.panel1.TabIndex = 0;
            // 
            // CursorpictureBox
            // 
            this.CursorpictureBox.BackColor = System.Drawing.Color.Transparent;
            this.CursorpictureBox.Cursor = System.Windows.Forms.Cursors.Default;
            this.CursorpictureBox.Image = ((System.Drawing.Image)(resources.GetObject("CursorpictureBox.Image")));
            this.CursorpictureBox.Location = new System.Drawing.Point(166, 110);
            this.CursorpictureBox.Name = "CursorpictureBox";
            this.CursorpictureBox.Size = new System.Drawing.Size(12, 19);
            this.CursorpictureBox.TabIndex = 0;
            this.CursorpictureBox.TabStop = false;
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
            this.label2.Location = new System.Drawing.Point(6, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Action";
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
            this.Action_typeBox.Location = new System.Drawing.Point(46, 43);
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
            // SaveCancelAllpanel
            // 
            this.SaveCancelAllpanel.Controls.Add(this.ApplyAllbutton);
            this.SaveCancelAllpanel.Controls.Add(this.CancelAllbutton);
            this.SaveCancelAllpanel.Location = new System.Drawing.Point(423, 230);
            this.SaveCancelAllpanel.Name = "SaveCancelAllpanel";
            this.SaveCancelAllpanel.Size = new System.Drawing.Size(165, 30);
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
            this.Addbutton.Location = new System.Drawing.Point(629, 248);
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
            this.ApplyCancelpanel.Location = new System.Drawing.Point(423, 268);
            this.ApplyCancelpanel.Name = "ApplyCancelpanel";
            this.ApplyCancelpanel.Size = new System.Drawing.Size(165, 30);
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
            this.Editpanel.Controls.Add(this.ButtonParametersBox);
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
            this.Editpanel.Location = new System.Drawing.Point(420, 66);
            this.Editpanel.Name = "Editpanel";
            this.Editpanel.Size = new System.Drawing.Size(296, 122);
            this.Editpanel.TabIndex = 8;
            // 
            // Deletebutton
            // 
            this.Deletebutton.Location = new System.Drawing.Point(6, 91);
            this.Deletebutton.Name = "Deletebutton";
            this.Deletebutton.Size = new System.Drawing.Size(96, 23);
            this.Deletebutton.TabIndex = 19;
            this.Deletebutton.Text = "Delete button";
            this.Deletebutton.UseVisualStyleBackColor = true;
            this.Deletebutton.Click += new System.EventHandler(this.Deletebutton_Click);
            // 
            // Fontbutton
            // 
            this.Fontbutton.Location = new System.Drawing.Point(209, 65);
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
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsToolStripMenuItem,
            this.setToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(722, 24);
            this.menuStrip.TabIndex = 10;
            this.menuStrip.Text = "menuStrip1";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.launchAtStartupToolStripMenuItem,
            this.actionButtonToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // launchAtStartupToolStripMenuItem
            // 
            this.launchAtStartupToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.yesToolStripMenuItem,
            this.noToolStripMenuItem});
            this.launchAtStartupToolStripMenuItem.Name = "launchAtStartupToolStripMenuItem";
            this.launchAtStartupToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.launchAtStartupToolStripMenuItem.Text = "Launch at startup";
            // 
            // yesToolStripMenuItem
            // 
            this.yesToolStripMenuItem.Name = "yesToolStripMenuItem";
            this.yesToolStripMenuItem.Size = new System.Drawing.Size(91, 22);
            this.yesToolStripMenuItem.Text = "Yes";
            // 
            // noToolStripMenuItem
            // 
            this.noToolStripMenuItem.Name = "noToolStripMenuItem";
            this.noToolStripMenuItem.Size = new System.Drawing.Size(91, 22);
            this.noToolStripMenuItem.Text = "No";
            // 
            // actionButtonToolStripMenuItem
            // 
            this.actionButtonToolStripMenuItem.Name = "actionButtonToolStripMenuItem";
            this.actionButtonToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.actionButtonToolStripMenuItem.Text = "Action button";
            this.actionButtonToolStripMenuItem.Click += new System.EventHandler(this.actionButtonToolStripMenuItem_Click);
            // 
            // setToolStripMenuItem
            // 
            this.setToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.globalBackcolorToolStripMenuItem,
            this.globalTextcolorToolStripMenuItem,
            this.globalFontToolStripMenuItem});
            this.setToolStripMenuItem.Name = "setToolStripMenuItem";
            this.setToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.setToolStripMenuItem.Text = "Set";
            // 
            // globalBackcolorToolStripMenuItem
            // 
            this.globalBackcolorToolStripMenuItem.Name = "globalBackcolorToolStripMenuItem";
            this.globalBackcolorToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.globalBackcolorToolStripMenuItem.Text = "Global backcolor";
            this.globalBackcolorToolStripMenuItem.Click += new System.EventHandler(this.globalBackcolorToolStripMenuItem_Click);
            // 
            // globalTextcolorToolStripMenuItem
            // 
            this.globalTextcolorToolStripMenuItem.Name = "globalTextcolorToolStripMenuItem";
            this.globalTextcolorToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.globalTextcolorToolStripMenuItem.Text = "Global textcolor";
            this.globalTextcolorToolStripMenuItem.Click += new System.EventHandler(this.globalTextcolorToolStripMenuItem_Click);
            // 
            // globalFontToolStripMenuItem
            // 
            this.globalFontToolStripMenuItem.Name = "globalFontToolStripMenuItem";
            this.globalFontToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.globalFontToolStripMenuItem.Text = "Global font";
            this.globalFontToolStripMenuItem.Click += new System.EventHandler(this.globalFontToolStripMenuItem_Click);
            // 
            // PresetComboBox
            // 
            this.PresetComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.PresetComboBox.FormattingEnabled = true;
            this.PresetComboBox.Location = new System.Drawing.Point(82, 6);
            this.PresetComboBox.Name = "PresetComboBox";
            this.PresetComboBox.Size = new System.Drawing.Size(121, 21);
            this.PresetComboBox.TabIndex = 19;
            // 
            // PresetPanel
            // 
            this.PresetPanel.Controls.Add(this.RenamePresetButton);
            this.PresetPanel.Controls.Add(this.DeletePresetButton);
            this.PresetPanel.Controls.Add(this.AddPresetButton);
            this.PresetPanel.Controls.Add(this.label3);
            this.PresetPanel.Controls.Add(this.PresetComboBox);
            this.PresetPanel.Location = new System.Drawing.Point(0, 27);
            this.PresetPanel.Name = "PresetPanel";
            this.PresetPanel.Size = new System.Drawing.Size(522, 33);
            this.PresetPanel.TabIndex = 20;
            // 
            // DeletePresetButton
            // 
            this.DeletePresetButton.Location = new System.Drawing.Point(420, 5);
            this.DeletePresetButton.Name = "DeletePresetButton";
            this.DeletePresetButton.Size = new System.Drawing.Size(94, 23);
            this.DeletePresetButton.TabIndex = 22;
            this.DeletePresetButton.Text = "Delete preset";
            this.DeletePresetButton.UseVisualStyleBackColor = true;
            this.DeletePresetButton.Click += new System.EventHandler(this.DeletePresetButton_Click);
            // 
            // AddPresetButton
            // 
            this.AddPresetButton.Location = new System.Drawing.Point(220, 5);
            this.AddPresetButton.Name = "AddPresetButton";
            this.AddPresetButton.Size = new System.Drawing.Size(94, 23);
            this.AddPresetButton.TabIndex = 21;
            this.AddPresetButton.Text = "Add preset";
            this.AddPresetButton.UseVisualStyleBackColor = true;
            this.AddPresetButton.Click += new System.EventHandler(this.AddPresetButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 13);
            this.label3.TabIndex = 20;
            this.label3.Text = "Current preset";
            // 
            // RenamePresetButton
            // 
            this.RenamePresetButton.Location = new System.Drawing.Point(320, 5);
            this.RenamePresetButton.Name = "RenamePresetButton";
            this.RenamePresetButton.Size = new System.Drawing.Size(94, 23);
            this.RenamePresetButton.TabIndex = 23;
            this.RenamePresetButton.Text = "Rename preset";
            this.RenamePresetButton.UseVisualStyleBackColor = true;
            this.RenamePresetButton.Click += new System.EventHandler(this.RenamePresetButton_Click);
            // 
            // ButtonParametersBox
            // 
            this.ButtonParametersBox.Cue = null;
            this.ButtonParametersBox.Location = new System.Drawing.Point(6, 67);
            this.ButtonParametersBox.Name = "ButtonParametersBox";
            this.ButtonParametersBox.Size = new System.Drawing.Size(179, 20);
            this.ButtonParametersBox.TabIndex = 20;
            // 
            // Settings
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(722, 307);
            this.Controls.Add(this.PresetPanel);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.Editpanel);
            this.Controls.Add(this.ApplyCancelpanel);
            this.Controls.Add(this.Addbutton);
            this.Controls.Add(this.SaveCancelAllpanel);
            this.Controls.Add(this.menuStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MainMenuStrip = this.menuStrip;
            this.Name = "Settings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Settings_FormClosing);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.CursorpictureBox)).EndInit();
            this.SaveCancelAllpanel.ResumeLayout(false);
            this.ApplyCancelpanel.ResumeLayout(false);
            this.Editpanel.ResumeLayout(false);
            this.Editpanel.PerformLayout();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.PresetPanel.ResumeLayout(false);
            this.PresetPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox ButtonTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button Applybutton;
        private System.Windows.Forms.ComboBox Action_typeBox;
        private System.Windows.Forms.Button ApplyAllbutton;
        private System.Windows.Forms.PictureBox CursorpictureBox;
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
        private CueTextbox ButtonParametersBox;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem setToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem globalBackcolorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem launchAtStartupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem yesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem noToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem globalTextcolorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem globalFontToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem actionButtonToolStripMenuItem;
        private System.Windows.Forms.ComboBox PresetComboBox;
        private System.Windows.Forms.Panel PresetPanel;
        private System.Windows.Forms.Button DeletePresetButton;
        private System.Windows.Forms.Button AddPresetButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button RenamePresetButton;
    }
}