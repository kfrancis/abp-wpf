using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using Acme.BookStore.Books;
using Acme.BookStore.WpfApp.Models;
using Acme.BookStore.WpfApp.Services;
using Acme.BookStore.WpfApp.Views.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Volo.Abp;
using Wpf.Ui.Mvvm.Contracts;
using Wpf.Ui.Mvvm.Services;

namespace Acme.BookStore.WpfApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private static IHost _host =
    Host.CreateDefaultBuilder()
        .AddAppSettingsSecretsJson()
        .ConfigureServices((hostContext, services) =>
        {
            services.AddHostedService<ApplicationHostService>();
            // Page resolver service
            services.AddSingleton<IPageService, PageService>();

            // Theme manipulation
            services.AddSingleton<IThemeService, ThemeService>();

            // TaskBar manipulation
            services.AddSingleton<ITaskBarService, TaskBarService>();

            // Service containing navigation, same as INavigationWindow... but without window
            services.AddSingleton<INavigationService, NavigationService>();

            services.AddSingleton<IBooksAppService, BooksAppService>();

            // Main window with navigation
            services.AddScoped<INavigationWindow, Views.Windows.MainWindow>();
            services.AddScoped<ViewModels.MainWindowViewModel>();

            // Views and ViewModels
            services.AddScoped<Views.Pages.DashboardPage>();
            services.AddScoped<ViewModels.DashboardViewModel>();
            services.AddScoped<Views.Pages.DataPage>();
            services.AddScoped<ViewModels.DataViewModel>();
            services.AddScoped<Views.Pages.SettingsPage>();
            services.AddScoped<ViewModels.SettingsViewModel>();
            services.AddScoped<Views.Pages.BookIndexPage>();
            services.AddScoped<ViewModels.BookIndexViewModel>();

            // Configuration
            services.Configure<AppConfig>(hostContext.Configuration.GetSection(nameof(AppConfig)));
        }).Build();

        /// <summary>
        /// Occurs when the application is loading.
        /// </summary>
        private async void OnStartup(object sender, StartupEventArgs e)
        {

            var splashScreen = new SplashWindow();
            this.MainWindow = splashScreen;
            splashScreen.Show();

            //in order to ensure the UI stays responsive, we need to
            //do the work on a different thread
            await Task.Factory.StartNew(async () =>
            {
                Log.Logger = new LoggerConfiguration()
#if DEBUG
                .MinimumLevel.Debug()
#else
            .MinimumLevel.Information()
#endif
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.Async(c => c.File("Logs/logs.txt"))
                .CreateLogger();

                var shouldContinue = true;
                try
                {
                    Log.Information("Starting WPF host.");

                    //since we're not on the UI thread
                    //once we're done we need to use the Dispatcher
                    //to create and show the main window
                    await this.Dispatcher.InvokeAsync(async () =>
                    {
                        await _host.StartAsync();
                        splashScreen.Close();
                    });
                }
                catch (Exception ex)
                {
                    shouldContinue = false;
                    Log.Fatal(ex, "Host terminated unexpectedly!");
                }
            }, CancellationToken.None, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
        }

        /// <summary>
        /// Occurs when the application is closing.
        /// </summary>
        private async void OnExit(object sender, ExitEventArgs e)
        {
            await _host.StopAsync();

            _host.Dispose();
            Log.CloseAndFlush();
        }

        /// <summary>
        /// Occurs when an exception is thrown by an application but not handled.
        /// </summary>
        private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            // For more info see https://docs.microsoft.com/en-us/dotnet/api/system.windows.application.dispatcherunhandledexception?view=windowsdesktop-6.0
            e.Handled = true;
            Log.Error(e.Exception, "Unhandled exception!");
        }
    }
}
