using System;
using System.Threading;
using MyDay.Shared.Models;

namespace MyDay.Client.Singletons
{
  /// <summary>
  /// Object tracking presentation data 
  /// </summary>
  public class PresentationController
  {
    //MatBlazor components theme
    public MatComponentsTheme Theme { get; set; }
    //New task dialog state
    public bool AddDialogOpen { get; set; }
    //New blocked dialog state
    public bool BlockerDialogOpen { get; set; }
    //Task for which blocker is being added
    public TaskModel BlockedTask { get; set; } = null;

    public string IndexTitle => _showingBacklog ? "Backlog" : "Today";
    //Width of Today columns - set based on number of columns (default 3)
    public string ColumnWidth { get; set; } = "32%";
    //Default state of new task - based on whether the task is added while backlog or today visible
    public TaskState DefaultState => _showingBacklog ? TaskState.Backlog : TaskState.ToDo;

    private bool _showingBacklog = false;
    public bool ShowingBacklog
    {
      get
      {
        return _showingBacklog;
      }
      set
      {
        _showingBacklog = value;
        OnViewSwitch.Invoke();
      }
    }

    public PresentationController()
    {
      Theme = new MatComponentsTheme();
    }

    public void TaskChanged()
    {
      OnTaskChange.Invoke();
    }

    public void OpenAddBlockerDialog(TaskModel blockedTask)
    {
      BlockerDialogOpen = true;
      this.BlockedTask = blockedTask;
    }
    //Event holding all the methods to be fired on task change
    public event Action OnTaskChange;

    public event Action OnViewSwitch;
  }
}
