namespace projetcsharp
{
    partial class dossier_medical
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dossier_medical));
            label4 = new Label();
            textBox3 = new TextBox();
            button1 = new Button();
            comboBox1 = new ComboBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            textBox4 = new TextBox();
            label5 = new Label();
            button4 = new Button();
            button2 = new Button();
            SuspendLayout();
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = SystemColors.ButtonHighlight;
            label4.Location = new Point(213, 336);
            label4.Name = "label4";
            label4.Size = new Size(39, 20);
            label4.TabIndex = 5;
            label4.Text = "note";
            // 
            // textBox3
            // 
            textBox3.Location = new Point(293, 303);
            textBox3.Multiline = true;
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(178, 112);
            textBox3.TabIndex = 6;
            // 
            // button1
            // 
            button1.Location = new Point(499, 381);
            button1.Name = "button1";
            button1.Size = new Size(94, 41);
            button1.TabIndex = 7;
            button1.Text = "enregistrer";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(293, 111);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(151, 28);
            comboBox1.TabIndex = 8;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = SystemColors.ButtonHighlight;
            label1.Location = new Point(213, 114);
            label1.Name = "label1";
            label1.Size = new Size(56, 20);
            label1.TabIndex = 9;
            label1.Text = "patient";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = SystemColors.ButtonHighlight;
            label2.Location = new Point(137, 226);
            label2.Name = "label2";
            label2.Size = new Size(145, 20);
            label2.TabIndex = 10;
            label2.Text = "Maladies chroniques";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = SystemColors.ButtonHighlight;
            label3.Location = new Point(157, 170);
            label3.Name = "label3";
            label3.Size = new Size(113, 20);
            label3.TabIndex = 11;
            label3.Text = "Groupe sanguin";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(293, 165);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(125, 27);
            textBox1.TabIndex = 12;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(293, 224);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(125, 27);
            textBox2.TabIndex = 13;
            // 
            // textBox4
            // 
            textBox4.Location = new Point(293, 263);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(125, 27);
            textBox4.TabIndex = 14;
            textBox4.TextChanged += textBox4_TextChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = SystemColors.ButtonHighlight;
            label5.Location = new Point(135, 272);
            label5.Name = "label5";
            label5.Size = new Size(147, 20);
            label5.TabIndex = 15;
            label5.Text = "Traitements réguliers";
            // 
            // button4
            // 
            button4.Location = new Point(24, 31);
            button4.Name = "button4";
            button4.Size = new Size(56, 29);
            button4.TabIndex = 16;
            button4.Text = "<--";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // button2
            // 
            button2.Location = new Point(499, 323);
            button2.Name = "button2";
            button2.Size = new Size(94, 33);
            button2.TabIndex = 17;
            button2.Text = "afficher";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // dossier_medical
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(752, 477);
            Controls.Add(button2);
            Controls.Add(button4);
            Controls.Add(label5);
            Controls.Add(textBox4);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(comboBox1);
            Controls.Add(button1);
            Controls.Add(textBox3);
            Controls.Add(label4);
            FormBorderStyle = FormBorderStyle.None;
            Name = "dossier_medical";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "dossier_medical";
            Load += dossier_medical_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label4;
        private TextBox textBox3;
        private Button button1;
        private ComboBox comboBox1;
        private Label label1;
        private Label label2;
        private Label label3;
        private TextBox textBox1;
        private TextBox textBox2;
        private TextBox textBox4;
        private Label label5;
        private Button button4;
        private Button button2;
    }
}