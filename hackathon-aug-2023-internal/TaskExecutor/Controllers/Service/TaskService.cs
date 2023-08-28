
namespace TaskExecutor.Controllers.Service
{
    public class TaskService<T>
    {
        private List<T> tasks = new List<T>();

        public void Add(T task)
        {
            tasks.Add(task);
        }

        public void Remove(T task)
        {
            tasks.Remove(task);
        }

        public List<T> GetAll()
        {
            return tasks;
        }
    }
}
