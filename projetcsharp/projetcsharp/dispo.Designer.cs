namespace projetcsharp
{
    partial class dispo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dispo));
            comboBox1 = new ComboBox();
            r2 = new RadioButton();
            r1 = new RadioButton();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            SuspendLayout();
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "lundi", "mardi", "mercredi", "jeudi", "vendredi", "samedi", "dimanche" });
            comboBox1.Location = new Point(204, 164);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(151, 28);
            comboBox1.TabIndex = 54;
            comboBox1.Text = "jour";
            // 
            // r2
            // 
            r2.AutoSize = true;
            r2.BackColor = SystemColors.ButtonHighlight;
            r2.Location = new Point(391, 168);
            r2.Margin = new Padding(3, 4, 3, 4);
            r2.Name = "r2";
            r2.Size = new Size(78, 24);
            r2.TabIndex = 55;
            r2.TabStop = true;
            r2.Text = "14->23";
            r2.UseVisualStyleBackColor = false;
            // 
            // r1
            // 
            r1.AutoSize = true;
            r1.BackColor = SystemColors.ButtonHighlight;
            r1.Location = new Point(494, 168);
            r1.Margin = new Padding(3, 4, 3, 4);
            r1.Name = "r1";
            r1.Size = new Size(70, 24);
            r1.TabIndex = 56;
            r1.TabStop = true;
            r1.Text = "6->14";
            r1.UseVisualStyleBackColor = false;
            // 
            // button1
            // 
            button1.Location = new Point(223, 215);
            button1.Name = "button1";
            button1.Size = new Size(94, 56);
            button1.TabIndex = 57;
            button1.Text = "ajouter";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(335, 215);
            button2.Name = "button2";
            button2.Size = new Size(94, 56);
            button2.TabIndex = 58;
            button2.Text = "modifier";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Location = new Point(448, 215);
            button3.Name = "button3";
            button3.Size = new Size(94, 56);
            button3.TabIndex = 59;
            button3.Text = "supprimer";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button4
            // 
            button4.Location = new Point(12, 30);
            button4.Margin = new Padding(3, 4, 3, 4);
            button4.Name = "button4";
            button4.Size = new Size(72, 37);
            button4.TabIndex = 60;
            button4.Text = "<-";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // dispo
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(800, 450);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(r1);
            Controls.Add(r2);
            Controls.Add(comboBox1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "dispo";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "dispo";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox comboBox1;
        private RadioButton r2;
        private RadioButton r1;
        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
    }
}