using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using News.Api.Models;
using News.Api.Services;

namespace News.Api.Startup;

public static class EndpointMappingSetup
{
    extension(WebApplication app)
    {
        public void MapEndpoints()
        {
            app
                .MapBaseEndpoints()
                .MapAdminEndpoints();
        }

        private WebApplication MapBaseEndpoints()
        {
            app.MapGet("/monitor", ([FromServices] MonitorService monitorService)
                    => monitorService.MonitorEnvironment())
                .Produces<string>();
            
            app.MapGet("/articles", ([FromServices] NewsArticleService newsArticleService)
                    => newsArticleService.GetNewsArticles())
                .Produces<NewsArticlesResponse>();

            return app;
        }

        private WebApplication MapAdminEndpoints()
        {
            app.MapPost("/admin/login", ([FromServices] AuthService authService, [FromBody] LoginRequest loginRequest)
                    => authService.AdminLogin(loginRequest))
                .Produces<LoginResponse>();
            
            app.MapPost("/create/article", [Authorize(Roles = AuthorizationRoles.Admin)]  ([FromServices] NewsArticleService newsArticleService, [FromBody] CreateNewsArticleRequest createNewsArticleRequest)
                    => newsArticleService.CreateNewsArticle(createNewsArticleRequest))
                .Produces(StatusCodes.Status200OK);
            
            app.MapPut("/update/article", [Authorize(Roles = AuthorizationRoles.Admin)]  ([FromServices] NewsArticleService newsArticleService, [FromBody] UpdateNewsArticleRequest updateNewsArticleRequest)
                    => newsArticleService.UpdateNewsArticle(updateNewsArticleRequest))
                .Produces(StatusCodes.Status200OK);
            
            app.MapDelete("/delete/article", [Authorize(Roles = AuthorizationRoles.Admin)]  ([FromServices] NewsArticleService newsArticleService, [FromQuery] int newsArticleId)
                    => newsArticleService.DeleteNewsArticle(newsArticleId))
                .Produces(StatusCodes.Status200OK);

            return app;
        }
    }
}