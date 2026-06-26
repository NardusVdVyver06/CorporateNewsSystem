using Microsoft.EntityFrameworkCore;
using News.Api.Database;
using News.Api.Models;
using News.Shared;

namespace News.Api.Services;

public class NewsArticleService(NewsArticleDbContext dbContext)
{
    public async Task<IResult> GetNewsArticles()
    {
        try
        {
            var articles = await dbContext.NewsArticles
                .OrderByDescending(newsArticle => newsArticle.PublishDate)
                .Select(newsArticle => new NewsArticle
                {
                    Id = newsArticle.Id,
                    PublishDate = newsArticle.PublishDate,
                    Author = newsArticle.Author,
                    Title = newsArticle.Title,
                    Content = newsArticle.Content
                }).ToListAsync();

            if (articles.Count == 0)
            {
                return Results.NoContent();
            }
            
            return Results.Ok(new NewsArticlesResponse
            {
                NewsArticles = articles
            });
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }
    
    #region Admin

    public async Task<IResult> CreateNewsArticle(CreateNewsArticleRequest request)
    {
        try
        {
            var newsArticle = new Database.Models.NewsArticle
            {
                PublishDate = DateTime.UtcNow,
                Author = request.Author,
                Title = request.Title,
                Content = request.Content
            };
            
            dbContext.NewsArticles.Add(newsArticle);

            await dbContext.SaveChangesAsync();
            
            return Results.Ok(newsArticle.Id);
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }
    
    public async Task<IResult> UpdateNewsArticle(UpdateNewsArticleRequest request)
    {
        try
        {
            var newsArticle = await dbContext.NewsArticles.Where(newsArticle => newsArticle.Id == request.Id).FirstOrDefaultAsync();
            if (newsArticle == null)
            {
                return Results.NotFound();
            }

            if (!string.IsNullOrEmpty(request.Author))
            {
                newsArticle.Author = request.Author;
            }
            
            if (!string.IsNullOrEmpty(request.Title))
            {
                newsArticle.Title = request.Title;
            }
            
            if (!string.IsNullOrEmpty(request.Content))
            {
                newsArticle.Content = request.Content;
            }

            await dbContext.SaveChangesAsync();
            
            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }
    
    public async Task<IResult> DeleteNewsArticle(int newsArticleId)
    {
        try
        {
            var newsArticle = await dbContext.NewsArticles.Where(newsArticle => newsArticle.Id == newsArticleId).FirstOrDefaultAsync();
            if (newsArticle == null)
            {
                return Results.NotFound();
            }
            
            dbContext.NewsArticles.Remove(newsArticle);
            
            await dbContext.SaveChangesAsync();
            
            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }

    #endregion
}