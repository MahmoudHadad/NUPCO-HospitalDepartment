using Structurizr;

namespace Infrastructure.Structurizr.Tags
{
    internal class TagIsReusable : Tag
    {
        public TagIsReusable(string name) : base(name)
        {
        }

        internal override ElementStyle GetElementStyle()
        {
            ElementStyle elementStyle = new(Name) { Shape = Shape.Component};

            return elementStyle;
        }
    }
}
