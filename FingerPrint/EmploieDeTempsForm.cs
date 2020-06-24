using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FingerPrint
{
    public partial class EmploieDeTempsForm : Form
    {
        List<FlowLayoutPanel> listF1Day = new List<FlowLayoutPanel>();

        public EmploieDeTempsForm()
        {
            InitializeComponent();
        }

        DateTime currentDate = DateTime.Today;

        private void EmploieDeTempsForm_Load(object sender, EventArgs e)
        {
            GenerateDayPanel(42);
            //AddLabelDayToF1Day(GetFirstDayOfWeekOfCurrentDate(), GetTotalDayOfWeekOfCurrentDate());
            DisplayCurrentDate();
        }

        //private int GetFirstDayOfWeekOfCurrentDate()
        //{
        //    DateTime firstDayOfMonth = new DateTime(currentDate.Year, currentDate.Month, 1);
        //    return firstDayOfMonth.DayOfWeek + 1;
        //}

        private int GetTotalDayOfWeekOfCurrentDate()
        {
            DateTime firstDayCurrentDate = new DateTime(currentDate.Year, currentDate.Month, 1);
            return firstDayCurrentDate.AddMonths(1).AddDays(-1).Day;
        }

        private void DisplayCurrentDate()
        {
            lblMonthAndYear.Text = currentDate.ToString("MMMM, yyyy");
            //AddLabelDayToF1Day(GetFirstDayOfWeekOfCurrentDate(), GetTotalDayOfWeekOfCurrentDate());
        }

        private void PrevMonth()
        {
            currentDate = currentDate.AddMonths(-1);
            DisplayCurrentDate();
        }

        private void NextMonth()
        {
            currentDate = currentDate.AddMonths(1);
            DisplayCurrentDate();
        }

        private void Today()
        {
            currentDate = DateTime.Today;
            DisplayCurrentDate();
        }

        private void GenerateDayPanel(int totalDays)
        {
            f1Days.Controls.Clear();

            for (int i = 1; i <= totalDays; i++)
            {
                FlowLayoutPanel f1 = new FlowLayoutPanel();
                f1.Location = new System.Drawing.Point(0, 89);
                f1.Name = $"f1Day{i}";
                f1.Size = new Size(128, 88);
                f1.BackColor = Color.White;
                f1.BorderStyle = BorderStyle.FixedSingle;
                f1Days.Controls.Add(f1);
            }
        }

        private void AddLabelDayToF1Day(int startDayAtF1Number, int totalDaysInMonth)
        {
            for (int i = 1; i <= totalDaysInMonth; i++)
            {
                Label lbl = new Label();
                lbl.Name = $"lblDay{i}";
                lbl.AutoSize = false;
                lbl.TextAlign = ContentAlignment.MiddleRight;
                lbl.Size = new Size(123, 29);
                lbl.Font = new Font("Microsoft Sans Serif", 12);
                //listF1Day((i - 1) + (startDayAtF1Number - 1)).Controls.Clear();
                //listF1Day((i - 1) + (startDayAtF1Number - 1)).Controls.Add(lbl);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PrevMonth();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            NextMonth();
        }

        private void btnToday_Click(object sender, EventArgs e)
        {
            Today();
        }
    }
}
