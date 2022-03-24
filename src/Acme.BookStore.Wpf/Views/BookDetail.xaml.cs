using System.Windows.Controls;
using Acme.BookStore.Wpf.ViewModels;

namespace Acme.BookStore.Wpf.Views
{
    /// <summary>
    /// Interaction logic for BookDetail.xaml
    /// </summary>
    public partial class BookDetail : UserControl, IAppView
    {
        public BookDetail(BookDetailViewModel viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();
        }
    }
}
