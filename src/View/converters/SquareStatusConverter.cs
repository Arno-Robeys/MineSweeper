using Model.MineSweeper;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace View.converters
{
    internal class SquareStatusConverter : IValueConverter
    {
        public object Flagged { get; set; }
        public object Mine { get; set; }
        public object Covered { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not SquareStatus status)
            {
                return DependencyProperty.UnsetValue;
            }

            return status switch
            {
                SquareStatus.Flagged => Flagged,
                SquareStatus.Covered => Covered,
                SquareStatus.Mine => Mine,
                _ => DependencyProperty.UnsetValue,
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

public class SquareStatusToVisibilityConverter : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values.Length < 2 || values[0] is not SquareStatus status || values[1] is not int mineCount)
        {
            return Visibility.Collapsed;
        }

        return status == SquareStatus.Uncovered && mineCount > 0 ? Visibility.Visible : Visibility.Collapsed;
        }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}



    public class MineCountColorConverter : IValueConverter
    {
        public object One { get; set; }
        public object Two { get; set; }
        public object Three { get; set; }
        public object Four { get; set; }
        public object Five { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value switch
            {
                1 => One,
                2 => Two,
                3 => Three,
                4 => Four,
                5 => Five,
                _ => DependencyProperty.UnsetValue,
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class GameEndConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(parameter is string param && value is GameStatus status)
            {
                if(param == "Visibility")
                {
                    if (status == GameStatus.Lost || status == GameStatus.Won)
                    {
                        return Visibility.Visible;
                    }
                    return Visibility.Collapsed;

                } else if(param == "Text")
                {
                    return status switch
                    {
                        GameStatus.Lost => "Sad, You lost! Try again?",
                        GameStatus.Won => "Congrats, You won!",
                        _ => DependencyProperty.UnsetValue,
                    };
                }
            }

            return DependencyProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }


}
