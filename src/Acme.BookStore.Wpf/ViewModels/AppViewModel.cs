using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Threading;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Extensions.Logging;
using MvvmHelpers;
using Volo.Abp.DependencyInjection;

namespace Acme.BookStore.Wpf.ViewModels
{
    public abstract class AppViewModel : BaseViewModel, ITransientDependency
    {
        private readonly IDialogCoordinator _dialogCoordinator;
        private readonly ILogger<AppViewModel> _logger;

        protected AppViewModel(IDialogCoordinator dialogCoordinator, ILogger<AppViewModel> logger)
        {
            _dialogCoordinator = dialogCoordinator;
            _logger = logger;
        }

        protected AppViewModel(IDialogCoordinator dialogCoordinator)
        {
            _dialogCoordinator = dialogCoordinator;
        }

        public virtual async Task InitializeAsync(object navigationData)
        {
            await Task.FromResult(false);
        }

        public object GetPropertyValue(string propertyName)
        {
            return GetType().GetProperty(propertyName).GetValue(this, null);
        }

        public T GetPropertyValue<T>(string propertyName)
        {
            return (T)Convert.ChangeType(GetPropertyValue(propertyName), typeof(T));
        }

        public bool LogException(Exception ex, bool shouldCatch = false, bool shouldDisplay = false)
        {
            if (ex == null) return shouldCatch;

            _logger?.LogException(ex.Demystify());
            if (shouldDisplay)
            {
                Dispatcher.CurrentDispatcher.Invoke(() =>
                {
                    _ = Task.Run(() => _dialogCoordinator.ShowMessageAsync(this, "Error", "Failed: " + ex.Message));
                });
            }

            return shouldCatch;
        }

        public async Task SetBusyAsync(Func<Task> func, string loadingMessage = null, bool showException = true)
        {
            IsBusy = true;
            try
            {
                await func();
            }
            catch (Exception ex) when (LogException(ex, true, showException))
            {
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
