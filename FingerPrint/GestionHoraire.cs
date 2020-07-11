using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FingerPrint
{
    public partial class GestionHoraire : Form
    {
        private string connectionString = @"Server=localhost;Database=presence_db;Uid=root;Pwd='';";

        public GestionHoraire()
        {
            InitializeComponent();
        }

        private void GestionHoraire_Load(object sender, EventArgs e)
        {
         //   Admistrateur.ComboFill("ProgrammeClasseComboViewAll", ref CBX_Classe, "nom", "idClasse");
        }

        private void BTN_EnregisterProg_Click(object sender, EventArgs e)
        {
            int idClasse = CBX_Classe.SelectedIndex;
            DateTime date_debut = DTP_DateDebutProg.Value;
            DateTime date_Fin = DTP_DateFinProg.Value;

            try
            {
                using (MySqlConnection mySqlCon = new MySqlConnection(connectionString))
                {

                    String query = $"select * from programmes where classe_idClasse = \"{idClasse}\" " +
                        $" and date between \"{date_debut.ToString("yyyy-MM-dd")}\" and " +
                        $"DATE_ADD(\"{date_debut.ToString("yyyy-MM-dd")}\",INTERVAL 7 DAY)  and visible = true;";

                    MySqlCommand mySqlCommand = new MySqlCommand(query, mySqlCon);
                    mySqlCon.Open();
                    mySqlCommand.CommandType = CommandType.Text;
                    MySqlDataReader reader2 = mySqlCommand.ExecuteReader();
                    if (reader2.Read())
                    {                        
                        String query2 = "INSERT INTO Programmes(Date,Heure_Debut,Heure_Fin,CLASSE_idCLASSE,idADMINISTRATEUR,idPROFESSEUR_MATIERE,visible)"+
                        $" VALUES({reader2.GetString("Date")}, {reader2.GetString("Heure_Debut")}, {reader2.GetString("Heure_Fin")}, _ProgrammeIDClasse, _ProgrammeIDAdmin, _ProgrammeIDProfMatiere, true); ";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error Message ");
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
