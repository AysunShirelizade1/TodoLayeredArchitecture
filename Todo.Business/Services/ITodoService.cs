using Todo.Business.DTOs;
using Todo.Entities.Models;

namespace Todo.Business.Services;

public interface ITodoService
{
    Task<PagedResponseDto<TodoResponseDto>> GetAllAsync(TodoQuery query);
    Task<TodoResponseDto?> GetByIdAsync(int id);
    Task<(bool IsSuccess, List<string> Errors, TodoResponseDto? Data)> CreateAsync(TodoCreateDto dto);
    Task<(bool IsSuccess, List<string> Errors, TodoResponseDto? Data)> UpdateAsync(TodoUpdateDto dto);
    Task<(bool IsSuccess, string Message)> DeleteAsync(int id);
}