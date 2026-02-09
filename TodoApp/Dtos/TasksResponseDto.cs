namespace TodoApp.Dtos;

public class TasksResponseDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public bool IsCompleted { get; set; }

}
public class TasksGetResponseDto
{
    public List<TasksResponseDto> Tasks { get; set; }
}
