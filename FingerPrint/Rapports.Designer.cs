﻿namespace FingerPrint
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Rapports));
            this.GBX_FormRapport = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.LBL_DateFinRapport = new System.Windows.Forms.Label();
            this.LBL_DateDebutRaport = new System.Windows.Forms.Label();
            this.BTN_ValiderRapport = new System.Windows.Forms.Button();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.GBX_ListeRapport = new System.Windows.Forms.GroupBox();
            this.Recherche_Rapport = new System.Windows.Forms.GroupBox();
            this.TBX_RechercheRapport = new System.Windows.Forms.TextBox();
            this.BTN_RechercheRapport = new System.Windows.Forms.Button();
            this.salairePrintPreviewDialog = new System.Windows.Forms.PrintPreviewDialog();
            this.salairePrintDocument = new System.Drawing.Printing.PrintDocument();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.DGV_salaire = new System.Windows.Forms.DataGridView();
            this.NomProfesseur = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NombreHeur = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GBX_FormRapport.SuspendLayout();
            this.GBX_ListeRapport.SuspendLayout();
            this.Recherche_Rapport.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGV_salaire)).BeginInit();
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
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(647, 431);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "Imprimmer";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
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
            // LBL_DateDebutRaport
            // 
            this.LBL_DateDebutRaport.AutoSize = true;
            this.LBL_DateDebutRaport.Location = new System.Drawing.Point(24, 27);
            this.LBL_DateDebutRaport.Name = "LBL_DateDebutRaport";
            this.LBL_DateDebutRaport.Size = new System.Drawing.Size(145, 13);
            this.LBL_DateDebutRaport.TabIndex = 3;
            this.LBL_DateDebutRaport.Text = "Date de Debut (01 Jan 2020)";
            // 
            // BTN_ValiderRapport
            // 
            this.BTN_ValiderRapport.Location = new System.Drawing.Point(602, 51);
            this.BTN_ValiderRapport.Name = "BTN_ValiderRapport";
            this.BTN_ValiderRapport.Size = new System.Drawing.Size(143, 28);
            this.BTN_ValiderRapport.TabIndex = 2;
            this.BTN_ValiderRapport.Text = "Valider";
            this.BTN_ValiderRapport.UseVisualStyleBackColor = true;
            this.BTN_ValiderRapport.Click += new System.EventHandler(this.BTN_ValiderRapport_Click);
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Location = new System.Drawing.Point(294, 53);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(202, 20);
            this.dateTimePicker2.TabIndex = 1;
            this.dateTimePicker2.ValueChanged += new System.EventHandler(this.dateTimePicker2_ValueChanged);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(27, 53);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(198, 20);
            this.dateTimePicker1.TabIndex = 0;
            // 
            // GBX_ListeRapport
            // 
            this.GBX_ListeRapport.Controls.Add(this.button1);
            this.GBX_ListeRapport.Controls.Add(this.DGV_salaire);
            this.GBX_ListeRapport.Location = new System.Drawing.Point(13, 189);
            this.GBX_ListeRapport.Name = "GBX_ListeRapport";
            this.GBX_ListeRapport.Size = new System.Drawing.Size(766, 460);
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
            // salairePrintPreviewDialog
            // 
            this.salairePrintPreviewDialog.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.salairePrintPreviewDialog.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.salairePrintPreviewDialog.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.salairePrintPreviewDialog.ClientSize = new System.Drawing.Size(400, 300);
            this.salairePrintPreviewDialog.Document = this.salairePrintDocument;
            this.salairePrintPreviewDialog.Enabled = true;
            this.salairePrintPreviewDialog.Icon = ((System.Drawing.Icon)(resources.GetObject("salairePrintPreviewDialog.Icon")));
            this.salairePrintPreviewDialog.Name = "printPreviewDialog1";
            this.salairePrintPreviewDialog.Visible = false;
            this.salairePrintPreviewDialog.Load += new System.EventHandler(this.salairePrintPreviewDialog_Load);
            // 
            // salairePrintDocument
            // 
            this.salairePrintDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.salairePrintDocument_PrintPage);
            // 
            // printDialog1
            // 
            this.printDialog1.UseEXDialog = true;
            // 
            // DGV_salaire
            // 
            this.DGV_salaire.AllowUserToAddRows = false;
            this.DGV_salaire.AllowUserToDeleteRows = false;
            this.DGV_salaire.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGV_salaire.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NomProfesseur,
            this.NombreHeur,
            this.Column2,
            this.Column3});
            this.DGV_salaire.Location = new System.Drawing.Point(6, 19);
            this.DGV_salaire.Name = "DGV_salaire";
            this.DGV_salaire.ReadOnly = true;
            this.DGV_salaire.Size = new System.Drawing.Size(747, 380);
            this.DGV_salaire.TabIndex = 2;
            this.DGV_salaire.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // NomProfesseur
            // 
            this.NomProfesseur.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.NomProfesseur.HeaderText = "Non Professeur";
            this.NomProfesseur.Name = "NomProfesseur";
            this.NomProfesseur.ReadOnly = true;
            // 
            // NombreHeur
            // 
            this.NombreHeur.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.NombreHeur.HeaderText = "Nombre Heure Cycle1";
            this.NombreHeur.Name = "NombreHeur";
            this.NombreHeur.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column2.HeaderText = "Nombre Heure Cycle2";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column3.HeaderText = "Total A Payer";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // Rapports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 661);
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
            ((System.ComponentModel.ISupportInitialize)(this.DGV_salaire)).EndInit();
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
        private System.Windows.Forms.PrintPreviewDialog salairePrintPreviewDialog;
        private System.Drawing.Printing.PrintDocument salairePrintDocument;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PrintDialog printDialog1;
        private System.Windows.Forms.DataGridView DGV_salaire;
        private System.Windows.Forms.DataGridViewTextBoxColumn NomProfesseur;
        private System.Windows.Forms.DataGridViewTextBoxColumn NombreHeur;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
    }
}