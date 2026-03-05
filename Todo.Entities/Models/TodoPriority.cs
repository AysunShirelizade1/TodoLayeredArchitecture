using Todo.Entities.Common;

namespace Todo.Entities.Models;

public class TodoPriority : BaseEntity
{
    public string Name { get; set; } = null!;

    public int Level { get; set; }

    public List<TodoItem> TodoItems { get; set; } = new();
}