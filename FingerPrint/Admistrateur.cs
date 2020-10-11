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
using MySql.Data;
using System.Threading.Tasks;

namespace FingerPrint
{
    public partial class Admistrateur : Form
    {
        public static string connectionString = @"Server=localhost;Database=presence_db;Uid=root;Pwd='';";
        public int adminID = 0;
        //int cycleID = 0;
        int classeID = 0;
        int professeurID = 0;
        int matiereID = 0;
        int programmeID = 0;
        int profMatiereID = 0;
        int presenceID = 0;
        public const int PORT = 2500;
        public const int EMPR12_DISNABLED = 1;
        public const int EMPR2_ENABLED = 2;
        public const int EMPR_RESET = 3;
        public Admistrateur()
        {
            InitializeComponent();
        }

        #region Initialisation du formulaire principal
        private void Admistrateur_Load(object sender, EventArgs e)
        {
            //Login oLogin = new Login();
            //oLogin.ShowDialog();

            initCombo();

          


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

        private void initCombo()
        {
            GridFill("AdminViewAll", DGV_ListeAdmin);
            //GridFill("CycleViewAll", DGV_ListeCycle);
            GridFill("ClasseViewAll", DGV_ListeClasse);
            GridFill("ProfViewAll", DGV_ListeProf);

            GridFill("MatiereViewAll", DGV_ListeMatiere);
            GridFill("ProgrammeViewFrorein", DGV_ListeProgramme);
            GridFill("ProfMatiereViewFrorein", DGV_MatiereProf);
            GridFill("PresenceProgrammeViewAll", DGV_ListePresence);

            ComboFill("ProgrammeClasseComboViewAll", ref CBX_SelectClasse, "nom", "idClasse");
            ComboFill("ProgrammeMatiereComboViewAll", ref CBX_SelectProgProfMatiere, "ProfMatiere", "idPROFESSEUR_MATIERE");
            ComboFill("MatiereProfMatiereComboViewAll", ref CBX_SelectMatiere, "Matiere", "idMatiere");
            ComboFill("MatiereProfProfComboViewAll", ref CBX_SelectProf, "Professeur", "idProfesseur");
            ComboFill("PresenceProgrammeComboViewAll", ref CBX_SelectNomProg, "nomProgramme", "idPROGRAMMES");

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

                    if (TXB_NomAdmin.Text == "")
                    {
                        MessageBox.Show("Le nom est obligatoire.");
                        TXB_NomAdmin.Focus();
                        TXB_NomAdmin.BackColor = Color.Red;
                    }else
                    if (CBX_TypeUtil.Text == "")
                    {
                        MessageBox.Show("Veuillez selectionner un type d'utilisateur.");
                        CBX_TypeUtil.BackColor = Color.Red;
                        CBX_TypeUtil.Focus();
                    }

                    else if (TXB_LoginAdmin.Text == "")
                    {
                        MessageBox.Show("Veuillez entrer votre identifiant(Login).");
                        TXB_LoginAdmin.Focus();
                        TXB_LoginAdmin.BackColor = Color.Red;
                    }

                    else if (TXB_MotDePasseAdmin.Text == "")
                    {
                        MessageBox.Show("Veuillez entrer votre mot de passe.");
                        TXB_MotDePasseAdmin.Focus();
                        TXB_MotDePasseAdmin.BackColor = Color.Red;
                    }
                    
                    else
                    {
                        mySqlCmd.Parameters.AddWithValue("_AdminPwd", TXB_MotDePasseAdmin.Text.Trim());
                        mySqlCmd.Parameters.AddWithValue("_AdminType", CBX_TypeUtil.Text.Trim());
                        mySqlCmd.Parameters.AddWithValue("_AdminLogin", TXB_LoginAdmin.Text.Trim());
                        mySqlCmd.Parameters.AddWithValue("_AdminNom", TXB_NomAdmin.Text.Trim());
                        mySqlCmd.Parameters.AddWithValue("_AdminPremon", TXB_PrenomAdmin.Text.Trim());
                        mySqlCmd.ExecuteNonQuery();
                        MessageBox.Show("Submited successfully");
                        GridFill("AdminViewAll", DGV_ListeAdmin);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Une Erreur est survenue, Veuillez contacter un programmeur." + Environment.NewLine + ex.Message);
            }

        }

        private void BTN_AnnulerAdmin_Click(object sender, EventArgs e)
        {
            cleanForm(GBX_FormAdmin,ref adminID, BTN_EnregisterAdmin);
        }

        public static void GridFill(String procedure, DataGridView dataGrid)
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
            foreach (Control oControlFormulaire in groupBox.Controls) {
                if (oControlFormulaire is TextBox)
                    oControlFormulaire.Text = String.Empty;
                if (oControlFormulaire is ComboBox)
                {
                    (oControlFormulaire as ComboBox).SelectedIndex = -1;
                }
            }

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

                    if (TXB_NomClasse.Text == "")
                    {
                        MessageBox.Show("Le nom de la classe est obligatoire.");
                        TXB_NomClasse.Focus();
                        TXB_NomClasse.BackColor = Color.Red;
                    }else

                    if (TXB_CodeClasse.Text == "")
                    {
                        MessageBox.Show("Le code de la classe est obligatoire");
                        TXB_CodeClasse.Focus();
                        TXB_CodeClasse.BackColor = Color.Red;
                    }else

                    if (CBX_CycleClasse.Text == "")
                    {
                        MessageBox.Show("Veuillez selectionner un cycle.");
                        CBX_CycleClasse.Focus();
                        CBX_CycleClasse.BackColor = Color.Red;
                    }
                    
                    else
                    {
                        mySqlCmd.Parameters.AddWithValue("_ClasseNom", TXB_NomClasse.Text.Trim());
                        mySqlCmd.Parameters.AddWithValue("_ClasseCycle", CBX_CycleClasse.Text.Trim());
                        mySqlCmd.Parameters.AddWithValue("_ClasseDescription", TXB_DescriptionClasse.Text.Trim());
                        mySqlCmd.Parameters.AddWithValue("_ClasseCode", TXB_CodeClasse.Text.Trim());
                        mySqlCmd.ExecuteNonQuery();
                        MessageBox.Show("Submited successfully");
                        GridFill("ClasseViewAll", DGV_ListeClasse);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Une Erreur est survenue, Veuillez contacter un programmeur." + Environment.NewLine + ex.Message);
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

            //MessageBox.Show(CBX_FormMatiere.SelectedValue.ToString());

            try
            {
                using (MySqlConnection mySqlCon = new MySqlConnection(connectionString))
                {
                    mySqlCon.Open();
                    MySqlCommand mySqlCmd = new MySqlCommand("MatiereAddOrEdit", mySqlCon);
                    mySqlCmd.CommandType = CommandType.StoredProcedure;
                    mySqlCmd.Parameters.AddWithValue("_MatiereID", matiereID);

                    if (TXB_CodeMatiere.Text == "")
                    {
                        MessageBox.Show("Veuillez entrer le code de la matière.");
                        TXB_CodeMatiere.Focus();
                        TXB_CodeMatiere.BackColor = Color.Red;
                    }
                    else if (TXB_NomMatiere.Text == "")
                    {
                        MessageBox.Show("Veuillez entrer le nom de la matière.");
                        TXB_NomMatiere.Focus();
                        TXB_NomMatiere.BackColor = Color.Red;
                    }
                    
                    else
                    {
                        mySqlCmd.Parameters.AddWithValue("_MatiereCode", TXB_CodeMatiere.Text.Trim());
                        mySqlCmd.Parameters.AddWithValue("_MatiereNom", TXB_NomMatiere.Text.Trim());
                        mySqlCmd.ExecuteNonQuery();
                        MessageBox.Show("Submited successfully");
                        GridFill("MAtiereViewAll", DGV_ListeMatiere);
                    }

                    //mySqlCmd.Parameters.AddWithValue("_CycleID_Cycle", CBX_FormMatiere.SelectedValue.ToString());    // Jonathan : Ce paramètre manquait   
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Une Erreur est survenue, Veuillez contacter un programmeur." + Environment.NewLine + ex.Message);
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

                    if (TXB_NomProf.Text == "")
                    {
                        TXB_NomProf.BackColor = Color.Red;
                        MessageBox.Show("Veuillez entrer un nom.");
                        TXB_NomProf.Focus();
                    }
                    else if (TXB_TelephoneProf.Text == "")
                    {
                        TXB_TelephoneProf.BackColor = Color.Red;
                        MessageBox.Show("Veuillez entrer un numero de telephone.");
                        TXB_TelephoneProf.Focus();
                    }
                    
                    else if (TXB_DiplomeProf.Text == "")
                    {
                        TXB_DiplomeProf.BackColor = Color.Red;
                        MessageBox.Show("Veuillez entrer le diplome du professeur.");
                        TXB_DiplomeProf.Focus();
                    }
                    else if (TXB_TypeProf.Text == "")
                    {
                        TXB_TypeProf.BackColor = Color.Red;
                        MessageBox.Show("Veuillez entrer le type de professeur.");
                        TXB_TypeProf.Focus();
                    }
                    else if (TXB_Taux1ierCycleProf.Text == "")
                    {
                        TXB_Taux1ierCycleProf.BackColor = Color.Red;
                        MessageBox.Show("Veuillez entrer le taux horaire du premier cycle.");
                        TXB_Taux1ierCycleProf.Focus();
                    }
                    else if (TXB_Taux2iemeCycleProf.Text == "")
                    {
                        TXB_Taux2iemeCycleProf.BackColor = Color.Red;
                        MessageBox.Show("Veuillez entrer le taux horaire du deuxieme cycle.");
                        TXB_Taux2iemeCycleProf.Focus();
                    }             

                    else
                    {
                        mySqlCmd.Parameters.AddWithValue("_ProfETaux_Horaire_1", TXB_Taux1ierCycleProf.Text.Trim());
                        mySqlCmd.Parameters.AddWithValue("_ProfETaux_Horaire_2", TXB_Taux2iemeCycleProf.Text.Trim());
                        mySqlCmd.Parameters.AddWithValue("_ProfType", TXB_TypeProf.Text.Trim());
                        mySqlCmd.Parameters.AddWithValue("_Diplome", TXB_DiplomeProf.Text.Trim());
                        mySqlCmd.Parameters.AddWithValue("_ProfTel_1", TXB_TelephoneProf.Text.Trim());
                        mySqlCmd.Parameters.AddWithValue("_ProfNom", TXB_NomProf.Text.Trim());
                        mySqlCmd.Parameters.AddWithValue("_ProfPrenom", TXB_PrenomProf.Text.Trim());

                        mySqlCmd.Parameters.AddWithValue("_ProfTel_2", TXB_Telephone2Prof.Text.Trim());
                        mySqlCmd.Parameters.AddWithValue("_ProfEmpreinte_1", TXB_Empreinte1Prof.Text.Trim());
                        mySqlCmd.Parameters.AddWithValue("_ProfEmpreinte_2", TXB_Empreinte2Prof.Text.Trim());
                        mySqlCmd.Parameters.AddWithValue("_ProfProfil", "Profil IMG");

                        mySqlCmd.Parameters.AddWithValue("_profDate_recrutement", DTP_Recrutement.Value);
                        mySqlCmd.Parameters.AddWithValue("_Description", TXB_DescriptionProf.Text.Trim());
                        mySqlCmd.ExecuteNonQuery();
                        MessageBox.Show("Submited successfully");
                        GridFill("ProfViewAll", DGV_ListeProf);
                    }


                    BTN_EnregisterProg.Text = "Modifier";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Une erreur est survenue, Veuillez contacter un programmeur" + Environment.NewLine + ex.Message);

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
        public static void ComboFill(string procedure, ref ComboBox oComboBox, string displayMember, string valueMember)
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

        private void BTN_EnregisterProg_Click(object sender, EventArgs e)
        {
            try
            {
                using (MySqlConnection mySqlCon = new MySqlConnection(connectionString))
                {
                    mySqlCon.Open();
                    MySqlCommand mySqlCmd = new MySqlCommand("ProgrammeAddOrEdit", mySqlCon);
                    mySqlCmd.CommandType = CommandType.StoredProcedure;
                    mySqlCmd.Parameters.AddWithValue("_ProgrammesID", programmeID);
                    mySqlCmd.Parameters.AddWithValue("_ProgrammeDate", DTP_Programme.Value);

                    //if (CBX_HeureDebutProg.SelectedIndex == -1)
                    //{
                    //    CBX_HeureDebutProg.BackColor = Color.Red;
                    //    MessageBox.Show("Veuillez entrer selectionner une heure de debut");
                    //}else
                    //if (CBX_HeureFinProg.SelectedIndex == -1)
                    //{
                    //    CBX_HeureFinProg.BackColor = Color.Red;
                    //    MessageBox.Show("Veuillez entrer une heure de fin");
                    //}
                    
                    //else
                    //{
                        //mySqlCmd.Parameters.AddWithValue("_ProgrammeHeureDebut", CBX_HeureDebutProg.SelectedItem.ToString());
                        mySqlCmd.Parameters.AddWithValue("_ProgrammeHeureDebut", CBX_HeureDebutProg.Text.ToString());
                        mySqlCmd.Parameters.AddWithValue("_ProgrammeHeureFin", CBX_HeureFinProg.Text.ToString());
                        //mySqlCmd.Parameters.AddWithValue("_ProgrammeHeureFin", CBX_HeureFinProg.SelectedItem.ToString());
                        mySqlCmd.Parameters.AddWithValue("_ProgrammeIDClasse", CBX_SelectClasse.SelectedValue.ToString());
                        mySqlCmd.Parameters.AddWithValue("_ProgrammeIDAdmin", "1");
                        mySqlCmd.Parameters.AddWithValue("_ProgrammeIDProfMatiere", CBX_SelectProgProfMatiere.SelectedValue.ToString());
                        mySqlCmd.ExecuteNonQuery();
                        MessageBox.Show("Submited successfully");
                        cleanForm(GBX_FormProg, ref programmeID, BTN_EnregisterProg);
                        GridFill("ProgrammeViewFrorein", DGV_ListeProgramme);
                    //}
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Une Erreur est survenue, Bien remplir votre formulaire programme." + Environment.NewLine + ex.Message);
            }
        }

        private void DGV_ListeProgramme_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DGV_ListeProgramme.CurrentRow.Index != -1)
            {
                //DTP_Programme.Value = DGV_ListeProgramme.CurrentRow.Cells[1].Value.ToString();

                CBX_HeureDebutProg.Text = DGV_ListeProgramme.CurrentRow.Cells[2].Value.ToString();
                CBX_HeureFinProg.Text = DGV_ListeProgramme.CurrentRow.Cells[3].Value.ToString();
                DTP_Programme.Value = DateTime.Parse(DGV_ListeProgramme.CurrentRow.Cells[1].Value.ToString());
                CBX_SelectClasse.Text = DGV_ListeProgramme.CurrentRow.Cells[4].Value.ToString();
                CBX_SelectProgProfMatiere.Text = DGV_ListeProgramme.CurrentRow.Cells[5].Value.ToString();

                programmeID = Convert.ToInt32(DGV_ListeProgramme.CurrentRow.Cells[0].Value.ToString());
                
                //BTN_EnregisterCycle.Text = "Modifier";
            }

        }

        private void BTN_EnregistrerMatProf_Click(object sender, EventArgs e)
        {
            try
            {
                using (MySqlConnection mySqlCon = new MySqlConnection(connectionString))
                {
                    mySqlCon.Open();
                    MySqlCommand mySqlCmd = new MySqlCommand("ProfMatiereAddOrEdit", mySqlCon);
                    mySqlCmd.CommandType = CommandType.StoredProcedure;
                    mySqlCmd.Parameters.AddWithValue("_idProfMatiere", profMatiereID);
                    mySqlCmd.Parameters.AddWithValue("_idProf", CBX_SelectProf.SelectedValue.ToString());
                    mySqlCmd.Parameters.AddWithValue("_idMatiere", CBX_SelectMatiere.SelectedValue.ToString());
                    mySqlCmd.ExecuteNonQuery();
                    MessageBox.Show("Submited successfully");
                    cleanForm(GBX_FormMatiereProf, ref profMatiereID, BTN_EnregistrerMatProf);
                    GridFill("ProfMatiereViewFrorein", DGV_MatiereProf);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Message ");
            }
        }

        private void TCL_Admin_SelectedIndexChanged(object sender, EventArgs e)
        {
            initCombo();
        }

        private void BTN_Rapports_Click(object sender, EventArgs e)
        {
            Rapports openForm = new Rapports();
            openForm.Show();
        }

        private void TBX_RecherchePof_KeyDown(object sender, KeyEventArgs e)
        {
            //BTN_RechercheProf_Click(null, null);
        }

        private void TXB_RechercheClasse_TextChanged(object sender, EventArgs e)
        {
            BTN_RechercheClasse_Click(null, null);
        }

        private void GBX_FormProfesseur_TextChanged(object sender, EventArgs e)
        {
            BTN_RechercheProf_Click(null, null);
        }

        private void TBX_RecherchePof_TextChanged(object sender, EventArgs e)
        {
            BTN_RechercheProf_Click(null, null);
        }

        private void BTN_EnregisterPresence_Click(object sender, EventArgs e)
        {
            try
            {
                using (MySqlConnection mySqlCon = new MySqlConnection(connectionString))
                {
                    mySqlCon.Open();
                    MySqlCommand mySqlCmd = new MySqlCommand("PresenceAddOrEdit", mySqlCon);
                    mySqlCmd.CommandType = CommandType.StoredProcedure;
                    mySqlCmd.Parameters.AddWithValue("_PresenceID", presenceID);
                    mySqlCmd.Parameters.AddWithValue("_ProgrammesID", CBX_SelectNomProg.SelectedValue.ToString());
                    mySqlCmd.Parameters.AddWithValue("_PresenceDate", DTP_Presence.Value);

                    if (TXB_HeureDebutPres.Text == "")
                    {
                        TXB_HeureDebutPres.BackColor = Color.Red;
                        TXB_HeureDebutPres.Focus();
                        MessageBox.Show("Veuillez entrer une heure de debut");
                    }else
                    if (TXB_HeureFinPres.Text == "")
                    {
                        TXB_HeureFinPres.BackColor = Color.Red;
                        TXB_HeureFinPres.Focus();
                        MessageBox.Show("Veuillez entrer une heure de fin");
                    }                    

                    else
                    {
                        mySqlCmd.Parameters.AddWithValue("_PresenceHeureDebut", TXB_HeureDebutPres.Text.Trim().ToString());
                        mySqlCmd.Parameters.AddWithValue("_PresenceHeureFin", TXB_HeureFinPres.Text.Trim().ToString());
                        mySqlCmd.ExecuteNonQuery();
                        MessageBox.Show("Submited successfully");
                        cleanForm(GBX_FormPresence, ref presenceID, BTN_EnregisterPresence);
                        GridFill("PresenceProgrammeViewAll", DGV_ListePresence);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Message ");
            }
        }

        private void BTN_SupprimerProg_Click(object sender, EventArgs e)
        {
            using (MySqlConnection mySqlCon = new MySqlConnection(connectionString))
            {
                mySqlCon.Open();
                MySqlCommand mySqlCmd = new MySqlCommand("ProgrammesDeleteByID", mySqlCon);
                mySqlCmd.CommandType = CommandType.StoredProcedure;
                mySqlCmd.Parameters.AddWithValue("_ProgrammesID", programmeID);
                mySqlCmd.ExecuteNonQuery();
                MessageBox.Show("Deleted successfully");
                cleanForm(GBX_FormAdmin, ref adminID, BTN_EnregisterProg);
                GridFill("ProgrammeViewFrorein", DGV_ListeProgramme);
            }
        }

        private void BTN_AnnulerProg_Click(object sender, EventArgs e)
        {
            cleanForm(GBX_FormAdmin, ref adminID, BTN_EnregisterProg);
        }

        private void BTN_SupprimerPresence_Click(object sender, EventArgs e)
        {
            using (MySqlConnection mySqlCon = new MySqlConnection(connectionString))
            {
                mySqlCon.Open();
                MySqlCommand mySqlCmd = new MySqlCommand("PresenceDeleteByID", mySqlCon);
                mySqlCmd.CommandType = CommandType.StoredProcedure;
                mySqlCmd.Parameters.AddWithValue("_PresenceID", presenceID);
                mySqlCmd.ExecuteNonQuery();
                MessageBox.Show("Deleted successfully");
                cleanForm(GBX_FormAdmin, ref adminID, BTN_EnregisterPresence);
                GridFill("PresenceProgrammeViewAll", DGV_ListePresence);
            }
        }

        private void BTN_SupprimerMatProf_Click(object sender, EventArgs e)
        {
            using (MySqlConnection mySqlCon = new MySqlConnection(connectionString))
            {
                mySqlCon.Open();
                MySqlCommand mySqlCmd = new MySqlCommand("ProfMatiereDeleteByID", mySqlCon);
                mySqlCmd.CommandType = CommandType.StoredProcedure;
                mySqlCmd.Parameters.AddWithValue("_ProfMatiereID", profMatiereID);
                mySqlCmd.ExecuteNonQuery();
                MessageBox.Show("Deleted successfully");
                cleanForm(GBX_FormAdmin, ref adminID, BTN_EnregistrerMatProf);
                GridFill("ProfMatiereViewFrorein", DGV_MatiereProf);
            }
        }

        private void BTN_AnnulerPresence_Click(object sender, EventArgs e)
        {
            cleanForm(GBX_FormAdmin, ref adminID, BTN_EnregisterPresence);
        }

        private void BTN_AnnulerMatProf_Click(object sender, EventArgs e)
        {
            cleanForm(GBX_FormAdmin, ref adminID, BTN_EnregistrerMatProf);
        }

        private void DGV_MatiereProf_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DGV_MatiereProf.CurrentRow.Index != -1)
            {
                //DTP_Programme.Value = DGV_ListeProgramme.CurrentRow.Cells[1].Value.ToString();

                CBX_SelectProf.Text = DGV_MatiereProf.CurrentRow.Cells[1].Value.ToString();
                CBX_SelectMatiere.Text = DGV_MatiereProf.CurrentRow.Cells[2].Value.ToString();
                profMatiereID = Convert.ToInt32(DGV_MatiereProf.CurrentRow.Cells[0].Value.ToString());

                BTN_EnregistrerMatProf.Text = "Modifier";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            EmploieDeTempsForm btn = new EmploieDeTempsForm();
            btn.ShowDialog();
            //this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            EmploieDeTempsForm btn = new EmploieDeTempsForm();
            btn.ShowDialog();
            this.Close();
        }


        private string FingerID()
        {
            int userCount = 0;
            int FingerID;
            do
            {
                int nb_Min = 1;
                int nb_Max = 126;
                Random num = new Random();

                FingerID = num.Next(nb_Min, nb_Max);
                //string msg = "WANDA_ENROLL_" + FingerID;
                //verifier que ID nest pas ds la base de donnee
                using (MySqlConnection mySqlCon4 = new MySqlConnection(connectionString))
                {
                    using (MySqlCommand mySqlCommand = new MySqlCommand($"SELECT COUNT(*) from professeur where Empreinte_1 = {FingerID} OR Empreinte_2 = {FingerID}", mySqlCon4))
                    {
                        mySqlCon4.Open();

                        userCount = Convert.ToInt32(mySqlCommand.ExecuteScalar());                       
                    }
                }

            } while (userCount == 1);

            return Convert.ToString(FingerID);

        }

        int id_send = 1;
        private int get_sendID()
        {
            if (id_send > 2)
            {
                id_send = 1;
            }
            return id_send;
        }


        private void set_BTN_State(int ste)
        {
            if (ste == EMPR12_DISNABLED)
            {
                BTN_Empreinte2.Enabled = false;
                BTN_Empreinte1.Enabled = false;
            }
            else if (ste == EMPR2_ENABLED)
            {
                BTN_Empreinte2.Enabled = true;
                BTN_Empreinte1.Enabled = false;
            }
            else if (ste == EMPR_RESET)
            {
                BTN_Empreinte1.Enabled = true;
                BTN_Empreinte2.Enabled = false;
            }
        }

        //public const string connect = "allo";
        private void BTN_Empreinte1_Click(object sender, EventArgs e)
        {
            // create the client 
            var client_empr1 = UDPUser.ConnectTo("192.168.1.200", PORT);

            string msge = FingerID(); 

            client_empr1.Send("WANDA_ENROLL_" + msge);

            LSV_RcevAdmin.Items.Add("ID " + get_sendID() + " Sent : " + msge);
            id_send += 1;
                      

            Task.Factory.StartNew(async () =>
            {
                while (true)
                {
                    try
                    {
                        var received = await client_empr1.Receive();

                        if (received.Message.Contains("QUIT"))
                        {// mettre label connection a red
                            LBL_Connect.Invoke(new MethodInvoker(delegate
                            {
                                LBL_Connect.BackColor = Color.Red;
                                LBL_Connect.Text = "Disconnected";
                            }
                            ));


                            LSV_RcevAdmin.Invoke(new MethodInvoker(delegate
                            {
                                LSV_RcevAdmin.Items.Add("FINGER PRINT disconnected");
                               
                            }
                            ));
                            break;
                        }
                        else if (received.Message.Contains("WDF_GOOD"))
                        {
                            LBL_Connect.Invoke(new MethodInvoker(delegate
                            {
                                LBL_Connect.BackColor = Color.Green;
                                LBL_Connect.Text = "Connected";
                            }
                            ));

                            LSV_RcevAdmin.Invoke(new MethodInvoker(delegate
                            {
                                LSV_RcevAdmin.Items.Add("Connected to FINGER PRINT");                                
                            }
                            ));
                        }

                        else if (received.Message.Contains(msge))
                        {

                            TXB_Empreinte1Prof.Invoke(new MethodInvoker(delegate
                            {
                                TXB_Empreinte1Prof.Text = received.Message;   // .Items.Add(received.Message);
                            }
                            ));
                        }

                        else
                        {
                            LSV_RcevAdmin.Invoke(new MethodInvoker(delegate
                            {
                                LSV_RcevAdmin.Items.Add(received.Message);
                            }
                            ));

                        }
                     
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.Message, "Error message EMP 1");
                    }
                }
            });

            set_BTN_State(EMPR2_ENABLED);

        }

        private void BTN_Empreinte2_Click(object sender, EventArgs e)
        {
            // create the client 
            var client_empr1 = UDPUser.ConnectTo("192.168.1.200", PORT);

            string msge = FingerID();

            client_empr1.Send("WANDA_ENROLL_" + msge);

            LSV_RcevAdmin.Items.Add("ID " + get_sendID() + " Sent : " + msge);
            id_send += 1;


            Task.Factory.StartNew(async () =>
            {
                while (true)
                {
                    try
                    {
                        var received = await client_empr1.Receive();

                        if (received.Message.Contains("QUIT"))
                        {// mettre label connection a red
                            LBL_Connect.Invoke(new MethodInvoker(delegate
                            {
                                LBL_Connect.BackColor = Color.Red;
                                LBL_Connect.Text = "Disconnected";
                            }
                            ));


                            LSV_RcevAdmin.Invoke(new MethodInvoker(delegate
                            {
                                LSV_RcevAdmin.Items.Add("FINGER PRINT disconnected");                                
                            }
                            ));
                            break;
                        }
                        else if (received.Message.Contains("WDF_GOOD"))
                        {
                            LBL_Connect.Invoke(new MethodInvoker(delegate
                            {
                                LBL_Connect.BackColor = Color.Green;
                                LBL_Connect.Text = "Connected";
                            }
                            ));

                            LSV_RcevAdmin.Invoke(new MethodInvoker(delegate
                            {
                                LSV_RcevAdmin.Items.Add("Connected to FINGER PRINT");
                                // Ajouter label connection Green
                            }
                            ));
                        }

                        else if (received.Message.Contains(msge))
                        {

                            TXB_Empreinte2Prof.Invoke(new MethodInvoker(delegate
                            {
                                TXB_Empreinte2Prof.Text = received.Message;   // .Items.Add(received.Message);
                            }
                            ));
                        }
                        else
                        {
                            LSV_RcevAdmin.Invoke(new MethodInvoker(delegate
                            {
                                LSV_RcevAdmin.Items.Add(received.Message);
                            }
                            ));

                        }

                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.Message, "Error message EMP 1");
                    }
                }
            });


            //Emp 1 2 disabled
            set_BTN_State(EMPR12_DISNABLED);
        }

        private void BTN_Reset_Click(object sender, EventArgs e)
        {
            LSV_RcevAdmin.Items.Clear();
            set_BTN_State(EMPR_RESET);
        }

       
        private void BTN_Quitter_Click(object sender, EventArgs e)
        {
            Login openForm = new Login();
            openForm.Show();
            this.Close();
        }

        private void BTN_Horaires_Click(object sender, EventArgs e)
        {
            GestionHoraire oGgestionHoraire = new GestionHoraire();
            oGgestionHoraire.Show();
        }

        private void BTN_PresAuto_Click(object sender, EventArgs e)
        {
            PresenceAuto oPresAutoForm = new PresenceAuto();
            oPresAutoForm.Show();
        }

        private void BTN_RechercheProg_Click(object sender, EventArgs e)
        {
            try
            {
                using (MySqlConnection mySqlCon = new MySqlConnection(connectionString))
                {
                    mySqlCon.Open();
                    MySqlDataAdapter sqlDa = new MySqlDataAdapter("ProgrammesSearchByValueForeing", mySqlCon);
                    sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                    sqlDa.SelectCommand.Parameters.AddWithValue("_SearchValue", TXB_RechercheProg.Text.Trim());
                    DataTable dtb = new DataTable();
                    sqlDa.Fill(dtb);
                    DGV_ListeProgramme.DataSource = dtb;
                    DGV_ListeProgramme.Columns[0].Visible = false;
                    DGV_ListeProgramme.Columns[DGV_ListeProgramme.Columns.Count - 1].Visible = false;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error message");
            }
        }
    }
}