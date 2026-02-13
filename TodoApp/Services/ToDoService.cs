using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApp.database.AppDbContextModels;
using TodoApp.Dtos;

namespace TodoApp.Services;

public class ToDoService : IToDoService
{
    private readonly AppDbContext _db;

    public ToDoService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<TasksResponseDto>> GetTasks(int userId)
    {
        var lst = await _db.TblTodoItems.Where(x => x.UserId == userId).ToListAsync();

        var result = lst.Select(x => new TasksResponseDto
        {
            Title = x.Title,
            TodoItemId = x.TodoItemId
        }).ToList();

        return result;
    }

    
    public async Task CreateTask(int userId,TasksRequestDto request)
    {
        await _db.TblTodoItems.AddAsync(new TblTodoItem
        {
            Title = request.Title,
            IsCompleted = false,
            UserId = userId,
            CreatedAt = DateTime.UtcNow,
        });
        await _db.SaveChangesAsync();
    }

    public async Task ToggleTask(int taskId, int userId)
    {
        var result = await _db.TblTodoItems.FirstOrDefaultAsync(x => x.TodoItemId == taskId && x.UserId == userId);
        if (result is null)
        {
            return;
        }
        result.IsCompleted = !result.IsCompleted;
        await _db.SaveChangesAsync();
    }

    public async Task DeleteTask(int taskId, int userId)
    {
        var result = await _db.TblTodoItems.FirstOrDefaultAsync(x=> x.TodoItemId == taskId && x.UserId == userId);
        if (result is null)
        {
            return;
        }
        _db.TblTodoItems.Remove(result);
        await _db.SaveChangesAsync();
    }


}
