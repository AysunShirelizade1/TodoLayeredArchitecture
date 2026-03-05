using Todo.Entities.Common;
namespace Todo.Entities.Models;
public class Comment : BaseEntity
{
    public string Text { get; set; } = null!;
    public int TodoItemId { get; set; }
    public TodoItem TodoItem { get; set; } = null!;
}