using AnemicModel;

namespace AppService
{
    public static partial class Architecture
    {
        public static ArchitectureDecision MicroFrontends { get; set; } = new(
            "There will be a wide range merge conflicts among the different vendors that will impact the demos and the overall delivery progress",
            "Multiple vendors involved",
            new DateOnly(2022, 08, 01),
            "Follow Micro-Frontends architecture",
            ArchitectureDecision.Status.Accepted,
            "Micro-Frontends");
        public static ArchitectureDecision SingleSPA { get; set; } = new(
            "It will be hard, inefficient, and full of unrecommended workarounds to implement the Micro-Frontends",
            "Micro-Frontends library is required",
            new DateOnly(2022, 08, 01),
            "Use single-spa to implement the Micro-Frontends architecture",
            ArchitectureDecision.Status.Accepted,
            "single-spa");
    }
}
