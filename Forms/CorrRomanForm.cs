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
    public partial class CorrRomanForm : Form
    {
        Correlation Lab07;
        const int ROUND_DIGITS = 4;
        public CorrRomanForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double[] x = { 0.35, 0.35, 0.30, 0.36, 0.31, 0.36, 0.34, 0.38, 0.37, 0.38, 0.36, 0.40 };
            double[] y = { 0.24, 0.32, 0.29, 0.31, 0.27, 0.32, 0.29, 0.37, 0.37, 0.30, 0.38, 0.33 };

            List<double> valuesX = ScanFromTextBox(XtextBox);
            List<double> valuesY = ScanFromTextBox(YtextBox);

            double[] X = valuesX.ToArray();
            double[] Y = valuesY.ToArray();
            //crutch = Crutch(crutch);
            Lab07 = new Correlation(X, Y);
            //valuesX.Clear();
            //valuesY.Clear();

            
            PrintDataInForm();

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
            CovtextBox.Text = Math.Round(Lab07.GetCov(), ROUND_DIGITS).ToString();
            SxtextBox.Text = Math.Round(Lab07.GetSx(), ROUND_DIGITS).ToString();
            SytextBox.Text = Math.Round(Lab07.GetSy(), ROUND_DIGITS).ToString();
            RXYtextBox.Text =  Math.Round(Lab07.GetCorr(), ROUND_DIGITS).ToString();
            RomantextBox.Text = Math.Round(Lab07.GetRomanovskiSrit(), ROUND_DIGITS).ToString() ;

            if (Math.Abs(Lab07.GetCorr()) > Lab07.GetRomanovskiSrit())
            {
                FinishtextBox.Text = ">";
                ConcluciontextBox.Text = "Отже:\n Між X та Y існує кореляційний зв'язок";
            }
            else
            if (Math.Abs(Lab07.GetCorr()) == Lab07.GetRomanovskiSrit())
            {
                FinishtextBox.Text = "=";
                ConcluciontextBox.Text = "Отже:\n Між X та Y існує кореляційний зв'язок";
            }
            else
            {
                FinishtextBox.Text = "<";
                ConcluciontextBox.Text = "Отже:\n Між X та Y немає кореляційног зв'язоку";
            }
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
