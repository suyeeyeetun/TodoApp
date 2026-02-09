using Microsoft.AspNetCore.Mvc;
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

    private int GetUserId()
    {
        return HttpContext.Session.GetInt32("UserId")
            ?? throw new UnauthorizedAccessException();
    }

}
