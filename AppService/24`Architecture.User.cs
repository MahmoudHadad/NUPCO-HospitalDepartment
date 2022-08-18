using AnemicModel;

namespace AppService
{
    public static partial class Architecture
    {
        private static readonly CommunicationChannel[] UserChannels = new[]
        {
            new CommunicationChannel
            (
                new[]
                { CommunicationChannel.Operation.Uses },
                protocols : new[] { CommunicationChannel.Protocol.HTTPS}
            ){Ecosystem = Inupco},
            new CommunicationChannel
            (
                new[]
                { CommunicationChannel.Operation.Uses },
                process: InupcoSPA,
                protocols : new[] { CommunicationChannel.Protocol.HTTPS}
            ),
        };

        public static User Customer { get; set; } = new(
            nameof(Customer)
            ,
            Place.External
            ,
            UserChannels
        );
        public static User NupcoUser { get; set; } = new("NUPCO"
            ,
            Place.Internal
            ,
            UserChannels
        );
        public static User Provider { get; set; } = new(
            nameof(Provider)
            ,
            Place.External
            ,
            UserChannels
        );
    }
}
