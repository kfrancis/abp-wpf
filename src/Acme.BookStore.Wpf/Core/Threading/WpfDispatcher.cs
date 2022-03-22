using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Volo.Abp.DependencyInjection;

namespace Acme.BookStore.Wpf.Core.Threading
{
    /// <summary>
    /// An <see cref="IDispatcher"/> that uses the <see cref="Application.Current"/>'s dispatcher to execute code on the UI thread.
    /// </summary>
    public class WpfDispatcher : IDispatcher, ISingletonDependency
    {
        /// <inheritdoc/>
        public DispatcherType DispatcherType { get; }

        /// <summary>
        /// Creates a new <see cref="WpfDispatcher"/>.
        /// </summary>
        public WpfDispatcher()
        {
            DispatcherType = DispatcherType.Main;
        }

        /// <inheritdoc/>
        public async Task RunAsync(Action execute)
        {
            await Application.Current.Dispatcher.InvokeAsync(() => execute());
        }

        /// <inheritdoc/>
        public async Task<T> RunAsync<T>(Func<T> execute)
        {
            return await Application.Current.Dispatcher.InvokeAsync(() => execute());
        }

        /// <inheritdoc/>
        public async Task RunAsync<T>(Func<Task> execute)
        {
            //// Uh-huh, this doesn't seem right.
            await Application.Current.Dispatcher.InvokeAsync(async () => await execute());
        }

        /// <inheritdoc/>
        public async Task<T> RunAsync<T>(Func<Task<T>> execute)
        {
            //// Uh-huh, this doesn't seem right.
            return await await Application.Current.Dispatcher.InvokeAsync(() => execute());
        }

        public Task RunAsync(Func<Task> execute)
        {
            throw new NotImplementedException();
        }

        public void Run(Action execute)
        {
            Application.Current.Dispatcher.Invoke(execute);
        }
    }
}
