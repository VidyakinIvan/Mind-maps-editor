using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuikGraph;

namespace Mind_maps_editor
{
    internal class MindEdge(MindVertex source, MindVertex target): IEdge<MindVertex>
    {
        public MindVertex Source { get; set; } = source;
        public MindVertex Target { get; set; } = target;
    }
}
