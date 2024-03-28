using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuikGraph;

namespace Mind_maps_editor
{
    public class Edge(int id, Vertex source, Vertex target) : QuikGraph.Edge<Vertex>(source, target)
    {
        public int ID { get; set; } = id;
    }
}
