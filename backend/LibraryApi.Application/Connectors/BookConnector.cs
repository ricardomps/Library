using LibraryApi.Application.Common;
using LibraryApi.Application.Mappers;
using LibraryApi.Application.Interfaces.Repositories;
using LibraryApi.Contracts.DTO;
using LibraryApi.Application.Interfaces.Connectors;
using LibraryApi.Domain.Entities;

namespace LibraryApi.Application.Connectors;

public class BookConnector : IBookConnector
{
    private readonly IBookRepository _bookRepository;

    public BookConnector(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<Result<IEnumerable<BookDTO>>> GetBooksAsync(string? searchTerm = null)
    {
        var books = await _bookRepository.GetAllAsync(searchTerm);
        if (books.IsFailure)
            return Result<IEnumerable<BookDTO>>.Failure(books.Error);
        return Result<IEnumerable<BookDTO>>.Success(books.Value.ToDto());
    }

    public async Task<Result> DeleteBookAsync(string id)
    {
        var bookResult = await _bookRepository.GetByIdAsync(id);

        if (bookResult.IsFailure)
            return Result.Failure(bookResult.Error);

        if (bookResult.Value != null && bookResult.Value.CanDelete)
            return await _bookRepository.DeleteAsync(bookResult.Value);
        else
            return Result.Failure("Cannot delete unavailable book!");
    }

    public async Task<Result> BorrowBookAsync(string id)
    {
        try
        {
            var bookResult = await _bookRepository.GetByIdAsync(id);

            if (bookResult.IsFailure)
                return Result.Failure(bookResult.Error);

            var book = bookResult.Value;
            book.Borrow();
            return await _bookRepository.UpdateAsync(book);
        }
        catch (Exception ex)
        {
            if (ex is InvalidOperationException)
                return Result.Failure(ex.Message);
            return Result.Failure("An error occurred while borrowing the book.");
        }

    }

    public async Task<Result> ReturnBookAsync(string id)
    {
        try
        {
            var bookResult = await _bookRepository.GetByIdAsync(id);

            if (bookResult.IsFailure)
                return Result.Failure(bookResult.Error);
            var book = bookResult.Value;
            book.Return();
            return await _bookRepository.UpdateAsync(book);
        }
        catch (Exception ex)
        {
            if (ex is InvalidOperationException)
                return Result.Failure(ex.Message);
            return Result.Failure("An error occurred while returning the book.");
        }
    }

    public async Task<Result<BookDTO>> CreateBookAsync(BookFormDTO bookFormDTO)
    {
        var book = Book.Create(title: bookFormDTO.Title, author: bookFormDTO.Author, publisher: bookFormDTO.Publisher);
        var result = await _bookRepository.InsertAsync(book);
        if (result.IsFailure)
            return Result<BookDTO>.Failure(result.Error);
        return Result<BookDTO>.Success(book.ToDto());
    }

    public async Task<Result<BookDTO>> UpdateBookAsync(string id, BookFormDTO bookFormDTO)
    {
        try
        {
            var bookResult = await _bookRepository.GetByIdAsync(id);

            if (bookResult.IsFailure)
                return Result<BookDTO>.Failure(bookResult.Error);

            var book = bookResult.Value;
            book.Update(
                title: bookFormDTO.Title,
                author: bookFormDTO.Author,
                publisher: bookFormDTO.Publisher
            );
            var updateResult = await _bookRepository.UpdateAsync(book);
            if (updateResult.IsFailure)
                return Result<BookDTO>.Failure(updateResult.Error);

            return Result<BookDTO>.Success(book.ToDto());
        }
        catch (Exception ex)
        {
            if (ex is InvalidOperationException)
                return Result<BookDTO>.Failure(ex.Message);
            return Result<BookDTO>.Failure("An error occurred while updating the book.");
        }
    }
}