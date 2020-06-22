using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace SMADlab04
{
    public partial class StartForm : Form
    {
        public StartForm()
        {
            InitializeComponent();



        }
       
        private void Form1_Load(object sender, EventArgs e)
        {

            AllocConsole();
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

        void lab5Run()
        {
            
            double[,] Data =
             {
                { 0.35 , 0.35, 0.30 , 0.36, 0.31, 0.36, 0.34, 0.34},
                { 0.38 , 0.37, 0.38 , 0.36, 0.4, 0.36, 0.48, 0.32},
                {0.32 , 0.31, 0.36 , 0.33, 0.40, 0.42, 0.43, 0.41 },
                { 0.35 , 0.38, 0.42 , 0.47, 0.60, 0.91, 0.89, 0.59 }
            };
    

            OneFactorDisperAnalis LAB05 = new OneFactorDisperAnalis(Data);
 
            Console.WriteLine("Оцінка мiжгрупової дисперсiї = "+Math.Round(LAB05.GetIntergroupDispersion(),4));
            Console.WriteLine("Оцінка залишкової дисперсiї " + Math.Round(LAB05.GetRemindDispersion(), 4));
            Console.WriteLine("Оцінка загальної дисперсiї = " + Math.Round(LAB05.GetGeneralDispersion(), 4));
            Console.WriteLine();
            Console.WriteLine("Оцінка емпiричного значення критерiю ФIШЕРА = " + Math.Round(LAB05.GetFisherCrit(), 4));
            Console.WriteLine();
            LAB05.Result();

        }
        void lab6Run()
        {

            double[,] Data =
             {
                { 0.35 , 0.35, 0.30 , 0.36, 0.31, 0.36, 0.34, 0.34},
                { 0.38 , 0.37, 0.38 , 0.36, 0.4, 0.36, 0.48, 0.32},
                {0.32 , 0.31, 0.36 , 0.33, 0.40, 0.42, 0.43, 0.41 },
                { 0.35 , 0.38, 0.42 , 0.47, 0.60, 0.91, 0.89, 0.59 }
            };


            TwoFactorDisperAnalis LAB05 = new TwoFactorDisperAnalis(Data);

            Console.WriteLine("Оцінка мiжгрупової дисперсiї за фактором 1= " + Math.Round(LAB05.GetIntergroupDispersion_1(), 4));
            Console.WriteLine("Оцінка мiжгрупової дисперсiї за фактором 2= " + Math.Round(LAB05.GetIntergroupDispersion_2(), 4));
            Console.WriteLine("Оцінка залишкової дисперсiї " + Math.Round(LAB05.GetRemindDispersion(), 4));
            Console.WriteLine("Оцінка загальної дисперсiї = " + Math.Round(LAB05.GetGeneralDispersion(), 4));
            Console.WriteLine();
            Console.WriteLine("Оцінка емпiричного значення критерiю ФIШЕРА за фактором 1 = " + Math.Round(LAB05.GetFisherCrit_1(), 4));
            Console.WriteLine("Оцінка емпiричного значення критерiю ФIШЕРА за фактором 2 = " + Math.Round(LAB05.GetFisherCrit_2(), 4));

            Console.WriteLine();
            LAB05.Result();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            lab5Run();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            lab6Run();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            CorrRomanForm lab07Form = new CorrRomanForm();
            lab07Form.Show();
            
        }

        void lab07()
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            CorrFisherForm lab08Form = new CorrFisherForm();
            lab08Form.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ReggSqrForm lab09Form = new ReggSqrForm();
            lab09Form.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ReggRoccCofForm lab10Form = new ReggRoccCofForm();
            lab10Form.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Forms.DSRForm dSRForm = new Forms.DSRForm();
            dSRForm.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {

        }
    }
}
