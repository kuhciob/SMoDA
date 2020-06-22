using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SMADlab04
{
    public partial class ReggRoccCofForm : Form
    {
        RegresiveAnalis Lab10;
        const int ROUND_DIGITS = 4;
        double[] X;
        double[] Y;
        public ReggRoccCofForm()
        {
            InitializeComponent();
        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<double> valuesX = LabToolz.ScanFromTextBox(XtextBox);
            List<double> valuesY = LabToolz.ScanFromTextBox(YtextBox);

            X = valuesX.ToArray();
            Y = valuesY.ToArray();
            Lab10 = new RegresiveAnalis(X, Y);
            PrintDataInForm();
            Lab10.BuildChart2(chart1);
        }
        void PrintDataInForm()
        {
            RxytextBox.Text = Math.Round(Lab10.GetEmpRegrXonY(), ROUND_DIGITS).ToString();
            RyxtextBox.Text = Math.Round(Lab10.GetEmpRegrYonX(), ROUND_DIGITS).ToString();
            XonYLabel.Text =Lab10.GetXYEquation();
            YonXLabel.Text = Lab10.GetYXEquation();
            KxytextBox.Text = Statistic.GetCov(X,Y).ToString();
            CortextBox.Text= Statistic.GetCorr(X, Y).ToString();

            double[] IntersectPoint = Lab10.GetIntersectionPoint();
            if (IntersectPoint == null)
            {
                Interlabel.Text = "Точки перетину немає";
            }
            else
            {
                Interlabel.Text = String.Format("Точка перетину прямих: ( {0} ; {1} )", Math.Round(IntersectPoint[0], ROUND_DIGITS), Math.Round(IntersectPoint[1], ROUND_DIGITS));
            }


        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
