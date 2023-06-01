using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace les2_TreeView
{
    /// <summary>
    /// Helper Class to query information about directories
    /// </summary>
    public class DirectoryStructure
    {
        /// <summary>
        /// Gets all logical drives on computer.
        /// </summary>
        /// <returns></returns>
        public static List<DirectoryItem> GetLogicalDrives()
        {
            return System.IO.Directory.GetLogicalDrives().Select(drive => new DirectoryItem { FullPath = drive, Type = DirectoryItemType.Drive }).ToList();
        }

        /// <summary>
        /// Get directory top level content
        /// </summary>
        /// <param name="fullPath">Full path to the directory</param>
        /// <returns></returns>
        public static List<DirectoryItem> GetDirectoryContents(string fullPath)
        {

            var items = new List<DirectoryItem>();

            #region Get Folders


            try
            {
                var dirs = System.IO.Directory.GetDirectories(fullPath);
                if (dirs.Length > 0)
                    items.AddRange(dirs.Select(dir => new DirectoryItem { FullPath = dir, Type = DirectoryItemType.Folder }));
            }
            catch { }



            #endregion

            #region Get Files 

            try
            {
                var fs = System.IO.Directory.GetFiles(fullPath);
                if (fs.Length > 0)
                    items.AddRange(fs.Select(file => new DirectoryItem { FullPath = file, Type = DirectoryItemType.File }));
            }
            catch { }

            #endregion
            return items;
        }


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
