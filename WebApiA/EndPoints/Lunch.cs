using System.Text;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;

namespace WebApiA.EndPoints;

public static class Lunch
{

    public static IEndpointRouteBuilder MapLunchApi(this IEndpointRouteBuilder app)
    {
        var lunchApiGroup = app.MapGroup("Lunch");
        
        lunchApiGroup.MapGet("/", Hello);
        lunchApiGroup.MapPost("/getaa", LoginFrom7);
        
        return app;
    }

    private static IResult Hello(HttpContext context)
    {
        return Results.Ok("Hello!");
    }

    private static IResult LoginFrom7(HttpClient client, HttpContext context, LoginRequest request)
    {
        Console.WriteLine(context.Request.Headers.Authorization);
        return Results.Ok(new LoginResponse
        {
            Message = $"{request.UserName} is login success! from {context.Connection.RemoteIpAddress}",
            Time = DateTime.Now
        });
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

internal class LoginResponse : Response
{
    public DateTime Time { get; set; }
}

internal class LoginRequest : Request
{
    public string UserName { get; set; }
}

internal class Response
{
    public string Message { get; set; } = String.Empty;
}

internal class Request
{
    public string Message { get; set; } = String.Empty;
}