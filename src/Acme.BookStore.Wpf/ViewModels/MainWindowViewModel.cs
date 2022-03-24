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
using WPFUI.Controls;

namespace Acme.BookStore.Wpf.ViewModels
{
    public partial class MainWindowViewModel : AppViewModel
    {
        private readonly IBooksAppService _booksAppService;
        private readonly ILoggerFactory _loggerFactory;
        private readonly IStringLocalizer<BookStoreResource> _localizer;
        private readonly ISnackbarService _snackbarService;

        public List<IDispatcher> Dispatchers { get; }



        public MainWindowViewModel(IDispatcher dispatcher,
                                   ILoggerFactory loggerFactory,
                                   IStringLocalizer<BookStoreResource> localizer,
                                   ISnackbarService snackbarService)
            : base(loggerFactory.CreateLogger<AppViewModel>(), localizer)
        {
            _loggerFactory = loggerFactory;
            _localizer = localizer;
            _snackbarService = snackbarService;

            Dispatchers = new List<IDispatcher>() { dispatcher };

            OpenSnackbar = new AsyncCommand<Snackbar>(OpenSnackbarAsync, o => IsNotBusy);
            CloseSnackbar = new AsyncCommand<Snackbar>(CloseSnackbarAsync, o => IsNotBusy);

            Title = localizer["Main"];
        }

        public MainWindowViewModel()
            : base()
        { } // for design-time

        //public ISnackbarMessageQueue TheSnackbarMessageQueue => _snackbarService.TheSnackbarMessageQueue;

        public IAsyncCommand<Snackbar> OpenSnackbar { get; private set; }
        public IAsyncCommand<Snackbar> CloseSnackbar { get; private set; }

        public bool GetIsNotBusy() => IsNotBusy;


        public async Task OpenSnackbarAsync(Snackbar sender)
        {

        }


        public async Task CloseSnackbarAsync(Snackbar sender)
        {

        }
    }
}
