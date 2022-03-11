using Acme.BookStore.Books;
using Acme.BookStore.Localization;
using Acme.BookStore.Wpf.Tests;
using Acme.BookStore.Wpf.ViewModels;
using Acme.BookStore.Wpf.Views;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Shouldly;
using Xunit;

namespace Acme.BookStore.Views;

public class MainWindow_Tests : BookStoreWpfTestBase
{
    private readonly IBooksAppService _booksAppService;
    private readonly IDialogCoordinator _dialogCoordinator;
    private readonly ILoggerFactory _loggerFactory;
    private readonly IStringLocalizer<BookStoreResource> _localizer;

    public MainWindow_Tests()
    {
        _booksAppService = GetRequiredService<IBooksAppService>();
        _dialogCoordinator = GetRequiredService<IDialogCoordinator>();
        _loggerFactory = GetRequiredService<ILoggerFactory>();
        _localizer = GetRequiredService<IStringLocalizer<BookStoreResource>>();
    }

    [Fact]
    public async Task CanLoadData()
    {
        var vm = new MainWindowViewModel(_booksAppService, _dialogCoordinator, _loggerFactory, _localizer);
        var yourView = new MainWindow(vm);

        await vm.LoadDataAsync();

        vm.IsNotBusy.ShouldBe(true);
        vm.IsBusy.ShouldBe(false);
    }
}
