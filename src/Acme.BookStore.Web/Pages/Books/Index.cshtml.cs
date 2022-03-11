using System;
using System.Threading.Tasks;
using Acme.BookStore.Books;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Acme.BookStore.Web.Pages.Books
{
    public class IndexModel : AbpPageModel
    {
        public string NameFilter { get; set; }
        public BookType? TypeFilter { get; set; }
        public DateTime? PublishDateFilterMin { get; set; }

        public DateTime? PublishDateFilterMax { get; set; }
        public float? PriceFilterMin { get; set; }

        public float? PriceFilterMax { get; set; }

        private readonly IBooksAppService _booksAppService;

        public IndexModel(IBooksAppService booksAppService)
        {
            _booksAppService = booksAppService;
        }

        public async Task OnGetAsync()
        {
            await Task.CompletedTask;
        }
    }
}
