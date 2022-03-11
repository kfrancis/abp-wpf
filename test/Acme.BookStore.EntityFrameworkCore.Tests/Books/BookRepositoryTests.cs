using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using Acme.BookStore.Books;
using Acme.BookStore.EntityFrameworkCore;
using Xunit;

namespace Acme.BookStore.Books
{
    public class BookRepositoryTests : BookStoreEntityFrameworkCoreTestBase
    {
        private readonly IBookRepository _bookRepository;

        public BookRepositoryTests()
        {
            _bookRepository = GetRequiredService<IBookRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _bookRepository.GetListAsync(
                    name: "edddca8d3f6a",
                    type: default
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("adc8cbe0-8ee8-4098-ae22-e2c5b3b23402"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _bookRepository.GetCountAsync(
                    name: "5bf615d1c78a",
                    type: default
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}