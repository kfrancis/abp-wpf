using System.Windows.Controls;
using Acme.BookStore.Wpf.ViewModels;
using Volo.Abp.DependencyInjection;
using Wpf.Ui.Appearance;
using Wpf.Ui.Controls;

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
            Theme.Apply(
                ThemeType.Dark,     // Theme type
                BackgroundType.Mica, // Background type
                true                                  // Whether to change accents automatically
            );
            InitializeComponent();
        }

        public BookIndex(BookIndexViewModel viewModel)
        {
            Theme.Apply(
              ThemeType.Dark,     // Theme type
              BackgroundType.Mica, // Background type
              true                                  // Whether to change accents automatically
            );
            DataContext = viewModel;
            InitializeComponent();
            _viewModel = viewModel;
        }
    }
}
