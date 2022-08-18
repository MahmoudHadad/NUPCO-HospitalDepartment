using AppService.Commands;

using AnemicModel;

namespace AppService
{
    public static partial class Architecture
    {
        public static void ArchitectBeta()
        {

            Document documentation = new("Documentation", new[] {
                new DocumentationSection("Context.md"),
            new DocumentationSection("Software Architecture.md")});

            SaveViewCommand saveViewCommand = new();

            Ecosystem ecosystem = Inupco;
            ecosystem.ArchitectureDecisions = new[] { MicroFrontends, SingleSPA };
            ecosystem.Document = documentation;
            ecosystem.Processes = new SoftwareProcess[] { InupcoSPA, InupcoSite, };
            ecosystem.Subsystems = new[] { Auth};
            ecosystem.ProcessDiagrams = new[] { InupcoProcessDiagram };
            ecosystem.SystemDiagram = InupcoSystemDiagram;
            ecosystem.Users = new[] { Customer, NupcoUser, Provider };

            saveViewCommand.Handle(ecosystem);
        }
    }
}