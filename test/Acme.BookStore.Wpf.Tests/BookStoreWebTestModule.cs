using System.Globalization;
using Abp.TestBase;
using Acme.BookStore.Localization;
using Acme.BookStore.Wpf;
using Localization.Resources.AbpUi;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.UI.Navigation;
using Volo.Abp.Validation.Localization;

namespace Acme.BookStore;

[DependsOn(
    typeof(AbpTestBaseModule),
    typeof(BookStoreWpfModule),
    typeof(BookStoreApplicationTestModule)
)]
public class BookStoreWebTestModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {

    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        ConfigureLocalizationServices(context.Services);
    }

    private static void ConfigureLocalizationServices(IServiceCollection services)
    {
        var cultures = new List<CultureInfo> { new CultureInfo("en"), new CultureInfo("tr") };
        services.Configure<RequestLocalizationOptions>(options =>
        {
            options.DefaultRequestCulture = new RequestCulture("en");
            options.SupportedCultures = cultures;
            options.SupportedUICultures = cultures;
        });

        services.Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<BookStoreResource>()
                .AddBaseTypes(
                    typeof(AbpValidationResource),
                    typeof(AbpUiResource)
                );
        });
    }
}
