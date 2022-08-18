using Structurizr;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Structurizr.Tags
{
    internal class TagDatastore : Tag
    {
        public TagDatastore(string name) : base(name)
        {
        }

        internal override ElementStyle GetElementStyle()
        {
            ElementStyle elementStyle = new(Name) { Shape = Shape.Cylinder, Opacity = 25};

            return elementStyle;
        }
    }
}
