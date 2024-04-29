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

namespace Mind_maps_editor
{
    internal class ViewModel : INotifyPropertyChanged
    {
        #region Fields
        private Node? selectedNode;
        private IModel? model;
        private ICreateEntityDialog createEntityDialog;
        private Graph? graph1;
        private RelayCommand? addEntityCommand;
        private RelayCommand? addEdgeCommand;
        private RelayCommand? clearCommand;
        #endregion
        #region Properties
        public Node? Node { get; set; }
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
                        if (model is not null && createEntityDialog.ShowCreateDialog() == true && !string.IsNullOrEmpty(createEntityDialog.EntityId))
                        {
                            if (model.ContainsEntity(createEntityDialog.EntityId))
                                MessageBox.Show("Сущность уже существует");
                            else
                                model?.AddEntity(createEntityDialog.EntityId);
                        }
                        Graph = (model as GraphModel)?.Graph;
                        OnPropertyChanged(nameof(Graph));
                    });
            }
        }
        public RelayCommand? AddEdgeCommand
        {
            get
            {
                return addEdgeCommand ??= new RelayCommand(obj =>
                {
                    if (selectedNode != null && Node != null)
                    {
                        model?.AddEdge(selectedNode.Id, Node.Id);
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
