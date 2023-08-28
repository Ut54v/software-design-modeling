using Worker.Core.Enums;

namespace TaskExecutor.Models
{
    public class NodeModel
    {
        public NodeModel(string _name, string _address , NodeStatus _status = NodeStatus.available)
        {
            Name = _name;
            Address = _address;
            Status = _status;
        }
        public int id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public NodeStatus Status { get; set; }
    }
}
