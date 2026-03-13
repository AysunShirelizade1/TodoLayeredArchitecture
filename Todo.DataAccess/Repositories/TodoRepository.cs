using Microsoft.EntityFrameworkCore;
using Todo.DataAccess.Context;
using Todo.Entities.Models;

namespace Todo.DataAccess.Repositories;

public class TodoRepository : ITodoRepository
{
    private readonly AppDbContext _context;

    public TodoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<TodoItem>> GetAllAsync(TodoQuery query)
    {
        var todoQuery = _context.TodoItems
            .AsNoTracking()
            .AsQueryable();

        if (query.StatusId.HasValue)
            todoQuery = todoQuery.Where(x => x.StatusId == query.StatusId.Value);

        if (query.PriorityId.HasValue)
            todoQuery = todoQuery.Where(x => x.PriorityId == query.PriorityId.Value);

        return await todoQuery
            .Skip((query.Page - 1) * query.PageSize)
            .Take(query.PageSize)
            .ToListAsync();
    }

    public async Task<int> CountAsync(TodoQuery query)
    {
        var todoQuery = _context.TodoItems
            .AsNoTracking()
            .AsQueryable();

        if (query.StatusId.HasValue)
            todoQuery = todoQuery.Where(x => x.StatusId == query.StatusId.Value);

        if (query.PriorityId.HasValue)
            todoQuery = todoQuery.Where(x => x.PriorityId == query.PriorityId.Value);

        return await todoQuery.CountAsync();
    }

    public async Task<TodoItem?> GetByIdAsync(int id)
    {
        return await _context.TodoItems
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<TodoItem> AddAsync(TodoItem todoItem)
    {
        await _context.TodoItems.AddAsync(todoItem);
        await _context.SaveChangesAsync();
        return todoItem;
    }

    public async Task<TodoItem?> UpdateAsync(TodoItem todoItem)
    {
        var existingTodo = await _context.TodoItems
            .FirstOrDefaultAsync(x => x.Id == todoItem.Id);

        if (existingTodo is null)
            return null;

        existingTodo.Title = todoItem.Title;
        existingTodo.Description = todoItem.Description;
        existingTodo.DueDate = todoItem.DueDate;
        existingTodo.StatusId = todoItem.StatusId;
        existingTodo.PriorityId = todoItem.PriorityId;
        existingTodo.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return existingTodo;
    }

    public async Task<TodoItem> DeleteAsync(TodoItem todoItem)
    {
        _context.TodoItems.Remove(todoItem);
        await _context.SaveChangesAsync();
        return todoItem;
    }

    public async Task<TodoPriority?> GetPriorityByNameAsync(string name)
    {
        return await _context.TodoPriorities
            .FirstOrDefaultAsync(x => x.Name == name);
    }

    public async Task<TodoStatus?> GetStatusByNameAsync(string name)
    {
        return await _context.TodoStatuses
            .FirstOrDefaultAsync(x => x.Name == name);
    }
}