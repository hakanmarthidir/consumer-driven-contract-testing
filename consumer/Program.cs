using consumer.Controllers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<ICategoryService, CategoryService>();
builder.Services.AddControllers();
builder.Services.AddHttpClient();

string httpClientName = "Category";
builder.Services.AddHttpClient(
    httpClientName ?? "",
    client =>
    {
        client.BaseAddress = new Uri("http://localhost:9222/");
    });

var app = builder.Build();

app.UseAuthorization();
app.MapControllers();

app.Run();
