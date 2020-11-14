using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;


namespace FingerPrint
{
    public partial class Diagnostic : Form
    {
        public const int PORT = 2500;
        public int Envoyer = 0;

        public Diagnostic()
        {
            InitializeComponent();
        }

        private void BTN_Send_Click(object sender, EventArgs e)
        {
            Envoyer = 1;
        }

        private void BTN_Connect_Click(object sender, EventArgs e)
        {
            var client = UDPUser.ConnectTo("192.168.1.200", PORT);
            client.Send("READY");
                                 
            Task.Factory.StartNew(async () =>
            {
                while (true)
                {
                    if (Envoyer == 1)
                    { 
                        client.Send(TXB_Input.Text.Trim());
                        Envoyer = 0;
                    }

                    try
                    {
                        var received = await client.Receive();

                        LBL_Etat.Invoke(new MethodInvoker(delegate
                        {
                            LBL_Etat.BackColor = Color.Green;
                            LBL_Etat.Text = "Connected";
                        }
                        ));
                        LSV_Diag.Invoke(new MethodInvoker(delegate
                        {
                            LSV_Diag.Items.Add(received.Message);

                        }
                        ));

                        if (received.Message.Contains("QUIT"))
                        {
                            LSV_Diag.Invoke(new MethodInvoker(delegate
                            {
                                LBL_Etat.BackColor = Color.Red;
                                LBL_Etat.Text = "Disconnected";
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
    }
}
