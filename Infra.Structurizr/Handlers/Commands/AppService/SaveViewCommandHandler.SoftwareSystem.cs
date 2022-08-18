using AnemicModel;

using Core;

using Structurizr;

namespace Infrastructure.Structurizr.Handlers.Commands.AppService
{
    public static partial class SaveViewCommandHandler
    {
        private static void HandleSoftwareSystem(Ecosystem ecosystem, Model model)
        {
            if (ecosystem.ExternalEcosystems.Exists())
            {
                foreach (Ecosystem external in ecosystem.ExternalEcosystems)
                {
                    model.AddSoftwareSystem(
                        Location.External
                        ,
                        external.Name
                        ,
                        external.Description);
                }
            }
        }

        private static void HandleSoftwareSystems(Model model, Diagram diagram, StaticView view)
        {
            if (diagram.ExternalEcosystemGroups.Exists())
            {
                foreach (IEnumerable<Ecosystem> externalEcosystems in diagram.ExternalEcosystemGroups)
                {
                    int ecosystemCount = externalEcosystems.Count();

                    SoftwareSystem[] SoftwareSystems = new SoftwareSystem[ecosystemCount];

                    for (int i = 0; i < ecosystemCount; i++)
                    {
                        Ecosystem external = externalEcosystems.ElementAt(i);
                        SoftwareSystem externalSystem = model.GetSoftwareSystemWithName(external.Name);
                        view.Add(externalSystem);
                        SoftwareSystems[i] = externalSystem;
                    }
                    HandleAnimation(view, SoftwareSystems);
                }
            }
        }
    }
}
