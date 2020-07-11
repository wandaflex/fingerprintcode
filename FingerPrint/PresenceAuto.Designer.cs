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
            this.SuspendLayout();
            // 
            // BTN_Start
            // 
            this.BTN_Start.Location = new System.Drawing.Point(27, 39);
            this.BTN_Start.Name = "BTN_Start";
            this.BTN_Start.Size = new System.Drawing.Size(140, 36);
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
            this.LBL_Connect.Location = new System.Drawing.Point(24, 9);
            this.LBL_Connect.Name = "LBL_Connect";
            this.LBL_Connect.Size = new System.Drawing.Size(73, 13);
            this.LBL_Connect.TabIndex = 2;
            this.LBL_Connect.Text = "Disconnected";
            // 
            // PresenceAuto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.LBL_Connect);
            this.Controls.Add(this.LSV_Rcev);
            this.Controls.Add(this.BTN_Start);
            this.Name = "PresenceAuto";
            this.Text = "PresenceAuto";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BTN_Start;
        private System.Windows.Forms.ListView LSV_Rcev;
        private System.Windows.Forms.Label LBL_Connect;
    }
}