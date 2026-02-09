using TodoApp.Dtos;

namespace TodoApp.Services
{
    public interface IToDoService
    {
        Task<List<TasksResponseDto>> GetTasks(int userId);
    }
}