using Microsoft.Msagl.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mind_maps_editor
{
    internal class TableModel: IModel
    {
        private Queue<Relation> mentalMap = new();
        #region Properties
        public IModel.Archetype ModelArchetype => IModel.Archetype.Table;
        public Queue<Relation> MentalMap
        {
            get => mentalMap;
            set => mentalMap = value;
        }
        #endregion
        #region Methods
        public void AddEntity(string id)
        {
        }
        public void RenameEntity(string oldId, string newId)
        {
        }
        public void RemoveEntity(string id)
        {
        }
        public void AddRelation(string sourceId, string targetId)
        {
        }
        public bool ContainsEntity(string id)
        {
            return false;
        }
        public void Clear()
        {
            mentalMap.Clear();
        }
        public List<List<string>> TransformationOut()
        {
            List<List<string>> result = new();
            foreach (var pair in mentalMap)
            {
                List<string> row = new();
                row.Add(pair.Source);
                row.Add(pair.Destination);
                result.Add(row);
            }
            return result;
        }
        public void TransformationIn(List<List<string>> data)
        {
            Clear();
            foreach (var row in data)
                mentalMap.Enqueue(new Relation() { Source = row[0], Destination = row[1] });
        }
        #endregion
    }
}
