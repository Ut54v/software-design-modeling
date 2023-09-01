
using TaskAllocatorCommons.Models;

namespace TaskExecutor.Core.Service
{
    public class TaskService
    {
        private List<TaskModel> tasks = new List<TaskModel>();
        private List<TaskModel> taskHistory = new List<TaskModel>();

        public void Add(TaskModel task)
        {
            tasks.Add(task);
        }

        public void Remove(TaskModel task)
        {
            taskHistory.Add(task);
            tasks.Remove(task);
        }

        public List<TaskModel> GetAll()
        {
            return tasks;
        }
    }
}
