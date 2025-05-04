namespace projetcsharp
{
    partial class dossierpatient
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dossierpatient));
            label5 = new Label();
            textBox4 = new TextBox();
            textBox2 = new TextBox();
            textBox1 = new TextBox();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            textBox3 = new TextBox();
            label4 = new Label();
            textBox5 = new TextBox();
            button4 = new Button();
            SuspendLayout();
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = SystemColors.ButtonHighlight;
            label5.Location = new Point(216, 241);
            label5.Name = "label5";
            label5.Size = new Size(147, 20);
            label5.TabIndex = 25;
            label5.Text = "Traitements réguliers";
            // 
            // textBox4
            // 
            textBox4.Location = new Point(369, 234);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(125, 27);
            textBox4.TabIndex = 24;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(369, 179);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(125, 27);
            textBox2.TabIndex = 23;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(369, 71);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(125, 27);
            textBox1.TabIndex = 22;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = SystemColors.ButtonHighlight;
            label3.Location = new Point(250, 128);
            label3.Name = "label3";
            label3.Size = new Size(113, 20);
            label3.TabIndex = 21;
            label3.Text = "Groupe sanguin";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = SystemColors.ButtonHighlight;
            label2.Location = new Point(218, 186);
            label2.Name = "label2";
            label2.Size = new Size(145, 20);
            label2.TabIndex = 20;
            label2.Text = "Maladies chroniques";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = SystemColors.ButtonHighlight;
            label1.Location = new Point(293, 74);
            label1.Name = "label1";
            label1.Size = new Size(56, 20);
            label1.TabIndex = 19;
            label1.Text = "patient";
            // 
            // textBox3
            // 
            textBox3.Location = new Point(369, 280);
            textBox3.Multiline = true;
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(178, 112);
            textBox3.TabIndex = 17;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = SystemColors.ButtonHighlight;
            label4.Location = new Point(269, 326);
            label4.Name = "label4";
            label4.Size = new Size(39, 20);
            label4.TabIndex = 16;
            label4.Text = "note";
            // 
            // textBox5
            // 
            textBox5.Location = new Point(369, 121);
            textBox5.Name = "textBox5";
            textBox5.Size = new Size(125, 27);
            textBox5.TabIndex = 26;
            // 
            // button4
            // 
            button4.Location = new Point(12, 32);
            button4.Name = "button4";
            button4.Size = new Size(56, 29);
            button4.TabIndex = 27;
            button4.Text = "<--";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // dossierpatient
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoValidate = AutoValidate.EnablePreventFocusChange;
            BackColor = SystemColors.ButtonHighlight;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(770, 474);
            Controls.Add(button4);
            Controls.Add(textBox5);
            Controls.Add(label5);
            Controls.Add(textBox4);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(textBox3);
            Controls.Add(label4);
            FormBorderStyle = FormBorderStyle.None;
            Name = "dossierpatient";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "dossierpatient";
            Load += dossierpatient_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label5;
        private TextBox textBox4;
        private TextBox textBox2;
        private TextBox textBox1;
        private Label label3;
        private Label label2;
        private Label label1;
        private TextBox textBox3;
        private Label label4;
        private TextBox textBox5;
        private Button button4;
    }
}