using Model.Data;
using Model.MineSweeper;
using System.Xml.Linq;

namespace ViewModel
{
    public class GameViewModel
    {

        private readonly IGame game;

        public GameBoardViewModel Board { get; }

        public GameViewModel(IGame game)
        {
            this.game = game;
            Board = new GameBoardViewModel(game.Board);
        }

    }

    public class GameBoardViewModel
    {
        private readonly IGameBoard board;
        public GameBoardViewModel(IGameBoard board)
        {
            this.board = board;
        }
        public IEnumerable<IEnumerable<Square>> Rows => GetRows(board);
        private IEnumerable<Square> Row(IGameBoard board, int row)
        {
            //Maakt een rij van getallen van 0 tot breedte van het bord en gaat voor elk getal het juiste square object van bord komen opvragen
            //De .Select geeft een IEnumerable van Square terug
            return Enumerable.Range(0, board.Width).Select(i => board[new Vector2D(row, i)]);
        }
        private IEnumerable<IEnumerable<Square>> GetRows(IGameBoard board)
        {
            //Exact hetzelfde als erboven alleen gebruikt het de method Row om de row te krijgen en zo een lijst te kunnen maken van rows
            return Enumerable.Range(0, board.Height).Select(i => Row(board, i));
        }
    }
}