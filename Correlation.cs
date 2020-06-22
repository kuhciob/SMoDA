using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMADlab04
{
    class Correlation
    {
        double[] X;
        double[] Y;
        double XAverStat;
        double YAverStat;
        double COVxy;
        double CORRxy;
        double Sx;
        double Sy;
        double[] FisherConfInterval;
        double t;
        double alfa = 0.05;
        double FisherFunc;
        double l;
        double RomanovskiCrit;

        public Correlation(double[] x,double[] y)
        {
            X = (double[])x.Clone();
            Y = (double[])y.Clone();
            XAverStat= Statistic.GetAverStatic(X);
            YAverStat = Statistic.GetAverStatic(Y);
            Sx = Statistic.GetMeanSqrDeviation(X);
            Sy = Statistic.GetMeanSqrDeviation(Y);
        }

        public double GetCov()
        {
           double COVxy = 0;
            for (int i = 0; i < X.Length && i < Y.Length; ++i)
                COVxy += (X[i] - Statistic.GetAverStatic(X)) * (Y[i] - Statistic.GetAverStatic(Y)) / X.Length;
            return COVxy;
        }
        public double GetSx()
        {
            //Sx = Statistic.GetMeanSqrDeviation(X);
            return Sx;

        }
        public double GetSy()
        {
            //Sx = Statistic.GetMeanSqrDeviation(X);
            return Sy;

        }
        public double GetCorr()
        {
            CORRxy = GetCov()/(Sx*Sy);
            return CORRxy;
        }
        public double GetRomanovskiSrit()
        {
            RomanovskiCrit = 3 * (1- GetCorr()* GetCorr()) / Math.Sqrt(X.Length);
            return RomanovskiCrit;
        }
        public bool IsXYCorrByRomanovski()
        {
            return (Math.Abs(GetCorr()) >= GetRomanovskiSrit()) ? true : false;
        }
        public double GetFisherFunc()
        {
            FisherFunc = Math.Log((1 + GetCorr()) / (1 - GetCorr())) / 2;
            return FisherFunc;
        }
        public void SetT(double t)
        {
            this.t = t;
        }
        public double[] GetFisherInterval()
        {
            FisherConfInterval = new double[2];
            FisherConfInterval[0] = GetFisherFunc() - t / Math.Sqrt(X.Length - 3);
            FisherConfInterval[1] = GetFisherFunc() + t / Math.Sqrt(X.Length - 3);
            return FisherConfInterval;
        }
        public double GetFishelLEngth()
        {
            return GetFisherInterval()[1] - GetFisherInterval()[0];
        }
        public bool IsXYCorrByFisher()
        {
            return (GetFishelLEngth() > Math.Abs(GetCorr())) ? true : false;
        }

        
    }
}
