using DemoApp.Api;
using DemoApp.Domain.Products;
using DemoApp.Infrastructure;
using DemoApp.Infrastructure.Translations;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DemoContext>(o =>o.UseSqlite(builder.Configuration.GetConnectionString("SQLite")));
builder.Services.AddInfrastructure();
builder.Services.AddScoped<ProductService>();
builder.Services.AddHttpClient<ITranslator, TranslatorHttpImpl>(httpClient =>
{
    httpClient.BaseAddress = new Uri("https://translate.googleapis.com/");
});
builder.Services.AddSingleton<LoggingMiddleware>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
using (var context = scope.ServiceProvider.GetRequiredService<DemoContext>())
{
    context.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseMiddleware<LoggingMiddleware>();
app.UseAuthorization();

app.MapControllers();

app.Run();
