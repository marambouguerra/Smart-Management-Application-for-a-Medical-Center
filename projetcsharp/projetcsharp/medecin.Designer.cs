﻿namespace projetcsharp
{
    partial class medecin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(medecin));
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            pictureBox1 = new PictureBox();
            button4 = new Button();
            button5 = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(123, 270);
            button1.Name = "button1";
            button1.Size = new Size(132, 84);
            button1.TabIndex = 0;
            button1.Text = "emploi";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(411, 270);
            button2.Name = "button2";
            button2.Size = new Size(126, 84);
            button2.TabIndex = 1;
            button2.Text = "suivre les consultations.";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Location = new Point(261, 270);
            button3.Name = "button3";
            button3.Size = new Size(135, 84);
            button3.TabIndex = 2;
            button3.Text = "ajouter des dossiers médicaux";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(279, 88);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(217, 140);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 3;
            pictureBox1.TabStop = false;
            // 
            // button4
            // 
            button4.Location = new Point(27, 52);
            button4.Name = "button4";
            button4.Size = new Size(56, 29);
            button4.TabIndex = 4;
            button4.Text = "<--";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // button5
            // 
            button5.Location = new Point(543, 270);
            button5.Name = "button5";
            button5.Size = new Size(126, 84);
            button5.TabIndex = 5;
            button5.Text = "ajout documents medicaux";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // medecin
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(787, 461);
            Controls.Add(button5);
            Controls.Add(button4);
            Controls.Add(pictureBox1);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 4, 3, 4);
            Name = "medecin";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "medecin";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button button1;
        private Button button2;
        private Button button3;
        private PictureBox pictureBox1;
        private Button button4;
        private Button button5;
    }
}