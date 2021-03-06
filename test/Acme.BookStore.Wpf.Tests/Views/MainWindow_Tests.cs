using Acme.BookStore.Books;
using Acme.BookStore.Localization;
using Acme.BookStore.Wpf.Core.Threading;
using Acme.BookStore.Wpf.Services;
using Acme.BookStore.Wpf.Tests;
using Acme.BookStore.Wpf.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Shouldly;
using Volo.Abp.MultiTenancy;
using Xunit;

namespace Acme.BookStore.Views;

public class MainWindow_Tests : BookStoreWpfTestBase
{
    private readonly ILoggerFactory _loggerFactory;
    private readonly IStringLocalizer<BookStoreResource> _localizer;
    private ISnackbarService _snackbarService;
    private ICurrentTenant _fakeCurrentTenant;
    private readonly IDispatcher _dispatcher;
    private MainWindowViewModel _viewModel;

    public MainWindow_Tests()
    {
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

    private void GivenEmptyViewModel()
    {
        _viewModel = new MainWindowViewModel(_dispatcher, _loggerFactory, _localizer, _snackbarService);
    }

    [UIFact]
    public void CanInstantiate()
    {
        GivenEmptyViewModel();

        _viewModel.ShouldSatisfyAllConditions(
            vm => vm.ShouldNotBeNull(),
            vm => vm.IsBusy.ShouldBeFalse(),
            vm => vm.IsNotBusy.ShouldBeTrue()
        );
    }
}
