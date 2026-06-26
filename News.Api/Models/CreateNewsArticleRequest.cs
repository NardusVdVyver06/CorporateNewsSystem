namespace NewsApi.Models;

public class CreateNewsArticleRequest
{
    public required string Author { get; set; }
    
    public required string Title { get; set; }
    
    public required string Content { get; set; }
}