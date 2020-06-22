using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SMADlab04
{
    class LabToolz
    {
        public static readonly int ROUND_DIGITS = 4;
        public static double RoundToThree(double Numb)
        {
            return Math.Round(Numb, LabToolz.ROUND_DIGITS);
        }
        public static List<double> ScanFromTextBox(TextBox Box)
        {
            string str = Box.Text;
            string res;

            str = str.Replace(";", "");
            str =str.Replace(',', '.');
            
            string[] valueStrings = str.Split(' ', '\n', '\t');
            
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
    }
}
