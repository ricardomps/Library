using LibraryApi.Application.Interfaces.Connectors;
using LibraryApi.Contracts.DTO;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Controllers;

[Route("[controller]")]
[ApiController]
[Produces("application/json")]
public class BooksController : ControllerBase
{
    private readonly IBookConnector _bookConnector;

    public BooksController(IBookConnector bookConnector)
    {
        _bookConnector = bookConnector;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<BookDTO>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<BookDTO>>> GetBooks([FromQuery] string? searchTerm = null)
    {
        var books = await _bookConnector.GetBooksAsync(searchTerm);
        if (books.IsFailure)
            return BadRequest(books.Error);
        return Ok(books.Value);
    }

    [HttpPost]
    [ProducesResponseType(typeof(BookDTO), StatusCodes.Status201Created)]
    public async Task<ActionResult<BookDTO>> CreateBook([FromBody] BookFormDTO bookFormDTO)
    {
        var result = await _bookConnector.CreateBookAsync(bookFormDTO);
        if (result.IsFailure)
            return BadRequest(result.Error);
        return StatusCode(StatusCodes.Status201Created, result.Value);
    }
}