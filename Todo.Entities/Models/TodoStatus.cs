using Todo.Entities.Common;

namespace Todo.Entities.Models;

public class TodoStatus : BaseEntity
{
    public string Name { get; set; } = null!;

    public List<TodoItem> TodoItems { get; set; } = new();
}