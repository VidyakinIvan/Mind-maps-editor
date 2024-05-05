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

namespace Mind_maps_editor
{
    internal class ViewModel : INotifyPropertyChanged
    {
        #region Fields
        private Node? selectedNode;
        private Node? activeNode;
        private IModel<string>? model;
        private ICreateEntityDialog createEntityDialog;
        private Graph? graph1;
        private RelayCommand? addEntityCommand;
        private RelayCommand? removeEntityCommand;
        private RelayCommand? addEdgeCommand;
        private RelayCommand? clearCommand;
        #endregion
        #region Properties
        public Graph? Graph
        {
            get => graph1;
            set
            {
                graph1 = value;
                OnPropertyChanged(nameof(Graph));
            }
        }
        #endregion
        #region Constructor
        public ViewModel(ICreateEntityDialog createEntityDialog)
        {
            this.createEntityDialog = createEntityDialog;
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
                                Graph graph = (model as GraphModel)!.Graph;
                                (model as GraphModel)!.layerConstraints.RemoveAllConstraints();
                                List<Node> nodes = model.Entities[layer].Select(nodeid => graph.FindNode(nodeid)).ToList();
                                (model as GraphModel)!.layerConstraints.AddSameLayerNeighbors(nodes);
                                graph.LayerConstraints = (model as GraphModel)!.layerConstraints;
                                Graph = graph;
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
                    Graph = (model as GraphModel)?.Graph;
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
                    Graph = (model as GraphModel)?.Graph;
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
                    Graph = (model as GraphModel)?.Graph;
                    OnPropertyChanged(nameof(Graph));
                });
            }
        }
        #endregion
        #region ViewMethods
        public void SelectionDisabled()
        {
            if (selectedNode != null)
            {
                selectedNode.Attr.Color = Microsoft.Msagl.Drawing.Color.Black;
                selectedNode.Attr.LineWidth = 1;
                selectedNode = null;
                OnPropertyChanged(nameof(Graph));
            }
        }
        public void SelectionChanged(Node node)
        {
            selectedNode = node;
            selectedNode.Attr.Color = Microsoft.Msagl.Drawing.Color.Red;
            OnPropertyChanged(nameof(Graph));
        }
        public void SetActiveNode(Node node)
        {
            activeNode = node;
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
