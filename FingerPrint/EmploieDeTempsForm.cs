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
    public partial class EmploieDeTempsForm : Form
    {
        //private listF1Day As new List(FlowLayoutPanel);


        public EmploieDeTempsForm()
        {
            InitializeComponent();
        }

        private void EmploieDeTempsForm_Load(object sender, EventArgs e)
        {
            GenerateDayPanel(30);
        }

        private void GenerateDayPanel(int totalDays)
        {
            f1Days.Controls.Clear();

            for (int i = 1; i <= totalDays; i++)
            {
                FlowLayoutPanel f1 = new FlowLayoutPanel();
                //f1.Location = new System.Drawing.Point(0, 89);
                f1.Name = $"f1Day{i}";
                f1.Size = new Size(128, 88);
                f1.BackColor = Color.White;
                f1.BorderStyle = BorderStyle.FixedSingle;
                f1Days.Controls.Add(f1);
            }
        }
    }
}
