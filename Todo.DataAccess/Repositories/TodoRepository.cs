using System.Security.Cryptography.X509Certificates;
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
    public async Task<List<TodoItem>> GetAllAsync()
    {
        return await _context.TodoItems
            .Include(x => x.Status)
            .Include(x => x.Priority)
            .Include(x => x.TodoItemTags)
                .ThenInclude(x => x.Tag)
            .Include(x => x.Comments)
            .AsNoTracking()
            .ToListAsync();
    }


    public async Task<TodoItem?> GetByIdAsync(int id)
    {
        return await _context.TodoItems
            .Include(x => x.Status)
            .Include(x => x.Priority)
            .Include(x => x.TodoItemTags)
                .ThenInclude(x => x.Tag)
            .Include(x => x.Comments)
            .AsNoTracking()
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
        _context.TodoItems.Update(todoItem);
        await _context.SaveChangesAsync();
        return todoItem;
    }

    public async Task<TodoItem> DeleteAsync(TodoItem todoItem)
    {
         _context.TodoItems.Remove(todoItem);
        await _context.SaveChangesAsync();
        return todoItem;
    }
    public async Task<TodoStatus?> GetStatusByNameAsync(string name)
    {
        return await _context.TodoStatuses
            .FirstOrDefaultAsync(x => x.Name == name);
    }

    public async Task<TodoPriority?> GetPriorityByNameAsync(string name)
    {
        return await _context.TodoPriorities
            .FirstOrDefaultAsync(x => x.Name == name);
    }
    

}