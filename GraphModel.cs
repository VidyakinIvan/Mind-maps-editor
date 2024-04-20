using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Msagl.Drawing;

namespace Mind_maps_editor
{
    internal class GraphModel
    {
        private Graph graph;
        public GraphModel()
        {
            graph = new Graph();
        }
        public void AddNode(string id)
        {
            Graph.AddNode(id);
        }
        public Graph Graph
        {
            get => graph;
            set
            {
                graph = value;
            }
        }
    }
}
