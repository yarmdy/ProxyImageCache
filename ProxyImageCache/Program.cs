var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMvc();
builder.Services.AddHttpClient();

var app = builder.Build();


app.UseRouting();
app.MapDefaultControllerRoute();

app.Run();
