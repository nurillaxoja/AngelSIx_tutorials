using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace les2_TreeView
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }
        #endregion

        #region On loaded
        /// <summary>
        /// When the application first opens
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // get every local drive on machine
            foreach (var drive in Directory.GetLogicalDrives())
            {
                var item = new TreeViewItem()
                {

                    Header = drive,
                    // Add the full path
                    Tag = drive
                };

                // add dummy item
                item.Items.Add(null);

                // liste on for item to ve expanded
                item.Expanded += Folder_Expanded;

                FolderView.Items.Add(item);
            }
        }

        #endregion


        #region Folder Expanded 
        /// <summary>
        /// When forder is expanded , find the sub folders 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Folder_Expanded(object sender, RoutedEventArgs e)
        {

            #region Initial Checks
            var item = (TreeViewItem)sender;

            // if item only contains dummy data
            if (item.Items.Count != 1 || item.Items[0] != null)
                return;

            item.Items.Clear();

            // get full path
            var fullPath = (string)item.Tag;

            #endregion

            #region Get Folders

            // create the blank list for directories
            var directories = new List<string>();

            try
            {
                var dirs = Directory.GetDirectories(fullPath);
                if (dirs.Length > 0)
                    directories.AddRange(dirs);
            }
            catch (Exception)
            {

                throw;
            }

            directories.ForEach(directoryPath =>
            {
                // create directory item
                var subItem = new TreeViewItem()
                {
                    Header = GetFileFolderName(directoryPath),
                    Tag = directoryPath,
                };

                //add dummy item so to expand foler
                subItem.Items.Add(null);

                // handle expanding
                subItem.Expanded += Folder_Expanded;

                // adding to the parent
                item.Items.Add(subItem);
            });

            #endregion

            #region Get Files 
            // create the blank list for directories
            var files = new List<string>();

            try
            {
                var fs = Directory.GetFiles(fullPath);
                if (fs.Length > 0)
                    files.AddRange(fs);
            }
            catch (Exception)
            {

                throw;
            }
            // for each file... 
            files.ForEach(filePath =>
            {
                // create file item
                var subItem = new TreeViewItem()
                {
                    Header = GetFileFolderName(filePath),
                    Tag = filePath,
                };

                // adding to the parent
                item.Items.Add(subItem);
            });


            #endregion

        }
        #endregion

        #region Helpers
        /// <summary>
        /// Find the file or folder name form full path
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetFileFolderName(string path)
        {
            if (string.IsNullOrEmpty(path))
                return string.Empty;

            // make all slashems backslashes
            var noremalizedPath = path.Replace('/', '\\');

            // find the last index of backslash
            var lastIndex = noremalizedPath.LastIndexOf('\\');

            // if dont find backslash retrun path iteslf
            if (lastIndex <= 0)
                return path;

            //retun name after baskslash
            return path.Substring(lastIndex + 1);

        }
        #endregion
    }
}
