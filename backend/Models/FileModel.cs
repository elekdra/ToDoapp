
namespace backend.Models
{
    public class FileModel
    {
        public FileModel(string taskName, string taskDescription, string priority, string mode, string status)
        {
            this.TaskName = taskName;
            this.TaskDescription = taskDescription;
            this.Priority = priority;
            this.Mode = mode;
            this.Status = status;

        }
        public string TaskName { get; set; }
        public string TaskDescription { get; set; }
        public string Priority { get; set; }
        public string Mode { get; set; }

        public string Status { get; set; }

    }
}