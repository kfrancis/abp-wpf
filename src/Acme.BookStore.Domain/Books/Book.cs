using Acme.BookStore.Books;
using System;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;
using Volo.Abp;

namespace Acme.BookStore.Books
{
    public class Book : AuditedAggregateRoot<Guid>
    {
        [NotNull]
        public virtual string Name { get; set; }

        public virtual BookType Type { get; set; }

        public virtual DateTime PublishDate { get; set; }

        public virtual float Price { get; set; }

        public Book()
        {

        }

        public Book(Guid id, string name, BookType type, DateTime publishDate, float price)
        {
            Id = id;
            Check.NotNull(name, nameof(name));
            Name = name;
            Type = type;
            PublishDate = publishDate;
            Price = price;
        }
    }
}