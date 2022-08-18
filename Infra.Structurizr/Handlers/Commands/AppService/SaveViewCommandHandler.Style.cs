using AnemicModel;

using Infrastructure.Structurizr.Tags;

using Structurizr;

namespace Infrastructure.Structurizr.Handlers.Commands.AppService
{
    public static partial class SaveViewCommandHandler
    {
        private static void AddElementStyles(Styles styles)
        {
            foreach (Tag tag in Tag.ALL)
            {
                ElementStyle elementStyle = tag.GetElementStyle();

                styles.Add(elementStyle);
            }
        }
        private static void AddRelationStyle(Styles styles)
        {
            RelationshipStyle relationshipStyle = new(nameof(Datastore)) { Opacity = 25 };

            styles.Add(relationshipStyle);
        }

        private static void HandleStyle(Workspace workspace)
        {
            ViewSet viewSet = workspace.Views;
            Styles styles = viewSet.Configuration.Styles;
            AddElementStyles(styles);
            AddRelationStyle(styles);
        }
    }
}
