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
            GViewer.ToolBarIsVisible = false;
            _ = GViewer.DataBindings.Add("Graph", DataContext, "Graph", false, DataSourceUpdateMode.OnPropertyChanged);
        }

        private void GViewer_Click(object sender, EventArgs e)
        {
            if (e is MouseEventArgs me && me.Button == System.Windows.Forms.MouseButtons.Right)
            {
                if (FindResource("cmGraph") is ContextMenu cm)
                {
                    cm.PlacementTarget = sender as System.Windows.Controls.Button;
                    cm.IsOpen = true;
                }
            }
        }

        private void MenuItemGraph_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as ViewModel)?.GraphLayout();
            wfh.Visibility = !wfh.IsVisible ? Visibility.Visible : Visibility.Hidden;
        }
    }
}