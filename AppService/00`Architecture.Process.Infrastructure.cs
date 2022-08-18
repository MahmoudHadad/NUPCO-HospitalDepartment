using AnemicModel;

namespace AppService
{
    public static partial class Architecture
    {
        public static Storage DigitizerStorage { get; set; } = new(
            "Digitization storage") { Description = "Digitized asset storage" };
    }
}
