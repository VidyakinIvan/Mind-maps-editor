using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Mind_maps_editor
{
    /// <summary>
    /// Логика взаимодействия для CreateEntityWindow.xaml
    /// </summary>
    public partial class CreateEntityWindow : Window, ICreateEntityDialog
    {
        #region Fields
        private new bool? DialogResult = false;
        #endregion
        #region Properties
        public string EntityId
        {
            get => EntityIdTextBox.Text;
            set => EntityIdTextBox.Text = value;
        }
        public bool? CreateDialogResult
        {
            get => this.DialogResult;
            set => this.DialogResult = value;
        }
        #endregion
        #region Constructor
        public CreateEntityWindow()
        {
            InitializeComponent();
        }
        #endregion
        #region Methods
        public bool? ShowCreateDialog()
        {
            EntityIdTextBox.Text = string.Empty;
            ShowDialog();
            return CreateDialogResult;
        }
        #endregion
        #region Events
        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            CreateDialogResult = true;
            Close();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            Visibility = Visibility.Hidden;
        }

        private void EntityIdTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(EntityIdTextBox.Text))
                btnOk.IsEnabled = false;
            else
                btnOk.IsEnabled = true;
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        #endregion
    }
}
