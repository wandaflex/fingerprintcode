using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FingerPrint
{
    public partial class PresenceAuto : Form
    {

        private string connectionString = @"Server=localhost;Database=presence_db;Uid=root;Pwd='';";
        private int profID = 0;
        private String profNom = "";
        private String empreinte_1 = "";
        private String empreinte_2 = "";
        public const int PORT = 2500;
        public PresenceAuto()
        {
            InitializeComponent();
        }

        private void BTN_Start_Click(object sender, EventArgs e)
        {
            // create the client 
            var client = UDPUser.ConnectTo("192.168.1.200", PORT);
            client.Send("READY");
            BTN_Start.Enabled = false;

            Task.Factory.StartNew(async () =>
            {
                while (true)
                {
                    try
                    {
                        var received = await client.Receive();

                        LBL_Connect.Invoke(new MethodInvoker(delegate
                        {
                            LBL_Connect.BackColor = Color.Green;
                            LBL_Connect.Text = "Connected";
                        }
                        ));
                        LSV_Rcev.Invoke(new MethodInvoker(delegate
                        {
                            LSV_Rcev.Items.Add(received.Message);

                            //invoca methode prise presence auto\


                        }
                        ));

                        if (received.Message.Contains("QUIT"))
                        {
                            LBL_Connect.Invoke(new MethodInvoker(delegate
                            {
                                LBL_Connect.BackColor = Color.Red;
                                LBL_Connect.Text = "Disconnected";
                            }
                            ));
                          
                            break;
                        }
                        else if (received.Message.Contains("ErrorFlags"))
                        {
                            //MessageBox.Show(received.Message);
                            DialogResult dresult = MessageBox.Show(received.Message, "Alert"
                              , MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                            if (dresult == DialogResult.OK)
                            {
                                client.Send("READY");
                            }
                        }
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.Message, "Error message");
                    }
                }
            });
        }

        void presenceAutomatique(String finguerID, DateTime dateTime)

        {
            try
            {
                using (MySqlConnection mySqlCon = new MySqlConnection(connectionString))
                {
                    mySqlCon.Open();
                    String query = "SELECT idPROFESSEUR,concat(nom,' ',prenom) \"Nom\",Empreinte_1,Empreinte_2  FROM Professeur";
                    MySqlCommand mySqlCmd = new MySqlCommand(query, mySqlCon);
                    mySqlCmd.CommandType = CommandType.Text;
                    MySqlDataReader reader = mySqlCmd.ExecuteReader();

                    while (reader.Read())
                    {
                        profID = int.Parse(reader.GetString("idPROFESSEUR"));
                        profNom = reader.GetString("Nom");
                        empreinte_1 = reader.GetString("Empreinte_1");
                        empreinte_1 = reader.GetString("Empreinte_2");

                        using (MySqlConnection mySqlCon2 = new MySqlConnection(connectionString))
                        {                           

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


                                Console.WriteLine(heureDebut_Programme + " " + heureFin_Programme + " " + heureDebut_Presence + " " + heureFin_Presence);

                                //double nb_heure = correctionTemp(heureDebut_Presence, heureFin_Presence, heureDebut_Programme, heureFin_Programme);
                                //TotalnbH += nb_heure;
                                //Console.WriteLine(nb_heure);
                                //Console.WriteLine(heureDebut_Programme + " " + heureFin_Programme);

                                //if (numero_cycle == 1)
                                //    nombre_Heure_Cycle1 += nb_heure;
                                //if (numero_cycle == 2)
                                //    nombre_Heure_Cycle2 += nb_heure;

                                //nb_heure = 0;
                            }


                            //double total_a_payer = ((nombre_Heure_Cycle1 * TauxHauraire_Cycle1) + (nombre_Heure_Cycle2 * TauxHauraire_Cycle2));


                            //Console.WriteLine($"Total a payer prof {profNom}  = "+total_a_payer);
                            //if (total_a_payer != 0)
                            //{
                            //    resultlabel.Text += String.Format("-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------\n");
                            //    resultlabel.Text += String.Format("{0,-100} {1,-30} {2,-30} {3:N0} \n", profNom, Math.Round(nombre_Heure_Cycle1), Math.Round(nombre_Heure_Cycle2), Math.Round(total_a_payer));
                            //    //$"{profNom}  = " + Math.Round(total_a_payer) + "\n\t";
                            //}

                            //nombre_Heure_Cycle1 = 0.0;
                            //nombre_Heure_Cycle2 = 0.0;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error Message ");
            }
        }

      
    }
}
