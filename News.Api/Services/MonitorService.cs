namespace News.Api.Services;

public class MonitorService(IConfiguration configuration)
{
    public IResult MonitorEnvironment()
    {
        return Results.Content($"News Api - {configuration.GetValue<string>("Environment")}");
    }
}