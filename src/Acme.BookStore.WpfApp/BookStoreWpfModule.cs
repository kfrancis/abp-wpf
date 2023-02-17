using Acme.BookStore.BackgroundJobs;
using Acme.BookStore.EntityFrameworkCore;
using Acme.BookStore.WpfApp.Models;
using Acme.BookStore.WpfApp.Services;
using Autofac.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Auditing;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.BackgroundWorkers;
using Volo.Abp.Modularity;
using Wpf.Ui.Mvvm.Contracts;
using Wpf.Ui.Mvvm.Services;

namespace Acme.BookStore.WpfApp;

[DependsOn(typeof(AbpAutofacModule),
    typeof(BookStoreApplicationModule),
    typeof(BookStoreEntityFrameworkCoreModule))]
public class BookStoreWpfModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpAuditingOptions>(options => options.IsEnabled = false);
        Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = BackgroundJobConsts.IsEnabled);
        Configure<AbpBackgroundWorkerOptions>(options => options.IsEnabled = BackgroundJobConsts.IsEnabled);
    }
}
