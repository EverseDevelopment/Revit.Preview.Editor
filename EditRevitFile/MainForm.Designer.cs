namespace EditRevitFile
{
    partial class MainForm
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
            this.Edit = new System.Windows.Forms.Button();
            this.InputBox = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.ImageBox = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Edit
            // 
            this.Edit.Location = new System.Drawing.Point(158, 204);
            this.Edit.Name = "Edit";
            this.Edit.Size = new System.Drawing.Size(259, 63);
            this.Edit.TabIndex = 0;
            this.Edit.Text = "Edit";
            this.Edit.UseVisualStyleBackColor = true;
            this.Edit.Click += new System.EventHandler(this.button1_Click);
            // 
            // InputBox
            // 
            this.InputBox.Location = new System.Drawing.Point(38, 67);
            this.InputBox.Name = "InputBox";
            this.InputBox.Size = new System.Drawing.Size(436, 20);
            this.InputBox.TabIndex = 1;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(498, 67);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 20);
            this.button3.TabIndex = 4;
            this.button3.Text = "...";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click_1);
            // 
            // ImageBox
            // 
            this.ImageBox.Location = new System.Drawing.Point(38, 121);
            this.ImageBox.Name = "ImageBox";
            this.ImageBox.Size = new System.Drawing.Size(436, 20);
            this.ImageBox.TabIndex = 5;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(498, 121);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 20);
            this.button1.TabIndex = 6;
            this.button1.Text = "...";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(609, 311);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.ImageBox);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.InputBox);
            this.Controls.Add(this.Edit);
            this.Name = "MainForm";
            this.Text = "Revit Preview Editor";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Edit;
        private System.Windows.Forms.TextBox InputBox;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox ImageBox;
        private System.Windows.Forms.Button button1;
    }
}

