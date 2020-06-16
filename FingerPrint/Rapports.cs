using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.Net.NetworkInformation;

namespace FingerPrint
{
    public partial class Rapports : Form
    {
        
        private string connectionString = @"Server=localhost;Database=presence_db;Uid=root;Pwd='';";
        private int profID = 0;
        private String profNom = "";
        private Double TauxHauraire_Cycle1 = 0.00;
        private Double TauxHauraire_Cycle2 = 0.00;
        private double nombre_Heure_Cycle1 = 0;
        private double nombre_Heure_Cycle2 = 0;
        private const int PERIODE = 55;
        private const int TOLLERENCE_ABSENCE = 5;
        private const int TEMP_PAUSE_1 = 15;
        private const int TEMP_PAUSE_2 = 30;
        private const int PENALITE_RETARD = 5;
        private DateTime H_DEBUT_PAUSE_1 = DateTime.Parse("10:45");
        private DateTime H_DEBUT_PAUSE_2 = DateTime.Parse("12:30");
        private DateTime H_FIN_PAUSE_1 = DateTime.Parse("11:00");
        private DateTime H_FIN_PAUSE_2 = DateTime.Parse("13:00");


        public Rapports()
        {
            InitializeComponent();
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void GBX_ListeRapport_Enter(object sender, EventArgs e)
        {

        }

        private void BTN_ValiderRapport_Click(object sender, EventArgs e)
        {
            DateTime dateDebut ;
            DateTime dateFin ;
            
            try
            {
                using (MySqlConnection mySqlCon = new MySqlConnection(connectionString))
                {
                    mySqlCon.Open();
                    String query = "SELECT idPROFESSEUR,concat(nom,' ',prenom) \"Nom\",Taux_horaire_1,Taux_horaire_2  FROM Professeur";
                    MySqlCommand mySqlCmd = new MySqlCommand(query, mySqlCon);
                    mySqlCmd.CommandType = CommandType.Text;
                    MySqlDataReader reader = mySqlCmd.ExecuteReader();

                    while (reader.Read())
                    {

                        using (MySqlConnection mySqlCon2 = new MySqlConnection(connectionString))
                        {

                            profID = int.Parse(reader.GetString("idPROFESSEUR"));
                            profNom = reader.GetString("Nom");
                            TauxHauraire_Cycle1 = Double.Parse(reader.GetString("Taux_horaire_1"));
                            TauxHauraire_Cycle2 = Double.Parse(reader.GetString("Taux_horaire_2"));


                            String query2 = "select concat(pr.Nom,'  ',pr.prenom) as \"nomProf\" , " +
                            "pres.HeureDebut as \"HDebuPres\",pres.HeureFin as \"HFinPres\", " +
                            "prg.Heure_Debut as \"HDebuProg\", prg.Heure_Fin as \"HFinProg\", cl.Cycle " +
                            "From presence as pres, programmes as prg, classe as cl, " +
                            "professeur_matiere as pm, professeur as pr, matiere as m " +
                            "where pres.PROGRAMMES_idPROGRAMMES = prg.idPROGRAMMES and " +
                            "prg.CLASSE_idCLASSE = cl.idCLASSE and " +
                            "prg.idPROFESSEUR_MATIERE = pm.idPROFESSEUR_MATIERE and " +
                            "pm.idPROFESSEUR = pr.idPROFESSEUR and " +
                            "pm.idMATIERE = m.idMATIERE and " +
                            $"pr.idPROFESSEUR = {profID} and " +
                            "prg.visible = true and m.visible = true and cl.visible = true";

                            Console.WriteLine(query2);
                         

                            MySqlCommand mySqlCmd2 = new MySqlCommand(query2, mySqlCon2);
                            mySqlCon2.Open();
                            mySqlCmd2.CommandType = CommandType.Text;
                            MySqlDataReader reader2 = mySqlCmd2.ExecuteReader();
                            while (reader2.Read())
                            {
                                DateTime heureDebut_Programme = DateTime.Parse(reader2.GetString("HDebuProg"));
                                DateTime heureFin_Programme = DateTime.Parse(reader2.GetString("HFinProg"));
                                int numero_cycle = int.Parse(reader2.GetString("Cycle"));
                                DateTime heureDebut_Presence = DateTime.Parse(reader2.GetString("HDebuPres"));
                                DateTime heureFin_Presence = DateTime.Parse(reader2.GetString("HFinProg"));

                                double nb_heure = correctionTemp(heureDebut_Presence, heureFin_Presence, heureDebut_Programme, heureFin_Programme);

                                //Console.WriteLine(heureDebut_Programme + " " + heureFin_Programme);

                                if (numero_cycle == 1)
                                    nombre_Heure_Cycle1 += nb_heure;
                                if (numero_cycle == 1)
                                    nombre_Heure_Cycle2 += nb_heure;

                            }

                            double total_a_payer = ((nombre_Heure_Cycle1 * TauxHauraire_Cycle1) + (nombre_Heure_Cycle2 * TauxHauraire_Cycle2));



                        }

                        
                    }



                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error Message ");
            }
        }

        public double correctionTemp(DateTime hDebut_presence, DateTime hFin_presence, DateTime hDebut_programme, DateTime hFin_programme)
        {
            TimeSpan nb_minute =  hFin_presence - hDebut_presence ;
            if (hDebut_presence < H_DEBUT_PAUSE_1 && hFin_presence > H_FIN_PAUSE_1)
                nb_minute = nb_minute - TimeSpan.Parse(TEMP_PAUSE_1.ToString("mm"));
            if (hDebut_presence < H_DEBUT_PAUSE_2 && hFin_presence > H_FIN_PAUSE_2)
                nb_minute = nb_minute - TimeSpan.Parse(TEMP_PAUSE_2.ToString("mm"));

            if ((hDebut_presence - hDebut_programme) > TimeSpan.Parse(TOLLERENCE_ABSENCE.ToString("mm")))
                nb_minute -= TimeSpan.Parse(PENALITE_RETARD.ToString("mm"));

            if ((hFin_presence - hFin_programme) > TimeSpan.Parse(TOLLERENCE_ABSENCE.ToString("mm")))
                nb_minute -= TimeSpan.Parse(PENALITE_RETARD.ToString("mm"));


            return int.Parse(nb_minute.ToString("mm"))/PERIODE;
        }
    }
}
