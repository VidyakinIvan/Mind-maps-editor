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

namespace Mind_maps_editor
{
    internal class ViewModel : INotifyPropertyChanged
    {
        #region Fields
        private IModel? model;
        private int id = 0;
        private Graph? graph1;
        private RelayCommand? addEntityCommand;
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
        #region LayoutConstructors
        public void GraphLayout()
        {
            if (model is null)
                model = new GraphModel();
            if (Graph is null)
                Graph = (model as GraphModel)?.Graph;
        }
        #endregion
        #region RelayCommands
        public RelayCommand? AddEntityCommand
        {
            get
            {
                return addEntityCommand ??
                    (addEntityCommand = new RelayCommand(obj =>
                    {
                        model?.AddEntity(id.ToString());
                        id++;
                        OnPropertyChanged(nameof(Graph));
                    }));
            }
        }
        public RelayCommand? ClearCommand
        {
            get
            {
                return clearCommand ?? (clearCommand = new RelayCommand(obj =>
                {
                    model?.Clear();
                    Graph = (model as GraphModel)?.Graph;
                    OnPropertyChanged(nameof(Graph));
                }));
            }
        }
        #endregion
        #region PropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        #endregion
    }
}
