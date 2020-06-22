using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace SMADlab04
{
    class RegresiveAnalis
    {
        const int ROUND_DIGITS = 4;
        double[] X;
        double[] Y;
        double n;

        double alfa;
        double beta;
        double alfaShtrih;
        double betaShtrih;

        double[,] MatrixA;
        double[,] MatrixAShtrih;
        double MatrixDeterminant;
        double MatrixDeterminantShtrih;
        double alfaDeterminant;
        double alfaShtrixDeterminant;
        double betaDeterminant;
        double betaShtrixDeterminant;

        double empireCorX_Y;
        double empireCorY_X;

        double EpmCorXonY;
        double EpmCorYonX;



        public RegresiveAnalis(double[] x, double[] y)
        {
            X = (double[])x.Clone();
            Y = (double[])y.Clone();
            MatrixA = new double[2, 2] { { 0, 0 }, { 0, 0 } };
            MatrixAShtrih = new double[2, 2] { { 0, 0 }, { 0, 0 } };
            n = X.Length;
        }

   

        public double[,] GetMatrixA()
        {
            MatrixA = new double[2, 2] { { 0, 0 }, { 0, 0 } };

            for (int i = 0; i < n; i++)
            {
                MatrixA[0, 1] += X[i];
                MatrixA[1, 0] += X[i];
                MatrixA[1, 1] += X[i]* X[i];
            }
            return MatrixA;
        }
        public double GetMatrixADeterminant()
        {
            MatrixDeterminant = 0;
            double sum1 = 0;
            double sum2 = 0;
            for (int i = 0; i < n; ++i)
            {
                sum1 += n * X[i] * X[i];
                sum2 += X[i];
            }
            sum2 = sum2 * sum2;
            MatrixDeterminant = sum1 - sum2;
            return MatrixDeterminant;
        }
        public double GetMatrixDeterminant(double[,]A)
        {
            return A[0,0]*A[1,1]-A[1,0]*A[0,1];
        }
        public double GetalfaDeterminant()
        {
            alfaDeterminant = 0;
            double sum1 = 0;
            double sum2 = 0;
            double sum3 = 0;
            double sum4 = 0;
            for (int i = 0; i < n; ++i)
            {
                sum1 += X[i] * X[i];
                sum2 += Y[i];
                sum3 += X[i];
                sum4 += X[i] * Y[i];
            }
            alfaDeterminant = sum1*sum2-sum3*sum4;
            return alfaDeterminant;
        }

        public double GetbetaDeterminant()
        {
            betaDeterminant = 0;
            double sum1 = 0;
            double sum2 = 0;
            double sum3 = 0;
            for (int i = 0; i < n; ++i)
            {
                sum1 += n * X[i] * Y[i];
                sum2 += X[i];
                sum3 += Y[i];
            }
            betaDeterminant = sum1  - sum2 * sum3;
            return betaDeterminant;
        }

        public double GetAlfa()
        {
            alfa = GetalfaDeterminant() / GetMatrixADeterminant();
            return alfa;
        }
        public double GetBeta()
        {
            beta = GetbetaDeterminant() / GetMatrixADeterminant();
            return beta;
        }
        public string GetYonXEquation()
        {
            return String.Format("y* = {0} + {1} x*", Math.Round(GetAlfa(), ROUND_DIGITS).ToString(), Math.Round(GetBeta(), ROUND_DIGITS).ToString());
        }
        public double[,] GetMatrixAShtrih()
        {
            MatrixAShtrih = new double[2, 2] { { 0, 0 }, { 0, 0 } };

            for (int i = 0; i < n; i++)
            {
                MatrixAShtrih[0, 1] += Y[i];
                MatrixAShtrih[1, 0] += Y[i];
                MatrixAShtrih[1, 1] += Y[i] * Y[i];
            }
            return MatrixAShtrih;
        }
        public double GetMatrixAShtrihDeterminant()
        {
            MatrixDeterminant = 0;
            double sum1 = 0;
            double sum2 = 0;
            for (int i = 0; i < n; ++i)
            {
                sum1 += n * Y[i] * Y[i];
                sum2 += Y[i];
            }
            sum2 = sum2 * sum2;
            MatrixDeterminant = sum1 - sum2;
            return MatrixDeterminant;
        }
        public double GetalfaShtrihDeterminant()
        {
            alfaShtrixDeterminant = 0;
            double sum1 = 0;
            double sum2 = 0;
            double sum3 = 0;
            double sum4 = 0;
            for (int i = 0; i < n; ++i)
            {
                sum1 += Y[i] * Y[i];
                sum2 += X[i];
                sum3 += Y[i];
                sum4 += X[i] * Y[i];
            }
            alfaShtrixDeterminant = sum1 * sum2 - sum3 * sum4;
            return alfaShtrixDeterminant;
        }

        public double GetbetaShtrihDeterminant()
        {
            betaDeterminant = 0;
            double sum1 = 0;
            double sum2 = 0;
            double sum3 = 0;
            for (int i = 0; i < n; ++i)
            {
                sum1 += n * X[i] * Y[i];
                sum2 += X[i];
                sum3 += Y[i];
            }
            betaDeterminant = sum1 - sum2 * sum3;
            return betaDeterminant;
        }

        public double GetAlfaShtrih()
        {
            alfa = GetalfaShtrihDeterminant() / GetMatrixAShtrihDeterminant();
            return alfa;
        }
        public double GetBetaShtrih()
        {
            beta = GetbetaShtrihDeterminant() / GetMatrixAShtrihDeterminant();
            return beta;
        }
        public string GetXonYEquation()
        {
            return String.Format("x* = {0} + {1} y*", Math.Round(GetAlfaShtrih(), ROUND_DIGITS).ToString(), Math.Round(GetBetaShtrih(), ROUND_DIGITS).ToString());
        }
        public void BuildChart(Chart chart)
        {
            chart.Series.Clear();

            double X1 = X.Max();
            double Y1 = GetAlfa() + GetBeta() * X1;
            double X2 = X.Min();
            double Y2 = GetAlfa() + GetBeta() * X2;
            chart.Series.Add("YonX");
            chart.Series["YonX"].BorderWidth = 3;
            chart.Series["YonX"].ChartType = SeriesChartType.Line;
            chart.Series["YonX"].Points.AddXY(Math.Round(X1, ROUND_DIGITS), Math.Round(Y1, ROUND_DIGITS));
            chart.Series["YonX"].Points.AddXY(Math.Round(X2, ROUND_DIGITS), Math.Round(Y2, ROUND_DIGITS));


            Y1 = Y.Max();
            X1 = GetAlfaShtrih() + GetBetaShtrih() * Y1;
            Y2 = Y.Min();
            X2 = GetAlfaShtrih() + GetBetaShtrih() * Y2;
            chart.Series.Add("XonY");
            chart.Series["XonY"].BorderWidth = 3;
            chart.Series["XonY"].ChartType = SeriesChartType.Line;
            chart.Series["XonY"].Points.AddXY(Math.Round(X1, ROUND_DIGITS), Math.Round(Y1, ROUND_DIGITS));
            chart.Series["XonY"].Points.AddXY(Math.Round(X2, ROUND_DIGITS), Math.Round(Y2, ROUND_DIGITS));
        }

        public double[] GetIntersectionPoint()
        {
            double[] Point = new double[2];
            if ((1 - GetBetaShtrih() * GetBeta()) != 0)
            {
                Point[0] = (GetAlfa() + GetBetaShtrih() * GetAlfaShtrih()) / (1 - GetBetaShtrih() * GetBeta());
                Point[1] = GetAlfaShtrih() + GetBetaShtrih() * Point[0];
                return Point;
            }
            else
                return null;
            
            
        } 

        public double GetEmpRegrXonY()
        {
            return Statistic.GetCorr(X, Y) * Statistic.GetModifyMeanSqrDeviation(X) / Statistic.GetModifyMeanSqrDeviation(Y);
        }
        public double GetEmpRegrYonX()
        {
            return Statistic.GetCorr(X, Y) * Statistic.GetModifyMeanSqrDeviation(Y) / Statistic.GetModifyMeanSqrDeviation(X);
        }
        public string GetYXEquation()
        {
            return String.Format("y* - {0} = {1} ( x* - {2} )", Math.Round(Statistic.GetAverStatic(Y), ROUND_DIGITS), Math.Round(GetEmpRegrYonX(), ROUND_DIGITS), Math.Round(Statistic.GetAverStatic(X), ROUND_DIGITS));
        }
        public string GetXYEquation()
        {
            return String.Format("x* - {0} = {1} ( y* - {2} )", Math.Round(Statistic.GetAverStatic(X), ROUND_DIGITS), Math.Round(GetEmpRegrXonY(), ROUND_DIGITS), Math.Round(Statistic.GetAverStatic(Y), ROUND_DIGITS));
        }
        public void BuildChart2(Chart chart)
        {
            chart.Series.Clear();

            double X1 = X.Max();
            double Y1 = GetEmpRegrYonX() * (X1 - Statistic.GetAverStatic(X)) + Statistic.GetAverStatic(Y); 
            double X2 = X.Min();
            double Y2 = GetEmpRegrYonX() * (X2 - Statistic.GetAverStatic(X)) + Statistic.GetAverStatic(Y); 
            chart.Series.Add("YonX");
            chart.Series["YonX"].BorderWidth = 3;
            chart.Series["YonX"].ChartType = SeriesChartType.Line;
            chart.Series["YonX"].Points.AddXY(Math.Round(X1, ROUND_DIGITS), Math.Round(Y1, ROUND_DIGITS));
            chart.Series["YonX"].Points.AddXY(Math.Round(X2, ROUND_DIGITS), Math.Round(Y2, ROUND_DIGITS));


            Y1 = Y.Max();
            X1 = GetEmpRegrXonY() * (Y1 - Statistic.GetAverStatic(Y)) + Statistic.GetAverStatic(X);
            Y2 = Y.Min();
            X2 = GetEmpRegrXonY() * (Y2 - Statistic.GetAverStatic(Y)) + Statistic.GetAverStatic(X);
            chart.Series.Add("XonY");
            chart.Series["XonY"].BorderWidth = 3;
            chart.Series["XonY"].ChartType = SeriesChartType.Line;
            chart.Series["XonY"].Points.AddXY(Math.Round(X1, ROUND_DIGITS), Math.Round(Y1, ROUND_DIGITS));
            chart.Series["XonY"].Points.AddXY(Math.Round(X2, ROUND_DIGITS), Math.Round(Y2, ROUND_DIGITS));
        }
        public double[] GetIntersectionPoint2()
        {
            double[] Point = new double[2];
            if ((1 - GetEmpRegrXonY() * GetEmpRegrYonX()) != 0)
            {
                Point[0] = (Statistic.GetAverStatic(X) - Statistic.GetAverStatic(X) * GetEmpRegrYonX() * GetEmpRegrXonY()) / (1 - GetEmpRegrXonY() * GetEmpRegrYonX());
                Point[1] = GetEmpRegrXonY()*(Point[0]- Statistic.GetAverStatic(X))+ Statistic.GetAverStatic(Y);
                return Point;
            }
            else
                return null;


        }
    }
}
