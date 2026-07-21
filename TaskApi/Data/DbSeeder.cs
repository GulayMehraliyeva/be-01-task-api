using TaskApi.Models;

namespace TaskApi.Data;

public static class DbSeeder
{
    public static void Seed(TaskDbContext context)
    {
        // If there are already tasks, do nothing
        if (context.Tasks.Any())
            return;

        context.Tasks.AddRange(
            new TaskItem
            {
                Title = "Study C#",
                Done = false
            },
            new TaskItem
            {
                Title = "Buy milk",
                Done = true
            },
            new TaskItem
            {
                Title = "Go to gym",
                Done = false
            }
        );

        context.SaveChanges();
    }
}