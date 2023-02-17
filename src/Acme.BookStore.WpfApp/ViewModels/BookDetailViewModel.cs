using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acme.BookStore.Localization;
using Acme.BookStore.WpfApp.Core;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace Acme.BookStore.WpfApp.ViewModels
{
    public partial class BookDetailViewModel : AppViewModel
    {
        private readonly Func<Task>? _closeAction;

        public BookDetailViewModel()
            : base()
        {
            Title = nameof(BookDetailViewModel);
        }

        public BookDetailViewModel(ILogger<AppViewModel> logger,
                                   IStringLocalizer<BookStoreResource> localizer,
                                   IDispatcher dispatcher,
                                   Func<Task>? closeAction = null)
            : base(logger, localizer, dispatcher)
        {
            _closeAction = closeAction;
        }

        [RelayCommand]
        public async Task CloseAsync()
        {
            if (_closeAction != null)
            {
                await _closeAction();
            }
        }
    }
}
