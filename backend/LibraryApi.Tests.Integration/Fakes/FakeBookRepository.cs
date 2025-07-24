using LibraryApi.Application.Common;
using LibraryApi.Application.Interfaces.Repositories;
using LibraryApi.Domain.Entities;

namespace LibraryApi.Tests.Integration.Fakes;

public class FakeBookRepository : IBookRepository
{
    private static List<Book> Books = new List<Book>();

    public void AddBook(Book book)
    {
        Books.Add(book);
    }

    public Task<Result> DeleteAsync(Book book)
    {
        Books.Remove(book);
        return Task.FromResult(Result.Success());
    }

    public Task<Result<IEnumerable<Book>>> GetAllAsync(string? searchTerm = null)
    {
        return Task.FromResult(Result<IEnumerable<Book>>.Success(Books));
    }

    public Task<Result<Book>> GetByIdAsync(string id)
    {
        var book = Books.FirstOrDefault(b => b.Id == id);
        if (book == null)
        {
            return Task.FromResult(Result<Book>.Failure("Book not found"));
        }
        return Task.FromResult(Result<Book>.Success(book));
    }

    public Task<Result> InsertAsync(Book book)
    {
        Books.Add(book);
        return Task.FromResult(Result.Success());
    }

    public Task<Result> UpdateAsync(Book book)
    {
        var existingBook = Books.FirstOrDefault(b => b.Id == book.Id);
        if (existingBook == null)
        {
            return Task.FromResult(Result.Failure("Book not found"));
        }

        Books.Remove(existingBook);
        Books.Add(book);
        return Task.FromResult(Result.Success());
    }
}