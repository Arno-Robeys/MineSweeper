using Cells;
using Model.MineSweeper;
using System.Windows.Input;

namespace ViewModel
{
    public class MainViewModel
    {
        public MainViewModel()
        {
            CurrentScreen = Cell.Create<ScreenViewModel>(null);

            var firstscreen = new HomeViewModel(CurrentScreen);

            CurrentScreen.Value = firstscreen;

        }

        public ICell<ScreenViewModel> CurrentScreen { get; }
    }

    public abstract class ScreenViewModel
    {
        protected ICell<ScreenViewModel> CurrentScreen { get; }
        protected ScreenViewModel(ICell<ScreenViewModel> screen)
        {
            this.CurrentScreen = screen;
        }
    }

    public class HomeViewModel : ScreenViewModel
    {
        public HomeViewModel(ICell<ScreenViewModel> screen) : base(screen)
        {

            Settings = new ActionCommand(() => CurrentScreen.Value = new SettingsViewModel(screen));

            StartEasyGame = new ActionCommand(() =>
            {
                CurrentScreen.Value = new MineSweeperViewModel(screen, 5, true);
            });

            StartMediumGame = new ActionCommand(() =>
            {
                CurrentScreen.Value = new MineSweeperViewModel(screen, 10, true);
            });

            StartHardGame = new ActionCommand(() =>
            {
                CurrentScreen.Value = new MineSweeperViewModel(screen, 20, false);
            });
        }

        public ICommand Settings { get; }

        public ICommand StartEasyGame { get; }

        public ICommand StartMediumGame { get; }

        public ICommand StartHardGame { get; }
    }

    public class SettingsViewModel : ScreenViewModel
    {
        public SettingsViewModel(ICell<ScreenViewModel> screen) : base(screen)
        {
            Home = new ActionCommand(() => CurrentScreen.Value = new HomeViewModel(screen));

            StartGame = new ActionCommand(() => CurrentScreen.Value = new MineSweeperViewModel(screen, BoardSize, Flooding));
        }
        public ICommand Home { get; }

        public ICommand StartGame { get; }

        public static int BoardSize { get; set; } = IGame.MinimumBoardSize;

        public bool Flooding { get; set; } = false;

        public static int MinimumSize
        {
            get
            {
                return IGame.MinimumBoardSize;
            }
        }

        public static int MaximumSize
        {
            get
            {
                return IGame.MaximumBoardSize;
            }
        }

    }

    public class MineSweeperViewModel : ScreenViewModel
    {
        public MineSweeperViewModel(ICell<ScreenViewModel> screen, int boardSize, bool flooding) : base(screen)
        {

            GameMS = new GameViewModel(IGame.CreateRandom(boardSize, 0.1, flooding));

            Home = new ActionCommand(() => CurrentScreen.Value = new HomeViewModel(screen));

            Settings = new ActionCommand(() => CurrentScreen.Value = new SettingsViewModel(screen));
        }
        public ICommand Home { get; }

        public ICommand Settings { get; }

        public GameViewModel GameMS { get; }

    }

    public class ActionCommand : ICommand
    {
        private readonly Action action;

        public ActionCommand(Action action)
        {
            this.action = action;
        }

        public event EventHandler CanExecuteChanged { add { } remove { } }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            this.action();
        }
    }
}
