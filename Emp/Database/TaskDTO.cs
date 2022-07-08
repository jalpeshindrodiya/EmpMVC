namespace Emp.Database
{
    public class TaskDTO
    {
        public int Id { get; set; }
        public string TaskType { get; set; }
        public string Task { get; set; }
        public string TaskDescription { get; set; }
        public string DueAsOn { get; set; }
        public string TaskCurrentStatus { get; set; }
    }
}
