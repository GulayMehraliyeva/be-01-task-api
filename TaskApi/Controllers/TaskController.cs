using Microsoft.AspNetCore.Mvc;
using TaskApi.Models;

using Microsoft.EntityFrameworkCore;
using TaskApi.Data;
using System.Threading.Tasks;

namespace TaskApi.Controllers
{
    [ApiController]
    [Route("")]
    public class TaskController : ControllerBase
    {
        private readonly TaskDbContext _context;

        public TaskController(TaskDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Root()
        {
            return Ok(new
            {
                name = "Task Api",
                version = 1.0,
                endpoints = new[]
                {
                    "/tasks"
                }
            });
        }

        [HttpGet("health")]
        public IActionResult Health()
        {
            return Ok(new
            {
                status = "ok"
            });
        }

        [HttpGet("tasks")]
        public async Task<IActionResult> GetTasks()
        {
            var tasks = await _context.Tasks.ToListAsync();
            return Ok(tasks);
        }

        [HttpGet("tasks/{id}")]
        public async Task<IActionResult> GetTask(int id)
        {
            var task = await _context.Tasks.FindAsync(id);

            if(task == null)
            {
                return NotFound(new
                {
                    error = $"Task {id} not found."
                });
            }



            return Ok(task);
        }

        [HttpPost("tasks")]
        public async Task<IActionResult> CreateTask(CreateTaskRequest request)
        {
            if(string.IsNullOrEmpty(request.Title))
            {
                return BadRequest(new
                {
                    error = "Title is required"
                });
            }

            var task = new TaskItem 
            {
                Title = request.Title,
                Done = false
            };

            _context.Tasks.Add(task);

            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetTask),
                new { id = task.Id },
                task);
        }

        [HttpPut("tasks/{id}")]
        public async Task<IActionResult> UpdateTask(int id, UpdateTaskRequest request)
        {
            var task = await _context.Tasks.FindAsync(id);

            if(task == null)
            {
                return NotFound(new
                {
                    error=$"Task {id} not found"
                });
            }

            if (string.IsNullOrWhiteSpace(request.Title))
            {
                return BadRequest(new
                {
                    error = "Title is required"
                });
            }

            task.Title = request.Title;
            task.Done = request.Done;

            await _context.SaveChangesAsync();

            return Ok(task);
        }

        [HttpDelete("tasks/{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var task = await _context.Tasks.FindAsync(id);

            if (task == null)
            {
                return NotFound(new
                {
                    error = $"Task {id} not found"
                });
            }

            _context.Remove(task);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
