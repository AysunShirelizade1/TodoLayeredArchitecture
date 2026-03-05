using Microsoft.EntityFrameworkCore;
using Todo.DataAccess.Context;
// using Todo.DataAccess.Repositories;
// using Todo.Business.Services;

var builder = WebApplication.CreateBuilder(args);

// ✅ DbContext - MÜTLƏQ Build-dən əvvəl
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

// ✅ Controllers + Swagger (classic)
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ✅ Əgər service/repo yazmısansa, aç
// builder.Services.AddScoped<ITodoRepository, TodoRepository>();
// builder.Services.AddScoped<ITodoService, TodoService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();