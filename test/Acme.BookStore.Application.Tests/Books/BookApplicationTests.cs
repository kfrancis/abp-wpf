using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace Acme.BookStore.Books
{
    public class BooksAppServiceTests : BookStoreApplicationTestBase
    {
        private readonly IBooksAppService _booksAppService;
        private readonly IRepository<Book, Guid> _bookRepository;

        public BooksAppServiceTests()
        {
            _booksAppService = GetRequiredService<IBooksAppService>();
            _bookRepository = GetRequiredService<IRepository<Book, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _booksAppService.GetListAsync(new GetBooksInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Id == Guid.Parse("adc8cbe0-8ee8-4098-ae22-e2c5b3b23402")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("71dcc12f-ba11-45d1-80e0-10cf2e04fe6f")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _booksAppService.GetAsync(Guid.Parse("adc8cbe0-8ee8-4098-ae22-e2c5b3b23402"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("adc8cbe0-8ee8-4098-ae22-e2c5b3b23402"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new BookCreateDto
            {
                Name = "969189246",
                Type = default,
                PublishDate = new DateTime(2016, 10, 5),
                Price = 981761101
            };

            // Act
            var serviceResult = await _booksAppService.CreateAsync(input);

            // Assert
            var result = await _bookRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Name.ShouldBe("969189246");
            result.Type.ShouldBe(default);
            result.PublishDate.ShouldBe(new DateTime(2016, 10, 5));
            result.Price.ShouldBe(981761101);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new BookUpdateDto()
            {
                Name = "0983a2bd",
                Type = default,
                PublishDate = new DateTime(2015, 9, 14),
                Price = 1433012327
            };

            // Act
            var serviceResult = await _booksAppService.UpdateAsync(Guid.Parse("adc8cbe0-8ee8-4098-ae22-e2c5b3b23402"), input);

            // Assert
            var result = await _bookRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Name.ShouldBe("0983a2bd");
            result.Type.ShouldBe(default);
            result.PublishDate.ShouldBe(new DateTime(2015, 9, 14));
            result.Price.ShouldBe(1433012327);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _booksAppService.DeleteAsync(Guid.Parse("adc8cbe0-8ee8-4098-ae22-e2c5b3b23402"));

            // Assert
            var result = await _bookRepository.FindAsync(c => c.Id == Guid.Parse("adc8cbe0-8ee8-4098-ae22-e2c5b3b23402"));

            result.ShouldBeNull();
        }
    }
}