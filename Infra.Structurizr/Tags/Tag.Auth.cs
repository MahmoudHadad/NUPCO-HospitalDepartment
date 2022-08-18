using Structurizr;

namespace Infrastructure.Structurizr.Tags
{
    internal class TagAuth : Tag
    {
        public TagAuth(string name) : base(name)
        {
        }

        internal override ElementStyle GetElementStyle()
        {
            ElementStyle elementStyle = new(Name) { Stroke = "#235594"};

            return elementStyle;
        }
    }
}
