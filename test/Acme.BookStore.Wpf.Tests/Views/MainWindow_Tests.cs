using Acme.BookStore.Books;
using Acme.BookStore.Localization;
using Acme.BookStore.Wpf.Core.Threading;
using Acme.BookStore.Wpf.Services;
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
    private ISnackbarService _snackbarService;
    private ICurrentTenant _fakeCurrentTenant;
    private IDispatcher _dispatcher;
    private MainWindowViewModel _viewModel;

    public MainWindow_Tests()
    {
        _booksAppService = GetRequiredService<IBooksAppService>();
        _dialogCoordinator = GetRequiredService<IDialogCoordinator>();
        _loggerFactory = GetRequiredService<ILoggerFactory>();
        _localizer = GetRequiredService<IStringLocalizer<BookStoreResource>>();
        _snackbarService = GetRequiredService<ISnackbarService>();
        _fakeCurrentTenant = GetRequiredService<ICurrentTenant>();
        _dispatcher = GetRequiredService<IDispatcher>();
        _viewModel = new();
    }

    protected override void AfterAddApplication(IServiceCollection services)
    {
        AddTestSubstitution(ref _fakeCurrentTenant, services);
        AddTestSubstitution(ref _snackbarService, services);
    }

    [UIFact]
    public async Task CanLoadData()
    {
        GivenEmptyViewModel();

        await _viewModel.LoadDataAsync();

        _viewModel.ShouldSatisfyAllConditions(
            vm => vm.ShouldNotBeNull(),
            vm => vm.LoadDataCommand.ShouldNotBeNull(),
            vm => vm.OpenBookCommand.ShouldNotBeNull(),
            vm => vm.Books.ShouldNotBeNull(),
            vm => vm.Books.Any().ShouldBeTrue(),
            vm => vm.IsBusy.ShouldBeFalse(),
            vm => vm.IsNotBusy.ShouldBeTrue(),
            vm => vm.GetIsNotBusy().ShouldBe(vm.IsNotBusy),
            vm => vm.Title.ShouldNotBeNullOrEmpty(),
            vm => vm.Subtitle.ShouldBeNullOrEmpty()
        );
    }

    private void GivenEmptyViewModel()
    {
        _viewModel = new MainWindowViewModel(_dispatcher, _booksAppService, _dialogCoordinator, _loggerFactory, _localizer, _snackbarService);
    }
}
