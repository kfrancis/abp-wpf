using System;
using System.Windows;
using Acme.BookStore.Wpf.ViewModels;
using MahApps.Metro.Controls;
using Volo.Abp.DependencyInjection;

namespace Acme.BookStore.Wpf;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : MetroWindow, ISingletonDependency
{
    public MainWindow(MainWindowViewModel viewModel)
    {
        DataContext = viewModel;
        InitializeComponent();
    }
}
