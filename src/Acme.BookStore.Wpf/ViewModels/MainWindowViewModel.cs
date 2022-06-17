using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Acme.BookStore.Books;
using Acme.BookStore.Localization;
using Acme.BookStore.Wpf.Core.Threading;
using Acme.BookStore.Wpf.Services;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using MvvmHelpers.Commands;
using MvvmHelpers.Interfaces;
using Wpf.Ui.Common;
using Wpf.Ui.Controls;

namespace Acme.BookStore.Wpf.ViewModels
{
    public partial class MainWindowViewModel : AppViewModel
    {
        private readonly ILogger<MainWindowViewModel> _log;
        private readonly IStringLocalizer<BookStoreResource> _localizer;
        private readonly ISnackbarService _snackbarService;

        public MainWindowViewModel(IDispatcher dispatcher,
                                   ILoggerFactory loggerFactory,
                                   IStringLocalizer<BookStoreResource> localizer,
                                   ISnackbarService snackbarService)
            : base(loggerFactory.CreateLogger<AppViewModel>(), localizer, dispatcher)
        {
            _log = loggerFactory.CreateLogger<MainWindowViewModel>();
            _localizer = localizer;
            _snackbarService = snackbarService;

            OpenSnackbar = new AsyncCommand<Snackbar>(OpenSnackbarAsync, o => IsNotBusy);
            CloseSnackbar = new AsyncCommand<Snackbar>(CloseSnackbarAsync, o => IsNotBusy);

            Title = localizer["Main"];
            Subtitle = localizer["Main_Description"];
            Icon = Enum.GetName(SymbolRegular.Book20);
        }

        // for design-time
        public MainWindowViewModel()
            : base()
        {
            Icon = Enum.GetName(SymbolRegular.Book20);
            Title = "Title";
            Subtitle = "Subtitle";
        }

        public IAsyncCommand<Snackbar> OpenSnackbar { get; private set; }
        public IAsyncCommand<Snackbar> CloseSnackbar { get; private set; }

        public IAsyncCommand Navigated { get; private set; }

        public bool GetIsNotBusy() => IsNotBusy;

        public async Task OpenSnackbarAsync(Snackbar sender)
        {
            _log.LogInformation("Snackbar opened");
            await Task.CompletedTask;
        }

        public async Task CloseSnackbarAsync(Snackbar sender)
        {
            _log.LogInformation("Snackbar closed");
            await Task.CompletedTask;
        }
    }
}
