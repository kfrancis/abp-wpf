using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using Wpf.Ui.Common;

namespace Acme.BookStore.Wpf.Core.Converters
{
    [ValueConversion(typeof(SymbolRegular), typeof(string))]
    internal class IconNameToIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return DependencyProperty.UnsetValue;

            return (SymbolRegular)Enum.Parse(typeof(SymbolRegular), value as string);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return DependencyProperty.UnsetValue;

            return Enum.GetName(typeof(SymbolRegular), (SymbolRegular)value);
        }
    }
}
