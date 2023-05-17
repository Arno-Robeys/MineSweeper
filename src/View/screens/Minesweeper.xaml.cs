using System;
using System.Windows.Controls;
using System.Windows.Threading;

namespace View.screens
{
    /// <summary>
    /// Interaction logic for Minesweeper.xaml
    /// </summary>
    public partial class Minesweeper : UserControl
    {

        private readonly DispatcherTimer _timer = new DispatcherTimer();
        private readonly DateTime _gameStartTime = DateTime.Now;
        public Minesweeper()
        {
            InitializeComponent();

            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += Timer_Tick;

            _timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            TimeSpan elapsedTime = DateTime.Now - _gameStartTime;

            //Tijdelijk, moet aangepast worden naar Bindings
            timerLabel.Content = elapsedTime.ToString(@"hh\:mm\:ss");
        }
    }
}
