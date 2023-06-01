using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace les2_TreeView
{
    /// <summary>
    ///  Information about a directory item as drive, a file or a folder
    /// </summary>
    public class DirectoryItem
    {
        /// <summary>
        /// type of item
        /// </summary>
        public DirectoryItemType Type {  get; set; }

        public string FullPath { get; set; }
        public string Name { get { return this.Type == DirectoryItemType.Drive ? this.FullPath : DirectoryStructure.GetFileFolderName(this.FullPath); } }
    }
}

