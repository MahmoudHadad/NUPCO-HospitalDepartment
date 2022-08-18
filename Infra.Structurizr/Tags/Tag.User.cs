using Structurizr;

namespace Infrastructure.Structurizr.Tags
{
    internal class TagUser : Tag
    {
        public TagUser(string name) : base(name)
        {
        }

        internal override ElementStyle GetElementStyle()
        {
            ElementStyle elementStyle = new(Name) { Shape = Shape.Person };

            return elementStyle;
        }
    }
}
