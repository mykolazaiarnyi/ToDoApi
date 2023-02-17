using Microsoft.EntityFrameworkCore;
using ToDoApi.BusinessLogic.Implementation;
using ToDoApi.BusinessLogic.Interfaces;
using ToDoApi.Domain;
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

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ToDoContext>();
    await context.Database.EnsureCreatedAsync();
    var userId = new Guid("8360e917-5e1d-44c7-a449-e115b69280bb");
    var user = await context.Users.FirstOrDefaultAsync(x => x.Id.Equals(userId));
    if (user is null)
    {
        user = new User
        {
            Id = userId,
            Name = "John Doe",
            ToDoItems = new List<ToDoItem>()
        };

        await context.Users.AddAsync(user);
        await context.SaveChangesAsync();
    }
}

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
