using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mind_maps_editor
{
    public interface ICreateEntityDialog
    {
        #region Properties
        public string EntityId { get; set; }
        public bool? CreateDialogResult { get; set; }
        #endregion
        #region Methods
        public bool? ShowCreateDialog();
        #endregion
    }
}
