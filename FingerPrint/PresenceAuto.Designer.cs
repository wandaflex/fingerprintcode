namespace FingerPrint
{
    partial class PresenceAuto
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
            this.BTN_Start = new System.Windows.Forms.Button();
            this.LSV_Rcev = new System.Windows.Forms.ListView();
            this.LBL_Connect = new System.Windows.Forms.Label();
            this.RBN_Heure_Debut = new System.Windows.Forms.RadioButton();
            this.RBN_Heure_Fin = new System.Windows.Forms.RadioButton();
            this.GBX_choix_heures = new System.Windows.Forms.GroupBox();
            this.GBX_choix_heures.SuspendLayout();
            this.SuspendLayout();
            // 
            // BTN_Start
            // 
            this.BTN_Start.Location = new System.Drawing.Point(12, 153);
            this.BTN_Start.Name = "BTN_Start";
            this.BTN_Start.Size = new System.Drawing.Size(90, 36);
            this.BTN_Start.TabIndex = 0;
            this.BTN_Start.Text = "Demarer";
            this.BTN_Start.UseVisualStyleBackColor = true;
            this.BTN_Start.Click += new System.EventHandler(this.BTN_Start_Click);
            // 
            // LSV_Rcev
            // 
            this.LSV_Rcev.HideSelection = false;
            this.LSV_Rcev.Location = new System.Drawing.Point(264, 9);
            this.LSV_Rcev.Name = "LSV_Rcev";
            this.LSV_Rcev.Size = new System.Drawing.Size(522, 434);
            this.LSV_Rcev.TabIndex = 1;
            this.LSV_Rcev.UseCompatibleStateImageBehavior = false;
            this.LSV_Rcev.View = System.Windows.Forms.View.List;
            // 
            // LBL_Connect
            // 
            this.LBL_Connect.AutoSize = true;
            this.LBL_Connect.BackColor = System.Drawing.Color.Red;
            this.LBL_Connect.Location = new System.Drawing.Point(9, 9);
            this.LBL_Connect.Name = "LBL_Connect";
            this.LBL_Connect.Size = new System.Drawing.Size(73, 13);
            this.LBL_Connect.TabIndex = 2;
            this.LBL_Connect.Text = "Disconnected";
            // 
            // RBN_Heure_Debut
            // 
            this.RBN_Heure_Debut.AutoSize = true;
            this.RBN_Heure_Debut.Location = new System.Drawing.Point(6, 35);
            this.RBN_Heure_Debut.Name = "RBN_Heure_Debut";
            this.RBN_Heure_Debut.Size = new System.Drawing.Size(101, 17);
            this.RBN_Heure_Debut.TabIndex = 3;
            this.RBN_Heure_Debut.TabStop = true;
            this.RBN_Heure_Debut.Text = "Debut Du Cours";
            this.RBN_Heure_Debut.UseVisualStyleBackColor = true;
            // 
            // RBN_Heure_Fin
            // 
            this.RBN_Heure_Fin.AutoSize = true;
            this.RBN_Heure_Fin.Location = new System.Drawing.Point(6, 58);
            this.RBN_Heure_Fin.Name = "RBN_Heure_Fin";
            this.RBN_Heure_Fin.Size = new System.Drawing.Size(86, 17);
            this.RBN_Heure_Fin.TabIndex = 4;
            this.RBN_Heure_Fin.TabStop = true;
            this.RBN_Heure_Fin.Text = "Fin Du Cours";
            this.RBN_Heure_Fin.UseVisualStyleBackColor = true;
            // 
            // GBX_choix_heures
            // 
            this.GBX_choix_heures.Controls.Add(this.RBN_Heure_Debut);
            this.GBX_choix_heures.Controls.Add(this.RBN_Heure_Fin);
            this.GBX_choix_heures.Location = new System.Drawing.Point(12, 52);
            this.GBX_choix_heures.Name = "GBX_choix_heures";
            this.GBX_choix_heures.Size = new System.Drawing.Size(200, 95);
            this.GBX_choix_heures.TabIndex = 5;
            this.GBX_choix_heures.TabStop = false;
            this.GBX_choix_heures.Text = "groupBox1";
            // 
            // PresenceAuto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.GBX_choix_heures);
            this.Controls.Add(this.LBL_Connect);
            this.Controls.Add(this.LSV_Rcev);
            this.Controls.Add(this.BTN_Start);
            this.Name = "PresenceAuto";
            this.Text = "PresenceAuto";
            this.GBX_choix_heures.ResumeLayout(false);
            this.GBX_choix_heures.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BTN_Start;
        private System.Windows.Forms.ListView LSV_Rcev;
        private System.Windows.Forms.Label LBL_Connect;
        private System.Windows.Forms.RadioButton RBN_Heure_Debut;
        private System.Windows.Forms.RadioButton RBN_Heure_Fin;
        private System.Windows.Forms.GroupBox GBX_choix_heures;
    }
}