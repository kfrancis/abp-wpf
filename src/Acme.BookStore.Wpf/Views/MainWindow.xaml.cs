using System.Windows;
using Acme.BookStore.Wpf.ViewModels;
using MahApps.Metro.Controls;

namespace Acme.BookStore.Wpf.Views;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : MetroWindow, IAppView
{
    public MainWindow(MainWindowViewModel viewModel)
    {
        DataContext = viewModel;
        InitializeComponent();
    }
}
