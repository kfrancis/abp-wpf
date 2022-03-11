using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Acme.BookStore.Books
{
    public interface IBooksAppService : IApplicationService
    {
        Task<PagedResultDto<BookDto>> GetListAsync(GetBooksInput input);

        Task<BookDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<BookDto> CreateAsync(BookCreateDto input);

        Task<BookDto> UpdateAsync(Guid id, BookUpdateDto input);
    }
}