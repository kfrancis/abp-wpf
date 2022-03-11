using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Acme.BookStore.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Acme.BookStore.Books
{
    [AllowAnonymous]
    public class BooksAppService : ApplicationService, IBooksAppService
    {
        private readonly IBookRepository _bookRepository;

        public BooksAppService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public virtual async Task<PagedResultDto<BookDto>> GetListAsync(GetBooksInput input)
        {
            var totalCount = await _bookRepository.GetCountAsync(input.FilterText, input.Name, input.Type, input.PublishDateMin, input.PublishDateMax, input.PriceMin, input.PriceMax);
            var items = await _bookRepository.GetListAsync(input.FilterText, input.Name, input.Type, input.PublishDateMin, input.PublishDateMax, input.PriceMin, input.PriceMax, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<BookDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Book>, List<BookDto>>(items)
            };
        }

        public virtual async Task<BookDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<Book, BookDto>(await _bookRepository.GetAsync(id));
        }

        [Authorize(BookStorePermissions.Books.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _bookRepository.DeleteAsync(id);
        }

        [Authorize(BookStorePermissions.Books.Create)]
        public virtual async Task<BookDto> CreateAsync(BookCreateDto input)
        {
            var book = ObjectMapper.Map<BookCreateDto, Book>(input);

            book = await _bookRepository.InsertAsync(book, autoSave: true);
            return ObjectMapper.Map<Book, BookDto>(book);
        }

        [Authorize(BookStorePermissions.Books.Edit)]
        public virtual async Task<BookDto> UpdateAsync(Guid id, BookUpdateDto input)
        {
            var book = await _bookRepository.GetAsync(id);
            ObjectMapper.Map(input, book);
            book = await _bookRepository.UpdateAsync(book, autoSave: true);
            return ObjectMapper.Map<Book, BookDto>(book);
        }
    }
}
