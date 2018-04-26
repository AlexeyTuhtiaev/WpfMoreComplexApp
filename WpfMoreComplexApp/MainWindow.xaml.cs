using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfMoreComplexApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Default Constractor
        /// </summary>
        #region constractor
        public MainWindow()
        {
            InitializeComponent();
        }
        #endregion

        /// <summary>
        /// When the App first opens
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region On Loaded
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (var drive in Directory.GetLogicalDrives())
            {
                var item = new TreeViewItem();
                item.Header = drive;
                FolderView.Items.Add(item);
            }
        }
        #endregion
    }
}
