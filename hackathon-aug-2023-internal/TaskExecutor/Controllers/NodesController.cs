using Microsoft.AspNetCore.Mvc;
using TaskAllocatorCommons.Models;
using TaskExecutor.Controllers.Service;
using TaskExecutor.Models;

namespace TaskExecutor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NodesController : ControllerBase
    {
        private readonly TaskService<MemeTaskModel> taskService;
        private readonly NodeService nodeService;
        public NodesController(TaskService<MemeTaskModel> _taskService , NodeService _nodeService)
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

        [HttpPost]
        [Route("meme-download-task")]
        public IActionResult Task([FromBody]MemeTaskModel memeTaskModel)
        {
            taskService.Add(memeTaskModel);
            return Ok();
        }

        [HttpGet]
        [Route("nodes")]
        public IActionResult GetAllNodes()
        {
            return Ok(nodeService.GetAll());
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
