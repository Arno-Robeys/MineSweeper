using Model.Data;
using Model.MineSweeper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //Maakt nieuwe game
            var game = IGame.CreateRandom(10, 0.1);
            var game2 = IGame.Parse(new List<string> {".....",".**..",".....","...*.",".....",});            
            game2 = game2.ToggleFlag(new Vector2D(3, 1));
            game2 = game2.UncoverSquare(new Vector2D(0, 0));
            game2 = game2.UncoverSquare(new Vector2D(1, 1));

            GameViewModel viewModel = new GameViewModel(game2);
            DataContext = viewModel;
        }

    }
}
