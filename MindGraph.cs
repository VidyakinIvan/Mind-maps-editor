using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuikGraph;
using QuikGraph.MSAGL;
using System.Runtime.CompilerServices;

namespace Mind_maps_editor
{
    internal class MindGraph: BidirectionalGraph<MindVertex, MindEdge>
    {
        public Microsoft.Msagl.Drawing.Graph MSAGLConvert()
        {
            var populator = this.CreateMsaglPopulator<MindVertex,MindEdge>();
            populator.Compute();
            return populator.MsaglGraph;
        }
    }
}
