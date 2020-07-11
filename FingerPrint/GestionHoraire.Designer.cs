namespace FingerPrint
{
    partial class GestionHoraire
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.DTP_DateFinProg = new System.Windows.Forms.DateTimePicker();
            this.BTN_EnregisterProg = new System.Windows.Forms.Button();
            this.LbL_DateProg = new System.Windows.Forms.Label();
            this.LBL_Classe = new System.Windows.Forms.Label();
            this.DTP_DateDebutProg = new System.Windows.Forms.DateTimePicker();
            this.CBX_Classe = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.DGV_programmeGH = new System.Windows.Forms.DataGridView();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGV_programmeGH)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.DTP_DateFinProg);
            this.groupBox1.Controls.Add(this.BTN_EnregisterProg);
            this.groupBox1.Controls.Add(this.LbL_DateProg);
            this.groupBox1.Controls.Add(this.LBL_Classe);
            this.groupBox1.Controls.Add(this.DTP_DateDebutProg);
            this.groupBox1.Controls.Add(this.CBX_Classe);
            this.groupBox1.Location = new System.Drawing.Point(10, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(690, 89);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(346, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "label2";
            // 
            // DTP_DateFinProg
            // 
            this.DTP_DateFinProg.Location = new System.Drawing.Point(340, 51);
            this.DTP_DateFinProg.Name = "DTP_DateFinProg";
            this.DTP_DateFinProg.Size = new System.Drawing.Size(200, 20);
            this.DTP_DateFinProg.TabIndex = 5;
            // 
            // BTN_EnregisterProg
            // 
            this.BTN_EnregisterProg.Location = new System.Drawing.Point(552, 50);
            this.BTN_EnregisterProg.Name = "BTN_EnregisterProg";
            this.BTN_EnregisterProg.Size = new System.Drawing.Size(75, 23);
            this.BTN_EnregisterProg.TabIndex = 4;
            this.BTN_EnregisterProg.Text = "button1";
            this.BTN_EnregisterProg.UseVisualStyleBackColor = true;
            this.BTN_EnregisterProg.Click += new System.EventHandler(this.BTN_EnregisterProg_Click);
            // 
            // LbL_DateProg
            // 
            this.LbL_DateProg.AutoSize = true;
            this.LbL_DateProg.Location = new System.Drawing.Point(131, 31);
            this.LbL_DateProg.Name = "LbL_DateProg";
            this.LbL_DateProg.Size = new System.Drawing.Size(35, 13);
            this.LbL_DateProg.TabIndex = 3;
            this.LbL_DateProg.Text = "label2";
            // 
            // LBL_Classe
            // 
            this.LBL_Classe.AutoSize = true;
            this.LBL_Classe.Location = new System.Drawing.Point(7, 31);
            this.LBL_Classe.Name = "LBL_Classe";
            this.LBL_Classe.Size = new System.Drawing.Size(38, 13);
            this.LBL_Classe.TabIndex = 2;
            this.LBL_Classe.Text = "Classe";
            // 
            // DTP_DateDebutProg
            // 
            this.DTP_DateDebutProg.Location = new System.Drawing.Point(134, 50);
            this.DTP_DateDebutProg.Name = "DTP_DateDebutProg";
            this.DTP_DateDebutProg.Size = new System.Drawing.Size(200, 20);
            this.DTP_DateDebutProg.TabIndex = 1;
            // 
            // CBX_Classe
            // 
            this.CBX_Classe.FormattingEnabled = true;
            this.CBX_Classe.Location = new System.Drawing.Point(7, 50);
            this.CBX_Classe.Name = "CBX_Classe";
            this.CBX_Classe.Size = new System.Drawing.Size(121, 21);
            this.CBX_Classe.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.DGV_programmeGH);
            this.groupBox2.Location = new System.Drawing.Point(10, 132);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(690, 343);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "groupBox2";
            // 
            // DGV_programmeGH
            // 
            this.DGV_programmeGH.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGV_programmeGH.Location = new System.Drawing.Point(10, 19);
            this.DGV_programmeGH.Name = "DGV_programmeGH";
            this.DGV_programmeGH.Size = new System.Drawing.Size(674, 318);
            this.DGV_programmeGH.TabIndex = 0;
            // 
            // GestionHoraire
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(716, 476);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "GestionHoraire";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GestionHoraire";
            this.Load += new System.EventHandler(this.GestionHoraire_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DGV_programmeGH)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button BTN_EnregisterProg;
        private System.Windows.Forms.Label LbL_DateProg;
        private System.Windows.Forms.Label LBL_Classe;
        private System.Windows.Forms.DateTimePicker DTP_DateDebutProg;
        private System.Windows.Forms.ComboBox CBX_Classe;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker DTP_DateFinProg;
        private System.Windows.Forms.DataGridView DGV_programmeGH;
    }
}