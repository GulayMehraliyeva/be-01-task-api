using Microsoft.AspNetCore.Mvc;
using TaskApi.Data;
using TaskApi.Models;

namespace TaskApi.Controllers
{
    [ApiController]
    [Route("")]
    public class TaskController : ControllerBase
    {
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
        public IActionResult GetTasks()
        {
            return Ok(TaskRepository.Tasks);
        }

        [HttpGet("tasks/{id}")]
        public IActionResult GetTask(int id)
        {
            var task = TaskRepository.Tasks.FirstOrDefault(x => x.Id == id);

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
        public IActionResult CreateTask(CreateTaskRequest request)
        {
            if(string.IsNullOrEmpty(request.Title))
            {
                return BadRequest(new
                {
                    error = "Title is required"
                });
            }

            int nextId = TaskRepository.Tasks.Max(x => x.Id) + 1;

            var task = new TaskItem 
            {
                Id = nextId,
                Title = request.Title,
                Done = false
            };

            TaskRepository.Tasks.Add(task);

            return StatusCode(201, task);
        }

        [HttpPut("tasks/{id}")]
        public IActionResult UpdateTask(int id, UpdateTaskRequest request)
        {
            var task = TaskRepository.Tasks.FirstOrDefault(x => x.Id == id);

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

            return Ok(task);
        }

        [HttpDelete("tasks/{id}")]
        public IActionResult DeleteTask(int id)
        {
            var task = TaskRepository.Tasks.FirstOrDefault(x => x.Id == id);

            if (task == null)
            {
                return NotFound(new
                {
                    error = $"Task {id} not found"
                });
            }

            TaskRepository.Tasks.Remove(task);

            return NoContent();
        }
    }
}
