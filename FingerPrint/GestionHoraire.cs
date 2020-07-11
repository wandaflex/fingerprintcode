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
            Admistrateur.ComboFill("ProgrammeClasseComboViewAll", ref CBX_Classe, "nom", "idClasse");
            Admistrateur.GridFill("ProgrammeViewFrorein", DGV_programmeGH);
        }

        private void BTN_EnregisterProg_Click(object sender, EventArgs e)
        {
            int idClasse = int.Parse(CBX_Classe.SelectedValue.ToString());
            DateTime date_debut = DTP_DateDebutProg.Value;
            DateTime date_Fin = DTP_DateFinProg.Value;

            try
            {
                using (MySqlConnection mySqlCon = new MySqlConnection(connectionString))
                {

                    String query = $"select * from programmes where classe_idClasse = {idClasse} " +
                        $" and date between '{date_debut.ToString("yyyy-MM-dd")}' and " +
                        $"DATE_ADD('{date_debut.ToString("yyyy-MM-dd")}',INTERVAL 7 DAY)  and visible = true;";
                    Console.WriteLine(query);
                    MySqlCommand mySqlCommand = new MySqlCommand(query, mySqlCon);
                    mySqlCon.Open();
                    mySqlCommand.CommandType = CommandType.Text;
                    MySqlDataReader reader2 = mySqlCommand.ExecuteReader();
                    MySqlConnection mySqlCon2 = new MySqlConnection(connectionString);
                    while (reader2.Read())
                    {
                       
                        Console.WriteLine("skjdkj");
                        String query2 = "INSERT INTO Programmes(Date,Heure_Debut,Heure_Fin,CLASSE_idCLASSE,idADMINISTRATEUR,idPROFESSEUR_MATIERE,visible)"+
                        $" VALUES (DATE_ADD('{reader2.GetString("Date")}',INTERVAL 7 DAY), {reader2.GetString("Heure_Debut")}, {reader2.GetString("Heure_Fin")}, _ProgrammeIDClasse, _ProgrammeIDAdmin, _ProgrammeIDProfMatiere, true); ";

                        Console.WriteLine(query2);
                        MySqlCommand mySqlCommand2 = new MySqlCommand(query2, mySqlCon2);
                        mySqlCon2.Open();
                        mySqlCommand2.CommandType = CommandType.Text;
                        MySqlDataReader reader = mySqlCommand.ExecuteReader();
                    }

                    mySqlCon2.Close();
                }

                Admistrateur.GridFill("ProgrammeViewFrorein", DGV_programmeGH);
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
