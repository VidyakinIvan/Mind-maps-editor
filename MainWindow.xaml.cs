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
using Microsoft.Msagl;
using Microsoft.Msagl.Drawing;
using Microsoft.Msagl.GraphViewerGdi;
using System.Windows.Forms;
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
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Graph graph = new();
            graph.AddNode("A");
            this.gViewer.Graph = graph;
        }
        private void GraphNodeClicked(object sender, EventArgs e)
        {
            Debug.WriteLine("GraphNodeClicked");
            if (sender is not GViewer gViewer || gViewer?.SelectedObject is not Node node)
            {
                return;
            }
            Debug.WriteLine(node.Label.Text);
            System.Windows.MessageBox.Show(node.Label.Text);
        }
    }
}