using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Mind_maps_editor
{
    public class BindingProxy : Freezable
    {
        #region Fields
        public static readonly DependencyProperty DataProperty =
            DependencyProperty.Register("Data", typeof(object), typeof(BindingProxy), new UIPropertyMetadata(null));
        #endregion
        #region Properties
        public object Data
        {
            get { return (object)GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }
        #endregion
        #region Methods
        protected override Freezable CreateInstanceCore()
        {
            return new BindingProxy();
        }
        #endregion
    }
}
