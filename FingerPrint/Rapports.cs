using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
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
        private const String TOLLERENCE_ABSENCE = "00:05:00";
        private const String TEMP_PAUSE_1 = "00:15:00";
        private const String TEMP_PAUSE_2 = "00:30:00";
        private const String PENALITE_RETARD = "00:15:00";
        private TimeSpan H_DEBUT_PAUSE_1 = TimeSpan.Parse("10:30:00");        
        private TimeSpan H_FIN_PAUSE_1 = TimeSpan.Parse("10:45:00");

        private TimeSpan H_DEBUT_PAUSE_2 = TimeSpan.Parse("13:30:00");
        private TimeSpan H_FIN_PAUSE_2 = TimeSpan.Parse("14:00:00");

        PaperSize paperSize = new PaperSize("papersize", 150, 500);//set the paper size
        int totalnumber = 0;//this is for total number of items of the list or array
        int itemperpage = 0;
        int numberItemsPrinted = 0;

        double total = 0;

        


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
            DGV_salaire.Rows.Clear();
            //resultlabel.Text = "";
            string titre = String.Format("{0,-50} {1,-30} {2,-30} {3} \n", "Nom Professeur","Nombre heure premier cycle", " nombre heure second cycle ", "Total a payer");
            //resultlabel.Text += titre + "\n\t";
            string dateDebut = dateTimePicker1.Value.Date.ToString("yyyy-MM-dd") ;
            string dateFin = dateTimePicker2.Value.Date.ToString("yyyy-MM-dd");
            
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
                            $" pres.date BETWEEN '{dateDebut}' AND DATE_FORMAT('{dateFin}','%Y-%m-%d') and " +
                            "prg.visible = true and m.visible = true and cl.visible = true";

                            Console.WriteLine(query2);
                         

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
                                if (numero_cycle == 2)
                                    nombre_Heure_Cycle2 += nb_heure;

                                nb_heure = 0;
                            }
                            

                            double total_a_payer = ((nombre_Heure_Cycle1 * TauxHauraire_Cycle1) + (nombre_Heure_Cycle2 * TauxHauraire_Cycle2));
                            

                            //Console.WriteLine($"Total a payer prof {profNom}  = "+total_a_payer);
                            if(total_a_payer != 0)
                            {
                                //resultlabel.Text += String.Format("\n------------------------------------------------------------------------------------------------------------------------------\n");
                                //resultlabel.Text += String.Format("{0,-50} {1,-30} {2,-30} {3:N0} \n", profNom, Math.Round(nombre_Heure_Cycle1,2), Math.Round(nombre_Heure_Cycle2,2), Math.Round(total_a_payer));
                                //$"{profNom}  = " + Math.Round(total_a_payer) + "\n\t";

                                int n = DGV_salaire.Rows.Add();
                                DGV_salaire.Rows[n].Cells[0].Value = profNom;
                                DGV_salaire.Rows[n].Cells[1].Value = Math.Round(nombre_Heure_Cycle1, 2);
                                DGV_salaire.Rows[n].Cells[2].Value = Math.Round(nombre_Heure_Cycle2, 2);
                                DGV_salaire.Rows[n].Cells[3].Value = Math.Round(total_a_payer);
                            }
                            total += total_a_payer;

                            nombre_Heure_Cycle1 = 0.0;
                            nombre_Heure_Cycle2 = 0.0;
                        }                        
                    }
                    int m = DGV_salaire.Rows.Add();
                    DGV_salaire.Rows[m].Cells[0].Value = "  ";
                    DGV_salaire.Rows[m].Cells[1].Value = "  ";
                    DGV_salaire.Rows[m].Cells[2].Value = "Total A payer";
                    DGV_salaire.Rows[m].Cells[3].Value = total;

                    //.Text += "\n\n Total A payer  ---------------------------------------------------------------------------------------------------------------------  " + total +"\n";
                }

                //resultTextBox.Text = resultlabel.Text;

                total = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error Message ");
            }
        }

        public double correctionTemp(TimeSpan hDebut_presence, TimeSpan hFin_presence, TimeSpan hDebut_programme, TimeSpan hFin_programme)
        {
            if ((hFin_presence - hFin_programme) > TimeSpan.Parse("00:02:00"))
                hFin_presence = hFin_programme;
            if (hDebut_programme > hDebut_presence)
                hDebut_presence = hDebut_programme;

            TimeSpan nb_minute =  hFin_presence - hDebut_presence ;
            Console.WriteLine(nb_minute);
            Console.WriteLine(hDebut_presence - hDebut_programme);
            Console.WriteLine(TimeSpan.Parse(TOLLERENCE_ABSENCE.ToString()));


            if ((hDebut_presence < H_DEBUT_PAUSE_1) && (hFin_presence > H_FIN_PAUSE_1))
                nb_minute = nb_minute - TimeSpan.Parse(TEMP_PAUSE_1);

            if ((hDebut_presence < H_DEBUT_PAUSE_2) && (hFin_presence > H_FIN_PAUSE_2))//1
                nb_minute = nb_minute - TimeSpan.Parse(TEMP_PAUSE_2);

            //if ((hDebut_presence - hDebut_programme) > TimeSpan.Parse(TOLLERENCE_ABSENCE.ToString()))
            //{
            //    nb_minute -= TimeSpan.Parse(PENALITE_RETARD.ToString());
            //}
            
              
            if ((hDebut_presence - hDebut_programme) > TimeSpan.Parse("00:02:00")) 
            {
                if((hDebut_presence - hDebut_programme) < TimeSpan.Parse(PENALITE_RETARD.ToString())){
                    nb_minute -= TimeSpan.Parse(PENALITE_RETARD.ToString());
                }
                else 
                {
                    if ((hDebut_presence - hDebut_programme) < TimeSpan.Parse("00:30:00")) {
                        //Console.WriteLine(nb_minute + "    -30-------------------------------------------------");
                        nb_minute -= TimeSpan.Parse("00:30:00");
                    }
                    else
                    {
                        nb_minute -= TimeSpan.Parse("00:55:00");
                    }
                }              

            }
                

            Console.WriteLine(nb_minute.TotalMinutes);
            Console.WriteLine(nb_minute);

            
            double temp = Math.Round(((nb_minute.TotalMinutes) / PERIODE), 2);
            return temp;
        }

        private void salairePrintDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Font enteteFont;
            Font detailFont;

            enteteFont = new Font("Times New Roman", 12.0F, FontStyle.Bold);
            detailFont = new Font("Times New Roman", 11.0F);

            float hauteurPoliceEnteteFloat = enteteFont.GetHeight();
            float hauteurPoliceDetailFloat = detailFont.GetHeight();

            // Mesurer le titre

            string titreString = "Liste de salaire des enseignants";
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
            xFloat -= e.MarginBounds.X + 3;

            //e.Graphics.DrawString(resultlabel.Text, detailFont, Brushes.Black, xFloat, yFloat);

            //yFloat +=
            //for (int i = numberItemsPrinted; i < resultTextBox.Lines.Count(); i++)
            //foreach (string ligneString in resultTextBox.Lines)

            e.Graphics.DrawString(DGV_salaire.Columns[0].HeaderText, enteteFont, Brushes.Black, xFloat, yFloat);
            e.Graphics.DrawString(DGV_salaire.Columns[1].HeaderText, enteteFont, Brushes.Black, xFloat + 300, yFloat);
            e.Graphics.DrawString(DGV_salaire.Columns[2].HeaderText, enteteFont, Brushes.Black, xFloat + 525, yFloat);
            e.Graphics.DrawString(DGV_salaire.Columns[3].HeaderText, enteteFont, Brushes.Black, xFloat + 700, yFloat);

            yFloat += hauteurPoliceDetailFloat + 10;

            for (int i = numberItemsPrinted; i< DGV_salaire.Rows.Count;i++)
            {

                numberItemsPrinted++;
                // Imprimer la ville
                if (numberItemsPrinted <= DGV_salaire.Rows.Count)
                {
                    //e.Graphics.DrawString(resultTextBox.Lines[i], detailFont, Brushes.Black, xFloat, yFloat);
                    try
                    {
                        e.Graphics.DrawString(DGV_salaire.Rows[i].Cells[0].Value.ToString(), detailFont, Brushes.Black, xFloat, yFloat);
                        e.Graphics.DrawString(DGV_salaire.Rows[i].Cells[1].Value.ToString(), detailFont, Brushes.Black, xFloat + 400, yFloat);
                        e.Graphics.DrawString(DGV_salaire.Rows[i].Cells[2].Value.ToString(), detailFont, Brushes.Black, xFloat + 525, yFloat);
                        e.Graphics.DrawString(DGV_salaire.Rows[i].Cells[3].Value.ToString(), detailFont, Brushes.Black, xFloat + 675, yFloat);
                    }
                    catch (NullReferenceException ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }

                    // Descendre le crayon verticalement

                    yFloat += hauteurPoliceDetailFloat+10;
                }
                else
                {
                    e.HasMorePages = false;
                }
               

                //Console.WriteLine(resultTextBox.Lines[i]);
                if (itemperpage < 30) // check whether  the number of item(per page) is more than 20 or not
                {
                    itemperpage += 1; // increment itemperpage by 1
                    Console.WriteLine(itemperpage);
                    e.HasMorePages = false; // set the HasMorePages property to false , so that no other page will not be added
                }
                else // if the number of item(per page) is more than 20 then add one page
                {
                    itemperpage = 0; //initiate itemperpage to 0 .
                    e.HasMorePages = true; //e.HasMorePages raised the PrintPage event once per page .
                    return;//It will call PrintPage event again
                }
            }

            //e.HasMorePages = true;
        }

        private void salairePrintPreviewDialog_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            itemperpage = 0;
            numberItemsPrinted = 0;
            //Console.WriteLine(resultTextBox.Lines.Count());
            //salairePrintDocument.DefaultPageSettings.PaperSize = 2;
            
            salairePrintPreviewDialog.ShowDialog();

            //Console.WriteLine(resultlabel.Text);
            //Console.WriteLine(resultTextBox.Lines[60]);
        }

        private void resultTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void resultlabel_Click(object sender, EventArgs e)
        {
                
        }
    }
}
