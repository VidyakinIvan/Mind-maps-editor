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
    internal class GraphViewModel(ICreateEntityDialog createEntityDialog, IRenameEntityDialog renameEntityDialog) : INotifyPropertyChanged, IViewModel<Node>
    {
        #region Fields
        private Node? selectedNode;
        private Node? activeNode;
        private IModel<Graph> model = new GraphModel();
        private ICreateEntityDialog createEntityDialog = createEntityDialog;
        private IRenameEntityDialog renameEntityDialog = renameEntityDialog;
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
                return model.MentalMap;
            }
            set
            {
                if (value is not null)
                {
                    model.MentalMap = value;
                    OnPropertyChanged(nameof(Graph));
                }
            }
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
                                model.AddEntity(createEntityDialog.EntityId);
                                model.AddRelation(activeNode.Id, createEntityDialog.EntityId);
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
                            model.RenameEntity(activeNode.Id, renameEntityDialog.EntityId);
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
                        model.RemoveEntity(activeNode.Id);
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
                        model.AddRelation(selectedNode.Id, activeNode.Id);
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
                    model.Clear();
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
        public void SelectionChanged(Node entity)
        {
            selectedNode = entity;
        }
        public void SetActiveEntity(Node node)
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
