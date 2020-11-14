namespace FingerPrint
{
    partial class Diagnostic
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
            this.TXB_Input = new System.Windows.Forms.TextBox();
            this.BTN_Rst = new System.Windows.Forms.Button();
            this.BTN_Send = new System.Windows.Forms.Button();
            this.LBL_Etat = new System.Windows.Forms.Label();
            this.LSV_Diag = new System.Windows.Forms.ListView();
            this.BTN_Connect = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // TXB_Input
            // 
            this.TXB_Input.Location = new System.Drawing.Point(12, 48);
            this.TXB_Input.Name = "TXB_Input";
            this.TXB_Input.Size = new System.Drawing.Size(286, 20);
            this.TXB_Input.TabIndex = 0;
            // 
            // BTN_Rst
            // 
            this.BTN_Rst.Location = new System.Drawing.Point(29, 74);
            this.BTN_Rst.Name = "BTN_Rst";
            this.BTN_Rst.Size = new System.Drawing.Size(86, 32);
            this.BTN_Rst.TabIndex = 1;
            this.BTN_Rst.Text = "Reset";
            this.BTN_Rst.UseVisualStyleBackColor = true;
            // 
            // BTN_Send
            // 
            this.BTN_Send.Location = new System.Drawing.Point(212, 74);
            this.BTN_Send.Name = "BTN_Send";
            this.BTN_Send.Size = new System.Drawing.Size(86, 32);
            this.BTN_Send.TabIndex = 3;
            this.BTN_Send.Text = "SEND";
            this.BTN_Send.UseVisualStyleBackColor = true;
            this.BTN_Send.Click += new System.EventHandler(this.BTN_Send_Click);
            // 
            // LBL_Etat
            // 
            this.LBL_Etat.AutoSize = true;
            this.LBL_Etat.BackColor = System.Drawing.Color.Red;
            this.LBL_Etat.Location = new System.Drawing.Point(231, 9);
            this.LBL_Etat.Name = "LBL_Etat";
            this.LBL_Etat.Size = new System.Drawing.Size(67, 13);
            this.LBL_Etat.TabIndex = 4;
            this.LBL_Etat.Text = "Disconected";
            // 
            // LSV_Diag
            // 
            this.LSV_Diag.HideSelection = false;
            this.LSV_Diag.Location = new System.Drawing.Point(312, 9);
            this.LSV_Diag.Name = "LSV_Diag";
            this.LSV_Diag.Size = new System.Drawing.Size(479, 430);
            this.LSV_Diag.TabIndex = 5;
            this.LSV_Diag.UseCompatibleStateImageBehavior = false;
            this.LSV_Diag.View = System.Windows.Forms.View.List;
            // 
            // BTN_Connect
            // 
            this.BTN_Connect.Location = new System.Drawing.Point(16, 9);
            this.BTN_Connect.Name = "BTN_Connect";
            this.BTN_Connect.Size = new System.Drawing.Size(87, 30);
            this.BTN_Connect.TabIndex = 6;
            this.BTN_Connect.Text = "Connection";
            this.BTN_Connect.UseVisualStyleBackColor = true;
            this.BTN_Connect.Click += new System.EventHandler(this.BTN_Connect_Click);
            // 
            // Diagnostic
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.BTN_Connect);
            this.Controls.Add(this.LSV_Diag);
            this.Controls.Add(this.LBL_Etat);
            this.Controls.Add(this.BTN_Send);
            this.Controls.Add(this.BTN_Rst);
            this.Controls.Add(this.TXB_Input);
            this.Name = "Diagnostic";
            this.Text = "Diagnostic";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TXB_Input;
        private System.Windows.Forms.Button BTN_Rst;
        private System.Windows.Forms.Button BTN_Send;
        private System.Windows.Forms.Label LBL_Etat;
        private System.Windows.Forms.ListView LSV_Diag;
        private System.Windows.Forms.Button BTN_Connect;
    }
}