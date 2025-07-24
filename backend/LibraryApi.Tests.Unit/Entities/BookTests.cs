using LibraryApi.Domain.Entities;

namespace LibraryApi.Tests.Unit.Entities;

public class BookTest
{
    [Fact]
    public void Create_ShouldInitializeBookWithGivenValues_AndSetAvailableTrue()
    {
        var book = Book.Create("Title", "Author", "Publisher");

        Assert.NotNull(book.Id);
        Assert.Equal("Title", book.Title);
        Assert.Equal("Author", book.Author);
        Assert.Equal("Publisher", book.Publisher);
        Assert.True(book.Available);
    }

    [Fact]
    public void Update_ShouldChangeBookDetails_WhenAvailable()
    {
        var book = Book.Create("OldTitle", "OldAuthor", "OldPublisher");
        book.Update("NewTitle", "NewAuthor", "NewPublisher");

        Assert.Equal("NewTitle", book.Title);
        Assert.Equal("NewAuthor", book.Author);
        Assert.Equal("NewPublisher", book.Publisher);
    }

    [Fact]
    public void Update_ShouldThrow_WhenBookIsUnavailable()
    {
        var book = Book.Create("Title", "Author", "Publisher");
        book.Borrow();

        Assert.False(book.Available);
        Assert.Throws<InvalidOperationException>(() =>
            book.Update("NewTitle", "NewAuthor", "NewPublisher"));
    }

    [Fact]
    public void Borrow_ShouldSetAvailableFalse_WhenBookIsAvailable()
    {
        var book = Book.Create("Title", "Author", "Publisher");
        book.Borrow();

        Assert.False(book.Available);
    }

    [Fact]
    public void Borrow_ShouldThrow_WhenBookIsUnavailable()
    {
        var book = Book.Create("Title", "Author", "Publisher");
        book.Borrow();

        Assert.Throws<InvalidOperationException>(() => book.Borrow());
    }

    [Fact]
    public void Return_ShouldSetAvailableTrue_WhenBookIsUnavailable()
    {
        var book = Book.Create("Title", "Author", "Publisher");
        book.Borrow();
        book.Return();

        Assert.True(book.Available);
    }

    [Fact]
    public void Return_ShouldThrow_WhenBookIsAvailable()
    {
        var book = Book.Create("Title", "Author", "Publisher");

        Assert.Throws<InvalidOperationException>(() => book.Return());
    }

    [Fact]
    public void CanDelete_ShouldBeTrue_WhenBookIsAvailable()
    {
        var book = Book.Create("Title", "Author", "Publisher");
        Assert.True(book.CanDelete);
    }

    [Fact]
    public void CanDelete_ShouldBeFalse_WhenBookIsUnavailable()
    {
        var book = Book.Create("Title", "Author", "Publisher");
        book.Borrow();
        Assert.False(book.CanDelete);
    }
}