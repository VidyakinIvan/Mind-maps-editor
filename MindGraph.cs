using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuikGraph;
using QuikGraph.MSAGL;
using Microsoft.Msagl.Drawing;
using System.Runtime.CompilerServices;
using System.Diagnostics;
using Microsoft.Msagl.GraphmapsWithMesh;

namespace Mind_maps_editor
{
    internal class MindGraph: BidirectionalGraph<MindNode, MindEdge>
    {
        #region NodeMethods
        public void AddNode(string id)
        {
            this.AddVertex(new MindNode(id));
        }
        public MindNode GetNode(string id)
        {
            return this.Vertices.FirstOrDefault(node => node.Id == id) ?? new MindNode(id);
        }
        #endregion
        #region ConvertMethods
        public Graph MSAGLConvert()
        {
            Graph msaglGraph = new();
            foreach (var node in this.Vertices)
            {
                msaglGraph.AddNode(node.Id);
            }
            foreach (var edge in this.Edges)
            {
                msaglGraph.AddEdge(edge.Source.Id, edge.Target.Id);
            }
            return msaglGraph;
        }
        public MindGraph MindGraphConvert(Graph graph)
        {
            MindGraph mindGraph = new();
            foreach (var node in graph.Nodes)
            {
                mindGraph.AddNode(node.Id);
            }
            foreach (var edge in graph.Edges)
            {
                mindGraph.AddEdge(new MindEdge(this.GetNode(edge.Source), this.GetNode(edge.Target)));
            }
            return mindGraph;
        }
        #endregion
    }
}
