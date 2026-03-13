namespace Todo.Business.DTOs;

public class TodoCreateDto
{
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public DateTime? DueDate { get; set; }
    public int StatusId { get; set; }
    public int PriorityId { get; set; }
}