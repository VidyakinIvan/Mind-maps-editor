using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Msagl.Drawing;
using System.Windows.Forms;
using Microsoft.Msagl.GraphViewerGdi;
using System.Diagnostics;
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

        private void btnGraph_Click(object sender, RoutedEventArgs e)
        {
            btnGraph.Visibility = Visibility.Hidden;
            btnTable.Visibility = Visibility.Hidden;
            wfh.Visibility = Visibility.Visible;
        }

    }
}