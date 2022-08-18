using AnemicModel;

using Core;

using Structurizr;

using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Infrastructure.Structurizr.Handlers.Commands.AppService
{
    public static partial class SaveViewCommandHandler
    {
        private static void AddRelation(
            SoftwareSystem system
            ,
            IEnumerable<CommunicationChannel>? channels
            ,
            SoftwareSystem from
            ,
            DeploymentNode? node = null)
        {
            if (channels.Exists())
            {
                IEnumerable<CommunicationChannel> toChannels = GetToChannels(channels, node);
                UseRelation(system, from, node, toChannels);
            }
        }

        private static void AddRelation(
            SoftwareSystem system
            ,
            IEnumerable<CommunicationChannel>? channels
            ,
            Container from
            ,
            DeploymentNode? node = null)
        {

            IEnumerable<CommunicationChannel> toChannels = GetToChannels(channels, node);
            UseRelation(system, from, node, toChannels);
        }

        private static void AddRelations(
            SoftwareSystem system
            ,
            Ecosystem ecosystem
            ,
            DeploymentNode? node = null)
        {
            SoftwareSystem from = system.Model.GetSoftwareSystemWithName(ecosystem.Name);
            AddRelation(system, ecosystem.CommunicationChannels, from, node);
        }

        private static void AddRelations(
            SoftwareSystem system
            ,
            IEnumerable<ProcessContainerProxy> processes
            ,
            DeploymentNode? node = null)
        {
            foreach (ProcessContainerProxy process in processes)
            {
                Container from = system.GetContainerWithName(process.Name);
                AddRelation(system, process.CommunicationChannels, from, node);
            }
        }

        private static void AddTag(CommunicationChannel channel, Relationship relationship)
        {
            if (channel.Process is Datastore)
            {
                relationship.Tags = nameof(Datastore);
            }
        }

        private static string GetDescription(CommunicationChannel channel)
        {
            return String.Join('/', channel.Operations.Select(o => o.ToString()));
        }

        private static InfrastructureNode GetInfraNodeRecursively(
            SoftwareSystem system
            ,
            string infraNodeName
            ,
            IEnumerable<DeploymentNode> currentNodes = null)
        {
            currentNodes  ??= system.Model.DeploymentNodes;

            InfrastructureNode resultInfraNode = null;

            foreach (DeploymentNode currentNode in currentNodes)
            {
                resultInfraNode = currentNode.GetInfrastructureNodeWithName(infraNodeName);

                if (resultInfraNode != null)
                {
                    return resultInfraNode;
                }
            }

            resultInfraNode = GetInfraNodeRecursively(
                system, infraNodeName, currentNodes.SelectMany(n => n.Children));

            return resultInfraNode;
        }

        private static string? GetTechnology(
            IEnumerable<CommunicationChannel.Protocol>? protocols)
        {
            string? technology = null;

            if (protocols != null && protocols.Any())
            {
                technology= string.Join(',', protocols.Select(p => p.GetType()
                            .GetMember(p.ToString())
                            .First()
                            .GetCustomAttribute<DisplayAttribute>().Name));
            }

            return technology;
        }

        private static IEnumerable<CommunicationChannel> GetToChannels(
            IEnumerable<CommunicationChannel> channels, DeploymentNode? node)
        {
            return channels.Where(c =>
                                    c != null
                                    &&
                                    (
                                        (node != null && c.Process is InfrastructureProcess)
                                        ||
                                        (node == null && c.Process is not InfrastructureProcess)
                                    )
                                );
        }

        private static InfrastructureNode GetToNode(
            SoftwareSystem system, DeploymentNode? node, CommunicationChannel channel)
        {
            InfrastructureNode toNode = node.GetInfrastructureNodeWithName(
                channel.Process.Name);

            if (toNode == null)
            {
                toNode = GetInfraNodeRecursively(system, channel.Process.Name);
            }

            return toNode;
        }

        private static void HandleRelations(
            Ecosystem ecosystem
            ,
            SoftwareSystem system
            ,
            DeploymentNode? node = null)
        {
            AddRelations(system, ecosystem, node);
            HandleRelations(ecosystem.ExternalEcosystems, system, node);
        }

        private static void HandleRelations(
            IEnumerable<Ecosystem>? ecosystems
            ,
            SoftwareSystem system
            ,
            DeploymentNode? node = null)
        {
            if (ecosystems.Exists())
            {
                foreach (Ecosystem ecosystem in ecosystems)
                {
                    AddRelations(system, ecosystem, node);  
                }
            }
        }

        private static void HandleRelations(
            IEnumerable<ProcessContainerProxy> processes
            ,
            SoftwareSystem system
            ,
            DeploymentNode? node = null)
        {
            IEnumerable<ProcessContainerProxy?> communicatingProcesses =
                processes
                .Where(
                    p =>
                    p != null
                    && p.CommunicationChannels != null
                    && p.CommunicationChannels.Any());

            AddRelations(system, communicatingProcesses, node);
        }

        private static void HandleRelations(SoftwareSystem system, User user, Person person)
        {
            if (user.CommunicationChannels != null && user.CommunicationChannels.Any())
            {
                foreach (CommunicationChannel channel in user.CommunicationChannels)
                {
                    string description = String.Join('/', channel.Operations);
                    string technologies = GetTechnology(channel.Protocols);
                    if (channel.Ecosystem != null)
                    {
                        person.Uses(system, description, technologies);
                    }
                    if (channel.Process != null)
                    {
                        Container container =
                            system.Containers.Single(c => c.Name == channel.Process.Name);
                        person.Uses(container, description, technologies);
                    }
                }
            }
        }

        private static void UseChannel(
            SoftwareSystem system,
            StaticStructureElement from,
            CommunicationChannel channel)
        {
            if (channel.Process.Exists())
            {
                UseProcess(system, from, channel);
            }
            else if (channel.Ecosystem.Exists())
            {
                UseEcosystem(system, from, channel);
            }
        }

        private static void UseEcosystem(
            SoftwareSystem system, StaticStructureElement from, CommunicationChannel channel)
        {
            SoftwareSystem toSoftwareSystem = system.Model.GetSoftwareSystemWithName(
                                channel.Ecosystem.Name);

            string description = GetDescription(channel);

            string technology = GetTechnology(channel.Protocols);

            Relationship relationship = from.Uses(toSoftwareSystem, description, technology);

            AddTag(channel, relationship);
        }

        private static void UseNode(
            SoftwareSystem system,
            StaticStructureElement from,
            DeploymentNode? node,
            CommunicationChannel channel)
        {
            ContainerInstance fromContainerInstance = node.ContainerInstances.Single(
                                    i => i.Name ==from.Name);
            InfrastructureNode toNode = GetToNode(system, node, channel);

            string description = GetDescription(channel);

            string? technology = GetTechnology(channel.Protocols);

            Relationship relationship = fromContainerInstance.Uses(toNode, description, technology);

            AddTag(channel, relationship);
        }

        private static void UseProcess(
            SoftwareSystem system, StaticStructureElement from, CommunicationChannel channel)
        {
            Container toContainer = system.GetContainerWithName(channel.Process.Name);

            string description = GetDescription(channel);

            string technology = GetTechnology(channel.Protocols);

            Relationship relationship = from.Uses(toContainer, description, technology);

            AddTag(channel, relationship);
        }

        private static void UseRelation(
            SoftwareSystem system,
            StaticStructureElement from,
            DeploymentNode? node,
            IEnumerable<CommunicationChannel> toChannels)
        {
            foreach (CommunicationChannel channel in toChannels)
            {
                if (node.Exists())
                {
                    UseNode(system, from, node, channel);
                }
                else if (channel.Exists())
                {
                    UseChannel(system, from, channel);
                }
            }
        }
    }
}
