using Core;

using Structurizr;

namespace Infrastructure.Structurizr.Handlers.Commands.AppService
{
    public static partial class SaveViewCommandHandler
    {
        private static void HandleAnimation(StaticView virew, IEnumerable<Element> elements)
        {
            if (elements.Exists())
            {
                virew.AddAnimation(elements.ToArray());
            }
        }
    }
}
