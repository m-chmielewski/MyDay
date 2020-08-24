using System;
using System.Threading.Tasks;
using MyDay.Server.Data;
using MyDay.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Linq;

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
      var tasks = await _context.Tasks.Where(t => t.UserId == GetUserId()).ToListAsync();
      return Ok(tasks);
    }

    [HttpPost]
    public async Task<ActionResult<TaskModel>> Post(TaskModel task)
    {
      task.UserId = GetUserId();
      _context.Add(task);
      await _context.SaveChangesAsync();
      return task;
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

    private string GetUserId()
    {
      return HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
    }
  }
}