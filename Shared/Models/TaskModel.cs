using System;

namespace MyDay.Shared.Models
{
  public class TaskModel
  {
    public int Id { get; set; }
    public string Title { get; set; }
    public string Notes { get; set; }
    public TaskState State { get; set; }
    public int Blocking { get; set; } = -1;
    public DateTime CreatedOn { get; set; } = DateTime.Now.Date;
  }

  public enum TaskState
  {
    ToDo,
    Blocker,
    Blocked,
    Done,
    Backlog
  }
}
