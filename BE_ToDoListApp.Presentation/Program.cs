using BE_ToDoListApp.Application.Utils;
using BE_ToDoListApp.Infrastructure.DependencyInjections;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddProblemDetails();

builder.Services.AddOpenApi();

builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddAuthorization();

EncryptUtil.Init(builder.Configuration);

var app = builder.Build();
app.UseInfrastructurePolicy();

// Configure the HTTP request pipeline.
app.MapOpenApi();
//Temporary Setup (Missing JWT Setup for Scalar)
app.MapScalarApiReference(options =>
{
    options.Title = "Scalar UI, Hi User";
    options.Theme = ScalarTheme.BluePlanet;
    options.DefaultHttpClient = new(ScalarTarget.CSharp, ScalarClient.HttpClient);
    options.ShowSidebar = true;
});


app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();
