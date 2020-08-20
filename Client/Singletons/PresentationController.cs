using System;
using System.Threading;
using MyDay.Shared.Models;

namespace MyDay.Client.Singletons
{
  public class PresentationController
  {
    public ComponentsTheme Theme { get; set; }

    public bool AddDialogOpen { get; set; }
    public bool BlockerDialogOpen { get; set; }

    public TaskModel BlockedTask { get; set; } = null;

    public string IndexTitle => _showingBacklog ? "Backlog" : "Today";

    public string ColumnWidth { get; set; } = "32%";

    public TaskState DefaultTarget => _showingBacklog ? TaskState.Backlog : TaskState.ToDo;

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
      Theme = new ComponentsTheme();
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

    public event Action OnTaskChange;
    public event Action OnViewSwitch;
  }
}
