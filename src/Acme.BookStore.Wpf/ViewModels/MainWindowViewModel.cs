using System.Diagnostics;
using System.Threading.Tasks;
using Acme.BookStore.Books;
using Acme.BookStore.Localization;
using CommunityToolkit.Mvvm.Input;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using MvvmHelpers;

namespace Acme.BookStore.Wpf.ViewModels
{
    public partial class MainWindowViewModel : AppViewModel
    {
        private readonly IBooksAppService _booksAppService;

        private ObservableRangeCollection<BookDto> _books;

        public MainWindowViewModel(IBooksAppService booksAppService,
                                   IDialogCoordinator dialogCoordinator,
                                   ILoggerFactory loggerFactory,
                                   IStringLocalizer<BookStoreResource> localizer)
            : base(dialogCoordinator, loggerFactory.CreateLogger<AppViewModel>(), localizer)
        {
            _booksAppService = booksAppService;
            Title = localizer["Main"];
        }

        public MainWindowViewModel()
            : base(DialogCoordinator.Instance)
        { } // for design-time

        public ObservableRangeCollection<BookDto> Books
        {
            get
            {
                if (_books == null) _books = new ObservableRangeCollection<BookDto>();
                return _books;
            }
        }

        public bool GetIsNotBusy() => IsNotBusy;

        [ICommand(CanExecute = "GetIsNotBusy", AllowConcurrentExecutions = true)]
        public async Task LoadDataAsync()
        {
            await SetBusyAsync(async () =>
            {
                if (_books.Count > 0) _books.Clear();

                var pagedResults = await _booksAppService.GetListAsync(new GetBooksInput());

                foreach (var bookDetails in pagedResults.Items)
                {
                    _books.Add(bookDetails);
                }
            });
        }
    }
}
