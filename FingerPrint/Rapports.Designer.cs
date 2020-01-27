namespace FingerPrint
{
    partial class Rapports
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
            this.GBX_FormRapport = new System.Windows.Forms.GroupBox();
            this.GBX_ListeRapport = new System.Windows.Forms.GroupBox();
            this.Recherche_Rapport = new System.Windows.Forms.GroupBox();
            this.TBX_RechercheRapport = new System.Windows.Forms.TextBox();
            this.BTN_RechercheRapport = new System.Windows.Forms.Button();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.BTN_ValiderRapport = new System.Windows.Forms.Button();
            this.LBL_DateDebutRaport = new System.Windows.Forms.Label();
            this.LBL_DateFinRapport = new System.Windows.Forms.Label();
            this.DGV_Rapport = new System.Windows.Forms.DataGridView();
            this.GBX_FormRapport.SuspendLayout();
            this.GBX_ListeRapport.SuspendLayout();
            this.Recherche_Rapport.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGV_Rapport)).BeginInit();
            this.SuspendLayout();
            // 
            // GBX_FormRapport
            // 
            this.GBX_FormRapport.Controls.Add(this.LBL_DateFinRapport);
            this.GBX_FormRapport.Controls.Add(this.LBL_DateDebutRaport);
            this.GBX_FormRapport.Controls.Add(this.BTN_ValiderRapport);
            this.GBX_FormRapport.Controls.Add(this.dateTimePicker2);
            this.GBX_FormRapport.Controls.Add(this.dateTimePicker1);
            this.GBX_FormRapport.Location = new System.Drawing.Point(14, 10);
            this.GBX_FormRapport.Name = "GBX_FormRapport";
            this.GBX_FormRapport.Size = new System.Drawing.Size(766, 102);
            this.GBX_FormRapport.TabIndex = 0;
            this.GBX_FormRapport.TabStop = false;
            this.GBX_FormRapport.Text = "Formulaire De Rapport";
            // 
            // GBX_ListeRapport
            // 
            this.GBX_ListeRapport.Controls.Add(this.DGV_Rapport);
            this.GBX_ListeRapport.Location = new System.Drawing.Point(13, 189);
            this.GBX_ListeRapport.Name = "GBX_ListeRapport";
            this.GBX_ListeRapport.Size = new System.Drawing.Size(766, 489);
            this.GBX_ListeRapport.TabIndex = 1;
            this.GBX_ListeRapport.TabStop = false;
            this.GBX_ListeRapport.Text = "Liste Rapport";
            this.GBX_ListeRapport.Enter += new System.EventHandler(this.GBX_ListeRapport_Enter);
            // 
            // Recherche_Rapport
            // 
            this.Recherche_Rapport.Controls.Add(this.TBX_RechercheRapport);
            this.Recherche_Rapport.Controls.Add(this.BTN_RechercheRapport);
            this.Recherche_Rapport.Location = new System.Drawing.Point(14, 127);
            this.Recherche_Rapport.Name = "Recherche_Rapport";
            this.Recherche_Rapport.Size = new System.Drawing.Size(764, 49);
            this.Recherche_Rapport.TabIndex = 2;
            this.Recherche_Rapport.TabStop = false;
            this.Recherche_Rapport.Text = "Recherche";
            // 
            // TBX_RechercheRapport
            // 
            this.TBX_RechercheRapport.Location = new System.Drawing.Point(13, 17);
            this.TBX_RechercheRapport.Name = "TBX_RechercheRapport";
            this.TBX_RechercheRapport.Size = new System.Drawing.Size(558, 20);
            this.TBX_RechercheRapport.TabIndex = 1;
            // 
            // BTN_RechercheRapport
            // 
            this.BTN_RechercheRapport.Location = new System.Drawing.Point(602, 16);
            this.BTN_RechercheRapport.Name = "BTN_RechercheRapport";
            this.BTN_RechercheRapport.Size = new System.Drawing.Size(143, 23);
            this.BTN_RechercheRapport.TabIndex = 0;
            this.BTN_RechercheRapport.Text = "Recherche";
            this.BTN_RechercheRapport.UseVisualStyleBackColor = true;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(27, 53);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(144, 20);
            this.dateTimePicker1.TabIndex = 0;
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Location = new System.Drawing.Point(294, 53);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(172, 20);
            this.dateTimePicker2.TabIndex = 1;
            this.dateTimePicker2.ValueChanged += new System.EventHandler(this.dateTimePicker2_ValueChanged);
            // 
            // BTN_ValiderRapport
            // 
            this.BTN_ValiderRapport.Location = new System.Drawing.Point(602, 51);
            this.BTN_ValiderRapport.Name = "BTN_ValiderRapport";
            this.BTN_ValiderRapport.Size = new System.Drawing.Size(143, 28);
            this.BTN_ValiderRapport.TabIndex = 2;
            this.BTN_ValiderRapport.Text = "Valider";
            this.BTN_ValiderRapport.UseVisualStyleBackColor = true;
            // 
            // LBL_DateDebutRaport
            // 
            this.LBL_DateDebutRaport.AutoSize = true;
            this.LBL_DateDebutRaport.Location = new System.Drawing.Point(24, 27);
            this.LBL_DateDebutRaport.Name = "LBL_DateDebutRaport";
            this.LBL_DateDebutRaport.Size = new System.Drawing.Size(145, 13);
            this.LBL_DateDebutRaport.TabIndex = 3;
            this.LBL_DateDebutRaport.Text = "Date de Debut (01 Jan 2020)";
            // 
            // LBL_DateFinRapport
            // 
            this.LBL_DateFinRapport.AutoSize = true;
            this.LBL_DateFinRapport.Location = new System.Drawing.Point(291, 27);
            this.LBL_DateFinRapport.Name = "LBL_DateFinRapport";
            this.LBL_DateFinRapport.Size = new System.Drawing.Size(130, 13);
            this.LBL_DateFinRapport.TabIndex = 4;
            this.LBL_DateFinRapport.Text = "Date de Fin (31 Jan 2020)";
            // 
            // DGV_Rapport
            // 
            this.DGV_Rapport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGV_Rapport.Location = new System.Drawing.Point(15, 22);
            this.DGV_Rapport.Name = "DGV_Rapport";
            this.DGV_Rapport.Size = new System.Drawing.Size(739, 457);
            this.DGV_Rapport.TabIndex = 0;
            // 
            // Rapports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 690);
            this.Controls.Add(this.Recherche_Rapport);
            this.Controls.Add(this.GBX_ListeRapport);
            this.Controls.Add(this.GBX_FormRapport);
            this.Name = "Rapports";
            this.Text = "Rapports";
            this.GBX_FormRapport.ResumeLayout(false);
            this.GBX_FormRapport.PerformLayout();
            this.GBX_ListeRapport.ResumeLayout(false);
            this.Recherche_Rapport.ResumeLayout(false);
            this.Recherche_Rapport.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGV_Rapport)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox GBX_FormRapport;
        private System.Windows.Forms.GroupBox GBX_ListeRapport;
        private System.Windows.Forms.GroupBox Recherche_Rapport;
        private System.Windows.Forms.TextBox TBX_RechercheRapport;
        private System.Windows.Forms.Button BTN_RechercheRapport;
        private System.Windows.Forms.Button BTN_ValiderRapport;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label LBL_DateFinRapport;
        private System.Windows.Forms.Label LBL_DateDebutRaport;
        private System.Windows.Forms.DataGridView DGV_Rapport;
    }
}