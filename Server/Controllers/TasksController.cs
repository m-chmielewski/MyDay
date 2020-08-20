using System;
using System.Threading.Tasks;
using MyDay.Server.Data;
using MyDay.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace MyDay.Server.Controllers
{
  [Route("tasks")]
  [ApiController]
  public class TasksController : ControllerBase
  {
    private readonly ApplicationDbContext _context;

    public TasksController(ApplicationDbContext context)
    {
      this._context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
      var tasks = await _context.Tasks.ToListAsync();
      return Ok(tasks);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
      var task = await _context.Tasks.FirstOrDefaultAsync(a => a.Id == id);
      return Ok(task);
    }

    [HttpPost]
    public async Task<ActionResult<int>> Post(TaskModel task)
    {
      _context.Add(task);
      await _context.SaveChangesAsync();
      return task.Id;
    }

    [HttpPut]
    public async Task<IActionResult> Put(TaskModel task)
    {
      _context.Entry(task).State = EntityState.Modified;
      await _context.SaveChangesAsync();
      return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
      var task = new TaskModel { Id = id };
      _context.Remove(task);
      await _context.SaveChangesAsync();
      return NoContent();
    }
  }
}