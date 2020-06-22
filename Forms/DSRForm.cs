using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SMADlab04.Forms
{
    public partial class DSRForm : Form
    {
        private double[] X;
        public DSRForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<double> valuesX = LabToolz.ScanFromTextBox(XtextBox);

            X = new double[valuesX.Count()];
            X = valuesX.ToArray();
            BuildAllCharts();
            Statistic.FillDSRGrid(X, Grid);
            SetAllData();
        }
        private void BuildAllCharts()
        {
            Statistic.BuildChartAccumFreq(X,AcumFchart);
            Statistic.BuildChartAccumRelFreq(X, AcumRFchart);
            Statistic.BuildChartFreqPoligonFreq(X, PoligonFchart);
            Statistic.BuildChartFreqPoligonFreq(X, PoligonRFchart);
            Statistic.BuildChartEmpiricalFunk(X,EmpFunkchart);
        }

        private void chart5_Click(object sender, EventArgs e)
        {

        }
        private void SetAllData()
        {
            AverStatlabel.Text = "Сер. статистичне = " + Math.Round(Statistic.GetAverStatic(X), LabToolz.ROUND_DIGITS);
            ModifyDisplabel.Text = "Мод. дисперсія = " + Math.Round(Statistic.GetModifyDispersion(X), LabToolz.ROUND_DIGITS);
            Dispersionlabel.Text = "Дисперсія = " + Math.Round(Statistic.GetDispersion(X), LabToolz.ROUND_DIGITS);
            Medianlabel.Text = "Медіана = " + Math.Round(Statistic.GetMedian(X), LabToolz.ROUND_DIGITS);
            Rangelabel.Text = "Розмах = " + Math.Round(Statistic.GetRange(X), LabToolz.ROUND_DIGITS);
            Sqrlabel.Text = "Сер. квадратичне = " + Math.Round(Statistic.GetMeanSqrDeviation(X), LabToolz.ROUND_DIGITS);
            ModifySqrlabel.Text = "Мод. сер. квадратичне = " + Math.Round(Statistic.GetModifyMeanSqrDeviation(X), LabToolz.ROUND_DIGITS);
            Varlabel.Text = "Варіація = " + Math.Round(Statistic.GetVariation(X), LabToolz.ROUND_DIGITS);

            Asymmetrylabel.Text = "Асиметрія = " + Math.Round(Statistic.GetAsymmetry(X), LabToolz.ROUND_DIGITS);
            Excesslabel.Text = "Ексцес = " + Math.Round(Statistic.GetExcess(X), LabToolz.ROUND_DIGITS);
            double[] ModArr = Statistic.GetMod(X);

            CentrMomtextBox.Text = Statistic.GetKCentralPoint(X, Convert.ToInt32(numericUpDown2.Value)).ToString();
            StartMomtextBox.Text = Statistic.GetKStartPoint(X, Convert.ToInt32(numericUpDown1.Value)).ToString();

            if (ModArr == null)
            {
                Modlabel.Text = "Моди немає";
            }
            else
            {
                Modlabel.Text = "Мода =";
                for (int i = 0; i < ModArr.Length; ++i)
                    Modlabel.Text += ModArr[i] + " , ";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<double> valuesX = LabToolz.ScanFromTextBox(XtextBox);

            X = new double[valuesX.Count()];
            X = valuesX.ToArray();
            CentrMomtextBox.Text = Statistic.GetKCentralPoint(X, Convert.ToInt32(numericUpDown2.Value)).ToString();
            StartMomtextBox.Text = Statistic.GetKStartPoint(X, Convert.ToInt32(numericUpDown1.Value)).ToString();

        }
    }
}
