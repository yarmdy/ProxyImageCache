var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMvc();
builder.Services.AddHttpClient();

builder.Services.Configure<FileCacheOptions>(builder.Configuration.GetSection("FileCache"));

var app = builder.Build();


app.UseRouting();
app.MapDefaultControllerRoute();

app.Run();


public class FileCacheOptions
{
    public DateTime LastModified { get; set; }=DateTime.MinValue;
}