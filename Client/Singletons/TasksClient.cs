using System.Linq;
using System.Threading.Tasks;
using MyDay.Shared.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Collections.ObjectModel;
using System.Collections;
using System.Collections.Generic;

namespace MyDay.Client.Singletons
{
  public class TasksClient
  {
    private readonly HttpClient _client;
    private readonly PresentationController _pc;
    public List<TaskModel> AllTasks { get; set; }

    public TasksClient(HttpClient client, PresentationController PC)
    {
      this._client = client;
      this._pc = PC;
    }

    public async Task<List<TaskModel>> GetTasks()
    {
      AllTasks = await _client.GetFromJsonAsync<List<TaskModel>>("https://localhost:5001/tasks");
      return AllTasks;
    }

    public async Task AddTask(TaskModel task)
    {
      var response = await _client.PostAsJsonAsync<TaskModel>("tasks", task);
      var id = await response.Content.ReadFromJsonAsync<int>();
      task.Id = id;
      AllTasks.Add(task);
    }

    public async Task Update(TaskModel task)
    {
      await _client.PutAsJsonAsync<TaskModel>("tasks", task);
    }

    public async Task Delete(TaskModel task)
    {
      AllTasks.Remove(task);
      await _client.DeleteAsync($"tasks/{task.Id}");
    }
  }
}

