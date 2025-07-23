using LibraryApi.Application.Common;
using LibraryApi.Application.Interfaces.Repositories;
using LibraryApi.Data;
using LibraryApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryApi.Integrations.Repositories;

public class BookRepository : IBookRepository
{
    private readonly ApplicationDbContext _db;

    public BookRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public Task<Result> DeleteAsync(Book book)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<IEnumerable<Book>>> GetAllAsync(string? searchTerm = null)
    {
        var books = await _db.Books
            .Where(b => string.IsNullOrEmpty(searchTerm) || b.Title.Contains(searchTerm) || b.Author.Contains(searchTerm))
            .ToListAsync();
        return Result<IEnumerable<Book>>.Success(books);
    }

    public Task<Result<Book>> GetByIdAsync(string id)
    {
        throw new NotImplementedException();
    }

    public async Task<Result> InsertAsync(Book book)
    {
        try
        {
            await _db.Books.AddAsync(book);
            await _db.SaveChangesAsync();
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure($"Error inserting book: {ex.Message}");
        }
    }

    public Task<Result> UpdateAsync(Book book)
    {
        throw new NotImplementedException();
    }
}