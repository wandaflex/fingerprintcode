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
        private double nombre_Heure_Cycle1 = 0.0;
        private double nombre_Heure_Cycle2 = 0.0;
        private const int PERIODE = 55;
        private const int TOLLERENCE_ABSENCE = 5;
        private const String TEMP_PAUSE_1 = "00:15";
        private const String TEMP_PAUSE_2 = "00:30";
        private const int PENALITE_RETARD = 5;
        private TimeSpan H_DEBUT_PAUSE_1 = TimeSpan.Parse("10:45");
        private TimeSpan H_DEBUT_PAUSE_2 = TimeSpan.Parse("12:30");
        private TimeSpan H_FIN_PAUSE_1 = TimeSpan.Parse("11:00");
        private TimeSpan H_FIN_PAUSE_2 = TimeSpan.Parse("13:00");


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
            string titre = String.Format("{0,-60} {1,-60} {2,-60} {3} \n", "nom Professeur","nombre heure premier cycle", " nombre heure second cycle ", "Total a payer");
            resultlabel.Text += titre + "\n\t";
            DateTime dateDebut = dateTimePicker1.Value ;
            DateTime dateFin = dateTimePicker2.Value ;
            
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
                            "DATE_FORMAT(pres.HeureDebut,'%H:%i:%s' ) as \"HDebuPres\", DATE_FORMAT(pres.HeureFin,'%H:%i:%s') as \"HFinPres\", " +
                            "DATE_FORMAT(prg.Heure_Debut,'%H:%i:%s' ) as \"HDebuProg\", DATE_FORMAT(prg.Heure_Fin,'%H:%i:%s' ) as \"HFinProg\", cl.Cycle " +
                            "From presence as pres, programmes as prg, classe as cl, " +
                            "professeur_matiere as pm, professeur as pr, matiere as m " +
                            "where pres.PROGRAMMES_idPROGRAMMES = prg.idPROGRAMMES and " +
                            "prg.CLASSE_idCLASSE = cl.idCLASSE and " +
                            "prg.idPROFESSEUR_MATIERE = pm.idPROFESSEUR_MATIERE and " +
                            "pm.idPROFESSEUR = pr.idPROFESSEUR and " +
                            "pm.idMATIERE = m.idMATIERE and " +
                            $"pr.idPROFESSEUR = {profID} and " +
                            //$" pres.date BETWEEN DATE_FORMAT('{dateDebut}','%Y-%m-%d') AND DATE_FORMAT('{dateFin}','%Y-%m-%d') and " +
                            "prg.visible = true and m.visible = true and cl.visible = true";

                            //Console.WriteLine(query2);
                         

                            MySqlCommand mySqlCmd2 = new MySqlCommand(query2, mySqlCon2);
                            mySqlCon2.Open();
                            mySqlCmd2.CommandType = CommandType.Text;
                            MySqlDataReader reader2 = mySqlCmd2.ExecuteReader();
                            while (reader2.Read())
                            {
                                //double TotalnbH = 0.0;
                                TimeSpan heureDebut_Programme = TimeSpan.Parse(reader2.GetString("HDebuProg"));
                                TimeSpan heureFin_Programme = TimeSpan.Parse(reader2.GetString("HFinProg"));
                                int numero_cycle = int.Parse(reader2.GetString("Cycle"));
                                TimeSpan heureDebut_Presence = TimeSpan.Parse(reader2.GetString("HDebuPres"));
                                TimeSpan heureFin_Presence = TimeSpan.Parse(reader2.GetString("HFinPres"));


                                Console.WriteLine(heureDebut_Programme + " " + heureFin_Programme + " " + heureDebut_Presence+" "+ heureFin_Presence);
                                
                                double nb_heure = correctionTemp(heureDebut_Presence , heureFin_Presence, heureDebut_Programme, heureFin_Programme);
                                //TotalnbH += nb_heure;
                                Console.WriteLine(nb_heure);
                                //Console.WriteLine(heureDebut_Programme + " " + heureFin_Programme);

                                if (numero_cycle == 1)
                                    nombre_Heure_Cycle1 += nb_heure;
                                if (numero_cycle == 1)
                                    nombre_Heure_Cycle2 += nb_heure;

                                nb_heure = 0;
                            }
                            

                            double total_a_payer = ((nombre_Heure_Cycle1 * TauxHauraire_Cycle1) + (nombre_Heure_Cycle2 * TauxHauraire_Cycle2));
                            

                            //Console.WriteLine($"Total a payer prof {profNom}  = "+total_a_payer);
                            if(total_a_payer != 0)
                            {
                                resultlabel.Text += String.Format("{0,-60} {1,60} {2,60} {3:N0} \n", profNom, Math.Round(nombre_Heure_Cycle1), Math.Round(nombre_Heure_Cycle2), Math.Round(total_a_payer));
                                    //$"{profNom}  = " + Math.Round(total_a_payer) + "\n\t";
                            }

                            nombre_Heure_Cycle1 = 0.0;
                            nombre_Heure_Cycle2 = 0.0;
                        }                        
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error Message ");
            }
        }

        public double correctionTemp(TimeSpan hDebut_presence, TimeSpan hFin_presence, TimeSpan hDebut_programme, TimeSpan hFin_programme)
        {
            TimeSpan nb_minute =  hFin_presence - hDebut_presence ;

            
            if (hDebut_presence < H_DEBUT_PAUSE_1 && hFin_presence > H_FIN_PAUSE_1)
                nb_minute = nb_minute - TimeSpan.Parse(TEMP_PAUSE_1);
            if (hDebut_presence < H_DEBUT_PAUSE_2 && hFin_presence > H_FIN_PAUSE_2)
                nb_minute = nb_minute - TimeSpan.Parse(TEMP_PAUSE_2);

            if ((hDebut_presence - hDebut_programme) > TimeSpan.Parse(TOLLERENCE_ABSENCE.ToString()))
                nb_minute -= TimeSpan.Parse(PENALITE_RETARD.ToString());

            if ((hFin_presence - hFin_programme) > TimeSpan.Parse(TOLLERENCE_ABSENCE.ToString()))
                nb_minute -= TimeSpan.Parse(PENALITE_RETARD.ToString());

            Console.WriteLine(nb_minute.TotalMinutes);
            return (nb_minute.TotalMinutes)/PERIODE;
        }

        private void salairePrintDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Font enteteFont;
            Font detailFont;

            enteteFont = new Font("Times New Roman", 14.0F, FontStyle.Bold);
            detailFont = new Font("Times New Roman", 12.0F);

            float hauteurPoliceEnteteFloat = enteteFont.GetHeight();
            float hauteurPoliceDetailFloat = detailFont.GetHeight();

            // Mesurer le titre

            string titreString = "Liste des villes";
            float largeurTitreFloat = e.Graphics.MeasureString(titreString, enteteFont).Width;

            // Position initiale du crayon : Coin supérieur gauche à l'intérieur des marges

            float xFloat = e.MarginBounds.X;
            float yFloat = e.MarginBounds.Y;

            // Imprimer le titre et la ligne (de la même largeur que le titre)

            e.Graphics.DrawString(titreString, enteteFont, Brushes.Black, xFloat, yFloat);

            yFloat += hauteurPoliceEnteteFloat;

            e.Graphics.DrawLine(new Pen(Color.Blue, 3.0F), xFloat, yFloat, xFloat + largeurTitreFloat, yFloat);

            // Imprimer la liste des villes dans l'ordre original sur des lignes consécutives

            // Positioner le crayon avant d'imprimer la première ville

            yFloat += hauteurPoliceEnteteFloat * 2.0F;


            //foreach (string villesString in villesComboBox.Items)
            //{
            //    // Imprimer la ville

            //    e.Graphics.DrawString(villesString, detailFont, Brushes.Red, xFloat, yFloat);

            //    // Descendre le crayon verticalement

            //    yFloat += hauteurPoliceDetailFloat;
            //}

        }
    }
}
