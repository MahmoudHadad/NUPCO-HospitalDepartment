using Structurizr;

namespace Infrastructure.Structurizr.Tags
{
    internal class TagSPA : Tag
    {
        public TagSPA(string name) : base(name)
        {
        }

        internal override ElementStyle GetElementStyle()
        {
            ElementStyle elementStyle = new(Name) { Shape =  Shape.WebBrowser };

            return elementStyle;
        }
    }
}
