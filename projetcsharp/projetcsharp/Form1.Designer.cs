namespace projetcsharp
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            administrateur = new Button();
            patient = new Button();
            secretiare = new Button();
            medecin = new Button();
            label1 = new Label();
            SuspendLayout();
            // 
            // administrateur
            // 
            administrateur.BackgroundImage = (Image)resources.GetObject("administrateur.BackgroundImage");
            administrateur.BackgroundImageLayout = ImageLayout.Stretch;
            administrateur.Location = new Point(152, 124);
            administrateur.Margin = new Padding(3, 4, 3, 4);
            administrateur.Name = "administrateur";
            administrateur.Size = new Size(187, 180);
            administrateur.TabIndex = 0;
            administrateur.UseVisualStyleBackColor = true;
            administrateur.Click += administrateur_Click;
            // 
            // patient
            // 
            patient.BackgroundImage = (Image)resources.GetObject("patient.BackgroundImage");
            patient.BackgroundImageLayout = ImageLayout.Stretch;
            patient.Location = new Point(152, 351);
            patient.Margin = new Padding(3, 4, 3, 4);
            patient.Name = "patient";
            patient.Size = new Size(187, 177);
            patient.TabIndex = 1;
            patient.UseVisualStyleBackColor = true;
            patient.Click += patient_Click;
            // 
            // secretiare
            // 
            secretiare.BackgroundImage = (Image)resources.GetObject("secretiare.BackgroundImage");
            secretiare.BackgroundImageLayout = ImageLayout.Stretch;
            secretiare.Location = new Point(371, 351);
            secretiare.Margin = new Padding(3, 4, 3, 4);
            secretiare.Name = "secretiare";
            secretiare.Size = new Size(187, 177);
            secretiare.TabIndex = 2;
            secretiare.UseVisualStyleBackColor = true;
            secretiare.Click += secretiare_Click;
            // 
            // medecin
            // 
            medecin.BackgroundImage = (Image)resources.GetObject("medecin.BackgroundImage");
            medecin.BackgroundImageLayout = ImageLayout.Stretch;
            medecin.Location = new Point(371, 124);
            medecin.Margin = new Padding(3, 4, 3, 4);
            medecin.Name = "medecin";
            medecin.Size = new Size(187, 180);
            medecin.TabIndex = 3;
            medecin.UseVisualStyleBackColor = true;
            medecin.Click += medecin_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Showcard Gothic", 20F, FontStyle.Bold);
            label1.Location = new Point(201, 53);
            label1.Name = "label1";
            label1.Size = new Size(329, 43);
            label1.TabIndex = 4;
            label1.Text = "Centre medicale";
            label1.Click += label1_Click_1;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(754, 600);
            Controls.Add(label1);
            Controls.Add(medecin);
            Controls.Add(secretiare);
            Controls.Add(patient);
            Controls.Add(administrateur);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 4, 3, 4);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Centre medical";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button administrateur;
        private Button patient;
        private Button secretiare;
        private Button medecin;
        private Label label1;
    }
}
