using System.Globalization;

namespace TaskApi.Models
{
    public class UpdateTaskRequest
    {
        public String Title { get; set; } = "";
        public bool Done { get; set; }
    }
}
