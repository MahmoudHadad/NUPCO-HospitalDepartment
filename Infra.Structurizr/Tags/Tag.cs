using AnemicModel;

using Structurizr;

namespace Infrastructure.Structurizr.Tags
{
    internal abstract class Tag
    {
        private static Dictionary<string, Tag> Tags { get; set; } = new();
        public static IEnumerable<Tag> ALL { get; } = Tags.Values;

        public static Tag Auth { get; set; } = new TagAuth(nameof(Auth));

        public static Tag Datastore { get; set; } = new TagDatastore(nameof(AnemicModel.Datastore));

        public static Tag DMZ { get; set; } = new TagDMZ(nameof(DMZ));

        public static Tag Firewall { get; set; } = new TagFirewall(nameof(AnemicModel.Firewall));

        public static Tag Frontend { get; set; } = new TagFrontend(nameof(Frontend));

        public static Tag IsReusable { get; set; } = new TagIsReusable(nameof(
            SoftwareProcess.IsReusable));
        public static Tag SPA { get; set; } = new TagSPA(nameof(SPA));

        public static Tag Storage { get; set; } = new TagStorage(nameof(AnemicModel.Storage));
        public static Tag User { get; set; } = new TagUser(nameof(User));

        internal string Name { get; set; }   
        internal Tag(string name)
        {
            Name = name;
            Tags.Add(name, this);
        }

        internal abstract ElementStyle GetElementStyle();

        internal static Tag? Lookup(string name)
        {
            Tag? result = null;

            if (Tags.ContainsKey(name))
            {
                result = Tags[name];
            }

            return result;
        }
    }
}
