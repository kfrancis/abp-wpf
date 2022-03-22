using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acme.BookStore.Wpf.Core.Threading
{
    /// <summary>
    /// Provides extension methods for the <see cref="IDispatcher"/> interface.
    /// </summary>
    public static class DispatcherExtensions
    {
        /// <summary>
        /// Gets the first found <see cref="IDispatcher"/> that manages the UI thread (see <see cref="DispatcherType.Main"/>).
        /// </summary>
        /// <param name="dispatchers">The collection of available <see cref="IDispatcher"/>s.</param>
        /// <returns>The <see cref="IDispatcher"/> managing the UI thread.</returns>
        public static IDispatcher GetUIDispatcher(this IEnumerable<IDispatcher> dispatchers)
            => GetDispatcher(dispatchers, DispatcherType.Main);

        /// <summary>
        /// Gets the first found <see cref="IDispatcher"/> with the specified <see cref="DispatcherType"/> flag.
        /// </summary>
        /// <param name="dispatchers">The collection of available <see cref="IDispatcher"/>s.</param>
        /// <param name="type">The <see cref="DispatcherType"/> requested from the desired <see cref="IDispatcher"/>.</param>
        /// <returns>The <see cref="IDispatcher"/> managing the background thread.</returns>
        public static IDispatcher GetDispatcher(this IEnumerable<IDispatcher> dispatchers, DispatcherType type)
        {
            if (dispatchers == null || !dispatchers.Any())
            {
                throw new ArgumentException("The collection must contain at least one available IDispatcher.", nameof(dispatchers));
            }

            var background = dispatchers.FirstOrDefault(d => d.DispatcherType.HasFlag(type));
            if (background != null)
            {
                return background;
            }
            else
            {
                throw new ArgumentException($"No IDispatchers of the specified type {type} were avaliable in the provided collection", nameof(dispatchers));
            }
        }

        /// <summary>
        /// Runs the given code on the UI thread's <see cref="IDispatcher"/>.
        /// </summary>
        /// <param name="dispatchers">The collection of available <see cref="IDispatcher"/>s.</param>
        /// <param name="execute">The code to execute.</param>
        public static async Task RunOnUIThreadAsync(this IEnumerable<IDispatcher> dispatchers, Action execute)
            => await dispatchers.GetUIDispatcher().RunAsync(execute);

        /// <summary>
        /// Runs the given code on the UI thread's <see cref="IDispatcher"/>.
        /// </summary>
        /// <param name="dispatchers">The collection of available <see cref="IDispatcher"/>s.</param>
        /// <param name="execute">The code to execute.</param>
        public static void RunOnUIThread(this IEnumerable<IDispatcher> dispatchers, Action execute)
            => dispatchers.GetUIDispatcher().Run(execute);

        /// <summary>
        /// Runs the given code on the UI thread's <see cref="IDispatcher"/>.
        /// </summary>
        /// <param name="dispatchers">The collection of available <see cref="IDispatcher"/>s.</param>
        /// <param name="execute">The code to execute.</param>
        public static async Task RunOnUIThreadAsync(this Action execute, IEnumerable<IDispatcher> dispatchers)
            => await dispatchers.GetUIDispatcher().RunAsync(execute);

        /// <summary>
        /// Runs the given code on the UI thread's <see cref="IDispatcher"/>.
        /// </summary>
        /// <param name="dispatchers">The collection of available <see cref="IDispatcher"/>s.</param>
        /// <param name="execute">The code to execute.</param>
        /// <typeparam name="T">The type of output <paramref name="execute"/> produces.</typeparam>
        /// <returns>The <typeparamref name="T"/> return value of <paramref name="execute"/>.</returns>
        public static async Task<T> RunOnUIThreadAsync<T>(this IEnumerable<IDispatcher> dispatchers, Func<T> execute)
            => await dispatchers.GetUIDispatcher().RunAsync(execute);

        /// <summary>
        /// Runs the given code on the UI thread's <see cref="IDispatcher"/>.
        /// </summary>
        /// <param name="dispatchers">The collection of available <see cref="IDispatcher"/>s.</param>
        /// <param name="execute">The code to execute.</param>
        /// <typeparam name="T">The type of output <paramref name="execute"/> produces.</typeparam>
        /// <returns>The <typeparamref name="T"/> return value of <paramref name="execute"/>.</returns>
        public static async Task<T> RunOnUIThreadAsync<T>(this Func<T> execute, IEnumerable<IDispatcher> dispatchers)
            => await dispatchers.GetUIDispatcher().RunAsync(execute);

        /// <summary>
        /// Runs the given code on the UI thread's <see cref="IDispatcher"/>.
        /// </summary>
        /// <param name="dispatchers">The collection of available <see cref="IDispatcher"/>s.</param>
        /// <param name="execute">The code to execute.</param>
        public static async Task RunOnUIThreadAsync(this IEnumerable<IDispatcher> dispatchers, Func<Task> execute)
            => await dispatchers.GetUIDispatcher().RunAsync(execute);

        /// <summary>
        /// Runs the given code on the UI thread's <see cref="IDispatcher"/>.
        /// </summary>
        /// <param name="dispatchers">The collection of available <see cref="IDispatcher"/>s.</param>
        /// <param name="execute">The code to execute.</param>
        public static async Task RunOnUIThreadAsync(this Func<Task> execute, IEnumerable<IDispatcher> dispatchers)
            => await dispatchers.GetUIDispatcher().RunAsync(execute);

        /// <summary>
        /// Runs the given code on the UI thread's <see cref="IDispatcher"/>.
        /// </summary>
        /// <param name="dispatchers">The collection of available <see cref="IDispatcher"/>s.</param>
        /// <param name="execute">The code to execute.</param>
        /// <typeparam name="T">The type of output <paramref name="execute"/> produces.</typeparam>
        /// <returns>The <typeparamref name="T"/> return value of <paramref name="execute"/>.</returns>
        public static async Task<T> RunOnUIThreadAsync<T>(this IEnumerable<IDispatcher> dispatchers, Func<Task<T>> execute)
            => await dispatchers.GetUIDispatcher().RunAsync(execute);

        /// <summary>
        /// Runs the given code on the UI thread's <see cref="IDispatcher"/>.
        /// </summary>
        /// <param name="dispatchers">The collection of available <see cref="IDispatcher"/>s.</param>
        /// <param name="execute">The code to execute.</param>
        /// <typeparam name="T">The type of output <paramref name="execute"/> produces.</typeparam>
        /// <returns>The <typeparamref name="T"/> return value of <paramref name="execute"/>.</returns>
        public static async Task<T> RunOnUIThreadAsync<T>(this Func<Task<T>> execute, IEnumerable<IDispatcher> dispatchers)
            => await dispatchers.GetUIDispatcher().RunAsync(execute);
    }
}
