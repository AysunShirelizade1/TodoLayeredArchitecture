namespace Todo.Entities.Models;

public class TodoQuery
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 5;
    public int? StatusId { get; set; }
    public int? PriorityId { get; set; }
    public string? Search { get; set; }
}