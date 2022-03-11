using Acme.BookStore.Books;
using System;
using System.ComponentModel.DataAnnotations;

namespace Acme.BookStore.Books
{
    public class BookUpdateDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public BookType Type { get; set; }
        [Required]
        public DateTime PublishDate { get; set; }
        [Required]
        public float Price { get; set; }
    }
}