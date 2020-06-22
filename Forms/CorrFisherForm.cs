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
    public partial class CorrFisherForm : Form
    {
        Correlation Lab08;
        const int ROUND_DIGITS = 4;
        public CorrFisherForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<double> valuesX = ScanFromTextBox(XtextBox);
            List<double> valuesY = ScanFromTextBox(YtextBox);

            double[] X = valuesX.ToArray();
            double[] Y = valuesY.ToArray();

            Lab08 = new Correlation(X, Y);
            Lab08.SetT(Convert.ToDouble(TtextBox.Text));

            PrintDataInForm();
            int a = 0;
        }

        private List<double> ScanFromTextBox(TextBox Box)
        {
            string str = Box.Text;

            string[] valueStrings = Box.Text.Split(' ', '\n', '\t');

            List<double> values = new List<double>();
            for (int i = 0; i < valueStrings.Length; ++i)
            {
                string input = valueStrings[i];
                if (String.IsNullOrWhiteSpace(input)) continue;

                try
                {
                    values.Add(Double.Parse(input, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture));
                }
                catch (Exception ex)
                {

                }

            }
            return values;
        }
        void PrintDataInForm()
        {
            double[] FisherInterval = Lab08.GetFisherInterval();
            double FisherLength = Lab08.GetFishelLEngth();
            CovtextBox.Text = Math.Round(Lab08.GetCov(), ROUND_DIGITS).ToString();
            SxtextBox.Text = Math.Round(Lab08.GetSx(), ROUND_DIGITS).ToString();
            SytextBox.Text = Math.Round(Lab08.GetSy(), ROUND_DIGITS).ToString();
            RXYtextBox.Text = Math.Round(Lab08.GetCorr(), ROUND_DIGITS).ToString();

            Z1textBox.Text = Math.Round(FisherInterval[0], ROUND_DIGITS).ToString();
            Z1textBox2.Text = Math.Round(FisherInterval[0], ROUND_DIGITS).ToString();
            Z1textBox3.Text = Math.Round(FisherInterval[0], ROUND_DIGITS).ToString();

            Z2textBox.Text = Math.Round(FisherInterval[1], ROUND_DIGITS).ToString();
            Z2textBox2.Text = Math.Round(FisherInterval[1], ROUND_DIGITS).ToString();
            Z2textBox3.Text = Math.Round(FisherInterval[1], ROUND_DIGITS).ToString();

            LtextBox.Text = Math.Round(FisherLength, ROUND_DIGITS).ToString();

            if (FisherLength > Math.Abs( Lab08.GetCorr() ) )
            {
                FinishtextBox.Text = " > ";
                ConcluciontextBox.Text = "Отже:\n Між X та Y НЕ існує кореляційний зв'язок";
            }
            else
            if (FisherLength == Math.Abs(Lab08.GetCorr()))
            {
                FinishtextBox.Text = "=";
                ConcluciontextBox.Text = "Отже:\n Між X та Y існує кореляційний зв'язок";
            }
            else
            {
                FinishtextBox.Text = "<";
                ConcluciontextBox.Text = "Отже:\n Між X та Y  існує кореляційний зв'язок";
            }
        }
    }
}
