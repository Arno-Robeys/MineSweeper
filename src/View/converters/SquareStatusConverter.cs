﻿using Model.MineSweeper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace View.converters
{
    internal class SquareStatusConverter : IValueConverter
    {
        public object Uncovered { get; set; }
        public object Covered { get; set; }
        public object Flagged { get; set; }
        public object Mine { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is SquareStatus status))
            {
                return DependencyProperty.UnsetValue;
            }

            return status switch
            {
                SquareStatus.Uncovered => Uncovered,
                SquareStatus.Covered => Covered,
                SquareStatus.Flagged => Flagged,
                SquareStatus.Mine => Mine,
                _ => DependencyProperty.UnsetValue,
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class SquareStatusToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is SquareStatus status))
            {
                return Visibility.Collapsed;
            }

            return status == SquareStatus.Uncovered ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
