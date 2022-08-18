using Structurizr;

namespace Infrastructure.Structurizr.Tags
{
    internal class TagDMZ : TagFirewall
    {
        public TagDMZ(string name) : base(name)
        {
        }

        internal override ElementStyle GetElementStyle()
        {
            ElementStyle elementStyle = base.GetElementStyle();
            elementStyle.Color = elementStyle.Stroke = "#fe0109";
            return elementStyle;
        }
    }
}
