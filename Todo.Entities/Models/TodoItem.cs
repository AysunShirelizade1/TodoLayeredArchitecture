namespace Todo.Entities.Models;
using Todo.Entities.Common;
public class TodoItem : BaseEntity
{
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public DateTime? DueDate { get; set; }
    public int StatusId { get; set; }
    public TodoStatus Status { get; set; } = null!;
    public int PriorityId { get; set; }
    public TodoPriority Priority { get; set; } = null!;

    public List<Comment> Comments { get; set; } = new();

    public List<TodoItemTag> TodoItemTags { get; set; } = new();
}