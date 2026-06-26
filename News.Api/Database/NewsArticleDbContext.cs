using Microsoft.EntityFrameworkCore;
using NewsApi.Database.Models;

namespace NewsApi.Database;

public class NewsArticleDbContext(DbContextOptions<NewsArticleDbContext> options) : DbContext(options)
{
    public DbSet<NewsArticle> NewsArticles => Set<NewsArticle>();
}