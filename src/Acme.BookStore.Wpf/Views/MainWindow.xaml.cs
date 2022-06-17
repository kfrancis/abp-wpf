using System.Windows;
using Acme.BookStore.Wpf.Core.Threading;
using Acme.BookStore.Wpf.ViewModels;
using Wpf.Ui.Appearance;
using Wpf.Ui.Controls;
using Wpf.Ui.Controls.Interfaces;

namespace Acme.BookStore.Wpf.Views;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window, IAppView
{
    private readonly BookIndexViewModel _bookIndexViewModel;

    public MainWindow(MainWindowViewModel viewModel, BookIndexViewModel bookIndexViewModel)
    {
        _bookIndexViewModel = bookIndexViewModel;

        Theme.Apply(
          ThemeType.Dark,     // Theme type
          BackgroundType.Mica, // Background type
          true                                  // Whether to change accents automatically
        );

        DataContext = viewModel;
        InitializeComponent();

        Loaded += MainWindow_Loaded;
        RootNavigation.Navigated += RootNavigation_Navigated;
        RootTitleBar.CloseActionOverride = CloseActionOverride;

        Theme.Changed += Theme_Changed;

        viewModel.Dispatchers.RunOnUIThread(() =>
        {
            RootGrid.Visibility = Visibility.Visible;
        });
    }

    private void RootNavigation_Navigated(INavigation navigation, INavigationItem current)
    {
        if (navigation.Current.PageType == typeof(BookIndex))
        {
            //navigation.Current...DataContext = _bookIndexViewModel;
        }
    }

    private void Theme_Changed(ThemeType currentTheme, System.Windows.Media.Color systemAccent)
    {
        System.Diagnostics.Debug.WriteLine($"DEBUG | {typeof(MainWindow)} was informed that the theme has been changed to {currentTheme}");
    }

    private void MainWindow_Loaded(object sender, RoutedEventArgs e)
    {
        Watcher.Watch(this);
    }

    private void CloseActionOverride(TitleBar titleBar, Window window)
    {
        Application.Current.Shutdown();
    }

    private void RootSnackbar_Closed(Snackbar snackbar)
    {

    }

    private void RootSnackbar_Opened(Snackbar snackbar)
    {

    }
}
