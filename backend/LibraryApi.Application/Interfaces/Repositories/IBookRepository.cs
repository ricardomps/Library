using LibraryApi.Application.Common;
using LibraryApi.Domain.Entities;

namespace LibraryApi.Application.Interfaces.Repositories;

public interface IBookRepository
{
    Task<Result<IEnumerable<Book>>> GetAllAsync(string? searchTerm = null);
    Task<Result<Book>> GetByIdAsync(string id);
    Task<Result> InsertAsync(Book book);
    Task<Result> UpdateAsync(Book book);
    Task<Result> DeleteAsync(Book book);
}