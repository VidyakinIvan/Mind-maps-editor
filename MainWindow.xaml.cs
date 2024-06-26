﻿using System.Text;
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
        #region Constructors
        public MainWindow()
        {
            InitializeComponent();
            wfh.Visibility = Visibility.Hidden;
            DataContext = new GraphViewModel(new CreateEntityWindow(), new RenameEntityWindow());
            GViewer.ToolBarIsVisible = false;
            _ = GViewer.DataBindings.Add("Graph", DataContext, "Graph", false, DataSourceUpdateMode.OnPropertyChanged);
        }
        #endregion
        #region EventHandlers
        private void GViewer_Click(object sender, EventArgs e)
        {
            if (e is MouseEventArgs me)
            {
                if (me.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    if (FindResource("cmGraph") is ContextMenu cm)
                    {
                        if (GViewer.SelectedObject is Node node)
                        {
                            (DataContext as IViewModel<Node>)?.SetActiveEntity(node);
                            cm.PlacementTarget = sender as System.Windows.Controls.Button;
                            cm.IsOpen = true;
                        }
                    }
                }
                else if (me.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    (DataContext as IViewModel<Node>)?.SelectionDisabled();
                    if (GViewer.SelectedObject is Node node)
                    {
                        (DataContext as IViewModel<Node>)?.SelectionChanged(node);
                    }
                }
            }
        }
        private void MenuItemGraph_Click(object sender, RoutedEventArgs e)
        {
            wfh.Visibility = !wfh.IsVisible ? Visibility.Visible : Visibility.Hidden;
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Environment.Exit(0);
        }
        #endregion
    }
}