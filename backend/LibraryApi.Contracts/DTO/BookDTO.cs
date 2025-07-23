namespace LibraryApi.Contracts.DTO;

public class BookDTO
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string Publisher { get; set; }
    public bool Available { get; set; }
}