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
    public partial class Login : Form
    {
        private string connectionString = @"Server=localhost;Database=presence_db;Uid=root;Pwd='';";
        public Login()
        {

            InitializeComponent();
        }

        private void BTN_Login_Click(object sender, EventArgs e)
        {
            //this.Close();
            try
            {
                using (MySqlConnection mySqlCon = new MySqlConnection(connectionString))
                {
                    string login = TXT_Utilisateur.Text.Trim();
                    string password = TXT_MotDePasse.Text.Trim();
                    if(login != "" && password != null)
                    {
                        String query = $"select * from administrateur where login = \"{login}\" and password = \"{password}\" and visible = true";

                        MySqlCommand mySqlCommand = new MySqlCommand(query, mySqlCon);
                        mySqlCon.Open();
                        mySqlCommand.CommandType = CommandType.Text;
                        MySqlDataReader reader2 = mySqlCommand.ExecuteReader();
                        if (reader2.Read())
                        {
                            if(reader2.GetString("type_utilisateur") == "Administrateur")
                            {
                                //this.Close();
                                Admistrateur oAdmistrateur = new Admistrateur();
                                oAdmistrateur.Show();
                                //this.Hide();
                            }
                            else
                            {
                                //this.Close();
                                GestionHoraire oGestionHoraire = new GestionHoraire();
                                oGestionHoraire.Show();
                                //this.Hide();
                            }
                           

                        }
                        else
                        {
                            MessageBox.Show("Login ou mot de passe incorrect");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Veillez entrer un nom d'utilisater et mot de passe!!!");
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error Message ");
            }
        }

        private void BTN_Quitter_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }
    }
}

