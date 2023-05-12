using Model.MineSweeper;
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
using ViewModel;

namespace View.screens
{
    /// <summary>
    /// Interaction logic for Minesweeper.xaml
    /// </summary>
    public partial class Minesweeper : UserControl
    {
        public Minesweeper()
        {
            InitializeComponent();
            //var game = IGame.CreateRandom(10, 0.1);
            //var game2 = IGame.Parse(new List<string> { ".....", ".**..", ".....", "...*.", ".....", });

            //GameViewModel viewModel = new(game2);
           
            this.DataContext = SettingsSetViewModel.CreateGame();
        }
    }
}
