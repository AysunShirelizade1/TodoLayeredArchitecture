using Todo.DataAccess.Repositories;
using Todo.Entities.Models;
namespace Todo.Business.Services;

public class TodoService : ITodoService
{
    public Task<bool> CompleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<TodoItem> CreateAsync(string title, string? description, DateTime? dueDate)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<List<TodoItem>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<TodoItem?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }
}