using Todo.Business.DTOs;
using Todo.Business.Mappers;
using Todo.Business.Validations;
using Todo.DataAccess.Repositories;
using Todo.Entities.Models;
namespace Todo.Business.Services;

public class TodoService : ITodoService
{
    private readonly ITodoRepository _todoRepository;

    public TodoService(ITodoRepository todoRepository)
    {
        _todoRepository = todoRepository;
    }

    public async Task<PagedResponseDto<TodoResponseDto>> GetAllAsync(TodoQuery query)
{
    var todos = await _todoRepository.GetAllAsync(query);
    var totalCount = await _todoRepository.CountAsync(query);

    var mappedTodos = todos.Select(TodoMapper.ToResponseDto).ToList();

    return new PagedResponseDto<TodoResponseDto>
    {
        Page = query.Page,
        PageSize = query.PageSize,
        TotalCount = totalCount,
        Data = mappedTodos
    };
}

    public async Task<TodoResponseDto?> GetByIdAsync(int id)
    {
        var todo = await _todoRepository.GetByIdAsync(id);

        if (todo is null)
            return null;

        return TodoMapper.ToResponseDto(todo);
    }

    public async Task<(bool IsSuccess, List<string> Errors, TodoResponseDto? Data)> CreateAsync(TodoCreateDto dto)
    {
        var errors = TodoValidator.ValidateCreate(dto);

        if (errors.Any())
            return (false, errors, null);

        var entity = TodoMapper.ToEntity(dto);

        var createdTodo = await _todoRepository.AddAsync(entity);

        return (true, new List<string>(), TodoMapper.ToResponseDto(createdTodo));
    }

    public async Task<(bool IsSuccess, List<string> Errors, TodoResponseDto? Data)> UpdateAsync(TodoUpdateDto dto)
    {
        var errors = TodoValidator.ValidateUpdate(dto);

        if (errors.Any())
            return (false, errors, null);

        var existingTodo = await _todoRepository.GetByIdAsync(dto.Id);

        if (existingTodo is null)
            return (false, new List<string> { "Todo item not found." }, null);

        TodoMapper.MapUpdateDtoToEntity(dto, existingTodo);

        var updatedTodo = await _todoRepository.UpdateAsync(existingTodo);

        if (updatedTodo is null)
            return (false, new List<string> { "Todo item could not be updated." }, null);

        return (true, new List<string>(), TodoMapper.ToResponseDto(updatedTodo));
    }

    public async Task<(bool IsSuccess, string Message)> DeleteAsync(int id)
    {
        var existingTodo = await _todoRepository.GetByIdAsync(id);

        if (existingTodo is null)
            return (false, "Todo item not found.");

        await _todoRepository.DeleteAsync(existingTodo);

        return (true, "Todo item deleted successfully.");
    }
}