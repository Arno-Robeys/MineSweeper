﻿using Cells;
using Model.MineSweeper;
using System.Diagnostics;
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
                CurrentScreen.Value = new MineSweeperViewModel(screen, 5, true, 0.1);
            });

            StartMediumGame = new ActionCommand(() =>
            {
                CurrentScreen.Value = new MineSweeperViewModel(screen, 10, true, 0.15);
            });

            StartHardGame = new ActionCommand(() =>
            {
                CurrentScreen.Value = new MineSweeperViewModel(screen, 20, false, 0.20);
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

            StartGame = new ActionCommand(() => CurrentScreen.Value = new MineSweeperViewModel(screen, BoardSize, Flooding, MineProbability));
        }
        public ICommand Home { get; }

        public ICommand StartGame { get; }

        public int BoardSize { get; set; } = IGame.MinimumBoardSize;

        public bool Flooding { get; set; } = false;

        public double MineProbability { get; set; } = 0.1;

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
        public MineSweeperViewModel(ICell<ScreenViewModel> screen, int boardSize, bool flooding, double mineProbability) : base(screen)
        {
            timer.Interval = 1000;
            timer.Elapsed += Timer_Tick;
            timer.Start();

            GameMS = new GameViewModel(IGame.CreateRandom(boardSize, mineProbability, flooding));

            Home = new ActionCommand(() => CurrentScreen.Value = new HomeViewModel(screen));

            Settings = new ActionCommand(() => CurrentScreen.Value = new SettingsViewModel(screen));
        }
        
        public ICommand Home { get; }

        public ICommand Settings { get; }

        public GameViewModel GameMS { get; }
        public ICell<TimeSpan> Timer { get; } = Cell.Create(TimeSpan.Zero);

        private System.Timers.Timer timer = new();

        private void Timer_Tick(object? sender, EventArgs e)
        {
            if(GameMS.GameStatus.Value != GameStatus.InProgress)
            {
                timer.Stop();
            } else Timer.Value = Timer.Value.Add(TimeSpan.FromSeconds(1));
        }

    }

    public class ActionCommand : ICommand
    {
        private readonly Action action;

        public ActionCommand(Action action)
        {
            this.action = action;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter) => true;

        public void Execute(object? parameter) => this.action();

    }
}
