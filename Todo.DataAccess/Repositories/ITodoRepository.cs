using Todo.Entities.Models;

namespace Todo.DataAccess.Repositories;

public interface ITodoRepository
{
    Task<List<TodoItem>> GetAllAsync(TodoQuery query);
    Task<int> CountAsync(TodoQuery query);
    Task<TodoItem?> GetByIdAsync(int id);
    Task<TodoItem> AddAsync(TodoItem todoItem);
    Task<TodoItem?> UpdateAsync(TodoItem todoItem);
    Task<TodoItem> DeleteAsync(TodoItem todoItem);
    Task<TodoStatus?> GetStatusByNameAsync(string name);
    Task<TodoPriority?> GetPriorityByNameAsync(string name);
}