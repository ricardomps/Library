using System.Net;
using System.Net.Http.Json;
using LibraryApi.Application.Interfaces.Repositories;
using LibraryApi.Contracts.DTO;
using LibraryApi.Domain.Entities;
using LibraryApi.Tests.Integration.Fakes;

namespace LibraryApi.Tests.Integration.Controllers
{
    public class BooksControllerTestsTest : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;
        private FakeBookRepository fakeBookRepository; 

        public BooksControllerTestsTest(CustomWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
            fakeBookRepository = (FakeBookRepository)factory.Services.GetService(typeof(IBookRepository));
        }

        [Fact]
        public async Task GetBooks_ShouldReturnSuccess()
        {
            var response = await _client.GetAsync("books");
            Assert.True(response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task CreateBook_ShouldReturnCreated()
        {
            var newBook = new BookFormDTO { Title = "Test Book", Author = "Test Author", Publisher = "Test Publisher" };
            var response = await _client.PostAsJsonAsync("books", newBook);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task BorrowBook_ShouldReturnNoContent()
        {
            var book = Book.Create("Book", "Author", "Publisher");
            fakeBookRepository.AddBook(book);
            var response = await _client.PostAsync($"books/{book.Id}/borrow", null);
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }
        
        [Fact]
        public async Task BorrowBook_ShouldReturnBadRequest_WhenBookIsBorrowed()
        {
            var book = Book.Create("Book", "Author", "Publisher");
            book.Borrow();
            fakeBookRepository.AddBook(book);
            var response = await _client.PostAsync($"books/{book.Id}/borrow", null);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task ReturnBook_ShouldReturnNoContent()
        {
            var book = Book.Create("Book", "Author", "Publisher");
            book.Borrow();
            fakeBookRepository.AddBook(book);
            var response = await _client.PostAsync($"books/{book.Id}/return", null);
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }
        
        [Fact]
        public async Task ReturnBook_ShouldReturnBadRequest_WhenBookIsNotBorrowed()
        {
            var book = Book.Create("Book", "Author", "Publisher");
            fakeBookRepository.AddBook(book);
            var response = await _client.PostAsync($"books/{book.Id}/return", null);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task UpdateBook_ShouldReturnOk_WhenBookExists()
        {
            var book = Book.Create("Book", "Author", "Publisher");
            fakeBookRepository.AddBook(book);
            var bookForm = new BookFormDTO { Title = "Updated Book", Author = "Updated Author", Publisher = "Updated Publisher" };
            var response = await _client.PutAsJsonAsync($"books/{book.Id}", bookForm);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var updatedBook = (await fakeBookRepository.GetByIdAsync(book.Id)).Value;
            Assert.Equal(bookForm.Title, updatedBook.Title);
            Assert.Equal(bookForm.Author, updatedBook.Author);
            Assert.Equal(bookForm.Publisher, updatedBook.Publisher);
        }

        [Fact]
        public async Task DeleteBook_ShouldReturnNoContent_WhenBookExists()
        {
            var book = Book.Create("Book", "Author", "Publisher");
            fakeBookRepository.AddBook(book);
            var response = await _client.DeleteAsync($"books/{book.Id}");
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact]
        public async Task DeleteBook_ShouldReturnBadRequest_WhenBookIsBorrowed()
        {
            var book = Book.Create("Book", "Author", "Publisher");
            book.Borrow();
            fakeBookRepository.AddBook(book);
            var response = await _client.DeleteAsync($"books/{book.Id}");
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}