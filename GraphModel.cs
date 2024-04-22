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
        private Graph graph = new();
        #endregion
        #region Properties
        public Graph Graph
        {
            get => graph;
            set
            {
                graph = value;
            }
        }
        #endregion
        #region Methods
        public void AddEntity(string id)
        {
            Graph.AddNode(id);
        }
        public void Clear()
        {
            Graph = new();
        }
        #endregion
    }
}
