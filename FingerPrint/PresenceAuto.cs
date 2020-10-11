﻿using System;
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
using System.Data.SqlClient;

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
            //DateTime dateNow = DateTime.Parse("6/10/2020 07:45:00 AM");
            DateTime dateNow = DateTime.Now;
            string message = "";

            //create the client
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

                            //invoca methode prise presence auto
                            //presenceAutomatique("aaaa",DateTime.Now);

                           
                        }
                        ));

                        if (received.Message.Contains("FINGER_ID"))
                        {
                            message = presenceAutomatique(received.Message, dateNow);
                            client.Send(message);
                        }

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

        public string presenceAutomatique(String fp_message, DateTime dateNow)
        {
            string[] messages = fp_message.Split('_');
            string finguerID = messages[2];

            string message = "";
            bool professeurTrouve = false;
            try
            {
                using (MySqlConnection mySqlCon = new MySqlConnection(connectionString))
                {
                    mySqlCon.Open();
                    String query = "SELECT idPROFESSEUR,concat(nom,' ',prenom) \"Nom\",Empreinte_1,Empreinte_2  FROM professeur";
                    MySqlCommand mySqlCmd = new MySqlCommand(query, mySqlCon);
                    mySqlCmd.CommandType = CommandType.Text;
                    MySqlDataReader professeurs = mySqlCmd.ExecuteReader();

                    while (professeurs.Read())
                    {
                        profID = int.Parse(professeurs.GetString("idPROFESSEUR"));
                        profNom = professeurs.GetString("Nom");
                        empreinte_1 = professeurs.GetString("Empreinte_1");
                        empreinte_2 = professeurs.GetString("Empreinte_2");

                        if ((empreinte_1 == finguerID ) || (empreinte_2 == finguerID))
                        {
                            professeurTrouve = true;
                            using (MySqlConnection mySqlCon2 = new MySqlConnection(connectionString))
                            {

                                String query2 = "select prg.idPROGRAMMES, pm.idPROFESSEUR as \"pmProfID\" , " +
                                //"DATE_FORMAT(pres.HeureDebut,'%H:%i:%s' ) as \"HDebuPres\", DATE_FORMAT(pres.HeureFin,'%H:%i:%s') as \"HFinPres\", " +
                                "DATE_FORMAT(prg.Heure_Debut,'%H:%i:%s' ) as \"HDebuProg\", DATE_FORMAT(prg.Heure_Fin,'%H:%i:%s' ) as \"HFinProg\", " +
                                "DATE_FORMAT(prg.date,'%Y-%m-%d') as \"dateProg\" " +
                                "From programmes as prg,  " +
                                "professeur_matiere as pm " +
                                "where " +
                                "prg.idPROFESSEUR_MATIERE = pm.idPROFESSEUR_MATIERE and " +
                                "prg.visible = true and pm.visible = true";

                                Console.WriteLine(query2);

                                MySqlCommand mySqlCmd2 = new MySqlCommand(query2, mySqlCon2);
                                mySqlCon2.Open();
                                mySqlCmd2.CommandType = CommandType.Text;
                                MySqlDataReader reader2 = mySqlCmd2.ExecuteReader();
                                while (reader2.Read())
                                {

                                    TimeSpan timeNow = TimeSpan.Parse(dateNow.ToString("hh:mm"));
                                    //DateTime 
                                    //double TotalnbH = 0.0;
                                    int progIDProgramme = int.Parse(reader2.GetString("idPROGRAMMES"));
                                    int pmProfID = int.Parse(reader2.GetString("pmProfID"));
                                    var dateProg = reader2.GetString("dateProg");
                                    TimeSpan heureDebut_Programme = TimeSpan.Parse(reader2.GetString("HDebuProg"));
                                    TimeSpan heureFin_Programme = TimeSpan.Parse(reader2.GetString("HFinProg"));                                    

                                    Console.WriteLine( dateProg +" "+ dateNow.ToString("yyyy-MM-dd") + " " + profID + " " + pmProfID + "  HDebut Prog " + heureDebut_Programme + "  HFinProg " + heureFin_Programme + " time now " + timeNow + "id prog" + progIDProgramme);
                                
                                    if(dateProg == dateNow.ToString("yyyy-MM-dd"))
                                    {
                                        if(profID == pmProfID)
                                        {
                                            using (MySqlConnection mySqlCon3 = new MySqlConnection(connectionString))
                                            {
                                                if ((heureDebut_Programme - timeNow) <= TimeSpan.Parse("00:40:00"))
                                                {
                                                    using (MySqlConnection mySqlCon4 = new MySqlConnection(connectionString))
                                                    {
                                                        using (MySqlCommand mySqlCommand = new MySqlCommand( $"SELECT COUNT(*) from presence where PROGRAMMES_idPROGRAMMES = {progIDProgramme} ", mySqlCon4))
                                                        {
                                                            mySqlCon4.Open();

                                                            int userCount = Convert.ToInt32(mySqlCommand.ExecuteScalar());
                                                            
                                                            if (RBN_Heure_Debut.Checked == true)
                                                            {
                                                                Console.WriteLine(dateProg + " " + profID + " " + pmProfID + " " + heureDebut_Programme + " " + heureFin_Programme);
                                                                Console.WriteLine(userCount);
                                                                    if (userCount == 0)
                                                                    {
                                                                        string querry = $"insert into presence (date,HeureDebut,HeureFin,PROGRAMMES_idPROGRAMMES,visible) values (\"{dateProg}\",\"{timeNow}\",\"00:00:00\", {progIDProgramme}, true )";
                                                                        Console.WriteLine(querry);
                                                                        MySqlCommand mySqlCmd3 = new MySqlCommand(querry, mySqlCon3);
                                                                        mySqlCon3.Open();
                                                                        mySqlCmd3.CommandType = CommandType.Text;
                                                                        mySqlCmd3.ExecuteReader();
                                                                    if (heureDebut_Programme > timeNow)
                                                                    {
                                                                        message = $"OK_{heureDebut_Programme}";
                                                                    }
                                                                    else
                                                                    {
                                                                        message = $"Retard_{heureDebut_Programme - timeNow}";
                                                                    }
                                                                    
                                                                    //MessageBox.Show("heure de debut enregistrer avec succes");
                                                                    }
                                                                    else
                                                                    { //si h de fin fin deja enregistrer                                                                   
                                                                    message = "Presence_Extiste";
                                                                    //MessageBox.Show("presence exite deja");
                                                                    }

                                                            }
                                                            else if (RBN_Heure_Fin.Checked == true)
                                                            {
                                                                using (MySqlConnection mySqlCon5 = new MySqlConnection(connectionString))
                                                                {
                                                                    using (MySqlCommand mySqlCommandUpdat = new MySqlCommand($"SELECT COUNT(*) from presence where PROGRAMMES_idPROGRAMMES = {progIDProgramme} and HeureFin = \"00:00:00\"", mySqlCon5))
                                                                    {
                                                                        mySqlCon5.Open();

                                                                        int userCountUP = Convert.ToInt32(mySqlCommandUpdat.ExecuteScalar());

                                                                        if (userCount != 0 && userCountUP != 0)
                                                                        {
                                                                            string querry = $"UPDATE presence SET HeureFin = \"{timeNow}\" where PROGRAMMES_idPROGRAMMES = \"{progIDProgramme}\"";
                                                                            MySqlCommand mySqlCmd3 = new MySqlCommand(querry, mySqlCon3);
                                                                            mySqlCon3.Open();
                                                                            mySqlCmd3.CommandType = CommandType.Text;
                                                                            mySqlCmd3.ExecuteReader();

                                                                            message = "HFin_OK";
                                                                            //MessageBox.Show("heure fin enregistrer avec succes");
                                                                        }
                                                                        else
                                                                        {
                                                                            message = "HFin_Extiste";
                                                                            //MessageBox.Show("HEure de fin deja enregistrer exite deja");
                                                                        }

                                                                    }
                                                                }

                                                            }
                                                            else
                                                            {
                                                                //message = "Prof_non_identifier";
                                                                MessageBox.Show("veillez selectionner type de presence ");
                                                            }

                                                        }
                                                    }                                                    
                                                }
                                               
                                            }
                                        }
                                    }
                                }
                            }
                        }                      

                    }
                    if(professeurTrouve == false)
                    {
                        message = "NON_Programmer";
                        MessageBox.Show(message);
                    }
                }
            }
            catch (Exception ex)
            {
                
                message = "Error_Message ";
                //MessageBox.Show(ex.ToString(), );
            }

            message = "WDFPRES_" + message;
            return message;
        }         
    }
}
