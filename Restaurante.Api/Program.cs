using Microsoft.AspNetCore.Diagnostics;
using MySql.Data.MySqlClient;
using Restaurante.Api.Services;
using Restaurante.Borders.Repositories;
using Restaurante.Borders.UseCases;
using Restaurante.Repositories;
using Restaurante.UseCases;
using System.Data;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IDbConnection>(sp =>
    new MySqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register the repository
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
// Register the use case
builder.Services.AddScoped<IAdicionarUsuarioUseCase, AdicionarUsuarioUseCase>();


var app = builder.Build();

app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        context.Response.ContentType = "application/json";

        var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;

        context.Response.StatusCode = exception switch
        {
            HttpStatusCodeException httpEx => httpEx.StatusCode,
            _ => StatusCodes.Status500InternalServerError
        };

        var problem = new
        {
            status = context.Response.StatusCode,
            title = "Erro ao processar a solicitação",
            detail = exception?.Message
        };

        await context.Response.WriteAsJsonAsync(problem);
    });
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
