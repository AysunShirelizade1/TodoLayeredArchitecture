using Microsoft.AspNetCore.Mvc;
using Todo.Business.Services;
using Todo.Business.DTOs;
using Todo.Entities.Models;
namespace Todo.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodoController : ControllerBase
{
    private readonly ITodoService _todoService;

    public TodoController(ITodoService todoService)
    {
        _todoService = todoService;
    }

    // GET: api/Todo
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] TodoQuery query)
    {
        if (query.Page <= 0 || query.PageSize <= 0)
            return BadRequest(new { errors = new[] { "Page and PageSize must be greater than 0." } });

        var result = await _todoService.GetAllAsync(query);
        return Ok(result);
    }

    // GET: api/Todo/1
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        var todo = await _todoService.GetByIdAsync(id);

        if (todo == null)
            return NotFound();

        return Ok(todo);
    }

    // POST: api/Todo
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] TodoCreateDto dto)
    {
        var result = await _todoService.CreateAsync(dto);

        if (!result.IsSuccess)
            return BadRequest(new { errors = result.Errors });

        return CreatedAtAction(
            nameof(GetById),
            new { id = result.Data!.Id },
            result.Data
        );
    }

    // PUT: api/Todo/1
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, [FromBody] TodoUpdateDto dto)
    {
        if (id != dto.Id)
            return BadRequest(new { errors = new[] { "Id mismatch." } });

        var result = await _todoService.UpdateAsync(dto);

        if (!result.IsSuccess)
        {
            if (result.Errors.Contains("Todo item not found."))
                return NotFound(new { errors = result.Errors });

            return BadRequest(new { errors = result.Errors });
        }

        return Ok(result.Data);
    }

    // DELETE: api/Todo/1
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _todoService.DeleteAsync(id);

        if (!result.IsSuccess)
            return NotFound(new { message = result.Message });

        return NoContent();
    }
}