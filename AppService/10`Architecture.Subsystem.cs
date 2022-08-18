using AnemicModel;

namespace AppService
{
    public static partial class Architecture
    {
        public static Subsystem Auth { get; set; } = new("(Auth",
            new SoftwareProcess[] { AuthWebAPI, AuthWebApiDB, });
    }
}
