using System.Windows;
using System.Windows.Controls;

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
        public static readonly DependencyProperty MineProbabilityProperty = DependencyProperty.Register("MineProbability", typeof(int), typeof(Settings));

    }

    
}
