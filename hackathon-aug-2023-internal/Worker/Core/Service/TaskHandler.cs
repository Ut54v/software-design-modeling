using Worker.Core.Enums;
using Worker.Core.Models;
using TaskAllocatorCommons.Enums;
using Worker.Core.Impl;
using TaskAllocatorCommons.Models;

namespace Worker.Core.Service
{
    public class TaskHandler
    {
        public NodeStatus nodeStatus = NodeStatus.available;
        private MemeDownloader _memeDownloader;
        public TaskHandler(MemeDownloader memeDownloader)
        {
            _memeDownloader = memeDownloader;
        }

        public async Task<ResponseStatus> HandelTask(TaskModel task)
        {
            nodeStatus = NodeStatus.busy;
            ResponseStatus response = ResponseStatus.Failed;
            switch (task.TaskType)
            {
                case TaskType.MemeDownloader:
                    response = await _memeDownloader.Execute((MemeTaskModel)task.TaskDefinition);
                    break;
            }
            return response;
        }
    }
}
