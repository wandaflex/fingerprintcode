using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FingerPrint
{
    public partial class PresenceAuto : Form
    {
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

      
    }
}
