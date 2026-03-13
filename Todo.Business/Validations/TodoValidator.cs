using Todo.Business.DTOs;

namespace Todo.Business.Validations;

public static class TodoValidator
{
    public static List<string> ValidateCreate(TodoCreateDto dto)
    {
        var errors = new List<string>();

        if (dto is null)
        {
            errors.Add("Request body is required.");
            return errors;
        }

        if (string.IsNullOrWhiteSpace(dto.Title))
            errors.Add("Title is required.");

        if (!string.IsNullOrWhiteSpace(dto.Title) && dto.Title.Length > 200)
            errors.Add("Title cannot exceed 200 characters.");

        if (dto.Description is not null && dto.Description.Length > 2000)
            errors.Add("Description cannot exceed 2000 characters.");

        if (dto.StatusId <= 0)
            errors.Add("StatusId is required..");

        if (dto.PriorityId <= 0)
            errors.Add("PriorityId is required.");

        return errors;
    }

    public static List<string> ValidateUpdate(TodoUpdateDto dto)
    {
        var errors = new List<string>();

        if (dto is null)
        {
            errors.Add("Request body is required.");
            return errors;
        }

        if (dto.Id <= 0)
            errors.Add("Invalid Id.");

        if (string.IsNullOrWhiteSpace(dto.Title))
            errors.Add("Title is required.");

        if (!string.IsNullOrWhiteSpace(dto.Title) && dto.Title.Length > 200)
            errors.Add("Title cannot exceed 200 characters.");

        if (dto.Description is not null && dto.Description.Length > 2000)
            errors.Add("Description cannot exceed 2000 characters.");

        if (dto.StatusId <= 0)
            errors.Add("StatusId is required.");

        if (dto.PriorityId <= 0)
            errors.Add("PriorityId is required.");

        return errors;
    }
}