using Todo.Entities.Common;
namespace Todo.Entities.Models;
public class Tag : BaseEntity
{
    public string Name { get; set; } = null!;

    public List<TodoItemTag> TodoItemTags { get; set; } = new();
}

