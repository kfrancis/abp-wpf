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
using Volo.Abp.DependencyInjection;
using Wpf.Ui.Common.Interfaces;

namespace Acme.BookStore.WpfApp.Views.Pages
{
    /// <summary>
    /// Interaction logic for BookIndexPage.xaml
    /// </summary>
    public partial class BookIndexPage : INavigableView<ViewModels.BookIndexViewModel>
    {
        public ViewModels.BookIndexViewModel ViewModel
        {
            get;
        }

        public BookIndexPage(ViewModels.BookIndexViewModel viewModel)
        {
            ViewModel = viewModel;

            InitializeComponent();
        }
    }
}
