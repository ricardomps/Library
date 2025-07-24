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

    public async Task<Result> DeleteAsync(Book book)
    {
        try
        {
            _db.Books.Remove(book);
            await _db.SaveChangesAsync();
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure($"Error deleting book: {ex.Message}");
        }
    }

    public async Task<Result<IEnumerable<Book>>> GetAllAsync(string? searchTerm = null)
    {
        try
        {
            var books = await _db.Books
                .AsNoTracking()
                .Where(b => string.IsNullOrEmpty(searchTerm)
                    || EF.Functions.ILike(b.Title, $"%{searchTerm}%")
                    || EF.Functions.ILike(b.Author, $"%{searchTerm}%"))
                .OrderBy(b => b.Title)
                .ToListAsync();
            return Result<IEnumerable<Book>>.Success(books);
        }
        catch (Exception ex)
        {
            return Result<IEnumerable<Book>>.Failure($"Error retrieving books: {ex.Message}");
        }
    }

    public async Task<Result<Book>> GetByIdAsync(string id)
    {
        try
        {
            var book = await _db.Books.FindAsync(id);
            if (book == null) return Result<Book>.Failure("Book not found");
            return Result<Book>.Success(book);
        }
        catch (Exception ex)
        {
            return Result<Book>.Failure($"Error retrieving book: {ex.Message}");
        }
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

    public async Task<Result> UpdateAsync(Book book)
    {
        try
        {
            _db.Books.Update(book);
            await _db.SaveChangesAsync();
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure($"Error updating book: {ex.Message}");
        }
    }
}