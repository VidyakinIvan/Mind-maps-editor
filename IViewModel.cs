using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mind_maps_editor
{
    internal interface IViewModel<T>
    {
        public void SelectionChanged(T entity);
        public void SelectionDisabled();
        public void SetActiveEntity(T entity);
    }
}
