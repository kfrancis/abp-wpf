using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Acme.BookStore.Books
{
    public interface IBookRepository : IRepository<Book, Guid>
    {
        Task<List<Book>> GetListAsync(
            string filterText = null,
            string name = null,
            BookType? type = null,
            DateTime? publishDateMin = null,
            DateTime? publishDateMax = null,
            float? priceMin = null,
            float? priceMax = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<long> GetCountAsync(
            string filterText = null,
            string name = null,
            BookType? type = null,
            DateTime? publishDateMin = null,
            DateTime? publishDateMax = null,
            float? priceMin = null,
            float? priceMax = null,
            CancellationToken cancellationToken = default);
    }
}
