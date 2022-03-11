using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Volo.Abp;
using Volo.Abp.Modularity;

namespace Acme.BookStore.Wpf.Tests
{
    public abstract class BookStoreWpfTestBase : BookStoreTestBase<BookStoreWpfTestModule>
    {
    }
}
