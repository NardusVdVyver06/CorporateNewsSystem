using Microsoft.EntityFrameworkCore;
using NewsApi.Database;
using NewsApi.Startup;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.ConfigureServices();
builder.Services.AddDbContext<NewsArticleDbContext>(options =>
    options.UseSqlite(
        builder.Configuration.GetConnectionString("DefaultConnection")));
builder.AddJwtAuthentication();

var app = builder.Build();

app.MapOpenApi();
app.MapScalarApiReference(options =>
{
    options.Title = "News API V1";
});
app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();
app.MapEndpoints();
app.SeedDatabase();

app.Run();
