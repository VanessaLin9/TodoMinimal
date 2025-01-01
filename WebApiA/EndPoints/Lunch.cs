namespace WebApiA.EndPoints;

public static class Lunch
{
    public static IEndpointRouteBuilder MapLunchApi(this IEndpointRouteBuilder app)
    {
        var lunchApiGroup = app.MapGroup("Lunch");
        
        lunchApiGroup.MapGet("/", LoginToB);
        
        return app;
    }
    
    private static IResult LoginToB()
    {
        return Results.Ok();
    }
}