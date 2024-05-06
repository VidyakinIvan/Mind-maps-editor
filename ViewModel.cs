using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Msagl.Drawing;
using System.Diagnostics;
using System.Security.Principal;
using System.Security.Policy;
using Microsoft.Msagl.Prototype.LayoutEditing;
using Microsoft.Msagl.GraphViewerGdi;

namespace Mind_maps_editor
{
    internal class ViewModel : INotifyPropertyChanged
    {
        #region Fields
        private Node? selectedNode;
        private Node? activeNode;
        private IModel<string>? model;
        private ICreateEntityDialog createEntityDialog;
        private IRenameEntityDialog renameEntityDialog;
        private RelayCommand? addEntityCommand;
        private RelayCommand? renameEntityCommand;
        private RelayCommand? removeEntityCommand;
        private RelayCommand? addEdgeCommand;
        private RelayCommand? clearCommand;
        #endregion
        #region Properties
        public Graph? Graph
        {
            get
            {
                Graph? graph = (model as GraphModel)?.Graph;
                graph = ReverseGraph(graph);
                return ReverseGraph((model as GraphModel)?.Graph);
            }
            set
            {
                if (model is GraphModel gm && value is not null)
                {
                    gm.Graph = value;
                    OnPropertyChanged(nameof(Graph));
                }
            }
        }
        #endregion
        #region Constructor
        public ViewModel(ICreateEntityDialog createEntityDialog, IRenameEntityDialog renameEntityDialog)
        {
            this.createEntityDialog = createEntityDialog;
            this.renameEntityDialog = renameEntityDialog;
        }
        #endregion
        #region LayoutConstructors
        public void GraphLayout()
        {
            model ??= new GraphModel();
            model.AddEntity("Корень", 0);
            Graph ??= (model as GraphModel)?.Graph;
        }
        #endregion
        #region RelayCommands
        public RelayCommand? AddEntityCommand
        {
            get
            {
                return addEntityCommand ??= new RelayCommand(obj =>
                    {
                        if (model is not null && createEntityDialog.ShowCreateDialog() == true && !string.IsNullOrEmpty(createEntityDialog.EntityId) && activeNode is not null)
                        {
                            if (model.ContainsEntity(createEntityDialog.EntityId))
                                MessageBox.Show("Сущность уже существует");
                            else
                            {
                                int layer = model.GetLayer(activeNode.Id)+1;
                                model.AddEntity(createEntityDialog.EntityId, layer);
                                model.AddEdge(activeNode.Id, createEntityDialog.EntityId);
                                //Graph graph = (model as GraphModel)!.Graph;
                                //(model as GraphModel)!.layerConstraints.RemoveAllConstraints();
                                //List<Node> nodes = model.Entities[layer].Select(nodeid => graph.FindNode(nodeid)).ToList();
                                //(model as GraphModel)!.layerConstraints.AddSameLayerNeighbors(nodes);
                                //graph.LayerConstraints = (model as GraphModel)!.layerConstraints;
                                //Graph = graph;
                                OnPropertyChanged(nameof(Graph));
                            }    
                        }
                    });
            }
        }
        public RelayCommand? RenameEntityCommand
        {
            get
            {
                return renameEntityCommand ??= new RelayCommand(obj =>
                {
                    if (model is not null && renameEntityDialog.ShowCreateDialog() == true && !string.IsNullOrEmpty(renameEntityDialog.EntityId) && activeNode is not null)
                    {
                        if (model.ContainsEntity(renameEntityDialog.EntityId))
                            MessageBox.Show("Сущность уже существует");
                        else
                        {
                            model?.RenameEntity(activeNode.Id, renameEntityDialog.EntityId);
                            SelectionDisabled();
                            OnPropertyChanged(nameof(Graph));
                        }
                    }
                });
            }
        }
        public RelayCommand? RemoveEntityCommand
        {
            get
            {
                return removeEntityCommand ??= new RelayCommand(obj =>
                {
                    if (activeNode is not null)
                    {
                        model?.RemoveEntity(activeNode.Id);
                        SelectionDisabled();
                    }
                    OnPropertyChanged(nameof(Graph));
                });
            }
        }
        public RelayCommand? AddRelationCommand
        {
            get
            {
                return addEdgeCommand ??= new RelayCommand(obj =>
                {
                    if (selectedNode is not null && activeNode is not null)
                    {
                        model?.AddEdge(selectedNode.Id, activeNode.Id);
                        SelectionDisabled();
                    }
                    OnPropertyChanged(nameof(Graph));
                });
            }
        }
        public RelayCommand? ClearCommand
        {
            get
            {
                return clearCommand ??= new RelayCommand(obj =>
                {
                    model?.Clear();
                    OnPropertyChanged(nameof(Graph));
                });
            }
        }
        #endregion
        #region ViewMethods
        public void SelectionDisabled()
        {
            if (selectedNode != null)
                selectedNode = null;
        }
        public void SelectionChanged(Node node)
        {
            selectedNode = node;
        }
        public void SetActiveNode(Node node)
        {
            activeNode = node;
        }
        private Graph? ReverseGraph(Graph? graph)
        {
            Graph? graph1 = new();
            if (graph is null)
                return null;
            else if (graph.Edges.Count() == 0)
                return graph;
            foreach (var edge in graph.Edges.Reverse())
            {
                graph1.AddEdge(edge.Source, edge.Target);
            }
            return graph1;
        }
        #endregion
        #region PropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
        #endregion
    }
}
