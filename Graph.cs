﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuikGraph;

namespace Mind_maps_editor
{
    public class Graph : BidirectionalGraph<Vertex, Edge>
    {
        public Graph() { }
        public Graph(bool allowParallelEdges) : base(allowParallelEdges) { }
        public Graph(bool allowParallelEdges, int vertexCapacity) : base(allowParallelEdges, vertexCapacity) { }
    }
}
