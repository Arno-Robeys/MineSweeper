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

        private static RowViewModel Row(IGameBoard board, int row, IGame game)
        {
        
            return new RowViewModel(Enumerable.Range(0, board.Width).Select(i => {
                Vector2D pos = new(i, row);
                return new SquareViewModel(board[pos], game, pos);
            }));
        }
        private static IEnumerable<RowViewModel> GetRows(IGameBoard board, IGame game)
        {
            //Maakt een lijst van rijen van het bord en gaat voor elke rij het juiste square object van bord komen opvragen
            //De .Select geeft een IEnumerable van Square terug
            return Enumerable.Range(0, board.Height).Select(y => 
                new RowViewModel(Enumerable.Range(0, board.Width).Select(x =>
                {
                    Vector2D pos = new(x, y);
                    return new SquareViewModel(board[pos], game, pos);
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
        public Square Square { get;}

        public SquareStatus Status => Square.Status;

        public int NeighboringMineCount => Square.NeighboringMineCount;

        public ICommand Uncover { get; }

        public SquareViewModel(Square square, IGame game, Vector2D pos)
        {
            Square = square;
            Uncover = new UncoverSquareCommand(game, pos);
        }

    }

    public class UncoverSquareCommand : ICommand
    {
        public IGame Game { get; }

        public Vector2D Position { get; }
        public UncoverSquareCommand(IGame game, Vector2D pos)
        {
            Game = game;
            Position = pos;
        }
        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            Debug.WriteLine("Clickie op XY: " + Position.X + " - " + Position.Y);
        }
    }


}