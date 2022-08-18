using AnemicModel;

namespace AppService
{
    public static partial class Architecture
    {
        public static Datastore AuthWebApiDB { get; set; } = new("Auth WEB API DB")
        {
            Description = "Stores user roles",
            Technology = "Microsoft SQL Server DB",
        };
        public static SoftwareProcess AuthWebAPI { get; set; } = new("Auth WEB API")
        {
            Description = "Authenticates/Authorizes",
            IsReusable = true,
            Technology = ".Net 6 WEB API",
            CommunicationChannels = new[] {
                new CommunicationChannel
                (
                    new[]{ CommunicationChannel.Operation.Reads, CommunicationChannel.Operation.Writes},
                    AuthWebApiDB,
                    new[]{ CommunicationChannel.Protocol.TcpIp}
                ),
                 new CommunicationChannel
                 (
                     new[]{ CommunicationChannel.Operation.Authenticates},
                     protocols: new[] {CommunicationChannel.Protocol.HTTPS }
                 ){Ecosystem = IdP },
                 new CommunicationChannel
                 (
                     new[]{ CommunicationChannel.Operation.Sync},
                     protocols: new[] {CommunicationChannel.Protocol.HTTPS }
                 ){Ecosystem = BO },
                 new CommunicationChannel
                 (
                     new[]{ CommunicationChannel.Operation.Sends, }
                 ){Ecosystem = Email },
            },
        };
        public static Website InupcoSite { get; set; } = new("iNUPCO site")
        {
            Description = "Serves the static assets for iNUPCO SPA portal",
            Technology = "Micro Frontend shell using single-spa with React",
        };
        public static SPA InupcoSPA { get; set; } = new("iNUPCO")
        {
            Description = "iNUPCO portal",
            Technology = "Micro Frontend shell using single-spa with React",
            CommunicationChannels = new[]
            {
                new CommunicationChannel(
                    new []{ CommunicationChannel.Operation.Downloads},
                    InupcoSite,
                    new[]{CommunicationChannel.Protocol.HTTPS}
                    ),
                new CommunicationChannel(
                    new []{ CommunicationChannel.Operation.Authenticates},
                    AuthWebAPI,
                    new[]{CommunicationChannel.Protocol.HTTPS}
                    ),
            }
        };
    }
}
