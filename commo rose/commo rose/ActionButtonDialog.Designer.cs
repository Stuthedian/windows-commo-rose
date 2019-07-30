namespace commo_rose
{
    partial class ActionButtonDialog
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
            this.MouseButtonsComboBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.KeyboardradioButton = new System.Windows.Forms.RadioButton();
            this.MouseradioButton = new System.Windows.Forms.RadioButton();
            this.FakeLabel = new System.Windows.Forms.Label();
            this.ScanKeyTextBox = new commo_rose.CueTextbox();
            this.SuspendLayout();
            // 
            // MouseButtonsComboBox
            // 
            this.MouseButtonsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.MouseButtonsComboBox.FormattingEnabled = true;
            this.MouseButtonsComboBox.Location = new System.Drawing.Point(122, 34);
            this.MouseButtonsComboBox.Name = "MouseButtonsComboBox";
            this.MouseButtonsComboBox.Size = new System.Drawing.Size(140, 21);
            this.MouseButtonsComboBox.TabIndex = 12;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Action button on:";
            // 
            // KeyboardradioButton
            // 
            this.KeyboardradioButton.AutoSize = true;
            this.KeyboardradioButton.Location = new System.Drawing.Point(26, 50);
            this.KeyboardradioButton.Name = "KeyboardradioButton";
            this.KeyboardradioButton.Size = new System.Drawing.Size(70, 17);
            this.KeyboardradioButton.TabIndex = 10;
            this.KeyboardradioButton.TabStop = true;
            this.KeyboardradioButton.Text = "Keyboard";
            this.KeyboardradioButton.UseVisualStyleBackColor = true;
            // 
            // MouseradioButton
            // 
            this.MouseradioButton.AutoSize = true;
            this.MouseradioButton.Location = new System.Drawing.Point(26, 26);
            this.MouseradioButton.Name = "MouseradioButton";
            this.MouseradioButton.Size = new System.Drawing.Size(57, 17);
            this.MouseradioButton.TabIndex = 9;
            this.MouseradioButton.TabStop = true;
            this.MouseradioButton.Text = "Mouse";
            this.MouseradioButton.UseVisualStyleBackColor = true;
            // 
            // FakeLabel
            // 
            this.FakeLabel.AutoSize = true;
            this.FakeLabel.Location = new System.Drawing.Point(250, 26);
            this.FakeLabel.Name = "FakeLabel";
            this.FakeLabel.Size = new System.Drawing.Size(0, 13);
            this.FakeLabel.TabIndex = 14;
            // 
            // ScanKeyTextBox
            // 
            this.ScanKeyTextBox.Cue = null;
            this.ScanKeyTextBox.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ScanKeyTextBox.Location = new System.Drawing.Point(122, 30);
            this.ScanKeyTextBox.Name = "ScanKeyTextBox";
            this.ScanKeyTextBox.Size = new System.Drawing.Size(140, 26);
            this.ScanKeyTextBox.TabIndex = 15;
            this.ScanKeyTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ScanKeyTextBox.Enter += new System.EventHandler(this.ScanKeyTextBox_Enter);
            this.ScanKeyTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ScanKeyTextBox_KeyDown);
            // 
            // ActionButtonDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(289, 101);
            this.Controls.Add(this.ScanKeyTextBox);
            this.Controls.Add(this.MouseButtonsComboBox);
            this.Controls.Add(this.FakeLabel);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.KeyboardradioButton);
            this.Controls.Add(this.MouseradioButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "ActionButtonDialog";
            this.Text = "Action button";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox MouseButtonsComboBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton KeyboardradioButton;
        private System.Windows.Forms.RadioButton MouseradioButton;
        private System.Windows.Forms.Label FakeLabel;
        private CueTextbox ScanKeyTextBox;
    }
}