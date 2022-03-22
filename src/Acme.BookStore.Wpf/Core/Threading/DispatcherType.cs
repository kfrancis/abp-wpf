using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acme.BookStore.Wpf.Core.Threading
{
    /// <summary>
    /// Flags describing the type of behavior an <see cref="IDispatcher"/> has.
    /// </summary>
    [Flags]
    public enum DispatcherType
    {
        /// <summary>
        /// The <see cref="IDispatcher"/> is a normal dispatcher service with no special features.
        /// </summary>
        None = 0,
        /// <summary>
        /// The <see cref="IDispatcher"/> is connected to the same thread used for UI updates (or, in the case of apps without UI, the main thread).
        /// </summary>
        Main = 1,
        /// <summary>
        /// The <see cref="IDispatcher"/> runs code on a background (non-blocking) thread of the application.
        /// </summary>
        Background = 1 << 1
    }
}
