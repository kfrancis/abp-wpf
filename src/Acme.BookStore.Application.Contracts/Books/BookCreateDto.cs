using Acme.BookStore.Books;
using System;
using System.ComponentModel.DataAnnotations;

namespace Acme.BookStore.Books
{
    public class BookCreateDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public BookType Type { get; set; } = ((BookType[])Enum.GetValues(typeof(BookType)))[0];
        [Required]
        public DateTime PublishDate { get; set; }
        [Required]
        public float Price { get; set; }
    }
}