using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMADlab04
{
    class OneFactorDisperAnalis
    {
        List<double[]> observers;
        double[,] data;
        int ObserversCount=0;
        int MeasurementsCount=0;

        double Dispersion_Intergroup=0;
        double Dispersion_Remind=0;
        double Dispersion_General=0;
        double FisherCrit = 0;
        double FisherCritFromTable=0;
        double[] GroupAverStats;
        double GeneralAverStat=0;
        double Q = 0;
        double Q1 = 0;
        double Q2 = 0;
        double Alfa = 0;


        public OneFactorDisperAnalis(double [,] arr)
        {
            ObserversCount = arr.GetLength(0);
            MeasurementsCount = arr.GetLength(1);
            GroupAverStats = new double[MeasurementsCount];
            data = (double[,])arr.Clone();
        }
        public void SetAlfa(double a)
        {
            Alfa = a;
        }

        void CalcGroupAverStats()
        {
            for (int i = 0; i < ObserversCount; ++i)
            {
                GroupAverStats[i] = 0;
                for (int j = 0; j < MeasurementsCount; ++j) 
                {
                    GroupAverStats[i] += data[i,j] / MeasurementsCount;
                }
            }

        }
        double GetGeneralAverStat()
        {
            this.GeneralAverStat = 0;
            CalcGroupAverStats();
            for (int i = 0; i < ObserversCount; ++i)
            {
                this.GeneralAverStat += GroupAverStats[i] / ObserversCount;
            }
            return this.GeneralAverStat;
        }

        double GetQ()
        {
            Q = 0;
            GetGeneralAverStat();
            for (int i = 0; i < ObserversCount; ++i)
            {
                for (int j = 0; j < MeasurementsCount; ++j)
                {
                    Q += (data[i,j]- GeneralAverStat) * (data[i, j] - GeneralAverStat);
                }
            }
            return Q;
        }

        double GetQ1()
        {
            this.Q1 = 0;
            GetGeneralAverStat();
            for (int i = 0; i < ObserversCount; ++i)
            {
                this.Q1 += MeasurementsCount * (GroupAverStats[i] - GeneralAverStat) * (GroupAverStats[i] - GeneralAverStat);
            }
            return this.Q1;
        }
        double GetQ2()
        {
            this.Q2 = 0;
            GetGeneralAverStat();
            for (int i = 0; i < ObserversCount; ++i)
            {
                for (int j = 0; j < MeasurementsCount; ++j)
                {
                    this.Q2 += (data[i, j] - GroupAverStats[i]) * (data[i, j] - GroupAverStats[i]);
                }
            }
            return this.Q2;
        }
        

        public double GetGeneralDispersion()
        {
            this.Dispersion_General = GetQ()/(ObserversCount * MeasurementsCount-1);
            return this.Dispersion_General;
        }
        public double GetIntergroupDispersion()
        {
            this.Dispersion_Intergroup = GetQ1() / (ObserversCount - 1);
            return this.Dispersion_Intergroup;
        }
        public double GetRemindDispersion()
        {
            this.Dispersion_Remind = GetQ2() / (ObserversCount * (MeasurementsCount - 1));
            return this.Dispersion_Remind;
        }

        public double GetFisherCrit()
        {
            this.FisherCrit = GetIntergroupDispersion() / GetRemindDispersion();
            return this.FisherCrit;
        }

        public void Result()
        {
            double hard005 = 2.95;
            double hard001 = 4.57;

            // для рівня значущості 0,05
            Console.WriteLine("Для рівня значущості 0,05");
            if (GetFisherCrit() < hard005)
            {
                Console.WriteLine(String.Format("Fемпіричне( {0} ) < Fкритичне( {1} )", GetFisherCrit(), hard005));
                Console.WriteLine("Фактор НЕ впливає на результати вимірювання.");
            }
            else
            {
                Console.WriteLine(String.Format("Fемпіричне( {0} ) > Fкритичне( {1} )", GetFisherCrit(), hard005));
                Console.WriteLine("Фактор впливає на результати вимірювання.");
            }

            // для рівня значущості 0,01
            Console.WriteLine("Для рівня значущості 0,01");
            if (GetFisherCrit() < hard005)
            {
                Console.WriteLine(String.Format("Fемпіричне( {0} ) < Fкритичне( {1} )", GetFisherCrit(), hard001));
                Console.WriteLine("Фактор НЕ впливає на результати вимірювання.");
            }
            else
            {
                Console.WriteLine(String.Format("Fемпіричне( {0} ) > Fкритичне( {1} )", GetFisherCrit(), hard001));
                Console.WriteLine("Фактор впливає на результати вимірювання.");
            }
        }


    }
}
