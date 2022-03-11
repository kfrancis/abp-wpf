using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using Acme.BookStore.Books;
using Acme.BookStore.Shared;

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