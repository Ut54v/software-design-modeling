using TaskAllocatorCommons.Enums;

namespace TaskAllocatorCommons.Models
{
    public class TaskModel
    {
        public Guid Id { get; set; }
        public TaskType TaskType { get; set; }
        public object TaskDefinition { get; set; }
        public TaskState Status { get; set; } = TaskState.pending; 
    }
}