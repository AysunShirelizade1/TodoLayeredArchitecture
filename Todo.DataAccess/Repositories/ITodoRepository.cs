using Todo.Entities.Models;
namespace Todo.DataAccess.Repositories;
public interface ITodoRepository
{
    Task<List<TodoItem>> GetAllAsync();
    Task<TodoItem?> GetByIdAsync(int id);
    Task<TodoItem> AddAsync(TodoItem todoItem);
    Task<TodoItem?> UpdateAsync(TodoItem todoItem);
    Task<TodoItem> DeleteAsync(TodoItem todoItem);
    Task<TodoStatus?> GetStatusByNameAsync(string name);
    Task<TodoPriority?> GetPriorityByNameAsync(string name);
}