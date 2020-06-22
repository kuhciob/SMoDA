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
    public partial class ReggSqrForm : Form
    {
        private RegresiveAnalis Lab09;
        const int ROUND_DIGITS = 4;
        public ReggSqrForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<double> valuesX = LabToolz.ScanFromTextBox(XtextBox);
            List<double> valuesY = LabToolz.ScanFromTextBox(YtextBox);

            double[] X = valuesX.ToArray();
            double[] Y = valuesY.ToArray();
            Lab09 = new RegresiveAnalis(X, Y);
            PrintDataInForm();
            Lab09.BuildChart(chart1);
        }
        void PrintDataInForm()
        {

            double[,] A = Lab09.GetMatrixA();
            double[,] A_shtrih = Lab09.GetMatrixAShtrih();
            A00textBox.Text = Math.Round(A[0, 0], ROUND_DIGITS).ToString();
            A01textBox.Text = Math.Round(A[0, 1], ROUND_DIGITS).ToString();
            A10textBox.Text = Math.Round(A[1, 0], ROUND_DIGITS).ToString();
            A11textBox.Text = Math.Round(A[1, 1], ROUND_DIGITS).ToString();

            A200textBox.Text = Math.Round(A_shtrih[0, 0], ROUND_DIGITS).ToString();
            A201textBox.Text = Math.Round(A_shtrih[0, 1], ROUND_DIGITS).ToString();
            A210textBox.Text = Math.Round(A_shtrih[1, 0], ROUND_DIGITS).ToString();
            A211textBox.Text = Math.Round(A_shtrih[1, 1], ROUND_DIGITS).ToString();

            DetAtextBox.Text= Math.Round(Lab09.GetMatrixADeterminant(), ROUND_DIGITS).ToString();
            DetA2textBox.Text = Math.Round(Lab09.GetMatrixAShtrihDeterminant(), ROUND_DIGITS).ToString();

            DetAlfatextBox.Text = Math.Round(Lab09.GetalfaDeterminant(), ROUND_DIGITS).ToString();
            DetBetatextBox.Text = Math.Round(Lab09.GetbetaDeterminant(), ROUND_DIGITS).ToString();

            DetAlfa2textBox.Text = Math.Round(Lab09.GetalfaShtrihDeterminant(), ROUND_DIGITS).ToString();
            DetBeta2textBox.Text = Math.Round(Lab09.GetbetaShtrihDeterminant(), ROUND_DIGITS).ToString();

            AlfatextBox.Text= Math.Round(Lab09.GetAlfa(), ROUND_DIGITS).ToString();
            BetatextBox.Text = Math.Round(Lab09.GetBeta(), ROUND_DIGITS).ToString();

            Alfa2textBox.Text = Math.Round(Lab09.GetAlfaShtrih(), ROUND_DIGITS).ToString();
            Beta2textBox.Text = Math.Round(Lab09.GetBetaShtrih(), ROUND_DIGITS).ToString();
 
            YonXLabel.Text = Lab09.GetYonXEquation();
            XonYLabel.Text = Lab09.GetXonYEquation();

            double[] IntersectPoint = Lab09.GetIntersectionPoint();
            if (IntersectPoint == null)
            {
                Interlabel.Text = "Точки перетину немає";
            }
            else
            {
                Interlabel.Text = String.Format("Точка перетину прямих: ( {0} ; {1} )", Math.Round(IntersectPoint[0], ROUND_DIGITS), Math.Round(IntersectPoint[1], ROUND_DIGITS));
            }


        }


        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
