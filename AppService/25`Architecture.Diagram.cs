using AnemicModel;

namespace AppService
{
    public static partial class Architecture
    {
        public static SystemDiagram InupcoSystemDiagram { get; private set; } = new("iNUPCO")
        { 
            ExternalEcosystemGroups = new[] { new[] { IdP, ExternalAD, InternalAD, BO, Email, } },
            UserGroups = new[]
            { 
                new[]{ NupcoUser}, 
                new[]{  Customer, },
                new[]{  Provider, },
            }
        };
        public static ProcessDiagram InupcoProcessDiagram { get; set; } = new
            (
                "iNUPCO~Auth"
                ,
                new[] 
                { 
                    new SoftwareProcess[] {InupcoSPA},
                    new SoftwareProcess[] {InupcoSite},
                    new SoftwareProcess[] {AuthWebAPI},
                    new SoftwareProcess[] {AuthWebApiDB},
                }
            )
        {
            ExternalEcosystemGroups = new[] { new[] { IdP, BO, Email, } },
            UserGroups = new[] { new[] { Customer, NupcoUser, Provider, } }
        };
    }
}
