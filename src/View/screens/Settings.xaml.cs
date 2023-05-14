using Model.MineSweeper;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using ViewModel;

namespace View.screens
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : UserControl
    {
        public Settings()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty BoardSizeProperty = DependencyProperty.Register("BoardSize", typeof(int), typeof(Settings));

        public static int BoardSize
        {
            get { return SettingsViewModel.BoardSize; }
            set { SettingsViewModel.BoardSize = (int)value;}
        }
    }

    
}
