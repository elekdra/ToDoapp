
namespace backend.Models
{
    public class FileModel
    {
        public FileModel(string taskName, string taskDescription, string priority)
        {
            this.TaskName = taskName;
            this.TaskDescription = taskDescription;
            this.Priority = priority;

        }
        public string TaskName { get; set; }
        public string TaskDescription { get; set; }
        public string Priority { get; set; }

    }
}