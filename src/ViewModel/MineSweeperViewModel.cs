﻿using Cells;
using Model.Data;
using Model.MineSweeper;
using System.Diagnostics;
using System.Windows.Input;

namespace ViewModel
{
    public class GameViewModel
    {

        private readonly ICell<IGame> game;

        public GameBoardViewModel Board { get; }

        public GameViewModel(IGame game)
        {
            this.game = Cell.Create(game);
            Board = new GameBoardViewModel(this.game);
        }

    }

    public class GameBoardViewModel
    {
        private readonly ICell<IGameBoard> board;

        public IEnumerable<RowViewModel> Rows { get;}

        public GameBoardViewModel(ICell<IGame> game)
        {
            this.board = game.Derive(g => g.Board);
            Rows = GetRows(this.board, game);
        }

        private static IEnumerable<RowViewModel> GetRows(ICell<IGameBoard> board, ICell<IGame> game)
        {
            //Maakt een lijst van rijen van het bord en gaat voor elke rij het juiste square object van bord komen opvragen
            //De .Select geeft een IEnumerable van Square terug
            return Enumerable.Range(0, board.Value.Height).Select(y => 
                new RowViewModel(Enumerable.Range(0, board.Value.Width).Select(x =>
                {
                    Vector2D pos = new(x, y);
                    return new SquareViewModel(game, pos);
                })));
        }
    }

    public class RowViewModel
    {
        public IEnumerable<SquareViewModel> Squares { get;}

        public RowViewModel(IEnumerable<SquareViewModel> squares) => Squares = squares;
    }

    public class SquareViewModel
    {
        public ICell<Square> Square { get;}

        public ICell<SquareStatus> Status => Square.Derive(s => s.Status);

        public ICell<int> NeighboringMineCount => Square.Derive(s => s.NeighboringMineCount);

        public ICell<GameStatus> GameStatus;

        public ICommand Uncover { get; }

        public ICommand ToggleFlag { get; }

        public SquareViewModel(ICell<IGame> game, Vector2D pos)
        {
            Square = game.Derive(g => g.Board[pos]);
            Uncover = new UncoverSquareCommand(game, pos);
            ToggleFlag = new UncoverSquareCommand(game, pos, true);
            GameStatus = game.Derive(g => g.Status);
        }

    }

    public class UncoverSquareCommand : ICommand
    {
        private readonly bool toggleFlag;
        public ICell<IGame> Game { get; }
        public Vector2D Position { get; }
        public UncoverSquareCommand(ICell<IGame> game, Vector2D pos, bool toggleFlag = false)
        {
            Game = game;
            Position = pos;
            this.toggleFlag = toggleFlag;
        }
        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return Game.Value.Status == GameStatus.InProgress && Game.Value.IsSquareCovered(Position) && (!Game.Value.Flags.Contains(Position) || toggleFlag);
        }

        public void Execute(object? parameter)
        {
            Debug.WriteLine($"Uncovering square at {Position}");
            Game.Value = toggleFlag ? Game.Value.ToggleFlag(Position) : Game.Value.UncoverSquare(Position);
            Debug.WriteLine($"Game status is now {Game.Value.Status}");
        }
    }
}