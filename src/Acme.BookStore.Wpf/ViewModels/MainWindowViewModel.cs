using System.Diagnostics;
using System.Threading.Tasks;
using Acme.BookStore.Books;
using CommunityToolkit.Mvvm.Input;
using MvvmHelpers;

namespace Acme.BookStore.Wpf.ViewModels
{
    public partial class MainWindowViewModel : AppViewModel
    {
        private readonly IBooksAppService _booksAppService;

        private ObservableRangeCollection<BookDto> _books;

        public MainWindowViewModel(IBooksAppService booksAppService)
        {
            _booksAppService = booksAppService;
        }

        public MainWindowViewModel() { } // for design-time

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
            try
            {
                if (_books.Count > 0) _books.Clear();

                var pagedResults = await _booksAppService.GetListAsync(new GetBooksInput());

                foreach (var bookDetails in pagedResults.Items)
                {
                    _books.Add(bookDetails);
                }
            }
            catch (System.Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
