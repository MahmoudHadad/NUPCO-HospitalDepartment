using AnemicModel;

using Core;

using Structurizr;

namespace Infrastructure.Structurizr.Handlers.Commands.AppService
{
    public static partial class SaveViewCommandHandler
    {
        private static void AddProcesses(
            SoftwareSystem system
            ,
            DeploymentNode node
            ,
            IEnumerable<ProcessContainerProxy> processes)
        {
            foreach (ProcessContainerProxy process in processes)
            {
                if (process.IsInfrastructureProcess)
                {
                    ProcessContainerProxy filledProcess = Processes.Single(
                        p => p.Name == process.Name);

                    InfrastructureNode infrastructureNode = node.AddInfrastructureNode(
                        filledProcess.Name);
                    infrastructureNode.Tags = GetAllTags(filledProcess);
                }
                else
                {
                    node.Add(system.GetContainerWithName(process.Name));
                }
            }
        }

        private static IEnumerable<BlockNodeProxy> GetAllDeploymentNodes(
            SoftwareSystem system
            ,
            IEnumerable<DeploymentBlock> blocks)
        {
            List<BlockNodeProxy> nodes = new();

            if (blocks.Exists())
            {
                foreach (DeploymentBlock block in blocks)
                {
                    BlockNodeProxy node = new(block.Name, block.GetType().Name);

                    node.Count = block.Count;

                    node.Description = block.Description;

                    node.Processes = GetAllProcesses(new[] { block }, false);

                    node.Technology = block.Technology;

                    nodes.Add(node);

                    if (block.Children != null && block.Children.Any())
                    {
                        node.AddRange(GetAllDeploymentNodes(system, block.Children));
                    }
                } 
            }

            return nodes;
        }

        private static void HandleDeploymentNodes(
            SoftwareSystem system
            , 
            IEnumerable<BlockNodeProxy> blocks
            ,
            DeploymentNode? currentNode = null)
        {
            foreach (BlockNodeProxy block in blocks)
            {
                DeploymentNode node =
                    currentNode == null
                    ? system.Model.AddDeploymentNode(block.Name)
                    : currentNode.AddDeploymentNode(block.Name);

                node.Instances = block.Count ?? 1;

                node.Description = block.Description;

                node.Tags = block.TypeName;

                node.Technology = block.Technology;


                AddProcesses(system, node, block.Processes);

                HandleRelations(block.Processes, system, node);

                if (block.Children != null && block.Children.Any())
                {
                    HandleDeploymentNodes(system, block.Children.Cast<BlockNodeProxy>(), node);
                }
            }
        }
    }
}
