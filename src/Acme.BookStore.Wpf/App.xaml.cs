using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Acme.BookStore.Wpf.Views;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;
using Volo.Abp;

namespace Acme.BookStore.Wpf;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private IAbpApplicationWithInternalServiceProvider _abpApplication;

    protected override async void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

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

                _abpApplication = await AbpApplicationFactory.CreateAsync<BookStoreWpfModule>(options =>
                {
                    options.UseAutofac();
                    options.Services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true));
                });

                await _abpApplication.InitializeAsync();
            }
            catch (Exception ex)
            {
                shouldContinue = false;
                Log.Fatal(ex, "Host terminated unexpectedly!");
            }

            //since we're not on the UI thread
            //once we're done we need to use the Dispatcher
            //to create and show the main window
            this.Dispatcher.Invoke(() =>
            {
                if (shouldContinue)
                    _abpApplication.Services.GetRequiredService<MainWindow>()?.Show();

                splashScreen.Close();
            });
        }, CancellationToken.None, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        await _abpApplication.ShutdownAsync();
        Log.CloseAndFlush();
    }

    private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
    {
        e.Handled = true;
        Log.Error(e.Exception, "Unhandled exception!");
    }
}
