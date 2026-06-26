using System.Net;
using News.Shared;

namespace News.Web.Services;

public class NewsApiClient(HttpClient httpClient)
{
    public async Task<List<NewsArticle>> GetArticlesAsync(CancellationToken cancellationToken = default)
    {
        using var response = await httpClient.GetAsync("articles", cancellationToken);

        if (response.StatusCode == HttpStatusCode.NoContent)
        {
            return [];
        }

        response.EnsureSuccessStatusCode();

        var articles = await response.Content.ReadFromJsonAsync<NewsArticlesResponse>(cancellationToken: cancellationToken);
        return articles?.NewsArticles ?? [];
    }
}
