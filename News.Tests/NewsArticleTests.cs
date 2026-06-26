using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using News.Api.Models;
using News.Shared;

namespace News.Tests;

public class NewsArticleTests(WebApplicationFactory<Program> factory) : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client = factory.CreateClient();

    [Fact]
    public async Task Monitor_Returns_Ok()
    {
        // Act
        var response = await _client.GetAsync("/monitor");
        var monitorResponse = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Contains("News Api", monitorResponse);
    }
    
    [Fact]
    public async Task GetNewsArticles_Returns_All_Articles()
    {
        // Act
        var response = await _client.GetAsync("/articles");
        var newsArticlesResponse = await response.Content.ReadFromJsonAsync<NewsArticlesResponse>();

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(newsArticlesResponse);
        Assert.NotNull(newsArticlesResponse.NewsArticles);
        Assert.NotEmpty(newsArticlesResponse.NewsArticles);
    }
    
    [Fact]
    public async Task AdminLogin_Returns_Unauthorized()
    {
        // Arrange
        var loginRequest = new LoginRequest
        {
            Username = "incorrect",
            Password = "incorrect"
        };
        
        // Act
        var response = await _client.PostAsJsonAsync("/admin/login", loginRequest);

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
    
    [Fact]
    public async Task AdminLogin_Returns_Token()
    {
        // Arrange
        var loginRequest = new LoginRequest
        {
            Username = "admin",
            Password = "Password123!"
        };
        
        // Act
        var response = await _client.PostAsJsonAsync("/admin/login", loginRequest);
        var loginResponse = await response.Content.ReadFromJsonAsync<LoginResponse>();

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(loginResponse);
        Assert.NotNull(loginResponse.Token);
    }
    
    [Fact]
    public async Task AdminCreateArticle_ReturnsNewArticle_ThenDeleteArticle()
    {
        // ==================== Admin Login ============================
        
        // Arrange
        var loginRequest = new LoginRequest
        {
            Username = "admin",
            Password = "Password123!"
        };
        
        // Act
        var response = await _client.PostAsJsonAsync("/admin/login", loginRequest);
        var loginResponse = await response.Content.ReadFromJsonAsync<LoginResponse>();

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(loginResponse);
        Assert.NotNull(loginResponse.Token);
        
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", loginResponse.Token);
        
        // ==================== Create News Article ============================
        
        // Arrange
        var createNewsArticleRequest = new CreateNewsArticleRequest
        {
            Author = "Integration Test",
            Title = "Integration Test",
            Content = "Some content"
        };
        
        // Act
        response = await _client.PostAsJsonAsync("/create/article", createNewsArticleRequest);
        var createArticleResponse = await response.Content.ReadFromJsonAsync<int>();
        
        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.True(createArticleResponse > 0);
        
        // ==================== Get Articles ============================
        
        // Act
        response = await _client.GetAsync("/articles");
        var newsArticlesResponse = await response.Content.ReadFromJsonAsync<NewsArticlesResponse>();

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(newsArticlesResponse);
        Assert.NotNull(newsArticlesResponse.NewsArticles);
        Assert.NotEmpty(newsArticlesResponse.NewsArticles);
        
        // ==================== Delete Article ============================
        
        // Act
        response = await _client.DeleteAsync($"/delete/article?newsArticleId={createArticleResponse}");
        
        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        
        // ==================== Get Articles ============================
        
        // Act
        response = await _client.GetAsync("/articles");
        var newsArticlesAfterDeletionResponse = await response.Content.ReadFromJsonAsync<NewsArticlesResponse>();

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(newsArticlesAfterDeletionResponse);
        Assert.NotNull(newsArticlesAfterDeletionResponse.NewsArticles);
        Assert.NotEqual(newsArticlesAfterDeletionResponse.NewsArticles.Count, newsArticlesResponse.NewsArticles.Count); // NOTE: Count must be different after deletion
    }
}
