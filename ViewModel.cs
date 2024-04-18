using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Msagl.Drawing;

namespace Mind_maps_editor
{
    internal class ViewModel : INotifyPropertyChanged
    {
        private Graph graph1;
        public ViewModel()
        {
            graph1 = new Graph();
            graph1.AddNode("A");
            graph1.AddNode("B");
            graph1.AddEdge("A", "B");
        }
        public Graph Graph
        {
            get => graph1;
            set
            {
                graph1 = value;
                OnPropertyChanged(nameof(Graph));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
