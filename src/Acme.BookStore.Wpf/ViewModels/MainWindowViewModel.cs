using System.Collections.Generic;
using System.Threading.Tasks;
using Acme.BookStore.Books;
using Acme.BookStore.Localization;
using Acme.BookStore.Wpf.Core.Threading;
using Acme.BookStore.Wpf.Services;
using Acme.BookStore.Wpf.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MahApps.Metro.Controls.Dialogs;
using MaterialDesignThemes.Wpf;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using MvvmHelpers;

namespace Acme.BookStore.Wpf.ViewModels
{
    public partial class MainWindowViewModel : AppViewModel
    {
        private readonly IBooksAppService _booksAppService;
        private readonly ILoggerFactory _loggerFactory;
        private readonly IStringLocalizer<BookStoreResource> _localizer;
        private readonly ISnackbarService _snackbarService;

        public List<IDispatcher> Dispatchers { get; }

        private ObservableRangeCollection<BookDto> _books;

        public MainWindowViewModel(IDispatcher dispatcher,
                                   IBooksAppService booksAppService,
                                   IDialogCoordinator dialogCoordinator,
                                   ILoggerFactory loggerFactory,
                                   IStringLocalizer<BookStoreResource> localizer,
                                   ISnackbarService snackbarService)
            : base(dialogCoordinator, loggerFactory.CreateLogger<AppViewModel>(), localizer)
        {
            _booksAppService = booksAppService;
            _loggerFactory = loggerFactory;
            _localizer = localizer;
            _snackbarService = snackbarService;

            Dispatchers = new List<IDispatcher>() { dispatcher };

            Title = localizer["Main"];
        }

        public MainWindowViewModel()
            : base(MahApps.Metro.Controls.Dialogs.DialogCoordinator.Instance)
        { } // for design-time

        public ISnackbarMessageQueue TheSnackbarMessageQueue => _snackbarService.TheSnackbarMessageQueue;

        public ObservableRangeCollection<BookDto> Books
        {
            get
            {
                if (_books == null) _books = new ObservableRangeCollection<BookDto>();
                return _books;
            }
        }

        //[ObservableProperty()]
        //public BookDto _selectedItem;

        public bool GetIsNotBusy() => IsNotBusy;

        [ICommand(CanExecute = "GetIsNotBusy", AllowConcurrentExecutions = true)]
        public async Task LoadDataAsync()
        {
            await SetBusyAsync(async () =>
            {
                if (_books != null && _books.Count > 0) _books.Clear();
                if (_books == null) _books = new ObservableRangeCollection<BookDto>();

                var pagedResults = await _booksAppService.GetListAsync(new GetBooksInput());

                foreach (var bookDetails in pagedResults.Items)
                {
                    _books.Add(bookDetails);
                }

                _snackbarService.Enqueue("Done!");
            });
        }

        [ICommand]
        public async Task OpenBookAsync()
        {
            await SetBusyAsync(async () =>
            {
                var logger = _loggerFactory.CreateLogger<BookDetailViewModel>();
                var dialog = new BookDetail();
                var vm = new BookDetailViewModel(DialogCoordinator, logger, _localizer, async () =>
                {
                    await DialogCoordinator.HideMetroDialogAsync(this, dialog);
                });
                await DialogCoordinator.ShowMetroDialogAsync(this, dialog);
            });
        }
    }
}
