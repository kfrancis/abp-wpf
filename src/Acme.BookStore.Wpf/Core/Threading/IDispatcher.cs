using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acme.BookStore.Wpf.Core.Threading
{
    /// <summary>
    /// Represents a service which can delegate and manage the execution of code (e.g. on the UI thread, in the background, etc.).
    /// </summary>
    public interface IDispatcher
    {
        /// <summary>
        /// A set of <see cref="DispatcherType"/> flags indicating whether this <see cref="IDispatcher"/> manages special kinds of threads, which can (and should) be utilized in scenarios such as updating UI elements from code.
        /// </summary>
        DispatcherType DispatcherType { get; }

        /// <summary>
        /// Executes an <see cref="Action"/> asynchronously.
        /// </summary>
        /// <param name="execute">The <see cref="Action"/> to run on this <see cref="IDispatcher"/>.</param>
        Task RunAsync(Action execute);

        void Run(Action execute);

        /// <summary>
        /// Executes a <see cref="Func{TResult}"/> asynchronously.
        /// </summary>
        /// <param name="execute">The <see cref="Func{TResult}"/> to run on this <see cref="IDispatcher"/>.</param>
        /// <returns>The <typeparamref name="T"/> result of the <see cref="Func{TResult}"/>.</returns>
        Task<T> RunAsync<T>(Func<T> execute);

        /// <summary>
        /// Executes a <see cref="Task"/> asynchronously.
        /// </summary>
        /// <param name="execute">The awaitable <see cref="Task"/> to run on this <see cref="IDispatcher"/>.</param>
        Task RunAsync(Func<Task> execute);

        /// <summary>
        /// Executes a <see cref="Task{TResult}"/> asynchronously.
        /// </summary>
        /// <param name="execute">The <see cref="Task{TResult}"/> to run on this <see cref="IDispatcher"/>.</param>
        /// <returns>The <typeparamref name="T"/> result of the <see cref="Task{TResult}"/>.</returns>
        Task<T> RunAsync<T>(Func<Task<T>> execute);
    }
}
