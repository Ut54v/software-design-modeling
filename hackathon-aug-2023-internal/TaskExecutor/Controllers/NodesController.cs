using Microsoft.AspNetCore.Mvc;
using TaskAllocatorCommons.Models;
using TaskExecutor.Core.Service;
using TaskExecutor.Models;

namespace TaskExecutor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NodesController : ControllerBase
    {
        private readonly TaskService taskService;
        private readonly NodeService nodeService;
        public NodesController(TaskService _taskService , NodeService _nodeService)
        {
            taskService = _taskService;
            nodeService = _nodeService;
        }
        [HttpPost]
        [Route("register")]
        public IActionResult RegisterNode([FromBody] NodeRegistrationRequest node)
        {
            nodeService.Add(node);
            return Ok();
        }

        [HttpGet]
        [Route("nodes")]
        public IActionResult GetAllNodes()
        {
            return Ok(nodeService.GetAll());
        }

        [HttpPost]
        [Route("task/meme")]
        public IActionResult AddTask(TaskModel taskModel)
        {
            taskService.Add(taskModel);
            return Ok();
        }

        [HttpGet]
        [Route("tasks")]
        public IActionResult GetAllTask()
        {
            return (IActionResult)taskService.GetAll();
        }

        [HttpDelete]
        [Route("unregister/{name}")]
        public IActionResult RegisterNode(string name)
        {
            nodeService.Remove(name);
            return Ok();
        }
    }
}
