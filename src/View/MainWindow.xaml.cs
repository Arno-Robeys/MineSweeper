﻿using Model.Data;
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
            var game2 = IGame.Parse(new List<string> {".....",".*...",".....","...*.","**...",});
            //game2 = game2.UncoverSquare(new Vector2D(1, 1));
            //game2 = game2.UncoverSquare(new Vector2D(2, 2));
            //game2 = game2.ToggleFlag(new Vector2D(4, 4));


            this.boardView.ItemsSource = Rows(game2.Board);
        }

        public IEnumerable<Square> Row(IGameBoard board, int row)
        {
            //Maakt een rij van getallen van 0 tot breedte van het bord en gaat voor elk getal het juiste square object van bord komen opvragen
            //De .Select geeft een IEnumerable van Square terug
            return Enumerable.Range(0, board.Width).Select(i => board[new Vector2D(row, i)]);
        }

        public IEnumerable<IEnumerable<Square>> Rows(IGameBoard board)
        {
            //Exact hetzelfde als erboven alleen gebruikt het de method Row om de row te krijgen en zo een lijst te kunnen maken van rows
            return Enumerable.Range(0, board.Height).Select(i => Row(board, i));
        }
    }
}