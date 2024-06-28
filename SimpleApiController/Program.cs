using SimpleApiController.Services;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllers();
    builder.Services.AddScoped<ProductsService>();
}
var app = builder.Build();
{
    app.MapControllers();
}

app.Run();
