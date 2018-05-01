using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;

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
                var item = new TreeViewItem()
                {
                    Header = drive,
                    Tag = drive
                };


                item.Items.Add(null);

                //Listen out item being expanded
                item.Expanded += Folder_Expanded;

                FolderView.Items.Add(item);
            }
        }
        #endregion

        #region Folder_Expanded
        private void Folder_Expanded(object sender, RoutedEventArgs e)
        {
            #region Initial Cheks

            var item = (TreeViewItem)sender;

            if (item.Items.Count != 1 && item.Items[0] != null)
            {
                return;
            }

            item.Items.Clear();

            var fullPath = (string)item.Tag;

            #endregion

            #region Get folders

            var directories = new List<string>();

            try
            {
                var dirs = Directory.GetDirectories(fullPath);
                if (dirs.Length > 0)
                {
                    directories.AddRange(dirs);
                }
            }
            catch (Exception)
            {
                //TODO: catching!!!!
            }

            directories.ForEach(directoryPath =>
            {
                var subItem = new TreeViewItem()
                {
                    Header = GetFileFolderName(directoryPath),
                    Tag = directoryPath
                };

                subItem.Items.Add(null);
                subItem.Expanded += Folder_Expanded;

                item.Items.Add(subItem);
            });
            #endregion

            #region Get Files

            var files = new List<string>();

            try
            {
                var fs = Directory.GetFiles(fullPath);
                if (fs.Length > 0)
                {
                    files.AddRange(fs);
                }
            }
            catch (Exception)
            {
                //TODO: catching!!!!
            }

            files.ForEach(filePath =>
            {
                var subItem = new TreeViewItem()
                {
                    Header = GetFileFolderName(filePath),
                    Tag = filePath
                };

                item.Items.Add(subItem);
            });

            #endregion
        }

        #endregion

        /// <summary>
        /// Find file or folder name from a full path
        /// </summary>
        /// <param name="path">The full path</param>
        /// <returns></returns>
        public static string GetFileFolderName(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return string.Empty;
            }

            var normolizePath = path.Replace('/', '\\');

            var lastIndex = normolizePath.LastIndexOf('\\');

            if (lastIndex <= 0)
            {
                return path;
            }

            return path.Substring(lastIndex+1);
        }
       
    }
}
