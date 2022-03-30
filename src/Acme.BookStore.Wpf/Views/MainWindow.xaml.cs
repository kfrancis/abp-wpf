using System.Windows;
using Acme.BookStore.Wpf.Core.Threading;
using Acme.BookStore.Wpf.ViewModels;
using WPFUI.Controls;

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

        WPFUI.Appearance.Theme.Set(
          WPFUI.Appearance.ThemeType.Dark,     // Theme type
          WPFUI.Appearance.BackgroundType.Mica, // Background type
          true                                  // Whether to change accents automatically
        );

        DataContext = viewModel;
        InitializeComponent();

        Loaded += MainWindow_Loaded;
        RootNavigation.Navigated += RootNavigation_Navigated;
        RootTitleBar.CloseActionOverride = CloseActionOverride;

        WPFUI.Appearance.Theme.Changed += Theme_Changed;

        viewModel.Dispatchers.RunOnUIThread(() =>
        {
            RootGrid.Visibility = Visibility.Visible;
        });
    }

    private void RootNavigation_Navigated(WPFUI.Controls.Interfaces.INavigation navigation, WPFUI.Controls.Interfaces.INavigationItem current)
    {
        if (navigation.Current.Type == typeof(BookIndex))
        {
            navigation.Current.Instance.DataContext = _bookIndexViewModel;
        }
    }

    private void Theme_Changed(WPFUI.Appearance.ThemeType currentTheme, System.Windows.Media.Color systemAccent)
    {
        System.Diagnostics.Debug.WriteLine($"DEBUG | {typeof(MainWindow)} was informed that the theme has been changed to {currentTheme}");
    }

    private void MainWindow_Loaded(object sender, RoutedEventArgs e)
    {
        WPFUI.Appearance.Watcher.Watch(this);
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
