using AnemicModel;

using Structurizr;
using Structurizr.Api;
using Structurizr.Documentation;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Structurizr.Handlers.Commands.AppService
{
    public static partial class SaveViewCommandHandler
    {
        internal static IEnumerable<BlockNodeProxy> BlockNodeProxies { get; set; } = new
            List<BlockNodeProxy>();
        internal static IEnumerable<ProcessContainerProxy> Processes { get; set; } = new
            List<ProcessContainerProxy>();

        public static void Handler(Ecosystem ecosystem)
        {
            ArgumentNullException.ThrowIfNull(ecosystem, nameof(ecosystem));

            Workspace workspace = new(ecosystem.Name, $"{ecosystem.Name} workspace");

            Model model = workspace.Model;

            SoftwareSystem system = model.AddSoftwareSystem(
                Location.Internal
                ,
                ecosystem.Name
                ,
                ecosystem.Description);

            HandleSoftwareSystem(ecosystem, model);
            HandleRelations(ecosystem, system);

            Processes =
            GetAllProcesses(
                ecosystem.Processes
                ,
                ecosystem.Subsystems
                ,
                ecosystem.Microservices
                ,
                ecosystem.ServiceCollections);

            HandleContainers(Processes, system);
            HandleRelations(Processes, system);
            HandleUsers(ecosystem.Users, model, system);
            HandleDiagrams(new[] { ecosystem.SystemDiagram }, workspace, system);
            HandleDiagrams(ecosystem.ProcessDiagrams, workspace, system);
            BlockNodeProxies = GetAllDeploymentNodes(system, ecosystem.DeploymentBlocks);
            HandleDeploymentNodes(system, BlockNodeProxies);
            HandleDiagrams(ecosystem.DeploymentDiagrams, workspace, system);
            HandleStyle(workspace);
            HandleDocumentation(ecosystem.Document, workspace, system);
            HandleDecisions(ecosystem.ArchitectureDecisions, workspace);


            StructurizrClient client = new(
                "5f57e847-93d1-49ac-ad5a-f004399e1ec0"
                ,
                "2c6f7333-c83d-4b48-9aef-552c78a324c1");

            client.PutWorkspace(53669, workspace);
        }
    }
}
