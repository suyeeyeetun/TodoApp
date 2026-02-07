using System;
using System.Collections.Generic;

namespace TodoApp.database.AppDbContextModels;

public partial class TblTodoItem
{
    public int TodoItemId { get; set; }

    public string Title { get; set; } = null!;

    public bool IsCompleted { get; set; }

    public int UserId { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual TblUser User { get; set; } = null!;
}
