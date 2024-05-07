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
    internal class GraphModel : IModel<Graph>
    {
        #region Fields
        private MindGraph graph = new();
        #endregion
        #region Properties
        public IModel<Graph>.Archetype ModelArchetype => IModel<Graph>.Archetype.Graph;
        public Graph MentalMap 
        {
            get => ReverseGraph(graph.MSAGLConvert());
            set
            {
                graph = graph.MindGraphConvert(value);
            }
        }
        #endregion
        public GraphModel()
        {
            AddEntity("Корень");
        }
        #region Methods
        public void AddEntity(string id)
        {
            graph.AddNode(id);
        }
        public void RenameEntity(string oldId, string newId)
        {
            Node? sourceNode = null;
            List<MindNode> targetNodes = new();
            List<MindEdge> edges = graph.Edges.ToList();
            foreach (var edge in edges)
            {
                if (edge.Target.Id == oldId)
                    sourceNode = graph.GetNode(edge.Source.Id);
                if (edge.Source.Id == oldId)
                    targetNodes.Add(graph.GetNode(edge.Target.Id));
            }
            graph.RemoveVertex(graph.GetNode(oldId));
            AddEntity(newId);
            if (sourceNode is not null)
                AddRelation(sourceNode.Id, newId);
            foreach (var node in targetNodes)
            {
                AddRelation(graph.GetNode(newId).Id, node.Id);
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
                }
            }
            graph.RemoveVertex(graph.GetNode(id));
        }
        public void AddRelation(string sourceId, string targetId)
        {
            graph.AddEdge(new MindEdge(graph.GetNode(sourceId), graph.GetNode(targetId)));
        }
        public bool ContainsEntity(string id)
        {
            return graph.Vertices.Any(node => node.Id == id);
        }
        public void Clear()
        {
            graph = new();
            AddEntity("Корень");
        }
        private Graph ReverseGraph(Graph? graph)
        {
            Graph? graph1 = new();
            if (graph is null)
                return new();
            else if (graph.Edges.Count() == 0)
                return graph;
            foreach (var edge in graph.Edges.Reverse())
            {
                graph1.AddEdge(edge.Source, edge.Target);
            }
            return graph1;
        }
        #endregion
    }
}
