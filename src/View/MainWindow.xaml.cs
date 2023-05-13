using Cells;
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
using View.screens;
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

            this.DataContext = new MainViewModel();
        }

    }

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
            StartGame = new ActionCommand(() => CurrentScreen.Value = new MineSweeperViewModel(screen));

            Settings = new ActionCommand(() => CurrentScreen.Value = new SettingsViewModel(screen));
        }
        public ICommand StartGame { get; }

        public ICommand Settings { get; }
    }

    public class SettingsViewModel : ScreenViewModel
    {
        public SettingsViewModel(ICell<ScreenViewModel> screen) : base(screen)
        {
            Home = new ActionCommand(() => CurrentScreen.Value = new HomeViewModel(screen));

            StartGame = new ActionCommand(() => CurrentScreen.Value = new MineSweeperViewModel(screen));
        }
        public ICommand Home { get; }

        public ICommand StartGame { get; }

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
        public MineSweeperViewModel(ICell<ScreenViewModel> screen) : base(screen)
        {
            Home = new ActionCommand(() => CurrentScreen.Value = new HomeViewModel(screen));

            Settings = new ActionCommand(() => CurrentScreen.Value = new SettingsViewModel(screen));
        }
        public ICommand Home { get; }

        public ICommand Settings { get; }

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
