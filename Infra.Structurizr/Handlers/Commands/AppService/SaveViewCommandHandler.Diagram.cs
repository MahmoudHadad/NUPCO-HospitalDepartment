

using AnemicModel;

using Core;

using Structurizr;

namespace Infrastructure.Structurizr.Handlers.Commands.AppService
{
    public static partial class SaveViewCommandHandler
    {
        private static void AddContainers(
            SoftwareSystem system, Diagram diagram, ContainerView containerVirew)
        {
            foreach (IEnumerable<SoftwareProcess> processesGroups in diagram.ProcessesGroups)
            {
                IEnumerable<Container> containers =
                    GetAllProcesses(processesGroups)
                    .Where(p => !p.IsInfrastructureProcess)
                    .Select(p => system.GetContainerWithName(p.Name));

                foreach (Container container in containers)
                {
                    containerVirew.Add(container);
                }

                HandleAnimation(containerVirew, containers);
            }
        }

        private static void AddContainers(
            SoftwareSystem system, Diagram diagram, DeploymentView deploymentView)
        {
            foreach (IEnumerable<SoftwareProcess> processesGroups in diagram.ProcessesGroups)
            {
                IEnumerable<ProcessContainerProxy> processContainerProxies =
                    GetAllProcesses(processesGroups);

                IEnumerable<ContainerInstance> containerInstances =
                    processContainerProxies
                    .Where(p => !p.IsInfrastructureProcess)
                    .Select(p => GetContainerInstance(system, p));

                IEnumerable<InfrastructureNode> nodes =
                    processContainerProxies
                    .Where(p => p.IsInfrastructureProcess)
                    .Select(p => GetInfrastructureNode(system, p));

                foreach (ContainerInstance containerInstance in containerInstances)
                {
                    deploymentView.Add(containerInstance);
                }

                foreach (InfrastructureNode infrastructureNode in nodes)
                {
                    deploymentView.Add(infrastructureNode);
                }

                if (containerInstances.Any() && nodes.Any())
                {
                    deploymentView.AddAnimation(containerInstances.ToArray(), nodes.ToArray());
                }
                else
                {
                    if (containerInstances.Any())
                    {
                        deploymentView.AddAnimation(containerInstances.ToArray());
                    }

                    if (nodes.Any())
                    {
                        deploymentView.AddAnimation(nodes.ToArray());
                    }
                }
            }
        }

        private static ContainerInstance GetContainerInstance(
            SoftwareSystem system
            ,
            ProcessContainerProxy containerProxy
            ,
            IEnumerable<DeploymentNode>? nodes = null)
        {
            IEnumerable<DeploymentNode> deploymentNodes =
                nodes ?? system.Model.DeploymentNodes;

            ContainerInstance instance = deploymentNodes
                                .SelectMany(d => d.ContainerInstances)
                                .SingleOrDefault(d => d.Name == containerProxy.Name);

            if (instance == null)
            {
                IEnumerable<DeploymentNode> children = deploymentNodes.SelectMany(
                    n => n.Children);

                if (children.Any())
                {
                    instance = GetContainerInstance(
                                system, containerProxy, children);
                }
            }

            return instance;
        }

        private static InfrastructureNode GetInfrastructureNode(
            SoftwareSystem system
            ,
            ProcessContainerProxy containerProxy
            ,
            IEnumerable<DeploymentNode>? nodes = null)
        {
            IEnumerable<DeploymentNode> deploymentNodes =
                nodes ?? system.Model.DeploymentNodes;

            InfrastructureNode node = deploymentNodes
                                .SelectMany(d => d.InfrastructureNodes)
                                .SingleOrDefault(i => i.Name == containerProxy.Name);

            if (node == null)
            {
                node = GetInfrastructureNode(
                    system, containerProxy, deploymentNodes.SelectMany(n => n.Children));
            }

            return node;
        }

        private static void HandleView(
            Workspace workspace, SoftwareSystem system, DeploymentDiagram diagram, string name)
        {
            DeploymentView deploymentView = workspace.Views.CreateDeploymentView(name, "");

            AddContainers(system, diagram, deploymentView);
        }

        private static void HandleView(
            Workspace workspace, SoftwareSystem system, ProcessDiagram diagram, string name)
        {
            ContainerView containerVirew = workspace.Views.CreateContainerView(
                system, name, "");

            AddUsers(system.Model, diagram, containerVirew);

            AddContainers(system, diagram, containerVirew);

            HandleSoftwareSystems(system.Model, diagram, containerVirew);

            containerVirew.EnableAutomaticLayout();
            containerVirew.ExternalSoftwareSystemBoundariesVisible = true;
        }


        private static void HandleView(
            Workspace workspace, SoftwareSystem system, SystemDiagram diagram, string name)
        {
            SystemContextView systemContextView = workspace.Views.CreateSystemContextView(
                system, name, "");

            AddUsers(system.Model, diagram, systemContextView);

            HandleAnimation(systemContextView, new[] { system });

            HandleSoftwareSystems(system.Model, diagram, systemContextView);

            systemContextView.EnableAutomaticLayout();
        }

        private static void HandleDiagrams(
            IEnumerable<Diagram> diagrams
            ,
            Workspace workspace
            ,
            SoftwareSystem system)
        {
            if (diagrams.Exists())
            {
                foreach (Diagram diagram in diagrams)
                {
                    string name = $"{diagram.GetType().Name.Replace("Diagram", "")}`{diagram.Name}";

                    if (diagram == null)
                    {
                        return;
                    }
                    if (diagram is DeploymentDiagram)
                    {
                        HandleView(workspace, system, diagram as DeploymentDiagram, name);
                    }
                    if (diagram is ProcessDiagram)
                    {
                        HandleView(workspace, system, diagram as ProcessDiagram, name);
                    }
                    if (diagram is SystemDiagram)
                    {
                        HandleView(workspace, system, diagram as SystemDiagram, name);
                    }
                } 
            }
        }
    }
}
