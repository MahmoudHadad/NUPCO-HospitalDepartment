using AnemicModel;

namespace Infrastructure.Structurizr
{
    internal class BlockNodeProxy : DeploymentBlock
    {
        public IEnumerable<ProcessContainerProxy>? Processes { get; set; }
        public string TypeName { get; set; }
        public BlockNodeProxy(string name, string typeName) : base(name)
        {
            Children = new List<DeploymentBlock>();
            TypeName = string.IsNullOrWhiteSpace(typeName) 
                ? throw new ArgumentNullException(nameof(typeName)) 
                : typeName;
        }
       
        internal void Add(BlockNodeProxy blockNode)
        {
            deploymentBlocks.Add(blockNode);
        }

        internal void AddRange(IEnumerable<BlockNodeProxy> blockNodes)
        {
            deploymentBlocks.AddRange(blockNodes);
        }
    }
}
