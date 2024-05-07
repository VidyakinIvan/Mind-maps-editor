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
        #region Fields
        enum Archetype
        {
            Graph,
            Table
        }
        #endregion
        #region Properties
        public Archetype ModelArchetype { get; }
        public T MentalMap { get; set; }
        #endregion
        #region Methods
        public void AddEntity(string id);
        public void RenameEntity(string oldId, string newId);
        public void RemoveEntity(string id);
        public void AddRelation(string sourceId, string targetId);
        public bool ContainsEntity(string id);
        public void Clear();
        #endregion
    }
}
