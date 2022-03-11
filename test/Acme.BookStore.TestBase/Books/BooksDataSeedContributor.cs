using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Acme.BookStore.Books;

namespace Acme.BookStore.Books
{
    public class BooksDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly IBookRepository _bookRepository;

        public BooksDataSeedContributor(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            await _bookRepository.InsertAsync(new Book
            (
                id: Guid.Parse("adc8cbe0-8ee8-4098-ae22-e2c5b3b23402"),
                name: "edddca8d3f6a",
                type: default,
                publishDate: new DateTime(2002, 3, 13),
                price: 1151190684
            ));

            await _bookRepository.InsertAsync(new Book
            (
                id: Guid.Parse("71dcc12f-ba11-45d1-80e0-10cf2e04fe6f"),
                name: "5bf615d1c78a",
                type: default,
                publishDate: new DateTime(2015, 8, 23),
                price: 1202727512
            ));
        }
    }
}