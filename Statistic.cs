using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace SMADlab04
{
    class Unit
    {
        public double numb;
        public int frequency;
        public double rel_frequency;
        public int acum_freq;
        public double acum_rel_freq;

        public Unit(double numb)
        {
            this.numb = numb;
            this.frequency = 0;
            this.rel_frequency = 0;
            this.acum_freq = 0;
            this.acum_rel_freq = 0;
        }
        public Unit(double numb, int freq, int count)
        {
            this.numb = numb;
            this.frequency = freq;
            this.rel_frequency = freq / (double)count;
        }

    }
    class CRow
    {
        public List<Unit> Row;
        public List<Unit> Elements;

        public CRow()
        {
            Row = new List<Unit>();
            Elements = new List<Unit>();
        }
        public CRow(List<double> Arr)
        {
            Row = new List<Unit>();
            Elements = new List<Unit>();
            SetRow(Arr);
            CreateListOfUniqEl();
            Cumulate();
        }
        public void Reset()
        {
            this.Row.Clear();
            this.Elements.Clear();
        }
        public void Reset(List<double> Arr)
        {
            this.Row.Clear();
            this.Elements.Clear();
            Row = new List<Unit>();
            Elements = new List<Unit>();
            SetRow(Arr);
            CreateListOfUniqEl();
            Cumulate();
        }
        public int FullLength()
        {
            return Row.Count();
        }

        public int UniqLength()
        {
            return Elements.Count();
        }

        void SetRow(List<double> Arr)
        {
            Arr.Sort();
            for (int i = 0; i < Arr.Count(); ++i)
            {
                Unit unit = new Unit(Arr[i]);
                this.Row.Add(unit);
            }

        }
        void CreateListOfUniqEl()
        {
            for (int i = 0; i < Row.Count();)
            {
                double tmp = Row[i].numb;
                int freq = 1;
                for (int j = i + 1; j < Row.Count(); ++j)
                {
                    double tmp2 = Row[j].numb;
                    if (tmp == Row[j].numb)
                    {
                        ++freq;
                    }
                    else
                    {
                        Unit unit = new Unit(tmp, freq, Row.Count());
                        this.Elements.Add(unit);
                        i = j;
                        freq = 1;
                        tmp = Row[i].numb;
                        break;
                    }
                }

                if (i + freq == Row.Count())
                {
                    Unit unit2 = new Unit(tmp, freq, Row.Count());
                    this.Elements.Add(unit2);
                    break;
                }


            }
        }

        void Cumulate()
        {
            int freq = 0;
            double rel_freq = 0;
            for (int i = 0; i < Elements.Count(); ++i)
            {
                freq += Elements[i].frequency;
                rel_freq += Elements[i].rel_frequency;
                Elements[i].acum_freq = freq;
                Elements[i].acum_rel_freq = rel_freq;
            }
        }

        public double[] GetMod()
        {
            List<int> ModIndx = new List<int>();
            int MaxCount = this.Elements[0].frequency;
            for (int i = 0; i < UniqLength(); ++i)
            {
                if (MaxCount < this.Elements[i].frequency)
                {
                    MaxCount = this.Elements[i].frequency;
                }
            }
            for (int i = 0; i < UniqLength(); ++i)
            {
                if (MaxCount == this.Elements[i].frequency)
                {
                    ModIndx.Add(i);
                }
            }
            if (ModIndx.Count() == UniqLength())
            {
                return null;
            }
            double[] ModArr = new double[ModIndx.Count()];
            for (int i = 0; i < ModIndx.Count(); ++i)
            {
                ModArr[i] = this.Elements[ModIndx[i]].numb;
            }

            return ModArr;
        }
    }
    public static class Statistic
    {
        #region DiscretStatRow
        
        public static double GetAverStatic(double[] Elements)
        {
            double AverStatic = 0;
            for (int i = 0; i < Elements.Length; ++i)
                AverStatic +=  Elements[i];
            return AverStatic / (double)Elements.Length;
        }
        public static double[] GetMod(double[] Elements)
        {
            CRow Row = new CRow(new List<double>(Elements));
            return Row.GetMod();
        }

        public static double GetRange(double[] Elements)
        {
            return Elements.Max()-Elements.Min();
        }
        public static double GetMedian(double[] Elements)
        {
            int Length = Elements.Length;
            if ((Length & 1) == 0)
                return (Elements[Length / 2] + Elements[(Length / 2) - 1]) / 2;
            else
                return Elements[Length / 2];
        }
        public static double GetKCentralPoint(double[] Elements,int k)
        {
            if (k == 1)
                return 0;
            double point = 0;
            double averstat = GetAverStatic(Elements);
            for (int i = 0; i < Elements.Length; ++i)
                point += Math.Pow((Elements[i] - averstat), k) ;
            return point / Elements.Length;
        }
        public static double GetKStartPoint(double[] Elements, int k)
        {
            double point = 0;
            double averstat = GetAverStatic(Elements);
            for (int i = 0; i < Elements.Length; ++i)
                point += Math.Pow(Elements[i], k);
            return point / Elements.Length;
        }
        public static double GetDispersion(double[] Elements)
        {
            return GetKCentralPoint(Elements,2);
        }

        public static double GetMeanSqrDeviation(double[] Elements)
        {
            return Math.Pow(GetDispersion(Elements), 0.5);
        }

        public static double GetModifyDispersion(double[] Elements)
        {
            return (Elements.Length / (double)(Elements.Length - 1)) * GetDispersion(Elements);
        }

        public static double GetModifyMeanSqrDeviation(double[] Elements)
        {
            return Math.Pow(GetModifyDispersion(Elements), 0.5);
        }

        public static double GetAsymmetry(double[] Elements)
        {
            return GetKCentralPoint(Elements,3) / Math.Pow(GetMeanSqrDeviation(Elements), 3);
        }

        public static double GetExcess(double[] Elements)
        {
            return GetKCentralPoint(Elements,4) / Math.Pow(GetMeanSqrDeviation(Elements), 4) - 3;
        }

        public static double GetVariation(double[] Elements)
        {
            return GetMeanSqrDeviation(Elements) / GetAverStatic(Elements);
        }
        public static double GetCoeffVariation(double[] Elements)
        {
            return GetVariation(Elements) * 100;
        }
        public static void BuildChartFreqPoligonFreq(double[] Elements,Chart chart)
        {
            chart.Series.Clear();
            chart.ChartAreas.Clear();

            chart.ChartAreas.Add("ChartArea1");

            chart.ChartAreas["ChartArea1"].AxisX.Interval = 1;
            chart.ChartAreas["ChartArea1"].AxisY.Interval = 1;
            chart.ChartAreas["ChartArea1"].AxisX.ArrowStyle = System.Windows.Forms.DataVisualization.Charting.AxisArrowStyle.Triangle;
            chart.ChartAreas["ChartArea1"].AxisY.ArrowStyle = System.Windows.Forms.DataVisualization.Charting.AxisArrowStyle.Triangle;

            chart.Series.Add("Poligon");
            chart.Series["Poligon"].IsXValueIndexed = true;
            chart.Series["Poligon"].BorderWidth = 3;
            chart.Series["Poligon"].ChartType = SeriesChartType.Line;

            CRow Row = new CRow(new List<double>(Elements));
            int length = Row.UniqLength();
        
            for (int i = 0; i < length; ++i)
                chart.Series["Poligon"].Points.AddXY(Row.Elements[i].numb, Row.Elements[i].frequency);
        }
        public static void BuildChartRelFreqPoligonFreq(double[] Elements,Chart chart)
        {
            chart.Series.Clear();
            chart.ChartAreas.Clear();

            chart.ChartAreas.Add("ChartArea1");

            chart.ChartAreas["ChartArea1"].AxisX.Interval = 1;
            chart.ChartAreas["ChartArea1"].AxisY.Interval = 1;
            chart.ChartAreas["ChartArea1"].AxisX.ArrowStyle = System.Windows.Forms.DataVisualization.Charting.AxisArrowStyle.Triangle;
            chart.ChartAreas["ChartArea1"].AxisY.ArrowStyle = System.Windows.Forms.DataVisualization.Charting.AxisArrowStyle.Triangle;

            chart.Series.Add("Poligon");
            chart.Series["Poligon"].IsXValueIndexed = true;
            chart.Series["Poligon"].BorderWidth = 3;
            chart.Series["Poligon"].ChartType = SeriesChartType.Line;

            CRow Row = new CRow(new List<double>(Elements));
            int length = Row.UniqLength();
            for (int i = 0; i < length; ++i)
                chart.Series["Poligon"].Points.AddXY(Row.Elements[i].numb, Row.Elements[i].rel_frequency);
        }
        public static void BuildChartAccumFreq(double[] Elements, Chart chart)
        {
            chart.Series.Clear();
            chart.ChartAreas.Clear();

            chart.ChartAreas.Add("ChartArea1");

            chart.ChartAreas["ChartArea1"].AxisX.Interval = 1;
            chart.ChartAreas["ChartArea1"].AxisY.Interval = 5;
            chart.ChartAreas["ChartArea1"].AxisX.ArrowStyle = System.Windows.Forms.DataVisualization.Charting.AxisArrowStyle.Triangle;
            chart.ChartAreas["ChartArea1"].AxisY.ArrowStyle = System.Windows.Forms.DataVisualization.Charting.AxisArrowStyle.Triangle;
            chart.ChartAreas["ChartArea1"].AxisY.Interval = Math.Round(1 / (double)Elements.Length, 4);
            chart.ChartAreas["ChartArea1"].AxisX.ScaleView.Zoomable = true;
            chart.ChartAreas["ChartArea1"].CursorX.AutoScroll = true;
            chart.ChartAreas["ChartArea1"].CursorX.IsUserSelectionEnabled = true;

            //chart.ChartAreas["ChartArea1"].AxisY.Maximum = this.MyRow.Length()+1;

            chart.Series.Add("Series");
            chart.Series["Series"].IsXValueIndexed = true;
            chart.Series["Series"].BorderWidth = 3;
            chart.Series["Series"].ChartType = SeriesChartType.Spline;

            CRow Row = new CRow(new List<double>(Elements));
            int length = Row.UniqLength();

            for (int i = 0; i < length; ++i)
                chart.Series["Series"].Points.AddXY(Row.Elements[i].numb, Row.Elements[i].acum_freq);
           
            
        }
        public static void BuildChartAccumRelFreq(double[] Elements, Chart chart)
        {
            chart.Series.Clear();
            chart.ChartAreas.Clear();

            chart.ChartAreas.Add("ChartArea1");

            chart.ChartAreas["ChartArea1"].AxisX.Interval = 1;
            chart.ChartAreas["ChartArea1"].AxisY.Interval = 5;
            chart.ChartAreas["ChartArea1"].AxisX.ArrowStyle = System.Windows.Forms.DataVisualization.Charting.AxisArrowStyle.Triangle;
            chart.ChartAreas["ChartArea1"].AxisY.ArrowStyle = System.Windows.Forms.DataVisualization.Charting.AxisArrowStyle.Triangle;
            chart.ChartAreas["ChartArea1"].AxisY.Interval = Math.Round(1 / (double)Elements.Length, 4);
            chart.ChartAreas["ChartArea1"].AxisX.ScaleView.Zoomable = true;
            chart.ChartAreas["ChartArea1"].CursorX.AutoScroll = true;
            chart.ChartAreas["ChartArea1"].CursorX.IsUserSelectionEnabled = true;

            // this.chart3.ChartAreas["ChartArea1"].AxisY.Maximum = this.MyRow.Length()+1;


            chart.Series.Add("Series");
            chart.Series["Series"].IsXValueIndexed = true;
            chart.Series["Series"].BorderWidth = 3;
            chart.Series["Series"].ChartType = SeriesChartType.Spline;

            CRow Row = new CRow(new List<double>(Elements));
            int length = Row.UniqLength();

            for (int i = 0; i < length; ++i)
                chart.Series["Series"].Points.AddXY(Row.Elements[i].numb, Row.Elements[i].acum_rel_freq);
        }
        public static void BuildChartEmpiricalFunk(double[] Elements, Chart chart)
        {
            CRow Row = new CRow(new List<double>(Elements));
            int length = Row.UniqLength();

            chart.Series.Clear();
            chart.ChartAreas.Clear();

            chart.ChartAreas.Add("ChartArea1");

            chart.ChartAreas["ChartArea1"].AxisX.Interval = 1;
            chart.ChartAreas["ChartArea1"].AxisY.Interval = 5;
            chart.ChartAreas["ChartArea1"].AxisX.ArrowStyle = System.Windows.Forms.DataVisualization.Charting.AxisArrowStyle.Triangle;
            chart.ChartAreas["ChartArea1"].AxisY.ArrowStyle = System.Windows.Forms.DataVisualization.Charting.AxisArrowStyle.Triangle;
            chart.ChartAreas["ChartArea1"].AxisY.Interval = Math.Round(1 / (double)Elements.Length, 4);
            chart.ChartAreas["ChartArea1"].AxisX.ScaleView.Zoomable = true;
            chart.ChartAreas["ChartArea1"].CursorX.AutoScroll = true;
            chart.ChartAreas["ChartArea1"].CursorX.IsUserSelectionEnabled = true;

            chart.ChartAreas["ChartArea1"].AxisX.Minimum = Elements.Min() - 10;
            chart.ChartAreas["ChartArea1"].AxisX.Maximum = Elements[length - 1];

            chart.Series.Add("Series");
            chart.Series["Series"].IsXValueIndexed = true;
            chart.Series["Series"].BorderWidth = 3;
            chart.Series["Series"].ChartType = SeriesChartType.Spline;
            chart.Series["Series"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            chart.Series["Series"].Color = System.Drawing.Color.DarkViolet;
            chart.Series["Series"].BorderWidth = 4;

            chart.Series["Series"].Points.AddXY(-10, 0);
            chart.Series["Series"].Points.AddXY(Row.Elements[0].numb, 0);

            for (int i = 1; i < length; ++i)
            {
                chart.Series.Add("Se" + i.ToString());
                // this.chart5.Series["Se" + i.ToString()].IsXValueIndexed = true;
                chart.Series["Se" + i.ToString()].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                chart.Series["Se" + i.ToString()].Color = System.Drawing.Color.DarkViolet;
                chart.Series["Se" + i.ToString()].BorderWidth = 4;

                chart.Series["Se" + i.ToString()].Points.AddXY(Row.Elements[i - 1].numb, Row.Elements[i].acum_rel_freq);
                chart.Series["Se" + i.ToString()].Points.AddXY(Row.Elements[i].numb, Row.Elements[i].acum_rel_freq);

            }
        }

        public static void FillDSRGrid(double[] Elements, System.Windows.Forms.DataGridView Grid)
        {
            CRow Row = new CRow(new List<double>(Elements));

            int Length = Row.UniqLength();
            Grid.ColumnCount = 2 + Elements.Length;

            Grid.Rows.Clear();
            string[] row = new string[Grid.ColumnCount];
            row[0] = "x";
            for (int i = 0; i < Length; ++i)
            {
                row[i + 1] = Row.Elements[i].numb.ToString();
            }
            Grid.Rows.Add(row);


            row[0] = "m";
            for (int i = 0; i < Length; ++i)
            {
                row[i + 1] = Row.Elements[i].frequency.ToString();
            }
            Grid.Rows.Add(row);

            row[0] = "p*";
            for (int i = 0; i < Length; ++i)
            {
                row[i + 1] = Math.Round(Row.Elements[i].rel_frequency, LabToolz.ROUND_DIGITS).ToString();
            }
            Grid.Rows.Add(row);

            row[0] = "acum(m)";
            for (int i = 0; i < Length; ++i)
            {
                row[i + 1] = Row.Elements[i].acum_freq.ToString();
            }
            Grid.Rows.Add(row);

            row[0] = "acum(p*)";
            for (int i = 0; i < Length; ++i)
            {
                row[i + 1] = Math.Round(Row.Elements[i].acum_rel_freq, LabToolz.ROUND_DIGITS).ToString();
            }

            string[] row2 = { " " };
            Grid.Rows.Add(row2);

            row[0] = "x - averstat(x)";
            double sum = 0;
            double averstat = Statistic.GetAverStatic(Elements);
            for (int i = 0; i < Length; ++i)
            {
                sum += Row.Elements[i].numb - averstat;
                row[i + 1] = (Row.Elements[i].numb-averstat).ToString();
            }
            row[Length + 1] = "SUM=" + sum;
            Grid.Rows.Add(row);

            row[0] = "(x - averstat(x))^2";
            sum = 0;
            averstat = Statistic.GetAverStatic(Elements);
            for (int i = 0; i < Length; ++i)
            {
                sum += (Row.Elements[i].numb - averstat) * (Row.Elements[i].numb - averstat);
                row[i + 1] = ((Row.Elements[i].numb - averstat)* (Row.Elements[i].numb - averstat)).ToString();
            }
            row[Length + 1] = "SUM=" + sum;
            Grid.Rows.Add(row);

            row[0] = "(x - averstat(x))^3";
            sum = 0;
            averstat = Statistic.GetAverStatic(Elements);
            for (int i = 0; i < Length; ++i)
            {
                sum += (Row.Elements[i].numb - averstat) * (Row.Elements[i].numb - averstat) * (Row.Elements[i].numb - averstat);
                row[i + 1] = ((Row.Elements[i].numb - averstat) * (Row.Elements[i].numb - averstat) * (Row.Elements[i].numb - averstat)).ToString();
            }
            row[Length+1] = "SUM=" + sum;
            Grid.Rows.Add(row);

            row[0] = "(x - averstat(x))^4";
            sum = 0;
            averstat = Statistic.GetAverStatic(Elements);
            for (int i = 0; i < Length; ++i)
            {
                sum += (Row.Elements[i].numb - averstat) * (Row.Elements[i].numb - averstat) * (Row.Elements[i].numb - averstat) * (Row.Elements[i].numb - averstat);
                row[i + 1] = ((Row.Elements[i].numb - averstat) * (Row.Elements[i].numb - averstat) * (Row.Elements[i].numb - averstat) * (Row.Elements[i].numb - averstat)).ToString();
            }
            row[Length + 1] = "SUM=" + sum;
            Grid.Rows.Add(row);

            Grid.Rows.Add(row2);

            row[0] = "x";
            row[1] = "[-intf;" + Row.Elements[0].numb.ToString() + ")";

            for (int i = 1; i < Length; ++i)
            {
                row[i + 1] = "[ " + Row.Elements[i - 1].numb.ToString() + ";" + Row.Elements[i].numb.ToString() + ")";
            }
            row[Length + 1] = "[ " + Row.Elements[Length - 1].numb.ToString() + ";+ inf]";
            Grid.Rows.Add(row);

            row[0] = "F*(x)";
            row[1] = "0";
            for (int i = 1; i < Length; ++i)
            {
                row[i + 1] = Math.Round(Row.Elements[i - 1].acum_rel_freq, LabToolz.ROUND_DIGITS).ToString();
            }
            row[Length + 1] = "1";
            Grid.Rows.Add(row);


            for (int i = 0; i < Grid.Columns.Count; ++i)
            {
                DataGridViewColumn column = Grid.Columns[i];
                column.Width = 80;
            }


        }
        
        #endregion

        public static double GetCov(double[]X, double[]Y)
        {
            double COVxy = 0;
            for (int i = 0; i < X.Length && i < Y.Length; ++i)
                COVxy += (X[i] - Statistic.GetAverStatic(X)) * (Y[i] - Statistic.GetAverStatic(Y)) / X.Length;
            return COVxy;
        }
        public static double GetSx(double[] X)
        {
            return Statistic.GetMeanSqrDeviation(X);
        }
        public static double GetSy(double[] Y)
        {
            return Statistic.GetMeanSqrDeviation(Y);
        }
        public static double GetCorr(double[] X, double[] Y)
        {
            double CORRxy = Statistic.GetCov(X,Y) / (GetSx(X) * GetSy(Y));
            return CORRxy;
        }
        public static double GetRomanovskiSrit(double[] X, double[] Y)
        {
            return 3 * (1 - GetCorr(X,Y) * GetCorr(X,Y)) / Math.Sqrt(X.Length);
        }
        public static bool IsXYCorrByRomanovski(double[] X, double[] Y)
        {
            return (Math.Abs(GetCorr(X,Y)) >= GetRomanovskiSrit(X, Y)) ? true : false;
        }
        public static double GetFisherFunc(double[] X, double[] Y)
        {
            return  Math.Log((1 + GetCorr(X, Y)) / (1 - GetCorr(X, Y))) / 2;
        }
        
        public static double[] GetFisherInterval(double[] X, double[] Y , double t)
        {
            double [] FisherConfInterval = new double[2];
            FisherConfInterval[0] = GetFisherFunc(X,Y) - t / Math.Sqrt(X.Length - 3);
            FisherConfInterval[1] = GetFisherFunc(X, Y) + t / Math.Sqrt(X.Length - 3);
            return FisherConfInterval;
        }
        public static double GetFishelLEngth(double[] X, double[] Y, double t)
        {
            return GetFisherInterval(X,Y,t)[1] - GetFisherInterval(X, Y, t)[0];
        }
        public static bool IsXYCorrByFisher(double[] X, double[] Y, double t)
        {
            return (GetFishelLEngth(X, Y, t) > Math.Abs(GetCorr(X, Y))) ? true : false;
        }

        public static double GetEmpRegrXonY(double[] X, double[] Y)
        {
            return Statistic.GetCorr(X, Y) * Statistic.GetModifyMeanSqrDeviation(X) / Statistic.GetModifyMeanSqrDeviation(Y);
        }
        public static double GetEmpRegrYonX(double[] X, double[] Y)
        {
            return Statistic.GetCorr(X, Y) * Statistic.GetModifyMeanSqrDeviation(Y) / Statistic.GetModifyMeanSqrDeviation(X);
        }

    }
}
