using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using Wpf.Ui.Common;

namespace Acme.BookStore.WpfApp.Helpers
{
    [ValueConversion(typeof(SymbolRegular), typeof(string))]
    public class IconNameToIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return DependencyProperty.UnsetValue;

            return (SymbolRegular)Enum.Parse(typeof(SymbolRegular), value as string ?? string.Empty);
        }

        public object? ConvertBack(object? value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return DependencyProperty.UnsetValue;
            if (value is string strVal && string.IsNullOrEmpty(strVal)) return DependencyProperty.UnsetValue;

            return Enum.GetName(typeof(SymbolRegular), (SymbolRegular)value);
        }
    }
}
