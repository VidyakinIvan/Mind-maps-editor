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

namespace Mind_maps_editor
{
    internal class ViewModel : INotifyPropertyChanged
    {
        private GraphModel graphModel;
        private int id = 0;
        private Graph graph1;
        private RelayCommand? testCommand;
        public ViewModel()
        {
            graph1 = new Graph();
            graphModel = new GraphModel();
            Graph = graphModel.Graph;
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
        public RelayCommand? TestCommand
        {
            get
            {
                return testCommand ??
                    (testCommand = new RelayCommand(obj =>
                    {
                        Debug.WriteLine("TestCommand");
                        graphModel.Graph.AddNode(new Node(id.ToString()));
                        id++;
                    }));
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
