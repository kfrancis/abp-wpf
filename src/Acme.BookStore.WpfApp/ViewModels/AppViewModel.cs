using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acme.BookStore.Localization;
using Acme.BookStore.WpfApp.Core;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Volo.Abp.DependencyInjection;

namespace Acme.BookStore.WpfApp.ViewModels
{
    public abstract class AppViewModel : BaseViewModel
    {
        private readonly IStringLocalizer<BookStoreResource> _localizer;
        private readonly ILogger<AppViewModel> _logger;

        protected AppViewModel(ILogger<AppViewModel> logger, IStringLocalizer<BookStoreResource> localizer, IDispatcher dispatcher)
            : this()
        {
            _logger = logger;
            _localizer = localizer;

            Dispatchers = new List<IDispatcher> { dispatcher };
        }

        protected AppViewModel()
        {
        }

        public List<IDispatcher> Dispatchers { get; }
        public IStringLocalizer<BookStoreResource> L => _localizer;

        public object GetPropertyValue(string propertyName)
        {
            return GetType().GetProperty(propertyName).GetValue(this, null);
        }

        public T GetPropertyValue<T>(string propertyName)
        {
            return (T)Convert.ChangeType(GetPropertyValue(propertyName), typeof(T));
        }

        public virtual async Task InitializeAsync(object navigationData)
        {
            await Task.FromResult(false);
        }

        public bool LogException(Exception ex, bool shouldCatch = false, bool shouldDisplay = false)
        {
            if (ex == null) return shouldCatch;

            _logger?.LogException(ex.Demystify());
            if (shouldDisplay)
            {
                //Dispatcher.CurrentDispatcher.Invoke(() =>
                //{
                //    _ = Task.Run(() => _dialogCoordinator.ShowMessageAsync(this, _localizer?["Error"] ?? "Error", (_localizer?["Failed"] ?? "Failed") + $": {ex.ToStringDemystified()}"));
                //});
            }

            return shouldCatch;
        }

        public async Task SetBusyAsync(Func<Task> func, string? loadingMessage = null, bool showException = true)
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
