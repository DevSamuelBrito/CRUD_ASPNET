using System;

namespace CRUD_ASPNET.Models;

public class Tasks
{
    public int Id { get; set; }
    public string Title { get; set; }  = string.Empty;
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}
