using Structurizr;

namespace Infrastructure.Structurizr.Tags
{
    internal class TagStorage : Tag
    {
        public TagStorage(string name) : base(name)
        {
        }

        internal override ElementStyle GetElementStyle()
        {
            ElementStyle elementStyle = new(Name) { Shape = Shape.Folder };

            return elementStyle;
        }
    }
}
