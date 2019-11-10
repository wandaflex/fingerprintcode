/*
 * 
 * PROGRAMMEUR : RAOUL BUGUEM & JONATHAN ZOGONA
 * 
 * 
 */



using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace FingerPrint
{
    public partial class Admistrateur : Form
    {
        string connectionString = @"Server=localhost;Database=presence_db;Uid=root;Pwd='';";
        public int adminID = 0;
        int cycleID = 0;
        int classeID = 0;
        public Admistrateur()
        {
            InitializeComponent();
        }

        private void Admistrateur_Load(object sender, EventArgs e)
        {
            GridFill("AdminViewAll", DGV_ListeAdmin);
            GridFill("CycleViewAll", DGV_ListeCycle);
            GridFill("ClasseViewAll", DGV_ListeClasse);

            //Cedric: Ajout fichier TXT pout lecture des hauraires.
            try
            {
                StreamReader sr = new StreamReader("./Horaires.txt");
                string line = sr.ReadLine();

                while (line != null)
                {
                    CBX_HeureDebutProg.Items.Add(line);
                    CBX_HeureFinProg.Items.Add(line);
                    line = sr.ReadLine();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }
        }

        #region Enregistrement Administrateur 
        private void BTN_EnregisterAdmin_Click(object sender, EventArgs e)
        {
            try
            {
                using (MySqlConnection mySqlCon = new MySqlConnection(connectionString))
                {
                    mySqlCon.Open();
                    MySqlCommand mySqlCmd = new MySqlCommand("AdminAddOrEdit", mySqlCon);
                    mySqlCmd.CommandType = CommandType.StoredProcedure;
                    mySqlCmd.Parameters.AddWithValue("_AdminID", adminID);
                    mySqlCmd.Parameters.AddWithValue("_AdminNom", TXB_NomAdmin.Text.Trim());
                    mySqlCmd.Parameters.AddWithValue("_AdminPremon", TXB_PrenomAdmin.Text.Trim());
                    mySqlCmd.Parameters.AddWithValue("_AdminLogin", TXB_LoginAdmin.Text.Trim());
                    mySqlCmd.Parameters.AddWithValue("_AdminPwd", TXB_MotDePasseAdmin.Text.Trim());
                    mySqlCmd.ExecuteNonQuery();
                    MessageBox.Show("Submited successfully");
                    GridFill("AdminViewAll", DGV_ListeAdmin);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Message ");

            }
         
        }

        private void BTN_AnnulerAdmin_Click(object sender, EventArgs e)
        {
            cleanForm(GBX_FormAdmin);
        }

        void GridFill(String procedure, DataGridView dataGrid)
        {
            using (MySqlConnection mySqlCon = new MySqlConnection(connectionString))
            {
                mySqlCon.Open();
                MySqlDataAdapter sqlDa = new MySqlDataAdapter(procedure, mySqlCon);
                sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                //sqlDa.SelectCommand.Parameters.AddWithValue("AdminSearchByValue", TXB_RechercheAdmin.Text.Trim());
                DataTable dtb = new DataTable();
                sqlDa.Fill(dtb);
                dataGrid.DataSource = dtb;
                dataGrid.Columns[0].Visible = false;
                dataGrid.Columns[dataGrid.Columns.Count-1].Visible = false; 
            }
        }

        public void cleanForm(GroupBox groupBox)
        {
            foreach (Control oControlFormulaire in groupBox.Controls)
                if (oControlFormulaire is TextBox)
                    oControlFormulaire.Text = String.Empty;
            adminID = 0;
       
            BTN_EnregisterAdmin.Text = "Enregistrer";
          
        }
        #endregion

        #region Classe
        private void BTN_EnregisterClasse_Click(object sender, EventArgs e)
        {
            try
            {
                using (MySqlConnection mySqlCon = new MySqlConnection(connectionString))
                {
                    mySqlCon.Open();
                    MySqlCommand mySqlCmd = new MySqlCommand("ClasseAddOrEdit", mySqlCon);
                    mySqlCmd.CommandType = CommandType.StoredProcedure;
                    mySqlCmd.Parameters.AddWithValue("_ClasseID", classeID);
                    mySqlCmd.Parameters.AddWithValue("_ClasseNom", TXB_NomClasse.Text.Trim());
                    mySqlCmd.Parameters.AddWithValue("_ClasseCode", TXB_CodeClasse.Text.Trim());
                    mySqlCmd.Parameters.AddWithValue("_ClasseDescription", TXB_DescriptionClasse.Text.Trim());
                    mySqlCmd.ExecuteNonQuery();
                    MessageBox.Show("Submited successfully");
                    GridFill("ClasseViewAll", DGV_ListeClasse);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Message ");

            }
        }

        private void DGV_ListeClasse_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DGV_ListeClasse.CurrentRow.Index != -1)
            {
                
                TXB_NomClasse.Text = DGV_ListeClasse.CurrentRow.Cells[1].Value.ToString();
                TXB_CodeClasse.Text = DGV_ListeClasse.CurrentRow.Cells[2].Value.ToString();
                TXB_DescriptionClasse.Text = DGV_ListeClasse.CurrentRow.Cells[3].Value.ToString();
                
                classeID = Convert.ToInt32(DGV_ListeClasse.CurrentRow.Cells[0].Value.ToString());
                BTN_EnregisterClasse.Text = "Modifier";
                
            }

        }

        #endregion

        private void DGV_ListeAdmin_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DGV_ListeAdmin.CurrentRow.Index != -1)
            {
                TXB_NomAdmin.Text = DGV_ListeAdmin.CurrentRow.Cells[1].Value.ToString();
                TXB_PrenomAdmin.Text = DGV_ListeAdmin.CurrentRow.Cells[2].Value.ToString();
                TXB_LoginAdmin.Text = DGV_ListeAdmin.CurrentRow.Cells[3].Value.ToString();
                TXB_MotDePasseAdmin.Text = DGV_ListeAdmin.CurrentRow.Cells[4].Value.ToString();
                adminID = Convert.ToInt32(DGV_ListeAdmin.CurrentRow.Cells[0].Value.ToString());
                BTN_EnregisterAdmin.Text = "Modifier";
            }
            
        }

        private void DGV_ListeProf_DoubleClick(object sender, EventArgs e)
        {

        }

        private void GBX_FormMatiere_Enter(object sender, EventArgs e)
        {

        }

        private void GBX_FormProg_Enter(object sender, EventArgs e)
        {

        }

        private void Label2_Click(object sender, EventArgs e)
        {

        }

        private void DateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void LBL_SelectNomProgramme_Click(object sender, EventArgs e)
        {

        }

        private void CBX_HeureDebutPres_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void BTN_EnregisterCycle_Click(object sender, EventArgs e)
        {
            try
            { 
               using (MySqlConnection mySqlCon = new MySqlConnection(connectionString))
               {
                   mySqlCon.Open();
                    MySqlCommand mySqlCmd = new MySqlCommand("CycleAddOrEdit", mySqlCon);
                    mySqlCmd.CommandType = CommandType.StoredProcedure;
                    mySqlCmd.Parameters.AddWithValue("_CycleID", cycleID);
                    mySqlCmd.Parameters.AddWithValue("_CycleNumero", TXB_NumCycle.Text.Trim());
                    mySqlCmd.Parameters.AddWithValue("_CycleDescription", TXB_DescriptionCycle.Text.Trim());
                    mySqlCmd.ExecuteNonQuery();
                    MessageBox.Show("Submited successfully");
                    GridFill("CycleViewAll", DGV_ListeCycle);
                    BTN_EnregisterCycle.Text = "Modifier";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Message ");

            }
        }

        private void DGV_ListeCycle_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DGV_ListeCycle.CurrentRow.Index != -1)
            {
                TXB_NumCycle.Text = DGV_ListeCycle.CurrentRow.Cells[1].Value.ToString();                
                TXB_DescriptionCycle.Text = DGV_ListeCycle.CurrentRow.Cells[2].Value.ToString();

                cycleID = Convert.ToInt32(DGV_ListeCycle.CurrentRow.Cells[0].Value.ToString());
                BTN_EnregisterCycle.Text = "Modifier";

            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void TXB_DescriptionProf_TextChanged(object sender, EventArgs e)
        {

        }

        private void GBX_FormProfesseur_Enter(object sender, EventArgs e)
        {

        }

        private void BTN_RechercheAdmin_Click(object sender, EventArgs e)
        {
            /*
            try
            {
                using (MySqlConnection mySqlCon = new MySqlConnection(connectionString))
                {
                    mySqlCon.Open();
                    MySqlCommand mySqlCmd = new MySqlCommand("AdminSearchByValue", mySqlCon);
                    mySqlCmd.CommandType = CommandType.StoredProcedure;
                    mySqlCmd.Parameters.AddWithValue("_SearchValue", TXB_RechercheAdmin.Text.Trim());
                    mySqlCmd.ExecuteNonQuery();
                    MessageBox.Show("Submited successfully");                   
                    GridFill("AdminSearchByValue", DGV_ListeAdmin);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Message ");
            }
            */
        }

        private void BTN_EnregistrerProf_Click(object sender, EventArgs e)
        {

        }
    }
}