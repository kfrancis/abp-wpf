using System;
using Volo.Abp.Application.Dtos;

namespace Acme.BookStore.Books
{
    public class GetBooksInput : PagedAndSortedResultRequestDto
    {
        public string FilterText { get; set; }

        public string Name { get; set; }
        public BookType? Type { get; set; }
        public DateTime? PublishDateMin { get; set; }
        public DateTime? PublishDateMax { get; set; }
        public float? PriceMin { get; set; }
        public float? PriceMax { get; set; }

        public GetBooksInput()
        {
        }
    }
}
