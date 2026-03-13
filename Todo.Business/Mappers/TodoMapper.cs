using Todo.Business.DTOs;
using Todo.Entities.Models;
namespace Todo.Business.Mappers;
public static class TodoMapper
{
    public static TodoItem ToEntity(TodoCreateDto dto)
    {
        return new TodoItem
        {
            Title = dto.Title,
            Description = dto.Description,
            DueDate = dto.DueDate,
            StatusId = dto.StatusId,
            PriorityId = dto.PriorityId
        };
    }
    public static TodoResponseDto ToResponseDto(TodoItem entity)
    {
        return new TodoResponseDto
        {
            Id = entity.Id,
            Title = entity.Title,
            Description = entity.Description,
            DueDate = entity.DueDate,
            PriorityId = entity.PriorityId,
            StatusId = entity.StatusId,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt
        };
    }
    public static void MapUpdateDtoToEntity(TodoUpdateDto dto, TodoItem entity)
    {
        entity.Title = dto.Title;
        entity.Description = dto.Description;   
        entity.DueDate = dto.DueDate;
        entity.StatusId = dto.StatusId;
        entity.PriorityId = dto.PriorityId;
        entity.UpdatedAt = DateTime.UtcNow;
    }
}