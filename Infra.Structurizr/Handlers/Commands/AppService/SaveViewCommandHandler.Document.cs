using AnemicModel;

using Structurizr;
using Structurizr.Documentation;

namespace Infrastructure.Structurizr.Handlers.Commands.AppService
{
    public static partial class SaveViewCommandHandler
    {
        private static void AddSections(
            Document document
            ,
            SoftwareSystem system
            ,
            DirectoryInfo root
            ,
            StructurizrDocumentationTemplate template)
        {
            foreach (DocumentationSection section in document.Sections)
            {
                if (section.Location == "Context.md")
                {
                    template.AddContextSection(system, new FileInfo(Path.Combine(
                            root.FullName, section.Location)));
                }
                else if (section.Location == "Software Architecture.md")
                {
                    template.AddSoftwareArchitectureSection(system, new FileInfo(Path.Combine(
                            root.FullName, section.Location)));
                }
            }
        }
        private static void HandleDocumentation(
            Document document, Workspace workspace, SoftwareSystem system)
        {
            if (document != null)
            {
                DirectoryInfo root = new(document.Location);

                StructurizrDocumentationTemplate template = new(workspace);
                
                AddSections(document, system, root, template);
            }
        }
    }
}
