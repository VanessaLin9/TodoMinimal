using WebApiA.EndPoints;
using XitMent.Core.Serilog;
using XitMent.Core.Serilog.Extensions;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpClient();

builder.Services.AddSwaggerGen();
builder.UseXitMentSerilog(new SerilogSetting(
    Environment.GetEnvironmentVariable("ASPNETCORE_ENVIROMENT").ToDeployEnvironment(),
    builder.Configuration["Project"]!,
    Environment.GetEnvironmentVariable("POD_NAME"),
    Environment.GetEnvironmentVariable("NODE_NAME"),
    Environment.GetEnvironmentVariable("ELK_USERNAME"),
    Environment.GetEnvironmentVariable("ELK_PASSWORD")
));



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapLunchApi();

app.Run();