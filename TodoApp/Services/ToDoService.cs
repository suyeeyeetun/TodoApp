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
        }).ToList();

        return result;
    }
    [HttpPost]
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
    //[HttpPost]
    //public async Task UpdateTask(int TodoItemId,TasksRequestDto request)
    //{

    //}


}
