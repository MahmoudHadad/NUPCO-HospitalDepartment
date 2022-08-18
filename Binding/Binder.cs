using AppService.Commands;

using Infrastructure.Structurizr.Handlers.Commands.AppService;

namespace Binding
{
    public static class Binder
    {
        public static void Bind()
        {
            SaveViewCommand.Handler = SaveViewCommandHandler.Handler;
        }
    }
}