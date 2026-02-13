using TodoApp.Dtos;

namespace TodoApp.Services
{
    public interface IToDoService
    {
        Task<List<TasksResponseDto>> GetTasks(int userId);
        Task CreateTask(int userId,TasksRequestDto request);
        Task DeleteTask(int taskId, int userId);
        Task ToggleTask(int taskId, int userId);
    }
}