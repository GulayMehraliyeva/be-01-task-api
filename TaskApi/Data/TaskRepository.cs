using TaskApi.Models;

namespace TaskApi.Data
{
    public static class TaskRepository
    {
        public static List<TaskItem> Tasks = new()
        {
            new TaskItem { Id = 1, Title = "Task 1", Done = true },
            new TaskItem { Id = 2, Title = "Task 2", Done = false },
            new TaskItem { Id = 3, Title = "Task 3", Done = true }
        };

    }
}
