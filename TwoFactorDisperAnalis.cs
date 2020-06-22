using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMADlab04
{
    class TwoFactorDisperAnalis
    {
        double[,] data;
        int ObserversCount = 0;
        int MeasurementsCount = 0;

        double Dispersion_Intergroup_1 = 0;
        double Dispersion_Intergroup_2 = 0;
        double Dispersion_Remind = 0;
        double Dispersion_General = 0;
        double FisherCrit_1 = 0;
        double FisherCrit_2 = 0;
        double FisherCritFromTable = 0;
        double[] GroupAverStats_1;
        double[] GroupAverStats_2;
        double GeneralAverStat = 0;
        double Q = 0;
        double Q1 = 0;
        double Q2 = 0;
        double Q3 = 0;
        double Alfa = 0;

        public TwoFactorDisperAnalis(double[,] arr)
        {
            ObserversCount = arr.GetLength(0);
            MeasurementsCount = arr.GetLength(1);
            data = (double[,])arr.Clone();
            GroupAverStats_1 = new double[ObserversCount];
            GroupAverStats_2 = new double[MeasurementsCount];

        }
        public void CalcGroupAverStats_1()
        {
            for (int i = 0; i < ObserversCount; ++i)
            {
                GroupAverStats_1[i] = 0;
                for (int j = 0; j < MeasurementsCount; ++j)
                {
                    GroupAverStats_1[i] += data[i, j] / MeasurementsCount;
                }
            }

        }
        public void CalcGroupAverStats_2()
        {
            for (int j = 0; j < MeasurementsCount; ++j)
            {
                GroupAverStats_2[j] = 0;
                for (int i = 0; i < ObserversCount; ++i)
                {
                    GroupAverStats_2[j] += data[i, j] / ObserversCount;
                }
            }

        }
        public double GetGeneralAverStat()
        {
            this.GeneralAverStat = 0;
            CalcGroupAverStats_1();
            for (int i = 0; i < ObserversCount; ++i)
            {
                this.GeneralAverStat += GroupAverStats_1[i] / ObserversCount;
            }
            return this.GeneralAverStat;
        }
        public double GetQ()
        {
            Q = 0;
            GetGeneralAverStat();
            for (int i = 0; i < ObserversCount; ++i)
            {
                for (int j = 0; j < MeasurementsCount; ++j)
                {
                    Q += (data[i, j] - GeneralAverStat) * (data[i, j] - GeneralAverStat);
                }
            }
            return Q;
        }
        public double GetQ1()
        {
            this.Q1 = 0;
            GetGeneralAverStat();
            for (int i = 0; i < ObserversCount; ++i)
            {
                this.Q1 += MeasurementsCount * (GroupAverStats_1[i] - GeneralAverStat) * (GroupAverStats_1[i] - GeneralAverStat);
            }
            return this.Q1;
        }
        public double GetQ2()
        {
            this.Q2 = 0;
            GetGeneralAverStat();
            for (int i = 0; i < MeasurementsCount; ++i)
            {
                this.Q2 += ObserversCount * (GroupAverStats_2[i] - GeneralAverStat) * (GroupAverStats_2[i] - GeneralAverStat);
            }
            return this.Q2;
        }
        public double GetQ3()
        {
            this.Q3 = 0;
            GetGeneralAverStat();
            for (int i = 0; i < ObserversCount; ++i) 
            {
                for (int j = 0; j < MeasurementsCount; ++j) 
                {
                    this.Q3 += (data[i, j] - GroupAverStats_1[i] - GroupAverStats_2[j] + GeneralAverStat) * (data[i, j] - GroupAverStats_1[i] - GroupAverStats_2[j] + GeneralAverStat);
                }
            }
            return this.Q3;
        }
        public double GetGeneralDispersion()
        {
            this.Dispersion_General = GetQ() / (ObserversCount * MeasurementsCount - 1);
            return this.Dispersion_General;
        }
        public double GetIntergroupDispersion_1()
        {
            this.Dispersion_Intergroup_1 = GetQ1() / (ObserversCount - 1);
            return this.Dispersion_Intergroup_1;
        }
        public double GetIntergroupDispersion_2()
        {
            this.Dispersion_Intergroup_2 = GetQ2() / (MeasurementsCount - 1);
            return this.Dispersion_Intergroup_2;
        }
        public double GetRemindDispersion()
        {
            this.Dispersion_Remind = GetQ3() / ((MeasurementsCount - 1) * (ObserversCount - 1));
            return this.Dispersion_Remind;
        }
        public double GetFisherCrit_1()
        {
            this.FisherCrit_1 = GetIntergroupDispersion_1() / GetRemindDispersion();
            return this.FisherCrit_1;
        }
        public double GetFisherCrit_2()
        {
            this.FisherCrit_2 = GetIntergroupDispersion_2() / GetRemindDispersion();
            return this.FisherCrit_2;
        }
        public void Result()
        {
            double hard005_1 = 3.182;
            double hard005_2 = 2.365;
            double hard001_1= 5.841;
            double hard001_2 = 3.499;

            // для рівня значущості 0,05
            Console.WriteLine("Для рівня значущості 0,05");
            if (GetFisherCrit_1() < hard005_1 && GetFisherCrit_2() < hard005_2)
            {
                Console.WriteLine(String.Format("Fемпіричне_1( {0} ) < Fкритичне_1( {1} )", GetFisherCrit_1(), hard005_1));
                Console.WriteLine(String.Format("Fемпіричне_2( {0} ) < Fкритичне_1( {1} )", GetFisherCrit_2(), hard005_2));
                Console.WriteLine("Обидва фактори суттєво НЕ впливають на результати вимірювання.");
            }
            else
            if (GetFisherCrit_1() > hard005_1 && GetFisherCrit_2() > hard005_2)
            {
                Console.WriteLine(String.Format("Fемпіричне_1( {0} ) > Fкритичне_1( {1} )", GetFisherCrit_1(), hard005_1));
                Console.WriteLine(String.Format("Fемпіричне_2( {0} ) > Fкритичне_1( {1} )", GetFisherCrit_2(), hard005_2));

                Console.WriteLine("Обидва фактори суттєво впливають на результати вимірювання.");
            }
            else
            if(GetFisherCrit_1() < hard005_1 && GetFisherCrit_2() > hard005_2)
            {
                Console.WriteLine(String.Format("Fемпіричне_1( {0} ) < Fкритичне_1( {1} )", GetFisherCrit_1(), hard005_1));
                Console.WriteLine(String.Format("Fемпіричне_2( {0} ) > Fкритичне_1( {1} )", GetFisherCrit_2(), hard005_2));

                Console.WriteLine("Перший фактор не впливає; а другий суттєво впливає на результати вимірювання.");
            }
            else
            if (GetFisherCrit_1() > hard005_1 && GetFisherCrit_2() < hard005_2)
            {
                Console.WriteLine(String.Format("Fемпіричне_1( {0} ) > Fкритичне_1( {1} )", GetFisherCrit_1(), hard005_1));
                Console.WriteLine(String.Format("Fемпіричне_2( {0} ) < Fкритичне_1( {1} )", GetFisherCrit_2(), hard005_2));

                Console.WriteLine("Перший фактор суттєво впливає; а другий не впливає на результати вимірювання.");
            }
            hard005_1 = hard001_1;
            hard005_2 = hard001_2;

            // для рівня значущості 0,01
            Console.WriteLine("Для рівня значущості 0,01");
            if (GetFisherCrit_1() < hard005_1 && GetFisherCrit_2() < hard005_2)
            {
                Console.WriteLine(String.Format("Fемпіричне_1( {0} ) < Fкритичне_1( {1} )", GetFisherCrit_1(), hard005_1));
                Console.WriteLine(String.Format("Fемпіричне_2( {0} ) < Fкритичне_1( {1} )", GetFisherCrit_2(), hard005_2));
                Console.WriteLine("Обидва фактори суттєво НЕ впливають на результати вимірювання.");
            }
            else
            if (GetFisherCrit_1() > hard005_1 && GetFisherCrit_2() > hard005_2)
            {
                Console.WriteLine(String.Format("Fемпіричне_1( {0} ) > Fкритичне_1( {1} )", GetFisherCrit_1(), hard005_1));
                Console.WriteLine(String.Format("Fемпіричне_2( {0} ) > Fкритичне_1( {1} )", GetFisherCrit_2(), hard005_2));

                Console.WriteLine("Обидва фактори суттєво впливають на результати вимірювання.");
            }
            else
            if (GetFisherCrit_1() < hard005_1 && GetFisherCrit_2() > hard005_2)
            {
                Console.WriteLine(String.Format("Fемпіричне_1( {0} ) < Fкритичне_1( {1} )", GetFisherCrit_1(), hard005_1));
                Console.WriteLine(String.Format("Fемпіричне_2( {0} ) > Fкритичне_1( {1} )", GetFisherCrit_2(), hard005_2));

                Console.WriteLine("Перший фактор не впливає; а другий суттєво впливає на результати вимірювання.");
            }
            else
            if (GetFisherCrit_1() > hard005_1 && GetFisherCrit_2() < hard005_2)
            {
                Console.WriteLine(String.Format("Fемпіричне_1( {0} ) > Fкритичне_1( {1} )", GetFisherCrit_1(), hard005_1));
                Console.WriteLine(String.Format("Fемпіричне_2( {0} ) < Fкритичне_1( {1} )", GetFisherCrit_2(), hard005_2));

                Console.WriteLine("Перший фактор суттєво впливає; а другий не впливає на результати вимірювання.");
            }
        }
    }
}
