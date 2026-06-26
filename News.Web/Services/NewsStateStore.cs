using News.Shared;

namespace News.Web.Services;

public class NewsStateStore
{
    // NOTE: This should rather get cached using a proper caching strategy, but just using this for the work item
    private List<NewsArticle> _articles = [];

    public void SetArticles(IEnumerable<NewsArticle> articles)
    {
        _articles = articles.ToList();
    }

    public NewsArticle? GetArticle(int newsArticleId)
    {
        return _articles.FirstOrDefault(article => article.Id == newsArticleId);
    }
}
