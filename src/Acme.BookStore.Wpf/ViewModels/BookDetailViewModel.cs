using System;
using System.Threading.Tasks;
using Acme.BookStore.Localization;
using CommunityToolkit.Mvvm.Input;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace Acme.BookStore.Wpf.ViewModels
{
    public partial class BookDetailViewModel : AppViewModel
    {
        private readonly Func<Task> _closeAction;

        public BookDetailViewModel() : base(MahApps.Metro.Controls.Dialogs.DialogCoordinator.Instance)
        {
            Title = nameof(BookDetailViewModel);
        }

        public BookDetailViewModel(IDialogCoordinator dialogCoordinator,
                                   ILogger<AppViewModel> logger,
                                   IStringLocalizer<BookStoreResource> localizer,
                                   Func<Task> closeAction = null)
            : base(dialogCoordinator, logger, localizer)
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
