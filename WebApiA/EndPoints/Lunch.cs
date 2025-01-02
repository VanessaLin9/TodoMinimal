using Newtonsoft.Json;

namespace WebApiA.EndPoints;

public static class Lunch
{
    private static HttpClient _httpClient;

    public static IEndpointRouteBuilder MapLunchApi(this IEndpointRouteBuilder app)
    {
        var lunchApiGroup = app.MapGroup("Lunch");
        
        lunchApiGroup.MapGet("/", LoginToB);
        
        return app;
    }
    
    private static IResult LoginToB()
    {
        var postAsync = PostAsync(new Request
        {
            Message = "GG login"
        });
        return Results.Ok(postAsync);
    }

    private static async Task<Response?> PostAsync(Request request)
    {
        var requestContent = JsonConvert.SerializeObject(request);
        var httpRequestMessage = new HttpRequestMessage();
        httpRequestMessage.Content = new StringContent(requestContent);
        httpRequestMessage.Method = HttpMethod.Post;
        httpRequestMessage.RequestUri = new Uri("/localhost");

        var response = await (await _httpClient.SendAsync(httpRequestMessage)).Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<Response>(response);
    }
}

internal class Response
{
    public string Message { get; set; }
}

internal class Request
{
    public string Message { get; set; }
}