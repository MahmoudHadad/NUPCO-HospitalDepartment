using Structurizr;

namespace Infrastructure.Structurizr.Tags
{
    internal class TagFrontend : Tag
    {
        public TagFrontend(string name) : base(name)
        {
        }

        internal override ElementStyle GetElementStyle()
        {
            ElementStyle elementStyle =
                new(Name) { Stroke = "#ec008c" };

            return elementStyle;
        }
    }
}
