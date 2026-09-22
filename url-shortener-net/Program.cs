using Scalar.AspNetCore;
using url_shortener_net.Data;
using Microsoft.EntityFrameworkCore;
using url_shortener_net.UseCases;
using url_shortener_net.Interface;
using url_shortener_net.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddScoped<IUrl, UrlRepository>();
builder.Services.AddScoped<CreateUrlUseCase>();
builder.Services.AddScoped<GetRedirectionUseCase>();

var connectionString = builder.Configuration.GetConnectionString("Connection");
builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(connectionString));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
