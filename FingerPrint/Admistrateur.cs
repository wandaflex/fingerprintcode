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
using System.Net;
using System.Net.Sockets;

namespace FingerPrint
{
    public partial class Admistrateur : Form
    {
        string connectionString = @"Server=localhost;Database=presence_db;Uid=root;Pwd='';";
        public int adminID = 0;

        Socket WandaUDP;
        EndPoint WandaLocal, WandaRemote;
        string IP_Adrress_PC = "192.168.0.116";
        string port_PC = "2500";

        string IP_Adrress_Client = "192.168.0.200";
        string port_Client = "2500";

        //int cycleID = 0;
        int classeID = 0;
        int professeurID = 0;
        int matiereID = 0;

        public Admistrateur()
        {
            InitializeComponent();
            WandaUDP = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            WandaUDP.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.ReuseAddress, true);

        }

        #region Initialisation du formulaire principal
        private void Admistrateur_Load(object sender, EventArgs e)
        {
            GridFill("AdminViewAll", DGV_ListeAdmin);
            //GridFill("CycleViewAll", DGV_ListeCycle);
            GridFill("ClasseViewAll", DGV_ListeClasse);
            GridFill("ProfViewAll", DGV_ListeProf);

            GridFill("MatiereViewAll", DGV_ListeMatiere);

            ComboFill("ProgrammeClasseComboViewAll", ref CBX_SelectClasse, "nom", "idClasse");
            ComboFill("ProgrammeMatiereComboViewAll", ref CBX_SelectProgProfMatiere, "ProfMatiere", "idPROFESSEUR_MATIERE");
            //ComboFill("ProgrammeMatiereComboViewAll", ref CBX_SelectProgMatiere, "Matiere", "idMatiere");


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

        #endregion

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
                    mySqlCmd.Parameters.AddWithValue("_AdminType", CBX_TypeUtil.SelectedItem.ToString().Trim());    // Jonathan : Ce paramètre manquait
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
            cleanForm(GBX_FormAdmin,ref adminID, BTN_EnregisterAdmin);
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
                dataGrid.Columns[dataGrid.Columns.Count - 1].Visible = false;
            }
        }

        public void cleanForm(GroupBox groupBox,ref int id,Button enregistrerButton)
        {
            foreach (Control oControlFormulaire in groupBox.Controls)
                if (oControlFormulaire is TextBox)
                    oControlFormulaire.Text = String.Empty;
            id = 0;

            enregistrerButton.Text = "Enregistrer";

        }
        #endregion

        #region Classe
        private void BTN_EnregisterClasse_Click(object sender, EventArgs e)
        {
            try
            {
                // Enregistrer
                using (MySqlConnection mySqlCon = new MySqlConnection(connectionString))
                {
                    mySqlCon.Open();
                    MySqlCommand mySqlCmd = new MySqlCommand("ClasseAddOrEdit", mySqlCon);
                    mySqlCmd.CommandType = CommandType.StoredProcedure;
                    mySqlCmd.Parameters.AddWithValue("_ClasseID", classeID);
                    mySqlCmd.Parameters.AddWithValue("_ClasseNom", TXB_NomClasse.Text.Trim());
                    mySqlCmd.Parameters.AddWithValue("_ClasseCode", TXB_CodeClasse.Text.Trim());
                    mySqlCmd.Parameters.AddWithValue("_ClasseCycle", CBX_CycleClasse.Text.Trim());
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
                CBX_CycleClasse.SelectedIndex = CBX_CycleClasse.FindStringExact(DGV_ListeClasse.CurrentRow.Cells[3].Value.ToString());
                TXB_DescriptionClasse.Text = DGV_ListeClasse.CurrentRow.Cells[4].Value.ToString();                

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


        private void BTN_EnregisterMatiere_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(CBX_FormMatiere.SelectedValue.ToString());

            try
            {
                using (MySqlConnection mySqlCon = new MySqlConnection(connectionString))
                {
                    mySqlCon.Open();
                    MySqlCommand mySqlCmd = new MySqlCommand("MatiereAddOrEdit", mySqlCon);
                    mySqlCmd.CommandType = CommandType.StoredProcedure;
                    mySqlCmd.Parameters.AddWithValue("_MatiereID", matiereID);
                    mySqlCmd.Parameters.AddWithValue("_MatiereCode", TXB_CodeMatiere.Text.Trim());
                    mySqlCmd.Parameters.AddWithValue("_MatiereNom", TXB_NomMatiere.Text.Trim());
                    //mySqlCmd.Parameters.AddWithValue("_CycleID_Cycle", CBX_FormMatiere.SelectedValue.ToString());    // Jonathan : Ce paramètre manquait
                   
                    mySqlCmd.ExecuteNonQuery();
                    MessageBox.Show("Submited successfully");
                    GridFill("MAtiereViewAll", DGV_ListeMatiere);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Message ");

            }
        }

        private void DGV_ListeMatiere_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DGV_ListeMatiere.CurrentRow.Index != -1)
            {
                TXB_CodeMatiere.Text = DGV_ListeMatiere.CurrentRow.Cells[1].Value.ToString();
                TXB_NomMatiere.Text = DGV_ListeMatiere.CurrentRow.Cells[2].Value.ToString();

                //CBX_FormMatiere.SelectedIndex = CBX_FormMatiere.FindStringExact(DGV_ListeMatiere.CurrentRow.Cells[3].Value.ToString());


                matiereID = Convert.ToInt32(DGV_ListeMatiere.CurrentRow.Cells[0].Value.ToString());
                BTN_EnregisterMatiere.Text = "Modifier";
            }
        }



        //private void BTN_EnregisterCycle_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        using (MySqlConnection mySqlCon = new MySqlConnection(connectionString))
        //        {
        //            mySqlCon.Open();
        //            MySqlCommand mySqlCmd = new MySqlCommand("CycleAddOrEdit", mySqlCon);
        //            mySqlCmd.CommandType = CommandType.StoredProcedure;
        //            mySqlCmd.Parameters.AddWithValue("_CycleID", cycleID);
        //            mySqlCmd.Parameters.AddWithValue("_CycleNumero", TXB_NumCycle.Text.Trim());
        //            mySqlCmd.Parameters.AddWithValue("_CycleDescription", TXB_DescriptionCycle.Text.Trim());
        //            mySqlCmd.ExecuteNonQuery();
        //            MessageBox.Show("Submited successfully");
        //            GridFill("CycleViewAll", DGV_ListeCycle);
        //            BTN_EnregisterCycle.Text = "Modifier";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "Error Message ");

        //    }
        //}

        //private void DGV_ListeCycle_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    if (DGV_ListeCycle.CurrentRow.Index != -1)
        //    {
        //        TXB_NumCycle.Text = DGV_ListeCycle.CurrentRow.Cells[1].Value.ToString();
        //        TXB_DescriptionCycle.Text = DGV_ListeCycle.CurrentRow.Cells[2].Value.ToString();

        //        cycleID = Convert.ToInt32(DGV_ListeCycle.CurrentRow.Cells[0].Value.ToString());
        //        BTN_EnregisterCycle.Text = "Modifier";

        //    }
        //}



        private void BTN_RechercheAdmin_Click(object sender, EventArgs e)
        {

            using (MySqlConnection mySqlCon = new MySqlConnection(connectionString))
            {
                mySqlCon.Open();
                MySqlDataAdapter sqlDa = new MySqlDataAdapter("AdminSearchByValue", mySqlCon);
                sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlDa.SelectCommand.Parameters.AddWithValue("_SearchValue", TXB_RechercheAdmin.Text.Trim());
                DataTable dtb = new DataTable();
                sqlDa.Fill(dtb);
                DGV_ListeAdmin.DataSource = dtb;
                DGV_ListeAdmin.Columns[0].Visible = false;
                DGV_ListeAdmin.Columns[DGV_ListeAdmin.Columns.Count - 1].Visible = false;
            }


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
            try
            {
                // Test Commment Jonathan
                using (MySqlConnection mySqlCon = new MySqlConnection(connectionString))
                {
                    mySqlCon.Open();
                    MySqlCommand mySqlCmd = new MySqlCommand("ProfAddOrEdit", mySqlCon);
                    mySqlCmd.CommandType = CommandType.StoredProcedure;
                    mySqlCmd.Parameters.AddWithValue("_ProfID", professeurID);
                    mySqlCmd.Parameters.AddWithValue("_ProfNom", TXB_NomProf.Text.Trim());
                    mySqlCmd.Parameters.AddWithValue("_ProfPrenom", TXB_PrenomProf.Text.Trim());
                    mySqlCmd.Parameters.AddWithValue("_ProfTel_1", TXB_TelephoneProf.Text.Trim());
                    mySqlCmd.Parameters.AddWithValue("_ProfTel_2", TXB_Telephone2Prof.Text.Trim());
                    mySqlCmd.Parameters.AddWithValue("_Diplome", TXB_DiplomeProf.Text.Trim());
                    mySqlCmd.Parameters.AddWithValue("_ProfType", TXB_TypeProf.Text.Trim());
                    mySqlCmd.Parameters.AddWithValue("_ProfEmpreinte_1", TXB_Empreinte1Prof.Text.Trim());
                    mySqlCmd.Parameters.AddWithValue("_ProfEmpreinte_2", TXB_Empreinte2Prof.Text.Trim());
                    mySqlCmd.Parameters.AddWithValue("_ProfProfil", "Profil IMG");
                    mySqlCmd.Parameters.AddWithValue("_ProfETaux_Horaire_1", TXB_Taux1ierCycleProf.Text.Trim());
                    mySqlCmd.Parameters.AddWithValue("_ProfETaux_Horaire_2", TXB_Taux2iemeCycleProf.Text.Trim());
                    mySqlCmd.Parameters.AddWithValue("_profDate_recrutement", DTP_Recrutement.Value);
                    mySqlCmd.Parameters.AddWithValue("_Description", TXB_DescriptionProf.Text.Trim());
                    mySqlCmd.ExecuteNonQuery();
                    MessageBox.Show("Submited successfully");
                    GridFill("ProfViewAll", DGV_ListeProf);
                    BTN_EnregisterProg.Text = "Modifier";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Message ");
            }
        }



        private void DGV_ListeProf_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DGV_ListeProf.CurrentRow.Index != -1)
            {
                TXB_NomProf.Text = DGV_ListeProf.CurrentRow.Cells[1].Value.ToString();
                TXB_PrenomProf.Text = DGV_ListeProf.CurrentRow.Cells[2].Value.ToString();
                TXB_TelephoneProf.Text = DGV_ListeProf.CurrentRow.Cells[3].Value.ToString();
                TXB_Telephone2Prof.Text = DGV_ListeProf.CurrentRow.Cells[4].Value.ToString();
                TXB_DiplomeProf.Text = DGV_ListeProf.CurrentRow.Cells[5].Value.ToString();
                TXB_TypeProf.Text = DGV_ListeProf.CurrentRow.Cells[6].Value.ToString();
                TXB_Empreinte1Prof.Text = DGV_ListeProf.CurrentRow.Cells[7].Value.ToString();
                TXB_Empreinte2Prof.Text = DGV_ListeProf.CurrentRow.Cells[8].Value.ToString();
                TXB_Taux1ierCycleProf.Text = DGV_ListeProf.CurrentRow.Cells[10].Value.ToString();
                TXB_Taux2iemeCycleProf.Text = DGV_ListeProf.CurrentRow.Cells[11].Value.ToString();
                TXB_DescriptionProf.Text = DGV_ListeProf.CurrentRow.Cells[12].Value.ToString();

                DTP_Recrutement.Value = DateTime.Parse(DGV_ListeProf.CurrentRow.Cells[13].Value.ToString());


                professeurID = Convert.ToInt32(DGV_ListeProf.CurrentRow.Cells[0].Value.ToString());
                BTN_EnregistrerProf.Text = "Modifier";

            }
        }

        private void BTN_SupprimerProf_Click(object sender, EventArgs e)
        {
            using (MySqlConnection mySqlCon = new MySqlConnection(connectionString))
            {
                mySqlCon.Open();
                MySqlCommand mySqlCmd = new MySqlCommand("ProfDeleteByID", mySqlCon);
                mySqlCmd.CommandType = CommandType.StoredProcedure;
                mySqlCmd.Parameters.AddWithValue("_ProfID", professeurID);
                mySqlCmd.ExecuteNonQuery();
                MessageBox.Show("Deleted successfully");
                cleanForm(GBX_FormProfesseur,ref professeurID,BTN_EnregistrerProf);
                GridFill("ProfViewAll", DGV_ListeProf);

            }
        }

        private void BTN_RechercheProf_Click(object sender, EventArgs e)
        {
            using (MySqlConnection mySqlCon = new MySqlConnection(connectionString))
            {
                mySqlCon.Open();
                MySqlDataAdapter sqlDa = new MySqlDataAdapter("ProfSearchByValue", mySqlCon);
                sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlDa.SelectCommand.Parameters.AddWithValue("_SearchValue", TBX_RecherchePof.Text.Trim());
                DataTable dtb = new DataTable();
                sqlDa.Fill(dtb);
                DGV_ListeProf.DataSource = dtb;
                DGV_ListeProf.Columns[0].Visible = false;
                DGV_ListeProf.Columns[DGV_ListeProf.Columns.Count - 1].Visible = false;
            }
        }

        private void BTN_RechercheClasse_Click(object sender, EventArgs e)
        {
            using (MySqlConnection mySqlCon = new MySqlConnection(connectionString))
            {
                mySqlCon.Open();
                MySqlDataAdapter sqlDa = new MySqlDataAdapter("ClasseSearchByValue", mySqlCon);
                sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlDa.SelectCommand.Parameters.AddWithValue("_SearchValue", TXB_RechercheClasse.Text.Trim());
                DataTable dtb = new DataTable();
                sqlDa.Fill(dtb);
                DGV_ListeClasse.DataSource = dtb;
                DGV_ListeClasse.Columns[0].Visible = false;
                DGV_ListeClasse.Columns[DGV_ListeClasse.Columns.Count - 1].Visible = false;
            }
        }

        private void BTN_RechercheMatiere_Click(object sender, EventArgs e)
        {
            using (MySqlConnection mySqlCon = new MySqlConnection(connectionString))
            {
                mySqlCon.Open();
                MySqlDataAdapter sqlDa = new MySqlDataAdapter("MatiereSearchByValue", mySqlCon);
                sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlDa.SelectCommand.Parameters.AddWithValue("_SearchValue", TXB_RechercheMatiere.Text.Trim());
                DataTable dtb = new DataTable();
                sqlDa.Fill(dtb);
                DGV_ListeMatiere.DataSource = dtb;
                DGV_ListeMatiere.Columns[0].Visible = false;
                DGV_ListeMatiere.Columns[DGV_ListeMatiere.Columns.Count - 1].Visible = false;
            }
            
        }

        private void TXB_RechercheMatiere_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                using (MySqlConnection mySqlCon = new MySqlConnection(connectionString))
                {
                    mySqlCon.Open();
                    MySqlDataAdapter sqlDa = new MySqlDataAdapter("MatiereSearchByValue", mySqlCon);
                    sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                    sqlDa.SelectCommand.Parameters.AddWithValue("_SearchValue", TXB_RechercheMatiere.Text.Trim());
                    DataTable dtb = new DataTable();
                    sqlDa.Fill(dtb);
                    DGV_ListeMatiere.DataSource = dtb;
                    DGV_ListeMatiere.Columns[0].Visible = false;
                    DGV_ListeMatiere.Columns[DGV_ListeMatiere.Columns.Count - 1].Visible = false;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        //private void BTN_RechercheCycle_Click(object sender, EventArgs e)
        //{
        //    using (MySqlConnection mySqlCon = new MySqlConnection(connectionString))
        //    {
        //        mySqlCon.Open();
        //        MySqlDataAdapter sqlDa = new MySqlDataAdapter("CycleSearchByValue", mySqlCon);
        //        sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
        //        sqlDa.SelectCommand.Parameters.AddWithValue("_SearchValue", TXB_RechercheCycle.Text.Trim());
        //        DataTable dtb = new DataTable();
        //        sqlDa.Fill(dtb);
        //        DGV_ListeCycle.DataSource = dtb;
        //        DGV_ListeCycle.Columns[0].Visible = false;
        //        DGV_ListeCycle.Columns[DGV_ListeCycle.Columns.Count - 1].Visible = false;
        //    }
        //}

        private void BTN_SupprimerClasse_Click(object sender, EventArgs e)
        {
            using (MySqlConnection mySqlCon = new MySqlConnection(connectionString))
            {
                mySqlCon.Open();
                MySqlCommand mySqlCmd = new MySqlCommand("ClasseDeleteByID", mySqlCon);
                mySqlCmd.CommandType = CommandType.StoredProcedure;
                mySqlCmd.Parameters.AddWithValue("_ClasseID", classeID);
                mySqlCmd.ExecuteNonQuery();
                MessageBox.Show("Deleted successfully");
                cleanForm(GBX_FormClasse, ref classeID, BTN_EnregisterClasse);
                GridFill("ClasseViewAll", DGV_ListeClasse);
            }
        }

        private void BTN_SupprimerAdmin_Click(object sender, EventArgs e)
        {
            using (MySqlConnection mySqlCon = new MySqlConnection(connectionString))
            {
                mySqlCon.Open();
                MySqlCommand mySqlCmd = new MySqlCommand("AdmimDeleteByID", mySqlCon);
                mySqlCmd.CommandType = CommandType.StoredProcedure;
                mySqlCmd.Parameters.AddWithValue("_AdminID", adminID);
                mySqlCmd.ExecuteNonQuery();
                MessageBox.Show("Deleted successfully");
                cleanForm(GBX_FormAdmin,ref adminID,BTN_EnregisterAdmin);
                GridFill("AdminViewAll", DGV_ListeAdmin);
            }
        }

        //private void BTN_SupprimerModifierCycle_Click(object sender, EventArgs e)
        //{
        //    using (MySqlConnection mySqlCon = new MySqlConnection(connectionString))
        //    {
        //        mySqlCon.Open();
        //        MySqlCommand mySqlCmd = new MySqlCommand("CycleDeleteByID", mySqlCon);
        //        mySqlCmd.CommandType = CommandType.StoredProcedure;
        //        mySqlCmd.Parameters.AddWithValue("_CycleID", cycleID);
        //        mySqlCmd.ExecuteNonQuery();
        //        MessageBox.Show("Deleted successfully");
        //        cleanForm(GBX_FormCycle);
        //        GridFill("CycleViewAll", DGV_ListeCycle);
        //    }
        //}

        // Methode pour le combobox
        private void ComboFill(string procedure, ref ComboBox oComboBox, string displayMember, string valueMember)
        {
            using (MySqlConnection mySqlCon = new MySqlConnection(connectionString))
            {
                mySqlCon.Open();
                MySqlDataAdapter sqlDa = new MySqlDataAdapter(procedure, mySqlCon);
                sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                //sqlDa.SelectCommand.Parameters.AddWithValue("AdminSearchByValue", TXB_RechercheAdmin.Text.Trim());
                DataTable dtb = new DataTable();
                sqlDa.Fill(dtb);
                oComboBox.DataSource = dtb;
                //Console.WriteLine("DATATABLE");
                //Console.WriteLine(dtb);
                oComboBox.DisplayMember = displayMember;
                oComboBox.ValueMember = valueMember;
            }
        }

        private void BTN_SupprimerClasse_Click_1(object sender, EventArgs e)
        {
            using (MySqlConnection mySqlCon = new MySqlConnection(connectionString))
            {
                mySqlCon.Open();
                MySqlCommand mySqlCmd = new MySqlCommand("ClasseDeleteByID", mySqlCon);
                mySqlCmd.CommandType = CommandType.StoredProcedure;
                mySqlCmd.Parameters.AddWithValue("_ClasseID", classeID);
                mySqlCmd.ExecuteNonQuery();
                MessageBox.Show("Deleted successfully");
                cleanForm(GBX_FormClasse, ref classeID, BTN_EnregisterClasse);
                GridFill("ClasseViewAll", DGV_ListeClasse);
            }
        }

        private void BTN_AnnulerClasse_Click(object sender, EventArgs e)
        {
            cleanForm(GBX_FormClasse, ref classeID, BTN_EnregisterClasse);
        }

        private void BTN_SupprimerMatiere_Click(object sender, EventArgs e)
        {

            using (MySqlConnection mySqlCon = new MySqlConnection(connectionString))
            {
                mySqlCon.Open();
                MySqlCommand mySqlCmd = new MySqlCommand("MatiereDeleteByID", mySqlCon);
                mySqlCmd.CommandType = CommandType.StoredProcedure;
                mySqlCmd.Parameters.AddWithValue("_MatiereID", matiereID);
                mySqlCmd.ExecuteNonQuery();
                MessageBox.Show("Deleted successfully");
                cleanForm(GBX_FormMatiere, ref matiereID, BTN_EnregisterMatiere);
                GridFill("MatiereViewAll", DGV_ListeMatiere);
            }
        }

        private void BTN_AnnulerProf_Click(object sender, EventArgs e)
        {
            cleanForm(GBX_FormProfesseur, ref professeurID, BTN_EnregistrerProf);
        }


        private void WandaMessageCallBack(IAsyncResult aResult)
        {
            try
            {
                int size = WandaUDP.EndReceiveFrom(aResult, ref WandaRemote);
                if (size > 0)
                {
                    byte[] receiveData = new byte[1464];
                    receiveData = (byte[])aResult.AsyncState;
                    ASCIIEncoding eEncoding = new ASCIIEncoding();
                    string receivedmessage = eEncoding.GetString(receiveData);

                    LSV_Reseau.Items.Add(receivedmessage);

                }
                byte[] buffer = new byte[1500];
                WandaUDP.BeginReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None, ref WandaRemote, new AsyncCallback(WandaMessageCallBack), buffer);

            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }
        }
        private void BTN_EtatReseau_Click(object sender, EventArgs e)
        {
            try
            {
                WandaLocal = new IPEndPoint(IPAddress.Parse(IP_Adrress_PC), Convert.ToInt32(port_PC));
                WandaUDP.Bind(WandaLocal); //This is the server  -- Address IP of the PC

                WandaRemote = new IPEndPoint(IPAddress.Parse(IP_Adrress_Client), Convert.ToInt32(port_Client));
                WandaUDP.Bind(WandaRemote); //This is the Client  -- Address IP of the Master FinguerPrint

                byte[] buffer = new byte[1500];
                WandaUDP.BeginReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None, ref WandaRemote, new AsyncCallback(WandaMessageCallBack), buffer);

                BTN_EtatReseau.Text = "Connected";
                BTN_EtatReseau.Enabled = false;
                BTN_FermerConnection.Enabled = true;

            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }
        }

        private string GetFingerID()
        {
            int FingerID = 0;
            int nb_Min = 1;
            int nb_Max = 126;
            Random num = new Random();

            FingerID = num.Next(nb_Min, nb_Max);

            return Convert.ToString(FingerID);
        }
        private void BTN_Empreinte1_Click(object sender, EventArgs e)
        {
            try
            {
                System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
                byte[] msg = new byte[1500];
                msg = enc.GetBytes("WDF_ENREOLL_"+ GetFingerID());
                WandaUDP.Send(msg);

                LSV_Reseau.Items.Add("ID Empreinte envoyer : " + GetFingerID());


                BTN_Empreinte2.Enabled = true;
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }
        }
    }
}