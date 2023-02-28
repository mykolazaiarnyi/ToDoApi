using Microsoft.EntityFrameworkCore;
using ToDoApi.API.Helpers;
using ToDoApi.API.Middlewares;
using ToDoApi.BusinessLogic.Implementation;
using ToDoApi.BusinessLogic.Interfaces;
using ToDoApi.Infrastructure.Data;
using ToDoApi.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var cosmosConnectionString = builder.Configuration.GetConnectionString("Cosmos");
builder.Services.AddDbContext<ToDoContext>(opt => opt.UseCosmos(cosmosConnectionString, "ToDoDb"));

builder.Services.AddScoped<IToDoService, ToDoService>();
builder.Services.AddScoped<IToDoRepository, ToDoRepository>();
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

await app.SeedUserAsync();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
