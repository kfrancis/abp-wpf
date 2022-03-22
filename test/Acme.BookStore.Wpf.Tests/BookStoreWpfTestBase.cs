using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NSubstitute;
using Volo.Abp;
using Volo.Abp.Modularity;

namespace Acme.BookStore.Wpf.Tests
{
    public abstract class BookStoreWpfTestBase : BookStoreTestBase<BookStoreWpfTestModule>
    {
        /// <summary>
        /// Shortcut for adding an NSubstitute.For a type and including the reference as a singleton
        /// </summary>
        /// <typeparam name="T">The interface to substitute</typeparam>
        /// <param name="backingStore">The local backing variable</param>
        /// <param name="services">The service collection to add the singleton to</param>
        public static void AddTestSubstitution<T>(ref T backingStore, IServiceCollection services)
            where T : class
        {
            backingStore = Substitute.For<T>();
            services.AddSingleton(backingStore);
        }
    }
}
