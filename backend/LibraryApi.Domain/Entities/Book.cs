namespace LibraryApi.Domain.Entities;

public class Book
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string Publisher { get; set; }
    public bool Available { get; set; }
    public bool CanDelete => Available;

    public static Book Create(string title, string author, string publisher)
    {
        return new Book
        {
            Id = Guid.NewGuid().ToString(),
            Title = title,
            Author = author,
            Publisher = publisher,
            Available = true
        };
    }

    public void Update(string title, string author, string publisher)
    {
        if (Available)
        {
            Title = title;
            Author = author;
            Publisher = publisher;
        }
        else
            throw new InvalidOperationException("Cannot update unavailable book!");
    }

    public void Borrow()
    {
        if (Available)
            Available = false;
        else
            throw new InvalidOperationException("Cannot borrow unavailable book!");
    }
    
    public void Return()
    {
        if (!Available)
            Available = true;
        else
            throw new InvalidOperationException("Cannot return available book!");
    }
}