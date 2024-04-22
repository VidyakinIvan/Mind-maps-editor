using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Msagl.Drawing;
using System.Windows.Forms;
using Microsoft.Msagl.GraphViewerGdi;
using System.Diagnostics;
using Microsoft.VisualBasic.Logging;
namespace Mind_maps_editor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            wfh.Visibility = Visibility.Hidden;
            DataContext = new ViewModel();
            gViewer.ToolBarIsVisible = false;
            gViewer.DataBindings.Add("Graph", DataContext, "Graph", false, DataSourceUpdateMode.OnPropertyChanged);
        }

        private void gViewer_Click(object sender, EventArgs e)
        {
            
            if (e is MouseEventArgs me && me.Button == System.Windows.Forms.MouseButtons.Right)
            {
                ContextMenu? cm = FindResource("cmGraph") as ContextMenu;
                if (cm != null)
                {
                    cm.PlacementTarget = sender as System.Windows.Controls.Button;
                    cm.IsOpen = true;
                }
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            wfh.Visibility = !wfh.IsVisible ? Visibility.Visible : Visibility.Hidden;
        }
    }
}