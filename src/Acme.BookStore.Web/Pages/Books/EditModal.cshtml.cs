using System;
using System.Threading.Tasks;
using Acme.BookStore.Books;
using Microsoft.AspNetCore.Mvc;

namespace Acme.BookStore.Web.Pages.Books
{
    public class EditModalModel : BookStorePageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public BookUpdateDto Book { get; set; }

        private readonly IBooksAppService _booksAppService;

        public EditModalModel(IBooksAppService booksAppService)
        {
            _booksAppService = booksAppService;
        }

        public async Task OnGetAsync()
        {
            var book = await _booksAppService.GetAsync(Id);
            Book = ObjectMapper.Map<BookDto, BookUpdateDto>(book);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _booksAppService.UpdateAsync(Id, Book);
            return NoContent();
        }
    }
}
