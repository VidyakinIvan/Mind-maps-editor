using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mind_maps_editor
{
    public static class Transformator
    {
        public static void Transform(IModel source, IModel target)
        {
            target.TransformationIn(source.TransformationOut());
        }
    }
}
