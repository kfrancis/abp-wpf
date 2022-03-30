using System.Collections.Generic;
using System.Threading.Tasks;
using Acme.BookStore.Books;
using Acme.BookStore.Localization;
using Acme.BookStore.Wpf.Core.Threading;
using Acme.BookStore.Wpf.Services;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using MvvmHelpers;

namespace Acme.BookStore.Wpf.ViewModels
{
    public partial class BookIndexViewModel : AppViewModel
    {
        private readonly IBooksAppService _bookAppService;

        private readonly ILogger<BookIndexViewModel> _logger;

        private readonly ISnackbarService _snackbarService;

        private ObservableRangeCollection<BookDto> _books;

        public BookIndexViewModel() : base()
        {
        }

        public BookIndexViewModel(ILogger<BookIndexViewModel> logger,
                                  IBooksAppService bookAppService,
                                  IDispatcher dispatcher,
                                  ISnackbarService snackbarService,
                                  IStringLocalizer<BookStoreResource> localizer)
            : base(logger, localizer, dispatcher)
        {
            _logger = logger;
            _bookAppService = bookAppService;
            _snackbarService = snackbarService;
            Title = localizer["Books"];
        }
        public ObservableRangeCollection<BookDto> Books
        {
            get
            {
                if (_books == null) _books = new ObservableRangeCollection<BookDto>();
                return _books;
            }
            set
            {
                SetProperty(ref _books, value);
            }
        }

        public bool GetIsNotBusy() => IsNotBusy;

        [ICommand(CanExecute = "GetIsNotBusy", AllowConcurrentExecutions = true)]
        public async Task LoadDataAsync()
        {
            await SetBusyAsync(async () =>
            {
                if (_books == null) _books = new ObservableRangeCollection<BookDto>();
                if (_books != null && _books.Count > 0) _books.Clear();

                var pagedResults = await _bookAppService.GetListAsync(new GetBooksInput());

                foreach (var bookDetails in pagedResults.Items)
                {
                    _books.Add(bookDetails);
                }
                _logger.LogInformation($"Found {_books.Count} books.");
                _snackbarService.Enqueue("Done!");
            });
        }

        [ICommand]
        public async Task OpenBookAsync()
        {
            await SetBusyAsync(async () =>
            {
                string t = "";
                //var dialog = new BookDetail();
                //var vm = new BookDetailViewModel(logger, _localizer, async () =>
                //{
                //    //await DialogCoordinator.HideMetroDialogAsync(this, dialog);
                //});
                //await DialogCoordinator.ShowMetroDialogAsync(this, dialog);
            });
        }
    }
}
