using Microsoft.EntityFrameworkCore;
using News.Api.Database.Models;

namespace News.Api.Database;

public class NewsArticleDbContext(DbContextOptions<NewsArticleDbContext> options) : DbContext(options)
{
    public DbSet<NewsArticle> NewsArticles => Set<NewsArticle>();
}