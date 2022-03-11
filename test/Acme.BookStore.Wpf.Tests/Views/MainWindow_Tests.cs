using Acme.BookStore.Books;
using Acme.BookStore.Localization;
using Acme.BookStore.Wpf.Tests;
using Acme.BookStore.Wpf.ViewModels;
using Acme.BookStore.Wpf.Views;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Shouldly;
using Volo.Abp.MultiTenancy;
using Xunit;

namespace Acme.BookStore.Views;

public class MainWindow_Tests : BookStoreWpfTestBase
{
    private readonly IBooksAppService _booksAppService;
    private readonly IDialogCoordinator _dialogCoordinator;
    private readonly ILoggerFactory _loggerFactory;
    private readonly IStringLocalizer<BookStoreResource> _localizer;
    private ICurrentTenant _fakeCurrentTenant;

    public MainWindow_Tests()
    {
        _booksAppService = GetRequiredService<IBooksAppService>();
        _dialogCoordinator = GetRequiredService<IDialogCoordinator>();
        _loggerFactory = GetRequiredService<ILoggerFactory>();
        _localizer = GetRequiredService<IStringLocalizer<BookStoreResource>>();
    }

    protected override void AfterAddApplication(IServiceCollection services)
    {
        _fakeCurrentTenant = Substitute.For<ICurrentTenant>();
        services.AddSingleton(_fakeCurrentTenant);
    }

    [UIFact]
    public async Task CanLoadData()
    {
        var vm = new MainWindowViewModel(_booksAppService, _dialogCoordinator, _loggerFactory, _localizer);

        var yourView = new MainWindow(vm);

        await vm.LoadDataAsync();

        vm.Books.ShouldNotBeNull();
        vm.Books.Any().ShouldBe(true);

        vm.IsNotBusy.ShouldBe(true);
        vm.IsBusy.ShouldBe(false);
    }
}
