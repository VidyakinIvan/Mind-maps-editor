using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Msagl.Drawing;

namespace Mind_maps_editor
{
    internal class GraphModel : IModel
    {
        #region Fields
        private MindGraph graph = new();
        #endregion
        #region Properties
        public Graph Graph
        {
            get => graph.MSAGLConvert();
            set
            {
                graph = graph.MindGraphConvert(value);
            }
        }
        #endregion
        #region Methods
        public void AddEntity(string id)
        {
            graph.AddNode(id);
        }
        public void RemoveEntity(string id)
        {
            graph.RemoveVertex(graph.GetNode(id));
        }
        public void AddEdge(string sourceId, string targetId)
        {
            graph.AddEdge(new MindEdge(graph.GetNode(sourceId), graph.GetNode(targetId)));
        }
        public bool ContainsEntity(string id)
        {
            return graph.Vertices.Any(node => node.Id == id);
        }
        public void Clear()
        {
            Graph = new();
        }
        #endregion
    }
}
