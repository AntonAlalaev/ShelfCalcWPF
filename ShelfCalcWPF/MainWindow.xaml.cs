using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ShelfCalcWPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Distance_TextInput(object sender, TextCompositionEventArgs e)
        {
           // nothing happen
        }

        private void Distance_TextChanged(object sender, TextChangedEventArgs e)
        {
            //Пересчет
            Recalculate();
        }


        private double RightRound(double Value, int Accuracy)
        {
            return Math.Round((Value * Math.Pow(10, Accuracy) + 0.499999), 0) / (Math.Pow(10, Accuracy));
        }
 
        private int normalShelfDistance(int Distance)
        {

            int retValue = 20;
            while (retValue < Distance)
            {
                retValue = retValue + 25;
            }
            return retValue ;
        }

        private int normalStatH(int Distance)
        {
            int RetValue = 38;
            if (Distance != 38)
            {
                if (Distance < 38)
                {
                    RetValue = 38;
                }
                else
                {
                    RetValue = normalShelfDistance(Distance + 1);
                }
            }
            return RetValue;
        }


        private int normalShelfBottom(int Distance)
        {
            int RetValue = 38;
            if (Distance != 38)
            {
                if (Distance < 38)
                {
                    RetValue = 38;
                }
                else
                {
                    if (Distance <= 63)
                    {
                        RetValue = 63;
                    }
                    else
                    {
                        RetValue = normalShelfDistance(Distance-18) + 18;
                    }
                    
                }
            }
            return RetValue;
        }

        private int StoikaCalc(int distance, int Amount, int Visota)
        {
            int Step = 0;
            int Height = 0;
            int totalstep = 0;
            for (int i = 1; i <= Amount+1; i++)
            {
                if (i == 1)
                {
                    //Первая полка
                    Height = Visota;
                    totalstep = (Height - 13) / 25;
                }
                else
                {
                    Step = 1;
                    while (Step * 25 - 14 < distance) { Step++; }

                    totalstep = totalstep + Step;
                    Height = (totalstep * 25) - 3 + 16;
                }
            }
            return Height-6;
        }

        private void Recalculate()
        {
            try
            {
                int ShelfDist = Convert.ToInt16(Distance.Text);
                int ShelfAmount = Convert.ToInt16(Amount.Text);
                int ShelfBottomDistance = Convert.ToInt16(Height.Text);
                ShelfDist = normalShelfDistance(ShelfDist);
                //int StatBottomDistance = Convert.ToInt16(statHeight.Text);

                // расчетное расстояние между полками
                CalculatedShelfDist.Text = ShelfDist.ToString();
                // нормализуем ShelfbottomDistance
                ShelfBottomDistance = normalShelfBottom(ShelfBottomDistance);

                int StHeight = StoikaCalc(ShelfDist, ShelfAmount, ShelfBottomDistance);
                StayHeight.Text = StHeight.ToString();
                wFloor.Text = Convert.ToString(StHeight + 113);
                FalseFloor.Text = Convert.ToString(StHeight + 133);
                Welded.Text = Convert.ToString(StHeight + 183);
                WorkShelf.Text = Convert.ToString(ShelfDist);
                BotomCal.Text = Convert.ToString(ShelfBottomDistance);
            }
            catch(Exception ex)
            {   }
        }

        private void Amount_TextChanged(object sender, TextChangedEventArgs e)
        {
            Recalculate();
        }

        private void Height_TextChanged(object sender, TextChangedEventArgs e)
        {
            Recalculate();
            CheckHeight();
        }
        private void CheckHeight()
        {
            try
            {
                if (Convert.ToInt16(Height.Text) < 38)
                {
                    Height.Background = new SolidColorBrush(Color.FromArgb(255, 222, 19, 19)); 
                }
                else
                {
                    Height.Background = new SolidColorBrush(Color.FromArgb(255, 195, 194, 194));
                }
            }
            catch (Exception ex)
            { }
        }

   

        private void statHeight_TextChanged(object sender, TextChangedEventArgs e)
        {
            Recalculate();

        }
    }
}
