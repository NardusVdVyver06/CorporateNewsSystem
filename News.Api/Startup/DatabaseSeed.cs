using Microsoft.EntityFrameworkCore;
using NewsApi.Database;
using NewsApi.Database.Models;

namespace NewsApi.Startup;

public static class DatabaseSeed
{
    extension(WebApplication app)
    {
        public void SeedDatabase()
        {
            using var scope = app.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<NewsArticleDbContext>();

            db.Database.Migrate();

            if (db.NewsArticles.Any())
            {
                return;
            }
            
            db.NewsArticles.AddRange(
                new NewsArticle
                {
                    PublishDate = DateTime.UtcNow,
                    Author = "James Owen",
                    Title = "NET 10 Ushers in a New Era of High-Performance Cloud Development",
                    Content = "Microsoft has announced the general availability of .NET 10, the latest version of its unified development platform. Building on the foundations established by previous releases, .NET 10 focuses on improving runtime performance, streamlining developer workflows, and expanding support for modern cloud-native applications.<br>One of the headline improvements is the enhanced runtime, which delivers noticeable performance gains across common workloads. Developers can expect faster application startup times, reduced memory consumption, and improved throughput, making it easier to build responsive applications that scale efficiently under heavy demand.<br>As businesses continue to modernize their technology stacks, .NET 10 positions itself as a compelling platform for building everything from lightweight web APIs to enterprise-scale distributed systems. With its emphasis on performance, productivity, and cloud readiness, Microsoft's latest release represents another significant step forward for the .NET ecosystem.",
                },
                new NewsArticle
                {
                    PublishDate = DateTime.UtcNow,
                    Author = "Frank Dessington",
                    Title = "Quantum Computing Takes Another Step Toward Commercial Reality",
                    Content = "Quantum computing continues to make headlines as technology companies and research institutions report significant progress in developing practical quantum systems. While the technology remains in its early stages, recent breakthroughs suggest that commercial applications may be closer than many experts previously anticipated.<br>Unlike traditional computers that process information using binary bits, quantum computers leverage quantum bits, or qubits, allowing them to perform certain complex calculations far more efficiently. Researchers believe this could eventually revolutionize industries such as pharmaceuticals, logistics, financial modelling, and materials science.<br>As investment in quantum technologies grows worldwide, many analysts believe the coming decade will be defined by steady progress rather than overnight transformation. While traditional computing will remain the foundation of modern software development, quantum computing is steadily positioning itself as a powerful complement for solving some of the world's most complex computational problems.",
                });

            db.SaveChanges();
        }
    }
}