namespace commo_rose
{
    partial class ActionButtonForm
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
            this.MouseKeyboardButtonsComboBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.KeyboardradioButton = new System.Windows.Forms.RadioButton();
            this.MouseradioButton = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // MouseKeyboardButtonsComboBox
            // 
            this.MouseKeyboardButtonsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.MouseKeyboardButtonsComboBox.FormattingEnabled = true;
            this.MouseKeyboardButtonsComboBox.Location = new System.Drawing.Point(122, 33);
            this.MouseKeyboardButtonsComboBox.Name = "MouseKeyboardButtonsComboBox";
            this.MouseKeyboardButtonsComboBox.Size = new System.Drawing.Size(100, 21);
            this.MouseKeyboardButtonsComboBox.TabIndex = 12;
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
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(127, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Action button key";
            // 
            // ActionButtonForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(289, 101);
            this.Controls.Add(this.MouseKeyboardButtonsComboBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.KeyboardradioButton);
            this.Controls.Add(this.MouseradioButton);
            this.Controls.Add(this.label3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "ActionButtonForm";
            this.Text = "Action button";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox MouseKeyboardButtonsComboBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton KeyboardradioButton;
        private System.Windows.Forms.RadioButton MouseradioButton;
        private System.Windows.Forms.Label label3;
    }
}