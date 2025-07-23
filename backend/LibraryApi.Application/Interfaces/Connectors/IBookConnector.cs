using LibraryApi.Application.Common;
using LibraryApi.Contracts.DTO;

namespace LibraryApi.Application.Interfaces.Connectors;

public interface IBookConnector
{
    Task<Result<IEnumerable<BookDTO>>> GetBooksAsync(string? searchTerm = null);
    Task<Result<BookDTO>> CreateBookAsync(BookFormDTO bookFormDTO);
    Task<Result<BookDTO>> UpdateBookAsync(string id, BookFormDTO bookFormDTO);
    Task<Result> DeleteBookAsync(string id);
    Task<Result> BorrowBookAsync(string id);
    Task<Result> ReturnBookAsync(string id);
}