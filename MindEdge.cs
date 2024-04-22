using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuikGraph;

namespace Mind_maps_editor
{
    internal class MindEdge(MindNode source, MindNode target): IEdge<MindNode>
    {
        public MindNode Source { get; set; } = source;
        public MindNode Target { get; set; } = target;
    }
}
