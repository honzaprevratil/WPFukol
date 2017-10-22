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
using System.Windows.Threading;

namespace WPFukol
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int RightButton = 2;
        public int Level = 0;
        public int PointsPerClick = 50;
        public int TimeProm = 60;
        public DispatcherTimer timer = new DispatcherTimer();
        int[] LevelsNumber = { 10, 20, 50 };

        public MainWindow()
        {
            InitializeComponent();
            AppendQuestion();

            timer.Interval = new TimeSpan(0,0,1);
            timer.Tick += (sender, args) => { TickDown(); };
            timer.Start();
        }
        private void TickDown(int setTime = 0)
        {
            if (setTime == 0)
            {
                TimeProm = TimeProm - 10;
                Timebar.Value = TimeProm;
                if (TimeProm < 0)
                {
                    GameOver("YOU LOST!!");
                }
            } else
            {
                TimeProm = setTime;
                Timebar.Value = TimeProm;
            }
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Click(1);
        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Click(2);
        }
        private void Close_Game(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Start_New_Game(object sender, RoutedEventArgs e)
        {
            Level = 0;
            TimeProm = 60;
            timer.Start();
            Progress.Value = 0;
            But1.Click -= Start_New_Game;
            But1.Click += Button_Click_1;
            But2.Click -= Close_Game;
            But2.Click += Button_Click_2;
            AppendQuestion();
        }
        private void GameOver(string title)
        {
            question.Content = title;
            But1.Content = "NEW GAME";
            But1.Click -= Button_Click_1;
            But1.Click += Start_New_Game;
            But2.Content = "QUIT";
            But2.Click -= Button_Click_2;
            But2.Click += Close_Game;
            timer.Stop();
        }
        private void Click(int clickedBut)
        {
            if (clickedBut == RightButton)
            {
                if (Progress.Value < (100 - PointsPerClick))
                {
                    Progress.Value += PointsPerClick;
                    TickDown(60);
                } else
                {
                    Progress.Value = 0;
                    TimeProm = TimeProm - 20;
                    if (Level < (LevelsNumber.Length - 1))
                    {
                        Level++;
                    } else
                    {
                        question.Content = "YOU WON!!";
                    }
                }
            } else
            {
                if (Progress.Value >= PointsPerClick)
                {
                    Progress.Value -= PointsPerClick;
                }
            }
            if ((string)question.Content == "YOU WON!!")
            {
                GameOver("YOU WON!!");
            } else
            {
                AppendQuestion();
            }
        }
        private void AppendQuestion()
        {
            Random rand = new Random();
            MathLogic Math = new MathLogic(); 
            int[] numbers = Math.NumbersByLevel(Level, LevelsNumber);
            question.Content = "How many is " + numbers[0] + " + " + numbers[1];
            LevelDisplay.Content = "("+(Level+1)+"/"+(LevelsNumber.Length)+")";
            if (rand.Next(0,2) == 1)
            {
                RightButton = 2;
                But2.Content = numbers[0] + numbers[1];
                But1.Content = rand.Next(0, 2) == 1 ? numbers[0] + numbers[1] + rand.Next(1,10) : numbers[0] + numbers[1] - rand.Next(1, 10);
            }
            else
            {
                RightButton = 1;
                But1.Content = numbers[0] + numbers[1];
                But2.Content = rand.Next(0, 2) == 1 ? numbers[0] + numbers[1] + rand.Next(1,10) : numbers[0] + numbers[1] - rand.Next(1, 10);
            }
        }
    }
}
