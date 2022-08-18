using Structurizr;

namespace Infrastructure.Structurizr.Tags
{
    internal class TagFirewall : Tag
    {
        public TagFirewall(string name) : base(name)
        {
        }

        internal override ElementStyle GetElementStyle()
        {
            ElementStyle elementStyle = new(Name) { Border =  Border.Dashed, FontSize = 50};

            return elementStyle;
        }
    }
}
