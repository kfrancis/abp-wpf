using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Acme.BookStore.Wpf.Core.Converters
{
    [ValueConversion(typeof(WPFUI.Common.Icon), typeof(string))]
    internal class IconNameToIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return DependencyProperty.UnsetValue;

            return (WPFUI.Common.Icon)Enum.Parse(typeof(WPFUI.Common.Icon), value as string);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return DependencyProperty.UnsetValue;

            return Enum.GetName(typeof(WPFUI.Common.Icon), (WPFUI.Common.Icon)value);
        }
    }
}
