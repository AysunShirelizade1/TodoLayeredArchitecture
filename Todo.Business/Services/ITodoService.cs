using Todo.Entities.Models;
namespace Todo.Business.Services;
public interface ITodoService
{
    Task<List<TodoItem>> GetAllAsync();
    Task<TodoItem?> GetByIdAsync(int id);
    Task<TodoItem> CreateAsync(string title, string? description, DateTime? dueDate);
    Task<bool> CompleteAsync(int id);
    Task<bool> DeleteAsync(int id);
}