using System.ComponentModel.DataAnnotations;

namespace NewsApi.Database.Models;

public class NewsArticle
{
    [Key]
    public int Id { get; set; }
    
    public required DateTime PublishDate { get; set; }
    
    [MaxLength(100)]
    public required string Author { get; set; }
    
    [MaxLength(100)]
    public required string Title { get; set; }
    
    [MaxLength(2000)]
    public required string Content { get; set; }
}