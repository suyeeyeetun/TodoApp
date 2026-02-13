using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TodoApp.database.AppDbContextModels;
using TodoApp.Dtos;
using TodoApp.Services;

namespace TodoApp.Controllers;

public class TasksController : Controller
{
    private readonly IToDoService _todoService;

    public TasksController(IToDoService todoService)
    {
        _todoService = todoService;
    }

    public async Task<IActionResult> Index()
    {
        int userId = GetUserId();
        var todos = await _todoService.GetTasks(userId);
        return View(todos);
    }
    [HttpPost]
    public async Task<IActionResult> Create(TasksRequestDto request)
    {
        int userId = GetUserId();
        await _todoService.CreateTask(userId, request);
        return RedirectToAction("Index");
    }

    [HttpPost("Tasks/ToggleComplete/{taskId}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ToggleComplete(int taskId)
    {
        int userId = GetUserId();
        await _todoService.ToggleTask(taskId, userId);
        //return RedirectToAction("Index");
        return Json(new { success = true });
    }

    [HttpPost("Tasks/Delete/{taskId}")]
    public async Task<IActionResult> Delete(int taskId)
    {
        int userId = GetUserId();
        await _todoService.DeleteTask(taskId,userId);
        return RedirectToAction("Index");
    }
    private int GetUserId()
    {
        return HttpContext.Session.GetInt32("UserId")
            ?? throw new UnauthorizedAccessException();
    }

}
