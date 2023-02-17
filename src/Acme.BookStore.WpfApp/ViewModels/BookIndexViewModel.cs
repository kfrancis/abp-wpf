using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acme.BookStore.Books;
using Acme.BookStore.Localization;
using Acme.BookStore.WpfApp.Core;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Wpf.Ui.Mvvm.Contracts;

namespace Acme.BookStore.WpfApp.ViewModels
{
    public partial class BookIndexViewModel : AppViewModel
    {
        private readonly IBooksAppService _bookAppService;

        private readonly ILogger<BookIndexViewModel> _logger;

        private readonly ISnackbarService _snackbarService;

        private ObservableCollection<BookDto> _books;

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

        public ObservableCollection<BookDto> Books
        {
            get
            {
                _books ??= new ObservableCollection<BookDto>();

                return _books;
            }
            set
            {
                SetProperty(ref _books, value);
            }
        }

        public bool GetIsNotBusy() => IsNotBusy;

        [RelayCommand]
        public async Task InitialAsync()
        {
            await LoadDataAsync();
        }

        [RelayCommand(CanExecute = "GetIsNotBusy", AllowConcurrentExecutions = true)]
        public async Task LoadDataAsync()
        {
            await SetBusyAsync(async () =>
            {
                if (_books != null && _books.Count > 0) _books.Clear();

                var pagedResults = await _bookAppService.GetListAsync(new GetBooksInput());

                foreach (var bookDetails in pagedResults.Items)
                {
                    _books.Add(bookDetails);
                }
                _logger.LogInformation($"Found {_books.Count} books.");
                await _snackbarService.ShowAsync("Done!");
            });
        }

        [RelayCommand]
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
