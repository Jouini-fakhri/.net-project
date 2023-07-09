namespace TicketAPP.Models;

public enum TaskStatus  {
    CREATED,ASSIGNED,ON_GOING,INTEGRATED,CLOSED
}

public enum TaskPriority  {
    HIGH ,LOW,MEDIUM
}


public class Task
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public TaskStatus Status { get; set; }
    public TaskPriority Priority { get; set; }
    public DateTime createdAt { get; set; }
    public DateTime closedAt { get; set; }
    public int ProjectId { get; set; } // Foreign key for the project
    public Project? Project { get; set; } // Navigation property for the project
}