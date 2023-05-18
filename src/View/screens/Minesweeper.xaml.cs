using Model.MineSweeper;
using System;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Threading;
using ViewModel;

namespace View.screens
{
    /// <summary>
    /// Interaction logic for Minesweeper.xaml
    /// </summary>
    public partial class Minesweeper : UserControl
    {

        private readonly DispatcherTimer Timer = new DispatcherTimer();
        private readonly DateTime GameStartTime = DateTime.Now;

        public Minesweeper()
        {
            InitializeComponent();

            Timer.Interval = TimeSpan.FromSeconds(1);
            Timer.Tick += Timer_Tick;

            Timer.Start();

        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            TimeSpan elapsedTime = DateTime.Now - GameStartTime;

            StopTimer();

            timerLabel.Content = elapsedTime.ToString(@"hh\:mm\:ss");
        }

        private void StopTimer()
        {
            MineSweeperViewModel mineSweeperViewModel = (MineSweeperViewModel)DataContext;

            GameStatus gameStatus = mineSweeperViewModel.GameMS.GameStatus.Value;

            if (gameStatus != GameStatus.InProgress)
            {
                Timer.Stop();
            }
        }
    }
}
