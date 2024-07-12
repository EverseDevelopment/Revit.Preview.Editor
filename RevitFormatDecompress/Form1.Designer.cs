namespace RevitFormatDecompress
{
    partial class Form1
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
            this.button1 = new System.Windows.Forms.Button();
            this.OutputBox = new System.Windows.Forms.TextBox();
            this.InputBox = new System.Windows.Forms.TextBox();
            this.FileBox = new System.Windows.Forms.TextBox();
            this.BoxStorage = new System.Windows.Forms.TextBox();
            this.CheckboxAll = new System.Windows.Forms.CheckBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(307, 341);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(189, 71);
            this.button1.TabIndex = 0;
            this.button1.Text = "Run";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // OutputBox
            // 
            this.OutputBox.Location = new System.Drawing.Point(112, 270);
            this.OutputBox.Name = "OutputBox";
            this.OutputBox.Size = new System.Drawing.Size(457, 20);
            this.OutputBox.TabIndex = 1;
            // 
            // InputBox
            // 
            this.InputBox.Location = new System.Drawing.Point(112, 191);
            this.InputBox.Name = "InputBox";
            this.InputBox.Size = new System.Drawing.Size(457, 20);
            this.InputBox.TabIndex = 2;
            // 
            // FileBox
            // 
            this.FileBox.Location = new System.Drawing.Point(112, 112);
            this.FileBox.Name = "FileBox";
            this.FileBox.Size = new System.Drawing.Size(457, 20);
            this.FileBox.TabIndex = 3;
            // 
            // BoxStorage
            // 
            this.BoxStorage.Location = new System.Drawing.Point(112, 50);
            this.BoxStorage.Name = "BoxStorage";
            this.BoxStorage.Size = new System.Drawing.Size(457, 20);
            this.BoxStorage.TabIndex = 4;
            // 
            // CheckboxAll
            // 
            this.CheckboxAll.AutoSize = true;
            this.CheckboxAll.Location = new System.Drawing.Point(112, 12);
            this.CheckboxAll.Name = "CheckboxAll";
            this.CheckboxAll.Size = new System.Drawing.Size(61, 17);
            this.CheckboxAll.TabIndex = 5;
            this.CheckboxAll.Text = "All Files";
            this.CheckboxAll.UseVisualStyleBackColor = true;
            this.CheckboxAll.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(598, 270);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 6;
            this.button2.Text = "...";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(598, 188);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 7;
            this.button3.Text = "...";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.CheckboxAll);
            this.Controls.Add(this.BoxStorage);
            this.Controls.Add(this.FileBox);
            this.Controls.Add(this.InputBox);
            this.Controls.Add(this.OutputBox);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox OutputBox;
        private System.Windows.Forms.TextBox InputBox;
        private System.Windows.Forms.TextBox FileBox;
        private System.Windows.Forms.TextBox BoxStorage;
        private System.Windows.Forms.CheckBox CheckboxAll;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
    }
}

