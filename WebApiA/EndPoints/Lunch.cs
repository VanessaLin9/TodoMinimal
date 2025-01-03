using System.Text;
using Newtonsoft.Json;

namespace WebApiA.EndPoints;

public static class Lunch
{

    public static IEndpointRouteBuilder MapLunchApi(this IEndpointRouteBuilder app)
    {
        var lunchApiGroup = app.MapGroup("Lunch");
        
        lunchApiGroup.MapGet("/", LoginToB);
        
        return app;
    }
    
    private static async Task<IResult> LoginToB(HttpClient httpClient)
    {
        var response =await PostAsync(httpClient, new Request
        {
            Message = "GG login"
        });
        return Results.Ok(response);
    }

    private static async Task<Response?> PostAsync(HttpClient httpClient, Request request)
    {
        var requestContent = JsonConvert.SerializeObject(request);
        var httpRequestMessage = new HttpRequestMessage();
        httpRequestMessage.Content = new StringContent(requestContent, Encoding.UTF8, "application/json");
        httpRequestMessage.Method = HttpMethod.Post;
        httpRequestMessage.RequestUri = new Uri("http://localhost:5006/login");

        var response = await (await httpClient.SendAsync(httpRequestMessage)).Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<Response>(response);
    }
}

internal class Response
{
    public string Message { get; set; } = String.Empty;
}

internal class Request
{
    public string Message { get; set; } = String.Empty;
}