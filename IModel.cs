using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Msagl.Drawing;

namespace Mind_maps_editor
{
    internal interface IModel<T>
    {
        public List<T> Entities { get; } 
        #region Methods
        public void AddEntity(string id);
        public void RemoveEntity(string id);
        public void AddEdge(string sourceId, string targetId);
        public bool ContainsEntity(string id);
        public void Clear();
        public void Regularize();
        #endregion
    }
}
