
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MySql.Data.MySqlClient;
using Restaurante.Api.Services;
using Restaurante.Borders.Entites;
using Restaurante.Borders.Repositories;
using Restaurante.Borders.UseCases;
using Restaurante.Repositories;
using Restaurante.UseCases;
using System.Data;
using System.Text;


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
builder.Services.AddScoped<ILoginUsuarioUseCase, LoginUsuarioUseCase>();
builder.Services.AddScoped<IListarUsuariosUseCase, ListarUsuariosUseCase>();
builder.Services.AddScoped<IObterUsuarioPorIdUseCase, ObterUsuarioPorIdUseCase>();
builder.Services.AddScoped<IExcluirUsuarioUseCase, ExcluirUsuarioUseCase>();

// Register the JWT token service
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
builder.Services.AddScoped<IPasswordHasher<Usuario>, PasswordHasher<Usuario>>();

var chaveSecreta = builder.Configuration["JwtSettings:SecretKey"];
var chaveSimetrica = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(chaveSecreta));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
        ValidAudience = builder.Configuration["JwtSettings:Audience"],
        IssuerSigningKey = chaveSimetrica
    };

});

builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Informe o token JWT desta maneira: {seu token}"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

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
