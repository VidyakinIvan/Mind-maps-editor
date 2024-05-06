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
    internal class GraphModel : IModel<string>
    {
        #region Fields
        public LayerConstraints layerConstraints;
        private MindGraph graph;
        #endregion
        #region Properties
        public List<List<string>> Entities
        {
            get;
        }
        public Graph Graph
        {
            get => graph.MSAGLConvert();
            set
            {
                graph = graph.MindGraphConvert(value);
            }
        }
        #endregion
        public GraphModel()
        {
            graph = new();
            layerConstraints = Graph.LayerConstraints;
            Entities = new();
        }
        #region Methods
        public void AddEntity(string id, int layer)
        {
            graph.AddNode(id);
            if (Entities.Count <= layer)
            {
                for (int i = Entities.Count; i <= layer; i++)
                {
                    Entities.Add(new());
                }
            }
            Entities[layer].Add(id);
        }
        public void RenameEntity(string oldId, string newId)
        {
            Node? sourceNode = null;
            List<MindNode> targetNodes = new();
            List<MindEdge> edges = graph.Edges.ToList();
            int layer = GetLayer(oldId);
            foreach (var edge in edges)
            {
                if (edge.Target.Id == oldId)
                    sourceNode = graph.GetNode(edge.Source.Id);
                if (edge.Source.Id == oldId)
                    targetNodes.Add(graph.GetNode(edge.Target.Id));
            }
            graph.RemoveVertex(graph.GetNode(oldId));
            Entities.ForEach(layer =>
            {
                if (layer.Contains(oldId))
                {
                    layer[layer.IndexOf(oldId)] = newId;
                }
            });
            AddEntity(newId, layer);
            if (sourceNode is not null)
                AddEdge(sourceNode.Id, newId);
            foreach (var node in targetNodes)
            {
                AddEdge(graph.GetNode(newId).Id, node.Id);
            }
        }
        public void RemoveEntity(string id)
        {
            List<MindEdge> edges = graph.Edges.ToList();
            foreach (var edge in edges)
            {
                if (edge.Source.Id == id)
                {
                    string nodeId = edge.Target.Id;
                    graph.RemoveVertex(graph.GetNode(nodeId));
                    Entities.ForEach(layer => layer.Remove(nodeId));
                }
            }
            graph.RemoveVertex(graph.GetNode(id));
            Entities.ForEach(layer => layer.Remove(id));
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
            Entities.Clear();
            AddEntity("Корень", 0);
            Entities.Add(new List<string>());
            Entities[0].Add("Корень");
        }
        public int GetLayer(string id)
        {
            return Entities.FindIndex(layer => layer.Contains(id));
        }
        #endregion
    }
}
