using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace Acme.BookStore.WpfApp.Helpers
{
    public class IndexOfConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (values[0] is not ItemsControl itemsControl)
            {
                return Binding.DoNothing;
            }
            var item = values[1];
            var itemContainer = itemsControl.ItemContainerGenerator.ContainerFromItem(item);

            // It may not yet be in the collection...
            if (itemContainer == null)
            {
                return Binding.DoNothing;
            }

            var itemIndex = itemsControl.ItemContainerGenerator.IndexFromContainer(itemContainer);
            return itemIndex;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            return targetTypes.Select(t => Binding.DoNothing).ToArray();
        }
    }
}
