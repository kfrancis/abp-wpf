using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using MvvmHelpers;
using Volo.Abp.DependencyInjection;

namespace Acme.BookStore.Wpf.ViewModels
{
    public abstract class AppViewModel : BaseViewModel, ITransientDependency
    {
        protected AppViewModel()
        {
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

        public async Task SetBusyAsync(Func<Task> func, string loadingMessage = null)
        {
            IsBusy = true;
            try
            {
                await Dispatcher.CurrentDispatcher.InvokeAsync(async () => {
                    await func();
                });
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
