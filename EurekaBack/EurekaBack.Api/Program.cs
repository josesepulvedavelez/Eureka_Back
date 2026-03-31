using EurekaBack.Infrastructure;
using EurekaBack.Api.Middleware;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json");
var configuration = builder.Configuration;

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

var connectionString = configuration.GetConnectionString("cadena") ?? throw new InvalidOperationException("Connection string 'cadena' not found.");
builder.Services.AddInfrastructure(connectionString);

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(EurekaBack.Application.Features.Articulos.Commands.CreateArticuloCommand).Assembly));

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssembly(typeof(EurekaBack.Application.Features.Articulos.Commands.CreateArticuloCommand).Assembly);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
