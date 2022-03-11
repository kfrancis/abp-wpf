using Acme.BookStore.EntityFrameworkCore;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.BackgroundWorkers;
using Volo.Abp.Modularity;

namespace Acme.BookStore.Wpf;

[DependsOn(typeof(AbpAutofacModule),
    typeof(BookStoreApplicationModule),
    typeof(BookStoreEntityFrameworkCoreModule))]
public class BookStoreWpfModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
        Configure<AbpBackgroundWorkerOptions>(options => options.IsEnabled = false);

        context.Services.AddSingleton<IDialogCoordinator>(DialogCoordinator.Instance);
    }
}