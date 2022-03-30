using System;
using System.Collections.Generic;
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
using Acme.BookStore.Wpf.ViewModels;
using Volo.Abp.DependencyInjection;

namespace Acme.BookStore.Wpf.Views
{
    /// <summary>
    /// Interaction logic for BookIndex.xaml
    /// </summary>
    public partial class BookIndex : Page, ISingletonDependency
    {
        private readonly BookIndexViewModel _viewModel;

        public BookIndex()
        {
            WPFUI.Appearance.Theme.Set(
  WPFUI.Appearance.ThemeType.Dark,     // Theme type
  WPFUI.Appearance.BackgroundType.Mica, // Background type
  true                                  // Whether to change accents automatically
);
            InitializeComponent();
        }

        public BookIndex(BookIndexViewModel viewModel)
        {
            WPFUI.Appearance.Theme.Set(
  WPFUI.Appearance.ThemeType.Dark,     // Theme type
  WPFUI.Appearance.BackgroundType.Mica, // Background type
  true                                  // Whether to change accents automatically
);
            DataContext = viewModel;
            InitializeComponent();
            _viewModel = viewModel;
        }
    }
}
