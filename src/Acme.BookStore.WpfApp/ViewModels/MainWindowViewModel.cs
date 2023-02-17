using System;
using System.Collections.ObjectModel;
using Acme.BookStore.Localization;
using Acme.BookStore.WpfApp.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Wpf.Ui.Common;
using Wpf.Ui.Controls;
using Wpf.Ui.Controls.Interfaces;
using Wpf.Ui.Mvvm.Contracts;

namespace Acme.BookStore.WpfApp.ViewModels
{
    public partial class MainWindowViewModel : AppViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly IDispatcher _dispatcher;
        private readonly ILoggerFactory _loggerFactory;
        private readonly IStringLocalizer<BookStoreResource> _localizer;
        private bool _isInitialized = false;

        [ObservableProperty]
        private ObservableCollection<INavigationControl> _navigationItems = new();

        [ObservableProperty]
        private ObservableCollection<INavigationControl> _navigationFooter = new();

        [ObservableProperty]
        private ObservableCollection<MenuItem> _trayMenuItems = new();

        public MainWindowViewModel()
        { }

        public MainWindowViewModel(INavigationService navigationService,
                               IDispatcher dispatcher,
                               ILoggerFactory loggerFactory,
                               IStringLocalizer<BookStoreResource> localizer)
        {
            if (!_isInitialized)
                InitializeViewModel();

            _navigationService = navigationService;
            _dispatcher = dispatcher;
            _loggerFactory = loggerFactory;
            _localizer = localizer;
        }

        private void InitializeViewModel()
        {
            Title = _localizer["Main"];
            Subtitle = _localizer["Main_Description"];
            Icon = Enum.GetName(SymbolRegular.Book20);

            NavigationItems = new ObservableCollection<INavigationControl>
            {
                new NavigationItem()
                {
                    Content = "Home",
                    PageTag = "dashboard",
                    Icon = SymbolRegular.Home24,
                    PageType = typeof(Views.Pages.DashboardPage)
                },
                new NavigationItem()
                {
                    Content = "Data",
                    PageTag = "data",
                    Icon = SymbolRegular.DataHistogram24,
                    PageType = typeof(Views.Pages.DataPage)
                },
                new NavigationItem()
                {
                    Content = "Books",
                    PageTag = "books",
                    Icon = SymbolRegular.Book24,
                    PageType = typeof(Views.Pages.BookIndexPage)
                }
            };

            NavigationFooter = new ObservableCollection<INavigationControl>
            {
                new NavigationItem()
                {
                    Content = "Settings",
                    PageTag = "settings",
                    Icon = SymbolRegular.Settings24,
                    PageType = typeof(Views.Pages.SettingsPage)
                }
            };

            TrayMenuItems = new ObservableCollection<MenuItem>
            {
                new MenuItem
                {
                    Header = "Home",
                    Tag = "tray_home"
                }
            };

            _isInitialized = true;
        }
    }
}
