using Model.Data;
using Model.MineSweeper;
using System.Diagnostics;
using System.Windows.Input;

namespace ViewModel
{
    public class GameViewModel
    {

        private readonly IGame game;

        public GameBoardViewModel Board { get; }

        public GameViewModel(IGame game)
        {
            this.game = game;
            Board = new GameBoardViewModel(game);
        }

    }

    public class GameBoardViewModel
    {
        private readonly IGameBoard board;

        public IEnumerable<RowViewModel> Rows { get;}

        public GameBoardViewModel(IGame game)
        {
            this.board = game.Board;
            Rows = GetRows(this.board, game);
        }

        private RowViewModel Row(IGameBoard board, int row, IGame game)
        {
            //Maakt een rij van getallen van 0 tot breedte van het bord en gaat voor elk getal het juiste square object van bord komen opvragen
            //De .Select geeft een IEnumerable van Square terug
            return new RowViewModel(Enumerable.Range(0, board.Width).Select(i => new SquareViewModel(board[new Vector2D(i, row)], game)));
        }
        private IEnumerable<RowViewModel> GetRows(IGameBoard board, IGame game)
        {
            //Exact hetzelfde als erboven alleen gebruikt het de method Row om de row te krijgen en zo een lijst te kunnen maken van rows
            return Enumerable.Range(0, board.Height).Select(i => Row(board, i, game));
        }
    }

    public class RowViewModel
    {
        public IEnumerable<SquareViewModel> Squares { get;}

        public RowViewModel(IEnumerable<SquareViewModel> squares) => Squares = squares;
    }

    public class SquareViewModel
    {
        public Square Square { get;}

        public SquareStatus Status => Square.Status;

        public int NeighboringMineCount => Square.NeighboringMineCount;

        public ICommand Uncover { get; }

        public SquareViewModel(Square square, IGame game)
        {
            Square = square;
            Uncover = new UncoverSquareCommand(game);
        }

    }

    public class UncoverSquareCommand : ICommand
    {
        public IGame Game { get; }
        public UncoverSquareCommand(IGame game)
        {
            Game = game;
        }
        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            Debug.WriteLine("Clickie");
        }
    }


}