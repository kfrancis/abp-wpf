using System;
using System.Threading.Tasks;
using Acme.BookStore.Localization;
using Acme.BookStore.Wpf.Core.Threading;
using CommunityToolkit.Mvvm.Input;
//using MahApps.Metro.Controls.Dialogs;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace Acme.BookStore.Wpf.ViewModels
{
    public partial class BookDetailViewModel : AppViewModel
    {
        private readonly Func<Task> _closeAction;

        public BookDetailViewModel()
            : base()
        {
            Title = nameof(BookDetailViewModel);
        }

        public BookDetailViewModel(ILogger<AppViewModel> logger,
                                   IStringLocalizer<BookStoreResource> localizer,
                                   IDispatcher dispatcher,
                                   Func<Task> closeAction = null)
            : base(logger, localizer, dispatcher)
        {
            _closeAction = closeAction;
        }

        [ICommand]
        public async Task CloseAsync()
        {
            if (_closeAction != null)
            {
                await _closeAction();
            }
        }
    }
}
