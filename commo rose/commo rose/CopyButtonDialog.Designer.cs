namespace commo_rose
{
    partial class CopyButtonDialog
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
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.PreservebackcolorcheckBox = new System.Windows.Forms.CheckBox();
            this.PreservetextcolorcheckBox = new System.Windows.Forms.CheckBox();
            this.PreservefontcheckBox = new System.Windows.Forms.CheckBox();
            this.OKButton = new System.Windows.Forms.Button();
            this.Cancelbutton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(64, 6);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Copy to:";
            // 
            // PreservebackcolorcheckBox
            // 
            this.PreservebackcolorcheckBox.AutoSize = true;
            this.PreservebackcolorcheckBox.Location = new System.Drawing.Point(208, 8);
            this.PreservebackcolorcheckBox.Name = "PreservebackcolorcheckBox";
            this.PreservebackcolorcheckBox.Size = new System.Drawing.Size(118, 17);
            this.PreservebackcolorcheckBox.TabIndex = 2;
            this.PreservebackcolorcheckBox.Text = "Preserve backcolor";
            this.PreservebackcolorcheckBox.UseVisualStyleBackColor = true;
            // 
            // PreservetextcolorcheckBox
            // 
            this.PreservetextcolorcheckBox.AutoSize = true;
            this.PreservetextcolorcheckBox.Location = new System.Drawing.Point(208, 27);
            this.PreservetextcolorcheckBox.Name = "PreservetextcolorcheckBox";
            this.PreservetextcolorcheckBox.Size = new System.Drawing.Size(111, 17);
            this.PreservetextcolorcheckBox.TabIndex = 3;
            this.PreservetextcolorcheckBox.Text = "Preserve textcolor";
            this.PreservetextcolorcheckBox.UseVisualStyleBackColor = true;
            // 
            // PreservefontcheckBox
            // 
            this.PreservefontcheckBox.AutoSize = true;
            this.PreservefontcheckBox.Location = new System.Drawing.Point(208, 46);
            this.PreservefontcheckBox.Name = "PreservefontcheckBox";
            this.PreservefontcheckBox.Size = new System.Drawing.Size(89, 17);
            this.PreservefontcheckBox.TabIndex = 4;
            this.PreservefontcheckBox.Text = "Preserve font";
            this.PreservefontcheckBox.UseVisualStyleBackColor = true;
            // 
            // OKButton
            // 
            this.OKButton.Location = new System.Drawing.Point(12, 42);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(75, 23);
            this.OKButton.TabIndex = 5;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // CancelButton
            // 
            this.Cancelbutton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancelbutton.Location = new System.Drawing.Point(110, 42);
            this.Cancelbutton.Name = "CancelButton";
            this.Cancelbutton.Size = new System.Drawing.Size(75, 23);
            this.Cancelbutton.TabIndex = 6;
            this.Cancelbutton.Text = "Cancel";
            this.Cancelbutton.UseVisualStyleBackColor = true;
            this.Cancelbutton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // CopyButtonDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(324, 74);
            this.Controls.Add(this.Cancelbutton);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.PreservefontcheckBox);
            this.Controls.Add(this.PreservetextcolorcheckBox);
            this.Controls.Add(this.PreservebackcolorcheckBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "CopyButtonDialog";
            this.Text = "Copy";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox PreservebackcolorcheckBox;
        private System.Windows.Forms.CheckBox PreservetextcolorcheckBox;
        private System.Windows.Forms.CheckBox PreservefontcheckBox;
        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.Button Cancelbutton;
    }
}