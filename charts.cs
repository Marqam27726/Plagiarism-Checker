using Bunifu.Framework.UI;
using LiveCharts.Wpf;
using LiveCharts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Animation;
using BunifuAnimatorNS;
using System.Windows.Media;
using System.Runtime.InteropServices;
using Color = System.Drawing.Color;

namespace bunifu
{
    public partial class charts : Form
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
         (
             int nLeftRect,     // x-coordinate of upper-left corner
             int nTopRect,      // y-coordinate of upper-left corner
             int nRightRect,    // x-coordinate of lower-right corner
             int nBottomRect,   // y-coordinate of lower-right corner
             int nWidthEllipse, // width of ellipse
             int nHeightEllipse // height of ellipse
         );
        public charts()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 25, 25));
            
            
            solidGauge1.From = 0;
            solidGauge1.To = 100;
            solidGauge1.Base.LabelsVisibility = System.Windows.Visibility.Hidden;
            solidGauge1.Base.GaugeActiveFill = new System.Windows.Media.LinearGradientBrush
            {
                GradientStops=new System.Windows.Media.GradientStopCollection
                {
                    new System.Windows.Media.GradientStop(System.Windows.Media.Colors.Purple,0),
                    new System.Windows.Media.GradientStop(System.Windows.Media.Colors.LightBlue,.6),
                }

            };
       

            //pieChart1.Series = new SeriesCollection
            //{
            //    new PieSeries
            //    {
            //        Title = "Plagiarised",
            //        Values = new ChartValues<double> {f1.fraction},
            //        PushOut = 10
            //    },
            //    new PieSeries
            //    {                  
            //        Title = "Non - Plagiarised",
            //        Values = new ChartValues<double> {100-f1.fraction},
            //        PushOut = 10
            //    },

            //};
        }

        private void meter_Load(object sender, EventArgs e)
        {

        }

        private void elementHost1_ChildChanged(object sender, System.Windows.Forms.Integration.ChildChangedEventArgs e)
        {

        }

        private void pieChart1_ChildChanged(object sender, System.Windows.Forms.Integration.ChildChangedEventArgs e)
        {

        }
        public void UpdateChart(List<double> values)
        {
            foreach (double item in values)
            {
                pieChart1.Series.Add(

                    new PieSeries
                    {
                        Title = "Non-Plagiarised/Plagiarised",
                        Values = new ChartValues<double>() { item },
                        PushOut = 10,
                    }

                );
            }
            
        }
        public void UpdateGauge(double newValue)
        {
            int val = Convert.ToInt32(newValue);
            solidGauge1.Value = val;
        }

        private void solidGauge1_ChildChanged(object sender, System.Windows.Forms.Integration.ChildChangedEventArgs e)
        {

        }

        private void charts_Load(object sender, EventArgs e)
        {

        }

        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bunifuLabel1_Click(object sender, EventArgs e)
        {

        }

        private void bunifuGradientPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
