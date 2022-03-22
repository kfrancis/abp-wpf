using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Dependency;
using Acme.BookStore.Wpf.Core.Threading;

namespace Acme.BookStore.Wpf.Tests
{
    /// <summary>
    /// A basic <see cref="IDispatcher"/> which simply runs the given code.
    /// </summary>
    public class SimpleDispatcher : IDispatcher, ISingletonDependency
    {
        /// <inheritdoc/>
        public DispatcherType DispatcherType { get; }

        /// <summary>
        /// Creates a new <see cref="SimpleDispatcher"/> with the specified flags.
        /// </summary>
        /// <param name="dispatcherType">A set of <see cref="DispatcherType"/> flags indicating whether this <see cref="IDispatcher"/> manages special kinds of threads, which can (and should) be utilized in scenarios such as updating UI elements from code.</param>
        public SimpleDispatcher(DispatcherType dispatcherType = DispatcherType.Main)
        {
            DispatcherType = dispatcherType;
        }

        /// <inheritdoc/>
        public async Task RunAsync(Action execute)
        {
            execute();
        }

        /// <inheritdoc/>
        public async Task<T> RunAsync<T>(Func<T> execute)
        {
            return execute();
        }

        /// <inheritdoc/>
        public async Task RunAsync(Func<Task> execute)
        {
            await execute();
        }

        /// <inheritdoc/>
        public async Task<T> RunAsync<T>(Func<Task<T>> execute)
        {
            return await execute();
        }

        public void Run(Action execute)
        {
            execute();
        }
    }
}
