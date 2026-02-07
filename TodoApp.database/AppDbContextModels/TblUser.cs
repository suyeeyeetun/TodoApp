using System;
using System.Collections.Generic;

namespace TodoApp.database.AppDbContextModels;

public partial class TblUser
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<TblTodoItem> TblTodoItems { get; set; } = new List<TblTodoItem>();
}
