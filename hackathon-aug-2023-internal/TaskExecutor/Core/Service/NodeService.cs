using TaskExecutor.Models;
using Worker.Core.Enums;

namespace TaskExecutor.Core.Service
{
    public class NodeService
    {
        private List<NodeModel> nodes = new List<NodeModel>();
        public void Add(NodeRegistrationRequest nodeRequest)
        {
            NodeModel node = new NodeModel(nodeRequest.Name, nodeRequest.Address);
            nodes.Add(node);
        }

        public void Remove(string name)
        {
            NodeModel node = nodes?.Find(x => x.Name.Equals(name));
            if (node != null)
            {
                nodes?.Remove(node);
            }
        }

        public List<NodeModel> GetAll()
        {
            return nodes;
        }

        public List<NodeModel> GetAvailableNodes()
        {
            return nodes.Where(x => x.Status.Equals(NodeStatus.available)).ToList();
        }
    }
}
