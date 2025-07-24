using LibraryApi.Contracts.DTO;
using LibraryApi.Domain.Entities;

namespace LibraryApi.Application.Mappers;

public static class BookMapper
{
    public static BookDTO ToDto(this Book book)
    {
        if (book == null) return null;

        return new BookDTO
        {
            Id = book.Id,
            Title = book.Title,
            Author = book.Author,
            Publisher = book.Publisher,
            Available = book.Available
        };
    }
    
    public static IEnumerable<BookDTO> ToDto(this IEnumerable<Book>? books)
    {
        if (books == null) return Array.Empty<BookDTO>();

        return books.Select(book => book.ToDto());
    }
}